using UnityEngine;

public class UpgradePauseController : MonoBehaviour
{
    [Header("Upgrade UI")]
    [SerializeField] private GameObject upgradeUI;

    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private MonoBehaviour playerMovementScript;

    private bool isUpgradeOpen = false;

    // Singleton instance
    public static UpgradePauseController Instance;

    private void Awake()
    {
        // Singleton pattern (only one upgrade pause controller)
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OpenUpgradeMenu()
    {
        isUpgradeOpen = true;
        upgradeUI.SetActive(true);

        // Pause the game
        Time.timeScale = 0f;
        AudioListener.pause = true;

        // Disable player movement
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = false;
        }

        Debug.Log("Upgrade menu opened.");
    }

    public void CloseUpgradeMenu()
    {
        isUpgradeOpen = false;
        upgradeUI.SetActive(false);

        // Resume the game
        Time.timeScale = 1f;
        AudioListener.pause = false;

        // Re-enable player movement
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = true;
        }

        Debug.Log("Upgrade menu closed.");
    }
}
