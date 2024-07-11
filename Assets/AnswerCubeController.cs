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
    
    

    

    public void NumkeyPressedHandler(object numKey)
    {
        int numberPressed = (int)numKey;
        HelperFunctions.Log($"The Number key was pressed: {numberPressed}");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(CreateAnswerCube c)
    {
        orderNumber = c.order;
        orderNumberText.text = orderNumber.ToString();
        answer = c.answer;
        answerText.text = answer.Value;
        PointScore = answer.Occurrence;

    }
}
