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
                inventoryTest.SetActive(true);
                inventoryOpen = true;
            } else {
                inventoryTest.SetActive(false);
                inventoryOpen = false;
            }
        }
    }
}
