using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenuFirstButton;
    public GameObject upgradeMenuFirstButton;
    public GameObject shopMenuFirstButton;

    public void OpenPauseMenu()
    {
        SetSelected(pauseMenuFirstButton);
    }

    public void OpenUpgradeMenu()
    {
        SetSelected(upgradeMenuFirstButton);
    }

    public void OpenShopMenu()
    {
        SetSelected(shopMenuFirstButton);
    }

    private void SetSelected(GameObject button)
    {
        EventSystem.current.SetSelectedGameObject(null); // Clear selection
        EventSystem.current.SetSelectedGameObject(button);
    }
}


