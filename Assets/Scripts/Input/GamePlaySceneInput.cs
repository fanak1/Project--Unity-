using UnityEngine;

public class GamePlaySceneInput : MonoBehaviour
{

    public GameObject inventoryTest;

    private bool inventoryOpen = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryOpen = false;
        inventoryTest.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!inventoryOpen) {
                if (!GameManager.Instance.TryToPause()) return;
                OpenInventory();

            } else {
                if (!GameManager.Instance.TryToResume()) return;
                CloseInventory();

            }
        }
    }

    public void OpenInventory()
    {
        GameManager.Instance.TryToPause();
        inventoryTest.SetActive(true);
        inventoryOpen = true;
    }

    public void CloseInventory()
    {
        TooltipManager.Instance.Hide();
        GameManager.Instance.TryToResume();
        inventoryTest.SetActive(false);
        inventoryOpen = false;
    }

    public void QuitToMainMenu()
    {
        GameManager.Instance.TryToResume();
        GameManager.Instance.Menu();
    }
}
