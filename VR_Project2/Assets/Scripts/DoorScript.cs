using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Animator anim;
    private List<DoorScript> doors = new List<DoorScript>();
    public Collider right;
    public Collider left;
    public Collider key;
    public bool doorLockState;
    public GameManager gm;
    private bool doorWasLocked;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        doors.Add(GetComponent<DoorScript>());
        if (doorLockState == true) {
            name = "LockedDoor";
        }
        else {
            name = "Door";
        }
        doorWasLocked = doorLockState;
    }
    
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (name == "Door") {
            if (other == right || other == left || other == key) {
                Debug.Log("key used");
                anim.SetTrigger("Open");
                if (doorWasLocked)
                {
                    gm.instantiateNewSurvey();
                }
            }
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
