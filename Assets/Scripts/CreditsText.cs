using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditsText : MonoBehaviour
{
    [SerializeField] private GameObject firstGameObject;

    RectTransform firstRect;

    TextMeshProUGUI firstText;

    public float textSpeed = 50;

    private string textsToVisualize = "More info at LinkedIn or in the page you downloaded this.\n\n"+
     "There's not so much too show :) \n 2 friends tried to create a simple game"+
     " and this is it. \n\n Hope you liked, this is the first one for sharing, i'll try making"+
     " better ones to learn more about game dev.\n\n Dereck is a beast at designing pixel arts."+
     " He does freelancers and is open to work. \n\n Gabriel is a dev for 3 years, but didn't post his old"+
     " games, he'll try to update older games to publish soon. He is open to work."+
     "\n\n Designers: Dereck Santos, Gabriel Franca \n\n"+
     "Game Developer: Gabriel Franca \n\n Musicians: Juhani Junkala, Alex McCulloch\n\n";

    private Vector2 firstRectFixedStartPosition;
    private TextMeshProUGUI title;
    private RectTransform blackPart;
    private Vector2 fixedSizeDeltaBlackPart;

    void Awake()
    {
        title = transform.parent.Find("Title").GetComponent<TextMeshProUGUI>();
        blackPart = transform.parent.Find("BlackPart").GetComponent<RectTransform>();

        fixedSizeDeltaBlackPart = blackPart.sizeDelta;

        firstRect = firstGameObject.GetComponent<RectTransform>();

        firstRectFixedStartPosition = firstRect.anchoredPosition;

        firstText = firstGameObject.GetComponent<TextMeshProUGUI>();

        firstText.text = textsToVisualize;
    }

    void Update()
    {
        if(firstRect.anchoredPosition.y <= 851f && firstRect.anchoredPosition.y >= -300f){
            MoveText();
        }
    }

    private void MoveText(){
        firstRect.anchoredPosition = new Vector2(firstRect.anchoredPosition.x,
            firstRect.anchoredPosition.y - Time.deltaTime * textSpeed);

        if(firstRect.anchoredPosition.y <= -300f && firstRect.anchoredPosition.y >= -301f){
            firstRect.anchoredPosition = Vector2.up*-302f;
            StartCoroutine(TurnOffTitle());
        }
    }
    IEnumerator TurnOffTitle(){
        float tempAlpha = 1f;
        Color color = title.color;

        while(true){
            tempAlpha -= 1f * Time.deltaTime;
            title.color = new Color(color.r, color.g, color.b, tempAlpha);
            yield return new WaitForSeconds(0.01f);
            if(tempAlpha<=0f){
                break;
            }
        }
        StartCoroutine(BlackScreenFit());
    }
    IEnumerator BlackScreenFit(){
        while(true){
            blackPart.sizeDelta = new Vector2(blackPart.sizeDelta.x + 2.5f,
            blackPart.sizeDelta.y);
            yield return new WaitForSeconds(0.01f);
            if(blackPart.sizeDelta.x >= 800f){
                break;
            }
        }
    }

    public void ClearTexts(){
        firstRect.anchoredPosition = firstRectFixedStartPosition;

        blackPart.sizeDelta = fixedSizeDeltaBlackPart;

        Color color = title.color;
        title.color = new Color(color.r, color.g, color.b, 1f);
    }
}
