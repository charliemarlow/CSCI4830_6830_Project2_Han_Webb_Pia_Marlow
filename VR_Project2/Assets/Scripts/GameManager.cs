using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{

    public Survey surveyPrefab;
    public Transform playerLocation;
    public Transform surveyZone;
    public LaserFingers laserRight;
    public LaserFingers laserLeft;
    private bool surveyInUse;
    public Material laserColor;
    public Material unlitWhite;
    public Flashlight flashlight;

    private Survey currentSurvey;
    private Renderer lastColored;
    private float timeSinceLastSurvey;
    private RaycastHit lastHit;
    private int surveyNumber = 0;
    private string finalDestination;
    private int batteriesUsed;

    // Start is called before the first frame update
    void Start()
    {
        surveyInUse = false;
        lastColored = null;
        timeSinceLastSurvey = 0f;

        string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        finalDestination = filePath + @"\darkRoomResults.txt";
        if (File.Exists(finalDestination))
        {
            File.Delete(finalDestination);

        }
        File.Create(finalDestination);

    }

    void instantiateNewSurvey()
    {
        currentSurvey = Instantiate(surveyPrefab, surveyZone);
        toggleSurvey(true);
    }

    void toggleSurvey(bool on)
    {
        laserRight.surveyTime = on;
        laserLeft.surveyTime = on;
        surveyInUse = on;
        if (on)
        {
            surveyNumber++;
        }
    }

    public void changeColor(RaycastHit hit)
    {
        if (lastColored != null)
        {
            lastColored.material = unlitWhite;
        }
        GameObject cube = hit.collider.gameObject;
        Renderer render = cube.GetComponent<Renderer>();
        render.material = laserColor;
        lastColored = render;
        lastHit = hit;
    }

    public void nextQuestion()
    {
        if(lastColored == null)
        {
            return;
        }
 
        lastColored.material = unlitWhite;
        lastColored = null;
        currentSurvey.logResult(lastHit, surveyNumber);
        currentSurvey.nextQuestion();

    }

    public void incrementBatteries()
    {
        batteriesUsed++;
    }

    public void end()
    {
        string forFile = Environment.NewLine;
        forFile += "Number of times the flashlight was turned on: " + flashlight.getNumberOfTimesUsed();
        forFile += Environment.NewLine;

        float totalTimeUsed = flashlight.getTotalTimeUsed();
        float currentTime = Time.time;
        float percentOfTimeWithFlashlight = (totalTimeUsed / currentTime) * 100;

        forFile += "Amount of time the flashlight was used: " + totalTimeUsed;
        forFile += Environment.NewLine;

        forFile += "Percent of time the flashlight was used: " + percentOfTimeWithFlashlight;
        forFile += Environment.NewLine;

        forFile += "Number of batteries used: " + batteriesUsed;
        System.IO.File.AppendAllText(finalDestination, forFile);

    }
    // Update is called once per frame
    void Update()
    {
        /*
        if(Time.time - timeSinceLastSurvey >= 60 && !surveyInUse)
        {
            instantiateNewSurvey();
        }

        if(currentSurvey == null && surveyInUse)
        {
            timeSinceLastSurvey = Time.time;
            toggleSurvey(false);
        }
        */

    }
}
