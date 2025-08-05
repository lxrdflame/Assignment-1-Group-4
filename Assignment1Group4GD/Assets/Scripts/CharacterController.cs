using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class CharacterControls : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private int moveSpeed;
    [SerializeField]
    private int JumpHeight;
    private Controls controls;
    private Vector2 lookInput;
    public Transform playerCamera;
    public Vector2 moveInput;
    private CharacterController characterController;
    private Vector3 velocity;
    private float gravity = -9.8f;
    [SerializeField] private float groundedGravity = -0.5f;
    [SerializeField] private float terminalVelocity = -50f;
    [SerializeField]
    private float lookSpeed;
    private float verticalLookRotation = 0f;
    [SerializeField]
    private int Range;
    public GameObject BulletPrefab;
    public Transform holdingPosition;
    //Interaction 
    public LayerMask ContactLayer;
    ShootingAniamtionsHandler Animations;
    public GameObject DamagerIndicatorText;

    [SerializeField]
    private PlayerStats stats;


    //Gun Settings
    public bool HasMachineGun, HasBazooka, HasLaserGun;
    public Transform MachineGunPoint, BazookaGunPoint, LaserGunPoint;
    public GameObject BazookaBullet;

    [SerializeField] private Transform startPoint; // The transform to start the line from
    [SerializeField] private float rayDistance = 100f; // Maximum raycast distance
    [SerializeField] private LayerMask hitLayers; // Layers to detect with raycast
    [SerializeField] private Color lineColor = Color.blue; // Color of the line
    [SerializeField] private float lineWidth = 0.1f; // Width of the line

    private LineRenderer lineRenderer;
    [SerializeField] private float ShootLimiter;
    private bool isShooting;
    public Slider ShootLimiterUI;
    private bool canShoot;

    bool isDashing = false;

    private void OnEnable()
    {
        controls = new Controls();
        controls.Player.Enable();
        controls.Player.LookAround.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.LookAround.canceled += ctx => lookInput = Vector2.zero;

        controls.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>(); // Update moveInput when movement input is performed
        controls.Player.Movement.canceled += ctx => moveInput = Vector2.zero; // Reset moveInput when movement input is canceled

        controls.Player.Jump.performed += ctx => Jump();
        controls.Player.Shoot.performed += ctx => Shoot();
        controls.Player.Shoot.canceled += ctx => CancelShoot();
        controls.Player.Dash.performed += ctx => StartCoroutine(Dash());

    }

    private void Update()
    {
        ApplyGravity();
        Move();
        LookAround();

        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Range, ContactLayer))
        {
            // Update line positions from start point to hit point
            lineRenderer.SetPosition(0, LaserGunPoint.position);
            lineRenderer.SetPosition(1, hit.point);

            // Enable the line renderer
        }

        

        if (isShooting && ShootLimiter < 16)
        {
            ShootLimiter += Time.deltaTime;
        }
        else if (!isShooting && ShootLimiter > 0)
        {

            ShootLimiter -= Time.deltaTime;
            ShootLimiter -= Time.deltaTime;


        }
        else if (isShooting && ShootLimiter >= 15)
        {
            CancelShoot();
            StartCoroutine(ShootLimitercoolDown());

        }

        ShootLimiterUI.value = ShootLimiter;
    }

    IEnumerator ShootLimitercoolDown()
    {
        yield return new WaitForSeconds(3);
        isShooting = false;

    }


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        stats = GetComponent<PlayerStats>();
    }

    private void Start()
    {
        Animations = GetComponent<ShootingAniamtionsHandler>();
        // Create and configure the LineRenderer component
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.blue;
        lineRenderer.endColor = Color.blue;
        lineRenderer.startWidth = 0.3f;
        lineRenderer.endWidth = 0.3f;
        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2; // Need 2 points for a line
        isShooting = false;

    }


    void Shoot()
    {
        canShoot = true;
        isShooting = true;
        StartCoroutine(Animations.SMGShoot());
        StartCoroutine(SMGSHoot());
        if (HasMachineGun)
        {
            StartCoroutine(MachineGunShoot());
        }
        if (HasBazooka)
        {
            StartCoroutine(BazookaShoot());
        }
        if (HasLaserGun)
        {
            StartCoroutine(LaserShoot());
            lineRenderer.enabled = true;
        }
    }

    IEnumerator Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            moveSpeed += 5;
            yield return new WaitForSeconds(0.5f);
            moveSpeed -= 5;
            isDashing = false;
        }

    }
    IEnumerator MachineGunShoot()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, Range, ContactLayer))
        {

            if (hit.collider != null)
            {

                GameObject Bullet = Instantiate(BulletPrefab, MachineGunPoint.position, Quaternion.identity);
                BulletController controller = Bullet.GetComponent<BulletController>();
                controller.hitPoint = hit.point;

                if (hit.collider.CompareTag("Head"))
                {
                    Transform Enemy = hit.collider.transform.parent;
                    EnemyScript HealthSCript = Enemy.GetComponent<EnemyScript>();
                    HealthSCript.HP -= 6 + stats.baseDamage;
                    GameObject DamagerText = Instantiate(DamagerIndicatorText, hit.point, Quaternion.identity);
                    DamagerText.transform.SetParent(hit.collider.gameObject.transform);
                    DamagerText.transform.rotation = transform.rotation;
                    Destroy(DamagerText, 1f);
                    TextMeshPro Text = DamagerText.GetComponent<TextMeshPro>();
                    Text.text = $"{6 + stats.baseDamage}";

                }
                else if (hit.collider.CompareTag("Body"))
                {
                    Transform Enemy = hit.collider.transform.parent;
                    EnemyScript HealthSCript = Enemy.GetComponent<EnemyScript>();
                    HealthSCript.HP -= 3 + stats.baseDamage;
                    GameObject DamagerText = Instantiate(DamagerIndicatorText, hit.point, Quaternion.identity);
                    DamagerText.transform.SetParent(hit.collider.gameObject.transform);
                    DamagerText.transform.rotation = transform.rotation;
                    Destroy(DamagerText, 1f);
                    TextMeshPro Text = DamagerText.GetComponent<TextMeshPro>();
                    Text.color = Color.yellow;
                    Text.text = $"{3 + stats.baseDamage}";
                }
            }
        }
        yield return new WaitForSeconds(0.05f / stats.fireRate);
        if (canShoot)
        {
            StartCoroutine(MachineGunShoot());
        }
    }

    IEnumerator BazookaShoot()
    {
        yield return new WaitForSeconds(1.1f);

        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, Range, ContactLayer))
        {

            if (hit.collider != null)
            {
                GameObject Bullet = Instantiate(BazookaBullet, BazookaGunPoint.position, Quaternion.identity);
                BulletController controller = Bullet.GetComponent<BulletController>();
                controller.hitPoint = hit.point;

                if (hit.collider.CompareTag("Head"))
                {
                    Transform Enemy = hit.collider.transform.parent;
                    EnemyScript HealthSCript = Enemy.GetComponent<EnemyScript>();
                    HealthSCript.HP -= 30 + stats.baseDamage;
                    GameObject DamagerText = Instantiate(DamagerIndicatorText, hit.point, Quaternion.identity);
                    DamagerText.transform.SetParent(hit.collider.gameObject.transform);
                    DamagerText.transform.rotation = transform.rotation;
                    Destroy(DamagerText, 1f);
                    TextMeshPro Text = DamagerText.GetComponent<TextMeshPro>();
                    Text.text = $"{30 + stats.baseDamage}";
                }
                else if (hit.collider.CompareTag("Body"))
                {
                    Transform Enemy = hit.collider.transform.parent;
                    EnemyScript HealthSCript = Enemy.GetComponent<EnemyScript>();
                    HealthSCript.HP -= 15 + stats.baseDamage;
                    GameObject DamagerText = Instantiate(DamagerIndicatorText, hit.point, Quaternion.identity);
                    DamagerText.transform.SetParent(hit.collider.gameObject.transform);
                    DamagerText.transform.rotation = transform.rotation;
                    Destroy(DamagerText, 1f);
                    TextMeshPro Text = DamagerText.GetComponent<TextMeshPro>();
                    Text.color = Color.yellow;
                    Text.text = $"{15 + stats.baseDamage}";
                }
            }
        }
        yield return new WaitForSeconds(2.4f / stats.fireRate);

        if (canShoot)
        {
            StartCoroutine(BazookaShoot());
        }
    }

    IEnumerator LaserShoot()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, Range, ContactLayer))
        {

            if (hit.collider != null)
            {


                if (hit.collider.CompareTag("Head"))
                {
                    Transform Enemy = hit.collider.transform.parent;
                    EnemyScript HealthSCript = Enemy.GetComponent<EnemyScript>();
                    HealthSCript.HP -= 6 + stats.baseDamage;
                    GameObject DamagerText = Instantiate(DamagerIndicatorText, hit.point, Quaternion.identity);
                    DamagerText.transform.SetParent(hit.collider.gameObject.transform);
                    DamagerText.transform.rotation = transform.rotation;
                    Destroy(DamagerText, 1f);
                    TextMeshPro Text = DamagerText.GetComponent<TextMeshPro>();
                    Text.text = $"{3 + stats.baseDamage}";
                }
                else if (hit.collider.CompareTag("Body"))
                {
                    Transform Enemy = hit.collider.transform.parent;
                    EnemyScript HealthSCript = Enemy.GetComponent<EnemyScript>();
                    HealthSCript.HP -= 3 + stats.baseDamage;
                    GameObject DamagerText = Instantiate(DamagerIndicatorText, hit.point, Quaternion.identity);
                    DamagerText.transform.SetParent(hit.collider.gameObject.transform);
                    DamagerText.transform.rotation = transform.rotation;
                    Destroy(DamagerText, 1f);
                    TextMeshPro Text = DamagerText.GetComponent<TextMeshPro>();
                    Text.color = Color.yellow;
                    Text.text = $"{1 + stats.baseDamage}";
                }
            }
        }
        yield return new WaitForSeconds(0.03f / stats.fireRate);

        if (canShoot)
        {
            StartCoroutine(LaserShoot());
        }
    }
    IEnumerator SMGSHoot()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, Range, ContactLayer))
        {

            if (hit.collider != null)
            {

                GameObject Bullet = Instantiate(BulletPrefab, holdingPosition.position, Quaternion.identity);
                BulletController controller = Bullet.GetComponent<BulletController>();
                controller.hitPoint = hit.point;

                if (hit.collider.CompareTag("Head"))
                {
                    Transform Enemy = hit.collider.transform.parent;
                    EnemyScript HealthSCript = Enemy.GetComponent<EnemyScript>();
                    HealthSCript.HP -= 6 + stats.baseDamage;
                    GameObject DamagerText = Instantiate(DamagerIndicatorText, hit.point, Quaternion.identity);
                    DamagerText.transform.SetParent(hit.collider.gameObject.transform);
                    DamagerText.transform.rotation = transform.rotation;
                    Destroy(DamagerText, 1f);
                    TextMeshPro Text = DamagerText.GetComponent<TextMeshPro>();
                    Text.text = $"{6 + stats.baseDamage}";
                }
                else if (hit.collider.CompareTag("Body"))
                {
                    Transform Enemy = hit.collider.transform.parent;
                    EnemyScript HealthSCript = Enemy.GetComponent<EnemyScript>();
                    HealthSCript.HP -= 3 + stats.baseDamage;
                    GameObject DamagerText = Instantiate(DamagerIndicatorText, hit.point, Quaternion.identity);
                    DamagerText.transform.SetParent(hit.collider.gameObject.transform);
                    DamagerText.transform.rotation = transform.rotation;
                    Destroy(DamagerText, 1f);
                    TextMeshPro Text = DamagerText.GetComponent<TextMeshPro>();
                    Text.color = Color.yellow;
                    Text.text = $"{3 + stats.baseDamage}";
                }
            }
        }
        yield return new WaitForSeconds(0.2f / stats.fireRate);

        if (canShoot)
        {
            StartCoroutine(SMGSHoot());
        }
    }
    void CancelShoot()
    {
        StartCoroutine(Animations.SMGStop());
        canShoot = false;
        lineRenderer.enabled = false;
        isShooting = false;
    }

    void Jump()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2))
        {
            velocity.y = Mathf.Sqrt(stats.jumpHeight * -2f * gravity);

        }
    }


    void Move()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

        move = transform.TransformDirection(move);

        characterController.Move(move * stats.moveSpeed * Time.deltaTime);

    }

    public void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            //float fallMultiplier = 2.5f;
            velocity.y += gravity * stats.descentMultiplier * Time.deltaTime;

            velocity.y = Mathf.Max(velocity.y, terminalVelocity);
        }
        else if (velocity.y < 0)
        {
            velocity.y = groundedGravity;
        }

        characterController.Move(velocity * Time.deltaTime);
    }

    public void LookAround()
    {
        /// Get horizontal and vertical look inputs and adjust based on sensitivity
        float LookX = lookInput.x * lookSpeed;
        float LookY = lookInput.y * lookSpeed;

        // Horizontal rotation: Rotate the player object around the y-axis
        transform.Rotate(0, LookX, 0);

        // Vertical rotation: Adjust the vertical look rotation and clamp it to prevent flipping
        verticalLookRotation -= LookY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90, 60);

        // Apply the clamped vertical rotation to the player camera
        playerCamera.localEulerAngles = new Vector3(verticalLookRotation, 0, 0);
    }

}