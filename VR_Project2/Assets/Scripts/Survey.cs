using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survey : MonoBehaviour
{

    public TextMesh question;
    public TextMesh nextButton;
    public bool isFinished;

    private int questionIndex;
    private bool isFirst;


    private string[] questionArray = {
        "How present\ndid you feel?",
        "How intense\nwas the experience?",
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
        isFirst = true;
        nextQuestion();
    }

    public void nextQuestion()
    {
        if (questionIndex < questionArray.Length)
        {
            question.text = questionArray[questionIndex];
        }
        questionIndex++;
        Debug.Log("question index == " + questionIndex);
        if (questionIndex >= questionArray.Length)
        {
            Destroy(this.gameObject);
        }
    }

    public void logResult(RaycastHit hit, int surveyNumber)
    {
        string tag = hit.collider.tag;
        string forFile = "Question #" + (questionIndex) + " " + Environment.NewLine +
            questionArray[questionIndex - 1].Replace("\n", " ") + 
            Environment.NewLine+  "Result: " + tag + Environment.NewLine + Environment.NewLine;
        if (questionIndex == 1)
        {
            forFile = "Survey Number " + surveyNumber + Environment.NewLine + forFile;
        }

        string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string finalDestination = filePath + @"\darkRoomResults.txt";
        System.IO.File.AppendAllText(finalDestination, forFile);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
