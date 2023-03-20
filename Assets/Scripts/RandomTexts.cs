using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomTexts : MonoBehaviour
{
    GameObject firstGameObject;
    GameObject secondGameObject;

    RectTransform firstRect;
    RectTransform secondRect;

    TextMeshProUGUI firstText;
    TextMeshProUGUI secondText;

    [HideInInspector] public bool coroutineHasFinished = false;

    private string[] characters = { "0", "1"};
    private List<string> list_string = new List<string>();
    private List<string> list_string2 = new List<string>();

    public float textSpeed = 50;

    void Start()
    {
        firstGameObject = transform.GetChild(0).gameObject;
        secondGameObject = transform.GetChild(1).gameObject;

        firstRect = firstGameObject.GetComponent<RectTransform>();
        secondRect = secondGameObject.GetComponent<RectTransform>();

        firstText = firstGameObject.GetComponent<TextMeshProUGUI>();
        secondText = secondGameObject.GetComponent<TextMeshProUGUI>();

        GoToList();
    }

    void Update()
    {
        if (coroutineHasFinished)
        {
            coroutineHasFinished = false;
            StartCoroutine(GenerateRandomElement());
        }

        firstRect.anchoredPosition = new Vector2(firstRect.anchoredPosition.x,
            firstRect.anchoredPosition.y - Time.deltaTime * textSpeed);
        secondRect.anchoredPosition = new Vector2(secondRect.anchoredPosition.x,
            secondRect.anchoredPosition.y - Time.deltaTime * textSpeed);

        if (firstRect.anchoredPosition.y <= -450)
        {
            firstRect.anchoredPosition = new Vector2(firstRect.anchoredPosition.x, 450);
        }
        if (secondRect.anchoredPosition.y <= -450)
        {
            secondRect.anchoredPosition = new Vector2(secondRect.anchoredPosition.x, 450);
        }
    }
    private void GoToList()
    {
        foreach (char c in firstText.text)
        {
            list_string.Add(c.ToString());
        }
        foreach (char c in secondText.text)
        {
            list_string2.Add(c.ToString());
        }
        StartCoroutine(GenerateRandomElement());
    }
    private IEnumerator GenerateRandomElement()
    {
        firstText.text = "";
        secondText.text = "";

        List<string> local_list = new List<string>();
        List<string> local_list2 = new List<string>();

        foreach(string s in list_string)
        {
            if(s == "\t")
            {
                local_list.Add("\t");
            }
            else
                local_list.Add(characters[Random.Range(0, characters.Length)]);
        }
        foreach (string s in list_string2)
        {
            if (s == "\t")
            {
                local_list2.Add("\t");
            }
            else
                local_list2.Add(characters[Random.Range(0, characters.Length)]);
        }

        foreach (string s in local_list)
        {
            if(s.Length<2)
               firstText.text += s;
            else
            {
                for(int i = firstText.text.Length-1; i>firstText.text.Length-s.Length; i--)
                {
                    if(i>0)
                        firstText.text.Remove(i);
                }
                firstText.text += s;
            }
        }
        foreach (string s in local_list2)
        {
            if (s.Length < 2)
                secondText.text += s;
            else
            {
                for (int i = secondText.text.Length - 1; i > secondText.text.Length - s.Length; i--)
                {
                    if (i > 0)
                        secondText.text.Remove(i);
                }
                secondText.text += s;
            }
        }

        yield return new WaitForSeconds(0.1f);

        coroutineHasFinished = true;
    }
}
