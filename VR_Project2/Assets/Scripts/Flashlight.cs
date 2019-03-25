using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public int batteryLevel;                   //level of charge in battery
    public GameObject bar;                     //battery indicator bar
    public GameObject block1, block2, block3;  //blocks for each charge level
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

    }

    // Update is called once per frame
    void Update()
    {
        lt.spotAngle = lightRadius;     //set spot light angle to specified value

       if (Input.GetKeyDown(KeyCode.Mouse0))    //turn on/off light when button is pressed
        {

            if (lt.enabled == false && batteryLevel > 0)
            {
                lt.enabled = true;
            }

            else
            {
                lt.enabled = false;
            }
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
            rend1.material.color = new Color(200, 0, 0);  //set last block to red
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
}
