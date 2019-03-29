using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TherapistInterface : MonoBehaviour
{

    public Flashlight flashlight;
    public Sunset sunset;

    public 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void rechargeFlashlight()
    {
        flashlight.batteryLevel = 3;
    }

    public void changeFlashlight()
    {
        flashlight.toggleFlashlight(!flashlight.isLit);
    }

    public void setFlashlightIntensity(float v)
    {
        flashlight.intensity = v;
    }

    public void setFlashlightScope(float v)
    {
        flashlight.lightRadius = v;
    }

    public void setFlashlightRange(float v) {
        flashlight.lightRange = v;
    }

    public void setSunlightSpeed(float v)
    {
        sunset.speed = v;
    }
}
