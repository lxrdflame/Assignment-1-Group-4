using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
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
    [SerializeField]
    private bool HaveSMG;
    //Interaction 
    public LayerMask InteractLayer;
    ShootingAniamtionsHandler Animations;
    public GameObject DamagerIndicatorText;



    private void OnEnable()
    {
        controls = new Controls();
        controls.Player.Enable();
        controls.Player.LookAround.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.LookAround.canceled += ctx => lookInput = Vector2.zero;

        controls.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>(); // Update moveInput when movement input is performed
        controls.Player.Movement.canceled += ctx => moveInput = Vector2.zero; // Reset moveInput when movement input is canceled

        controls.Player.Interact.performed += ctx => Interact();
        controls.Player.Jump.performed += ctx => Jump();
        controls.Player.Shoot.performed += ctx => Shoot();
        controls.Player.Shoot.canceled += ctx => CancelShoot();

        controls.Player.Reload.performed += ctx => Reload();


    }

    private void Update()
    {
        ApplyGravity();
        Move();
        LookAround();
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Animations = GetComponent<ShootingAniamtionsHandler>();
    }
    void Interact()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;


        /*if (Physics.Raycast(ray, out hit, PickupRange, InteractLayer))
        {
        }*/
    }

    void Shoot()
    {
        if (!HaveSMG)
        {
            Ray ray = new Ray(playerCamera.position, playerCamera.forward);
            RaycastHit hit;


            if (Physics.Raycast(ray, out hit, Range))
            {

                if (hit.collider != null)
                {
                    StartCoroutine(Animations.ShootAnimation());

                    GameObject Bullet = Instantiate(BulletPrefab, holdingPosition.position, Quaternion.identity);
                    BulletController controller = Bullet.GetComponent<BulletController>();
                    controller.hitPoint = hit.point;

                    if (hit.collider.CompareTag("Head"))
                    {
                        Transform Enemy = hit.collider.transform.parent;
                        EnemyScript HealthSCript = Enemy.GetComponent<EnemyScript>();
                        HealthSCript.HP -= 6;
                        GameObject DamagerText = Instantiate(DamagerIndicatorText, hit.point, Quaternion.identity);
                        DamagerText.transform.rotation = transform.rotation;
                        Destroy(DamagerText, 1f);
                        TextMeshPro Text = DamagerText.GetComponent<TextMeshPro>();
                        Text.text = "6";
                    }
                    else if (hit.collider.CompareTag("Body"))
                    {
                        Transform Enemy = hit.collider.transform.parent;
                        EnemyScript HealthSCript = Enemy.GetComponent<EnemyScript>();
                        HealthSCript.HP -= 3;
                        GameObject DamagerText = Instantiate(DamagerIndicatorText, hit.point, Quaternion.identity);
                        DamagerText.transform.rotation = transform.rotation;
                        Destroy(DamagerText, 1f);
                        TextMeshPro Text = DamagerText.GetComponent<TextMeshPro>();
                        Text.color = Color.yellow;
                        Text.text = "3";
                    }
                }
            }
        }
        else if (HaveSMG)
        {
            StartCoroutine(SMGSHoot());
        }
    }

    IEnumerator SMGSHoot()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, Range))
        {

            if (hit.collider != null)
            {
                StartCoroutine(Animations.ShootAnimation());

                GameObject Bullet = Instantiate(BulletPrefab, holdingPosition.position, Quaternion.identity);
                BulletController controller = Bullet.GetComponent<BulletController>();
                controller.hitPoint = hit.point;

                if (hit.collider.CompareTag("Head"))
                {
                    Transform Enemy = hit.collider.transform.parent;
                    EnemyScript HealthSCript = Enemy.GetComponent<EnemyScript>();
                    HealthSCript.HP -= 6;
                    GameObject DamagerText = Instantiate(DamagerIndicatorText, hit.point, Quaternion.identity);
                    DamagerText.transform.rotation = transform.rotation;
                    Destroy(DamagerText, 1f);
                    TextMeshPro Text = DamagerText.GetComponent<TextMeshPro>();
                    Text.text = "6";
                }
                else if (hit.collider.CompareTag("Body"))
                {
                    Transform Enemy = hit.collider.transform.parent;
                    EnemyScript HealthSCript = Enemy.GetComponent<EnemyScript>();
                    HealthSCript.HP -= 3;
                    GameObject DamagerText = Instantiate(DamagerIndicatorText, hit.point, Quaternion.identity);
                    DamagerText.transform.rotation = transform.rotation;
                    Destroy(DamagerText, 1f);
                    TextMeshPro Text = DamagerText.GetComponent<TextMeshPro>();
                    Text.color = Color.yellow;
                    Text.text = "3";
                }
            }
        }
        yield return new WaitForSeconds(0.2f);

        if (Physics.Raycast(ray, out hit, Range))
        {

            if (hit.collider != null)
            {
                StartCoroutine(Animations.ShootAnimation());

                GameObject Bullet = Instantiate(BulletPrefab, holdingPosition.position, Quaternion.identity);
                BulletController controller = Bullet.GetComponent<BulletController>();
                controller.hitPoint = hit.point;

                if (hit.collider.CompareTag("Head"))
                {
                    Transform Enemy = hit.collider.transform.parent;
                    EnemyScript HealthSCript = Enemy.GetComponent<EnemyScript>();
                    HealthSCript.HP -= 6;
                    GameObject DamagerText = Instantiate(DamagerIndicatorText, hit.point, Quaternion.identity);
                    DamagerText.transform.rotation = transform.rotation;
                    Destroy(DamagerText, 1f);
                    TextMeshPro Text = DamagerText.GetComponent<TextMeshPro>();
                    Text.text = "6";
                }
                else if (hit.collider.CompareTag("Body"))
                {
                    Transform Enemy = hit.collider.transform.parent;
                    EnemyScript HealthSCript = Enemy.GetComponent<EnemyScript>();
                    HealthSCript.HP -= 3;
                    GameObject DamagerText = Instantiate(DamagerIndicatorText, hit.point, Quaternion.identity);
                    DamagerText.transform.rotation = transform.rotation;
                    Destroy(DamagerText, 1f);
                    TextMeshPro Text = DamagerText.GetComponent<TextMeshPro>();
                    Text.color = Color.yellow;
                    Text.text = "3";
                }
            }
        }
        yield return new WaitForSeconds(0.2f);

        StartCoroutine(SMGSHoot());
    }
    void CancelShoot()
    {
        StopAllCoroutines();
    }

    void Jump()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2))
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
        }
    }

    void Reload()
    {
        StartCoroutine(Animations.ReloadAnimation());
    }
    void Move()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

        move = transform.TransformDirection(move);

        characterController.Move(move * moveSpeed * Time.deltaTime);
    }

    public void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            float fallMultiplier = 2.5f;
            velocity.y += gravity * fallMultiplier * Time.deltaTime;
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
