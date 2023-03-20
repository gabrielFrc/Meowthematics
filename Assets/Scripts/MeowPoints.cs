using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MeowPoints : MonoBehaviour
{
    private TextMeshProUGUI meowPointsText;
    [SerializeField] private PaperScript paperScript;

    private AudioSource meowPointsSound;

    static public float meowPoints = 0;
    private const string pointsName = "MeowPoints: ";

    //Cat reaction
    [HideInInspector] public Image catMood;
    [SerializeField] private Sprite normalCat;
    [SerializeField] private Sprite madCat;
    [SerializeField] private Sprite happyCat;
    [SerializeField] private Animator catReactionAnim;

    [SerializeField] private AudioSource madCatSound;
    [SerializeField] private AudioSource happyCatSound;

    private void Start()
    {
        catReactionAnim.Play("CatReaction");

        meowPointsText = transform.Find("Canvas/MeowP/MeowPointsText").GetComponent<TextMeshProUGUI>();

        meowPointsSound = GetComponent<AudioSource>();

        catMood = GameObject.Find("MeowPoints/Canvas/CatProfile/CatReaction").GetComponent<Image>();
    }
    private void Update()
    {
        meowPointsText.text = pointsName + meowPoints.ToString();
    }
    public void CatMood(string catMoodString)
    {
        if (catMoodString == "Happy")
        {
            catReactionAnim.enabled = false;

            meowPointsSound.Play();
            happyCatSound.Play();
            catMood.sprite = happyCat;
        }
        else if (catMoodString == "Mad")
        {
            catReactionAnim.enabled = false;

            madCatSound.Play();
            catMood.sprite = madCat;
        }
        else
        {
            catMood.sprite = normalCat;

            catReactionAnim.enabled = true;
            catReactionAnim.Play("CatReaction");
        }
    }
}
