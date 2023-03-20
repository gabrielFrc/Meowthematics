using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PaperScript : MonoBehaviour
{
    private TextMeshProUGUI notes;
    private const int texting_limit = 145;
    private TextMeshProUGUI current_length;

    private TMP_InputField inputField_notes;
    private TMP_InputField inputField_answer;

    private bool isAnswerCorrect = false;
    private string correctAnswer = "";

    private TextMeshProUGUI answerText;

    private TextMeshProUGUI titleText;
    private TextMeshProUGUI questionText;

    //Paper difficulty
    [SerializeField] private Image[] difficulties;
    private Image paperDifficulty;

    //Problem order
    private string catName;

    //Answer control
    [SerializeField] private GameManager gameManager;
    private RandomQuestions randomQuestions;
    private GroupOfStrings gOF;

    //Dropper
    [SerializeField] private Dropper dropper;

    //Paper animation
    private PaperAnimation paperAnim;

    //MeowPoints
    [SerializeField] MeowPoints meowPoints;

    //Giving help
    private TextMeshProUGUI helpText;

    private void Awake()
    {
        notes = GameObject.Find("Paper/Canvas/Notes/Anotation/Text Area/Text").GetComponentInChildren<TextMeshProUGUI>();
        current_length = GameObject.Find("Paper/Canvas/Notes/TextLimit").GetComponent<TextMeshProUGUI>();
        current_length.text = notes.text.Length - 1 + "/" + texting_limit;

        inputField_answer = GameObject.Find("Paper/Canvas/Answer").GetComponent<TMP_InputField>();
        inputField_notes = GameObject.Find("Paper/Canvas/Notes/Anotation").GetComponent<TMP_InputField>();

        answerText = GameObject.Find("Paper/Canvas/Answer/Text Area/Text").
            GetComponentInChildren<TextMeshProUGUI>();

        titleText = GameObject.Find("Paper/Canvas/ExerciseText/Title").GetComponent<TextMeshProUGUI>();
        questionText = GameObject.Find("Paper/Canvas/ExerciseText/Question").GetComponent<TextMeshProUGUI>();

        paperAnim = GetComponent<PaperAnimation>();

        randomQuestions = GetComponent<RandomQuestions>();

        paperDifficulty = transform.Find("Canvas/Difficulty").GetComponent<Image>();

        helpText = transform.Find("Canvas/HelpGuide").GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Start()
    {
        GenerateProblem();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GenerateProblem();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (inputField_answer.text != "")
            {
                isAnswerCorrect = inputField_answer.text == correctAnswer;

                Debug.Log("The correct answer is: " + correctAnswer);

                if (isAnswerCorrect)
                {
                    if (!gameManager.hasUsedCalculator(false))
                    {
                        Debug.Log(MeowPoints.meowPoints += PointsToReceive() * 1.5f);
                        dropper.OnCorrectAnswer(2);
                    }
                    else
                    {
                        Debug.Log(MeowPoints.meowPoints += PointsToReceive());
                        dropper.OnCorrectAnswer(1);
                    }

                    meowPoints.CatMood("Happy");
                }
                else
                {
                    meowPoints.CatMood("Mad");
                }
                Invoke("NormalMood", 1.5f);

                gameManager.hasUsedCalculator(true);

                inputField_notes.text = "";
                inputField_answer.text = "";

                notes.text = "";
                answerText.text = "";

                paperAnim.AnimatePaper("PaperOut");

                Invoke("GenerateProblem",0.2f);
            }
        }
        current_length.text = notes.text.Length - 1 + "/" + texting_limit;
    }
    private void NormalMood()
    {
        meowPoints.CatMood("Normal");
    }
    private float PointsToReceive()
    {
        switch (gOF.difficulty)
        {
            case "Easy":
                return 50f;
            case "Medium":
                return 100f;
            case "Hard":
                return 200f;
        }
        Debug.Log("Error in PointsToReceive,");
        return 0f;
    }
    private void GenerateProblem()
    {
        //The more repeated numbers, more common it is
        // OBS: EACH QUESTION WILL ADD 2/3/4 MORE DEPENDING ON HIS DIFFICULTY RARITY
        List<int> indexesRarity = new List<int>();

        int index = 0;
        foreach(GroupOfStrings groupOS in randomQuestions.ListGroupOS())
        {
            switch(groupOS.difficulty)
            {
                case "Easy":
                    for(int i = 1; i < 4; i++)
                    {
                        indexesRarity.Add(index);
                    }
                    break;
                case "Medium":
                    for (int i = 1; i < 2; i++)
                    {
                        indexesRarity.Add(index);
                    }
                    break;
                case "Hard":
                    for (int i = 1; i < 2; i++)
                    {
                        indexesRarity.Add(index);
                    }
                    break;

            }
            index++;
        }

        int randomNumber = indexesRarity[Random.Range(0, indexesRarity.Count)];
        gOF = randomQuestions.GroupOfStringsSelected(randomNumber);

        foreach (Image image in difficulties)
        {
            if (image.gameObject.name == gOF.difficulty)
            {
                paperDifficulty.sprite = image.sprite;
            }
        }

        catName = gOF.catNames[Random.Range(0, gOF.catNames.Length)];

        titleText.text = catName +
            gOF.titles[Random.Range(0, gOF.titles.Length)];

        string guideToChangeProblemStr = "Press X to change.";

        if (randomNumber == 1)
        {
            helpText.text = "Can you give me a paw? \n\n " +
                "This problem is a equation \n\n 2x + 2 = 8 \n 2x = 8 - 2 \n 2x = 6 \n x = 6/2 \n" +
                "x = 3\n\n" +
                "2x * 3 = 30 \n (2x * 3) = 30 \n 6x = 30 \n x = 30 / 6 \n x = 5 \n\n"+guideToChangeProblemStr;

            int randomForSymbol = Random.Range(0, 3);

            int Par1 = Random.Range(1, 11);
            int Par2;
            if (randomForSymbol < 2)
                Par2 = Random.Range(0, 101);
            else
            {
                Par2 = Random.Range(0, 11);
            }
            int xValue = Random.Range(0, 11);

            correctAnswer = xValue.ToString();

            string equationSymbol = "";
            int equationResult = 0;

            if (randomForSymbol == 0)
            {
                equationResult = Par1 * xValue + Par2;
                equationSymbol = " + ";
            }
            else if (randomForSymbol == 1)
            {
                equationResult = Par1 * xValue - Par2;
                equationSymbol = " - ";
            }
            else if (randomForSymbol == 2)
            {
                equationResult = Par1 * xValue * Par2;
                equationSymbol = " * ";
            }

            questionText.text = catName +
                gOF.firstPart[Random.Range(0, gOF.firstPart.Length)] +
                Par1.ToString() + "x" + equationSymbol + Par2.ToString() +
                " = " + equationResult +
                gOF.secondPart[Random.Range(0, gOF.secondPart.Length)] +
                gOF.conclusion[Random.Range(0, gOF.conclusion.Length)];
        }
        else if (randomNumber == 0)
        {
            helpText.text = "Can you give me a paw? \n\n " +
                "This problem is a decimal operation \n\n 2 + 2,02 = 4,02 \n 2,3 - 2 = 0,3 \n" +
                "2 * 2,5 = 5 \n\n obs: result is always in this format: 0,00 (Two decimals) \n\n"+guideToChangeProblemStr;

            float Par1 = Random.Range(30f, 500f);
            float Par2;
            while (true)
            {
                float secPar = Random.Range(30f, 500f);
                if (secPar < Par1)
                {
                    Par2 = secPar;
                    break;
                }
            }
            Par1 = float.Parse(Par1.ToString("n2"));
            Par2 = float.Parse(Par2.ToString("n2"));

            correctAnswer = (Par1 - Par2).ToString("n2");

            questionText.text = catName +
                gOF.firstPart[Random.Range(0, gOF.firstPart.Length)] +
                "$" + Par1 +
                gOF.secondPart[Random.Range(0, gOF.secondPart.Length)] +
                "$" + Par2 +
                gOF.conclusion[Random.Range(0, gOF.conclusion.Length)];
        }
        else if (randomNumber == 2)
        {
            helpText.text = "I guess you dont need a paw now :(";

            int Par1 = Random.Range(30, 500);
            int Par2;
            while (true)
            {
                int secPar = Random.Range(30, 500);
                if (secPar < Par1)
                {
                    Par2 = secPar;
                    break;
                }
            }
            Par1 = int.Parse(Par1.ToString());
            Par2 = int.Parse(Par2.ToString());

            correctAnswer = (Par1 - Par2).ToString();

            questionText.text = catName +
                gOF.firstPart[Random.Range(0, gOF.firstPart.Length)] +
                Par1 +
                gOF.secondPart[Random.Range(0, gOF.secondPart.Length)] +
                Par2 +
                gOF.conclusion[Random.Range(0, gOF.conclusion.Length)];
        }
        else if (randomNumber == 3)
        {
            helpText.text = "Can you give me a paw? \n\n " +
                "This is a speed formula problem \n\n distanceKM / ((mins / 60) + hours) \n 10 / ((30 / 60) + 2)" +
                " \n" +
                "10 / (0,5 + 2) \n 10 / 2,5 \n velocity = 4 \n\n" +
                " obs: result is always in this format: 0,00 (Two decimals) \n\n"+guideToChangeProblemStr;

            float Par1 = Random.Range(1, 6); // Hours
            int[] listOfMins = { 3, 6, 9, 15, 30, 45, 60 };
            float Par2 = listOfMins[Random.Range(0, listOfMins.Length - 1)]; //Mins
            int[] listOfDistance = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
            float Par3 = listOfDistance[Random.Range(0, listOfDistance.Length - 1)]; // Distance

            correctAnswer = (Par3 / ((Par2 / 60) + Par1)).ToString("n2");

            questionText.text = catName +
                gOF.firstPart[Random.Range(0, gOF.firstPart.Length)] +
                "(Hours: " + Par1 + ") (Mins: " + Par2 + ") (Distance: " + Par3 + ")" +
                gOF.secondPart[Random.Range(0, gOF.secondPart.Length)] +
                gOF.conclusion[Random.Range(0, gOF.conclusion.Length)];
        }
    }
}
