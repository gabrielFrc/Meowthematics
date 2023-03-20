using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    private GameObject Paper;
    private bool isPaperActive = true;
    private PaperAnimation paperAnim;
    private AudioSource paperSound;

    private GameObject Menu;

    private GameObject achviements;

    private GameObject credits;

    private AudioSource gameSong;

    [SerializeField] private RandomTexts[] randomTexts;

    private void Awake()
    {
        Menu = GameObject.Find("Menu");

        achviements = GameObject.Find("Achviements");

        credits = GameObject.Find("Credits");

        gameSong = GameObject.Find("/Background").GetComponent<AudioSource>();
    }
    void Start()
    {
        Paper = GameObject.Find("Paper");
        Paper.SetActive(false);
        paperAnim = Paper.GetComponent<PaperAnimation>();
        paperSound = Paper.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!gameSong.isPlaying && !Menu.activeSelf && !achviements.activeSelf && !credits.activeSelf)
        {
            gameSong.Play();
        }
        else if (Menu.activeSelf)
        {
            if(Paper.activeSelf)
                Paper.SetActive(false);
            gameSong.Stop();
        }

        //Will activate paper of exercises, but when u desativate it, will count as you tried to
        //acess calculator to get the result
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!Paper.activeSelf && !Menu.activeSelf && !achviements.activeSelf && !credits.activeSelf)
            {
                Paper.SetActive(true);
                paperSound.Play();
                paperAnim.AnimatePaper("PaperIn");
            }
            else if (Paper.activeSelf)
            {
                paperSound.Play();
                paperAnim.AnimatePaper("PaperOut");
                isPaperActive = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !achviements.activeSelf && !credits.activeSelf)
        {
            Menu.SetActive(true);

            foreach (RandomTexts randomTxts in randomTexts)
                randomTxts.coroutineHasFinished = true;
        }
    }
    public bool hasUsedCalculator(bool reset)
    {
        if (!reset)
        {
            isPaperActive = !isPaperActive;
            return isPaperActive;
        }
        else
        {
            isPaperActive = true;
            return true;
        }
    }
}
