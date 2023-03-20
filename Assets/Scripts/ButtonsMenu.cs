using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonsMenu : MonoBehaviour
{
    private GameObject menu;
    private GameObject settingsScreen;
    private GameObject buttonsParent;
    private GameObject achviementsScreen;
    private GameObject creditsScreen;
    private CreditsText creditsText;
    private GenerateImages generateImages;
    private Coroutine lastCoroutine;

    [SerializeField] private Button[] menuButtons;
    [SerializeField] private Button[] onOffSettingsButtons;
    [SerializeField] private Slider[] settingsSliders;

    [Header("Settings")]
    [SerializeField] private AudioSource[] musics;
    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private GameObject helpGuide;
    [SerializeField] private RandomTexts[] randomTexts; // To activate binary texts back

    private void Start()
    {
        menu = GameObject.Find("Menu");

        settingsScreen = menu.transform.Find("SettingsScreen").gameObject;
        settingsScreen.SetActive(false);

        buttonsParent = menuButtons[0].transform.parent.gameObject;

        achviementsScreen = GameObject.Find("Achviements");
        achviementsScreen.SetActive(false);

        creditsScreen = GameObject.Find("Credits");
        creditsText = creditsScreen.GetComponentInChildren<CreditsText>();
        generateImages = creditsScreen.transform.Find("BlackPart").GetComponentInChildren<GenerateImages>();
        creditsScreen.SetActive(false);

        foreach (Button button in menuButtons)
        {
            button.onClick.AddListener(delegate{ChangeView(button);});
        }
        foreach (Button button in onOffSettingsButtons)
        {
            button.onClick.AddListener(delegate {OnOffSettings(button); });
        }
        foreach (Slider slider in settingsSliders)
        {
            slider.onValueChanged.AddListener(delegate { SliderSettings(slider); });
        }
    }
    private void ChangeView(Button button)
    {
        if(button.gameObject.name == "PlayButton")
        {
            menu.SetActive(false);
        }
        else if(button.gameObject.name == "AchviementsButton")
        {
            menu.SetActive(false);
            achviementsScreen.SetActive(true);
        }
        else if(button.gameObject.name == "SettingsButton")
        {
            settingsScreen.SetActive(true);
            buttonsParent.SetActive(false);
        }
        else if(button.gameObject.name == "BackButton")
        {
            buttonsParent.SetActive(true);
            settingsScreen.SetActive(false);
        }
        else if(button.gameObject.name == "BackButtonAchvie")
        {
            menu.SetActive(true);

            foreach(RandomTexts randomTxts in randomTexts)
                randomTxts.coroutineHasFinished = true;

            achviementsScreen.SetActive(false);
        }
        else if(button.gameObject.name == "BackButtonCredits")
        {
            menu.SetActive(true);

            foreach(RandomTexts randomTxts in randomTexts)
                randomTxts.coroutineHasFinished = true;

            creditsScreen.SetActive(false);
        }
        else if(button.gameObject.name == "CreditsButton")
        {
            menu.SetActive(false);
            
            creditsScreen.SetActive(true);
            creditsText.ClearTexts();

            if(lastCoroutine!=null)
                StopCoroutine(routine: lastCoroutine);
            lastCoroutine = StartCoroutine(routine: generateImages.Generate());
        }
        else if(button.gameObject.name == "QuitButton"){
            Application.Quit();
        }
        else
        {
            Debug.Log("No buttons has been found.");
        }
    }
    private void OnOffSettings(Button button)
    {
        TextMeshProUGUI tPro = button.GetComponentInChildren<TextMeshProUGUI>();
        if(tPro.text == "On")
        {
            tPro.text = "Off";
        }
        else
        {
            tPro.text = "On";
        }

        if(button.gameObject.name == "SfxValue")
        {
            foreach(AudioSource audio in sfx)
            {
                audio.mute = !audio.mute;
            }
        }
        if(button.gameObject.name == "HelpValue")
        {
            helpGuide.SetActive(!helpGuide.activeSelf);
        }

    }
    private void SliderSettings(Slider slider)
    {
        foreach (AudioSource audio in musics)
        {
            audio.volume = slider.value;
        }
    }
}
