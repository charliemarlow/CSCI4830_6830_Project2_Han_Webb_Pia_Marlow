﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Animator anim;
    private List<DoorScript> doors = new List<DoorScript>();
    public bool doorLockState;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        doors.Add(GetComponent<DoorScript>());
    }
    
    void Update()
    {
        if (doorLockState == true)
        {
            name = "LockedDoor";
        } else
        {
            name = "Door";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Player") || other.name.Equals("Key"))
        {
            Debug.Log(other.name);
            if (doorLockState == true)
            {
                if (other.name.Equals("Key"))
                {
                    Debug.Log("key used");
                    anim.SetTrigger("Open");
                }
            }
            else
            {
                Debug.Log("door was not locked, entering");
                anim.SetTrigger("Open");
            }
        } else
        {
            Debug.Log("Not a player");
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("exit");
    }

    void pauseAnimationEvent()
    {
        anim.enabled = false;
    }


    public bool GetLockState
    {
        get { return doorLockState; }
        set { doorLockState = value; }
    }

    public void ChangeLockState(bool other)
    {
        doorLockState = other;
    }
}
