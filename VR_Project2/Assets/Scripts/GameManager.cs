using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{

    public Survey surveyPrefab;
    public Transform playerLocation;
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

    private int numberOfSurveys = 0;
    public Transform survey1;
    public Transform survey2;
    public Transform survey3;
    public Transform[] surveyZones = new Transform[3];


    // Start is called before the first frame update
    void Start()
    {
        surveyZones[0] = survey1;
        surveyZones[1] = survey2;
        surveyZones[2] = survey3;

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

    public void instantiateNewSurvey()
    {
        if(surveyInUse)
        {
            return;
        }
        if (numberOfSurveys < surveyZones.Length)
        {
            currentSurvey = Instantiate(surveyPrefab, surveyZones[numberOfSurveys]);
        }
        numberOfSurveys++;
        Debug.Log("number of surveys: " + numberOfSurveys);
        //currentSurvey = Instantiate(surveyPrefab, surveyZone.position + (surveyZone.forward * 2),  surveyZone.rotation);
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

        forFile += "Amount of time the flashlight was used: " + totalTimeUsed + " seconds";
        forFile += Environment.NewLine;

        forFile += "Percent of time the flashlight was used: " + percentOfTimeWithFlashlight + "%";
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
        */
        if(currentSurvey == null && surveyInUse)
        {
            timeSinceLastSurvey = Time.time;
            toggleSurvey(false);
        }
      

    }
}
