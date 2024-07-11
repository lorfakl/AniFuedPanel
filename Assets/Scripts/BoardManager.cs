using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utilities;
using Utilities.Events;

//needs question text ref
//transform ref to parent of answers
//send out next question event when this event occurs the answer cubes re-render their text and spin arounnd 
//board manager changes the question text

public class BoardManager : MonoBehaviour
{
    private HashSet<int> usedIndices;
    private System.Random random;
    private List<int> correctAnswers;
    private List<int> remainingAnswers;
    private int revealIndex;

    [SerializeField]
    GameEvent nextQuestion;

    [SerializeField]
    GameEvent numberKeyPressed;

    [SerializeField]
    TMP_Text questionText;

    [SerializeField]
    Transform answerCubeGroupParent;

    [SerializeField]
    GameObject answerCubePrefab;

    public void HandleStartGameEvent()
    {
        //this means the start game button was pressed and we need to instaniate 6 answer cubes
        Questionnaire firstQuestion = JsonParser.Questionnaire[0];
        usedIndices.Add(0);
        questionText.text = firstQuestion.Question;
        foreach(int i in remainingAnswers)
        {
            CreateAnswerCube createAnswerCube = new CreateAnswerCube { answer = firstQuestion.Top8[i], order = i };
            CreateCube(createAnswerCube, answerCubePrefab, answerCubeGroupParent.position, answerCubeGroupParent);
        }
    }

    public void NumkeyPressedHandler(object numKey)
    {
        int numberPressed = (int)numKey;
        HelperFunctions.Log($"Answer Cube at order: {numberPressed} has been answered add that to the list");
        AddCorrectAnswer(numberPressed);
    }

    public static AnswerCubeController CreateCube(CreateAnswerCube c, GameObject cubePrefab, Vector3 location, Transform parent)
    {
        GameObject answerCubeGo = Instantiate(cubePrefab, location, Quaternion.identity, parent);
        AnswerCubeController cubeInstance = answerCubeGo.GetComponent<AnswerCubeController>();
        cubeInstance.Initialize(c);
        return cubeInstance;
    }

    // Start is called before the first frame update
    void Start()
    {
        usedIndices = new HashSet<int>();
        random = new System.Random();
        correctAnswers = new List<int>();
        remainingAnswers = new List<int> { 1, 2, 3, 4, 5, 6 };
        revealIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the right or left arrow key is pressed
        if (Input.GetKeyUp(KeyCode.Space))
        {
            NextQuestion();
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            numberKeyPressed.Raise(RevealAnswer());
        }
    }

    void NextQuestion()
    {
        if (usedIndices.Count < JsonParser.Questionnaire.Count)
        {
            int randomIndex;
            do
            {
                randomIndex = random.Next(JsonParser.Questionnaire.Count);
            } while (usedIndices.Contains(randomIndex));

            usedIndices.Add(randomIndex);

            Debug.Log("Selected index: " + JsonParser.Questionnaire[randomIndex].Question);
            nextQuestion.Raise(JsonParser.Questionnaire[randomIndex]);
            HelperFunctions.Log("Questionare obj sent out on Raise Next Question event");
        }
        else
        {
            Debug.Log("All indices have been used.");
        }
    }

    public void AddCorrectAnswer(int answer)
    {
        if (answer < 1 || answer > 6)
        {
            Debug.LogError("Answer must be between 1 and 6.");
            return;
        }

        if (!correctAnswers.Contains(answer))
        {
            correctAnswers.Add(answer);
            remainingAnswers.Remove(answer);
        }
    }

    // Function to reveal remaining answers sequentially
    public int RevealAnswer()
    {
        if (revealIndex < remainingAnswers.Count)
        {
            int answerToReveal = remainingAnswers[revealIndex];
            revealIndex++;
            return answerToReveal;
        }
        else
        {
            return -1;
        }
    }
}
