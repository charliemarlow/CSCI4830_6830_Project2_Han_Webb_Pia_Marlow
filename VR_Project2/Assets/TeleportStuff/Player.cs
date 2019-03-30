using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public VRTeleporter teleporter;
    public OVRInput.Controller myController;

    void Update()
    {

        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick, myController)) 
        {
            teleporter.ToggleDisplay(true);
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstick, myController))
        {
            teleporter.Teleport();
            teleporter.ToggleDisplay(false);
        }
    }
}
