using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public OVRGrabber left;
    public OVRGrabber right;
    public OVRGrabbable grabbableFlashlight;

    public int batteryLevel;                   //level of charge in battery
    public GameObject bar;                     //battery indicator bar
    public GameObject block1, block2, block3;  //blocks for each charge level
    public GameObject fakeLight;               // fake cylinder that gives appearance of a lit light
    public GameObject lightBeam;               //spot light object
    public float lightRadius;                  //radius of spot light
    private Color batteryGreen;                //full battery color
    private Renderer rend1, rend2, rend3;      //renderers for each battery block
    private Light lt;                          //spot light component

    // Start is called before the first frame update
    void Start()
    {
        rend1 = block1.GetComponent<Renderer>();
        rend2 = block2.GetComponent<Renderer>();
        rend3 = block3.GetComponent<Renderer>();
        lt = lightBeam.GetComponent<Light>();
        batteryGreen = rend3.material.color;
        lightBeam.GetComponent<Light>().enabled = false;    //initialize light to off
        fakeLight.SetActive(false);

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

    // Update is called once per frame
    void Update()
    {
        lt.spotAngle = lightRadius;     //set spot light angle to specified value

        bool isLeft;

        if (grabbableFlashlight.grabbedBy == left)
        {
            isLeft = true;
        }

        else {
            isLeft = false;
        }




        float lastTimeOn = 0.0f;
       float indexTriggerState = getIndexTriggerState(isLeft);
        bool isGrabbed = grabbableFlashlight.grabbedBy == left || grabbableFlashlight.grabbedBy == right;
       if (indexTriggerState > .5  && Time.time - lastTimeOn > 1 && isGrabbed)    //turn on/off light when button is pressed
        {
            Debug.Log("Trigger pulled");
            if (lt.enabled == false && batteryLevel > 0)
            {
                Debug.Log("Light enabled");
                lt.enabled = true;
                fakeLight.SetActive(true);
            }

            else
            {
                Debug.Log("Light not enabled");
                lt.enabled = false;
                fakeLight.SetActive(false);
            }
            lastTimeOn = Time.time;
        }


       if (batteryLevel >= 3)       //full battery level
        {
            rend1.material.color = batteryGreen;
            rend3.enabled = true;
            rend2.enabled = true;
            rend1.enabled = true;
        }

        else if(batteryLevel == 2)  //2 bars of battery
        {
            rend3.enabled = false;
            rend2.enabled = true;
            rend1.enabled = true;
        }

       else if(batteryLevel == 1)   //1 bar of battery
        {
            rend3.enabled = false;
            rend2.enabled = false;
            rend1.enabled = true;
            rend1.material.color = new Color(150, 0, 0);  //set last block to red
        }

        else if (batteryLevel <= 0) //turn off light if batteryLevel reaches zero
        {
            batteryLevel = 0;
            rend3.enabled = false;
            rend2.enabled = false;
            rend1.enabled = false;
            lt.enabled = false;
        }
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

        Debug.Log("Batteries replaced.");
        batteryLevel = 3;
    }
}
