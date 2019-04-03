using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public AudioSource sound;
    public OVRGrabber left;
    public OVRGrabber right;
    public OVRGrabbable grabbableFlashlight;

    public float batteryTime;                  //time for each battery level
    public int batteryLevel;                   //level of charge in battery
    public GameObject bar;                     //battery indicator bar
    public GameObject block1, block2, block3;  //blocks for each charge level
    public GameObject fakeLight;               // fake cylinder that gives appearance of a lit light
    public GameObject lightBeam;               //spot light object
    public float lightRadius;                  //radius of spot light
    private Color batteryGreen;                //full battery color
    private Renderer rend1, rend2, rend3;      //renderers for each battery block
    private Light lt;                          //spot light component

    public bool isLit;
    public float intensity;
    public float lightRange;
    private float lastTimeOn = 0;
    private int numberOfTimesUsed;
    private float timeSinceLastUse;
    private float totalTimeUsed;
    private bool isFirstTimeOn = true;
    // Start is called before the first frame update
    void Start()
    {
        numberOfTimesUsed = 0;
        rend1 = block1.GetComponent<Renderer>();
        rend2 = block2.GetComponent<Renderer>();
        rend3 = block3.GetComponent<Renderer>();
        lt = lightBeam.GetComponent<Light>();
        batteryGreen = rend3.material.color;
        lightBeam.GetComponent<Light>().enabled = false;    //initialize light to off
        toggleFlashlight(false);


    }

    public int getNumberOfTimesUsed() {
        return numberOfTimesUsed;
    }

    public float getTotalTimeUsed()
    {
        if (isLit)
        {
            totalTimeUsed += (Time.time - timeSinceLastUse);
        }
        return totalTimeUsed;
    }

    public void toggleFlashlight(bool on)
    {
        if (on) {
            numberOfTimesUsed++;
            timeSinceLastUse = Time.time;
            Debug.Log("number of times used: " + numberOfTimesUsed);
        }
        else
        {
            // current time - time it was last turned on = time on
            totalTimeUsed += (Time.time - timeSinceLastUse);
            Debug.Log("Total time used: " + totalTimeUsed);
        }
        lt.enabled = on;
        fakeLight.SetActive(on);
        isLit = on;

    }

    float getIndexTriggerState(bool isLeft) {
        float indexTriggerState = 0.0f;
        if (isLeft) {
            indexTriggerState = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch);
        }
        else {
            indexTriggerState = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
        }
        return indexTriggerState;
    }

    bool isLeftController()
    {
        bool isLeft;

        if (grabbableFlashlight.grabbedBy == left)
        {
            isLeft = true;
        }

        else
        {
            isLeft = false;
        }
        return isLeft;
    }

    void setBatteryRender(bool rend3Enable, bool rend2Enable, bool rend1Enable)
    {
        rend3.enabled = rend3Enable;
        rend2.enabled = rend2Enable;
        rend1.enabled = rend1Enable;
    }

    void updateBatteryLevel()
    {
        if (batteryLevel >= 3)       //full battery level
        {
            rend1.material.color = batteryGreen;
            setBatteryRender(true, true, true);
        }

        else if (batteryLevel == 2)  //2 bars of battery
        {
            setBatteryRender(false, true, true);
        }

        else if (batteryLevel == 1)   //1 bar of battery
        {
            setBatteryRender(false, false, true);
            rend1.material.color = new Color(150, 0, 0);  //set last block to red
        }
        else if (batteryLevel <= 0) //turn off light if batteryLevel reaches zero
        {
            batteryLevel = 0;
            setBatteryRender(false, false, false);
            toggleFlashlight(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        lt.intensity = intensity;
        lt.spotAngle = lightRadius;     //set spot light angle to specified value
        lt.range = lightRange;


        bool isLeft = isLeftController();
        float indexTriggerState = getIndexTriggerState(isLeft);
        bool isGrabbed = grabbableFlashlight.grabbedBy == left || grabbableFlashlight.grabbedBy == right;

       if (indexTriggerState > .5  && Time.time - lastTimeOn > 1 && isGrabbed)    //turn on/off light when button is pressed
        {   
            if (lt.enabled == false && batteryLevel > 0)
            {
                // turn light on
                toggleFlashlight(true);
            }

            else
            {
                // turn light off
                toggleFlashlight(false);
            }
            lastTimeOn = Time.time;
        }

        if (isLit)
        {
            float timeOn = Time.time - timeSinceLastUse ;
            if(timeOn > batteryTime)
            {
                batteryLevel--;
                totalTimeUsed += (Time.time - timeSinceLastUse);
                timeSinceLastUse = Time.time;

            }
        }

        updateBatteryLevel();
       
    }

    private void OnCollisionEnter(Collision collision)  //function to replace batteries
    {
        if(collision.rigidbody == null)
        {
            return;
        }

        Rigidbody rb = collision.rigidbody;
        Battery b = rb.GetComponent<Battery>();

        if (b == null)
        {
            Debug.Log("Hit by " + rb.name);
            return;
        }

        sound.Play();
        Debug.Log("Batteries replaced.");
        batteryLevel = 3;
    }

}
