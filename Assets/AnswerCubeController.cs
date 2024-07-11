using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Utilities;

public struct CreateAnswerCube
{
    public Answer answer;
    public int order;
}

public class AnswerCubeController : MonoBehaviour
{
    //reveal on num key press (NumPressedEvent)
    //reveal on arrow key press (via Reveal Manager)
    //when revealed play sound
    //when revealed rotate to reveal cube side (Tween)
    //able to be loaded with OrderNumberText
    //takes in a Answer object to set respondantCount text and answerText
    //built from a Board manager
    

    [SerializeField]
    AudioSource revealAudioSource;

    [SerializeField]
    TMP_Text respondantCountText;

    [SerializeField]
    TMP_Text answerText;

    [SerializeField]
    TMP_Text orderNumberText;

    private int orderNumber;
    private Answer answer;

    public int PointScore { get; private set; }



    private bool shouldDispalyAnswer = false;

    public void HandleNewQuestions(object question)
    {
        Questionnaire ques = HelperFunctions.CastObject<Questionnaire>(question);
        answer = ques.Top8[orderNumber];
        answerText.text = answer.ToString();
        PointScore = answer.Occurrence;
        respondantCountText.text = PointScore.ToString();
        Rotate(false);
    }
    public void NumkeyPressedHandler(object numKey)
    {
        int numberPressed = (int)numKey;
        HelperFunctions.Log($"The Number key was pressed: {numberPressed}");
        if(numberPressed == orderNumber)
        {
            Rotate(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        respondantCountText.enabled = false;
        answerText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldDispalyAnswer) 
        {
            respondantCountText.enabled = true;
            answerText.enabled = true;
            shouldDispalyAnswer = false;
        }
    }

    private void Rotate(bool isStandardReveal)
    {
        
        if(isStandardReveal)
        {
            shouldDispalyAnswer = true;
            transform.DORotate(new Vector3 { x = -90, y = 0, z = 0 }, 1.5f)
            .OnComplete(() => { PlayAudio(isStandardReveal); });
        }
        else
        {
            transform.DORotate(new Vector3 { x = 720, y = 0, z = 0 }, 1.5f)
            .OnComplete(() => { PlayAudio(isStandardReveal); });
        }
    }

    void PlayAudio(bool shouldPlayNormal)
    {
        if(shouldPlayNormal)
        {
            revealAudioSource.Play();
        }
        else
        {
            revealAudioSource.DOPlayBackwards();
        }
        
    }

    public void Initialize(CreateAnswerCube c)
    {
        orderNumber = c.order;
        orderNumberText.text = orderNumber.ToString();
        answer = c.answer;
        answerText.text = answer.Value;
        PointScore = answer.Occurrence;
        respondantCountText.text = PointScore.ToString();  

    }
}
