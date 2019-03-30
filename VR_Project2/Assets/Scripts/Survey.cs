using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survey : MonoBehaviour
{

    public TextMesh question;
    public TextMesh nextButton;
    public bool isFinished;

    private int questionIndex;

    private string[] questionArray = {
        "How present did you feel?",
        "How intense was the experience?",
        "Rate your reliance\non the flash light",
        "How much darkness\ndid you experience?",
        "How much anxiety\ndid you feel before this?",
        "How much anxiety\ndo you feel now?"
    };

    // Start is called before the first frame update
    void Start()
    {
        isFinished = false;
        questionIndex = 0;
        nextQuestion();
    }

    public void nextQuestion() 
    {
        question.text = questionArray[questionIndex];
        questionIndex++;
        Debug.Log("question index == " + questionIndex);
        Debug.Log("question array Length " + questionArray.Length);
        if(questionIndex == questionArray.Length - 1)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
