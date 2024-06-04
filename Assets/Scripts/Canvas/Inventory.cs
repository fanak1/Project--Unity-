using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : StaticInstance<Inventory>
{
    private GameObject tabBeingDisplay;
    [SerializeField] private GameObject[] tabs;

    private GameObject buttonBeingDisplay;
    [SerializeField] private GameObject[] buttons;

    [SerializeField] private Color buttonActiveColor;
    [SerializeField] private Color buttonInactiveColor;
    [SerializeField] private Vector2 buttonActiveSize;
    [SerializeField] private Vector2 buttonInactiveSize;
    [SerializeField] private Vector2 buttonActivePosition;
    [SerializeField] private Vector2 buttonInactivePosition;

    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI mpText;
    [SerializeField] private TextMeshProUGUI atkText;
    [SerializeField] private TextMeshProUGUI spdText;
    [SerializeField] private TextMeshProUGUI defText;

    [SerializeField] private GameObject abilitiesContainer;
    [SerializeField] private GameObject abilityDisplayPrefabs;

    private GameObject player;


    void DisplayStats(Stats stats) {
        hpText.SetText(stats.hp.ToString());
        mpText.SetText(stats.mp.ToString());
        atkText.SetText(stats.atk.ToString());
        spdText.SetText(stats.spd.ToString());
        defText.SetText(stats.def.ToString());
    }

    void DisplayAbilities(List<ScriptableAlbilities> abilities) {
        foreach(ScriptableAlbilities ability in abilities) {
            CreateAnAbilityDisplay(ability);
        }
    }

    void CreateAnAbilityDisplay(ScriptableAlbilities ability) {
        var abilityDisplay = Instantiate(abilityDisplayPrefabs);
        TextMeshProUGUI text = abilityDisplay.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if(text != null) {
            text.SetText(ability.description);
        }
        abilityDisplay.transform.SetParent(abilitiesContainer.transform);
    }


    void SetActiveForButton(GameObject button) {
        if(buttonBeingDisplay != button) {
            SetInactiveForButton(buttonBeingDisplay);

            Image image = button.GetComponent<Image>();
            image.color = buttonActiveColor;
            RectTransform rect = button.GetComponent<RectTransform>();
            rect.localPosition = new Vector2(rect.localPosition.x, buttonActivePosition.y);
            rect.sizeDelta = buttonActiveSize;

            buttonBeingDisplay = button;
        }
    }

    void SetInactiveForButton(GameObject button) {
        Image image = button.GetComponent<Image>();
        image.color = buttonInactiveColor;
        RectTransform rect = button.GetComponent<RectTransform>();
        rect.localPosition = new Vector2(rect.localPosition.x, buttonInactivePosition.y);
        rect.sizeDelta = buttonInactiveSize;
    }

    public void SwitchToTab(int index) {
        if(tabBeingDisplay != tabs[index]) {
            tabBeingDisplay.SetActive(false);
            SetActiveForButton(buttons[index]);
            tabs[index].SetActive(true);
            
            tabBeingDisplay = tabs[index];
        }
    }

    private void Start() {
        foreach(GameObject tab in tabs) {
            tab.SetActive(false);
        }

        foreach(GameObject button in buttons) {
            SetInactiveForButton(button);
        }

        buttonBeingDisplay = buttons[0];
        Image image = buttonBeingDisplay.GetComponent<Image>();
        image.color = buttonActiveColor;
        RectTransform rect = buttonBeingDisplay.GetComponent<RectTransform>();
        rect.localPosition = new Vector2(rect.localPosition.x, buttonActivePosition.y);
        rect.sizeDelta = buttonActiveSize;

        tabBeingDisplay = tabs[0];
        tabBeingDisplay.SetActive(true);

        gameObject.SetActive(false);
    }

    private void OnEnable() {
        player = GameObject.FindGameObjectWithTag("Player");

        if(player != null) {
            UnitBase playerBase = player.GetComponent<UnitBase>();
            if(playerBase != null) {
                DisplayStats(playerBase.ShowStats());
                DisplayAbilities(playerBase.ShowAbilities());
            }
        }
    }

    private void OnDisable() {

    }

}
