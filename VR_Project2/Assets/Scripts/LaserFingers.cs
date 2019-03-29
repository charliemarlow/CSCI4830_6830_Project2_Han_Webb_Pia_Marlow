﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFingers : MonoBehaviour
{
    public bool isLeft;
    public bool surveyTime;

    public float maxLaserDistance;

    public Laser laser;

    // Start is called before the first frame update
    void Start()
    {
        surveyTime = false;
        
    }


    float getIndexTriggerState()
    {
        float indexTriggerState = 0.0f;
        if (isLeft)
        {
            indexTriggerState = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch);
        }
        else
        {
            indexTriggerState = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
        }
        return indexTriggerState;
    }


    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(new Ray(laser.transform.position, laser.transform.forward), out hit, maxLaserDistance)){
            laser.length = hit.distance;    // shortens the laser
            if (getIndexTriggerState() >= .5)
            {
                Debug.Log("Selected " + hit.collider.attachedRigidbody);
            }
        }
        else
        {
            laser.length = maxLaserDistance;
        }
        if (surveyTime)
        {
            float indexTriggerValue = getIndexTriggerState();
            laser.length = 5f;
        }
        
    }
}
