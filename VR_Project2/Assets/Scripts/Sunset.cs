using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunset : MonoBehaviour
{
    public float timeOffset; // amount of time, in seconds, to offset from sunrise
    private float time;  
    public Transform sun;    // directional lights transform
    public Light sunLight;  // sunlight
    private float intensity; // how intense the light is
    public Color fogday = Color.grey;  // color of fog during the day
    public Color fognight = Color.black;  // color of fog at night

    public float speed;   // speed of sunset 
    private float ambientIntensity;  // intensity of ambient light

    public float sunsetStart;
    public float sunsetEnd = 54000;
    public float totalNight = 58100;
    public float scaleDownFactor = 5000;
    private bool isDay = true;

    private Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = sun.rotation;
        StartCoroutine(sunset());
    }

    void setAmbientLighting(float ambientIntensity)
    {
        RenderSettings.reflectionIntensity = ambientIntensity;
        RenderSettings.ambientIntensity = ambientIntensity;
    }

    IEnumerator sunset()
    {

        while (isDay)
        {
            time += Time.deltaTime * speed;
            float offsetTime = time + timeOffset;

            if(offsetTime >= sunsetStart && offsetTime <= sunsetEnd) {
                // scale down ambient lighting during sunset
                ambientIntensity = (sunsetEnd - offsetTime) / scaleDownFactor;
                setAmbientLighting(ambientIntensity);
                sunLight.intensity = sunLight.intensity = (totalNight - offsetTime) / (scaleDownFactor *2);
            }
            else if(offsetTime >= sunsetEnd) {
                // turn off ambient lighting
                setAmbientLighting(0.0f);
            }

            // set up variables for controlling sun rotation/fog settings
            float secondsInADay = 60 * 60 * 24;
            float halfDay = secondsInADay / 2;
            float quarterDay = halfDay / 2;

            // rotate sun and calculate intensity
            if (offsetTime <= totalNight)
            {
                sun.rotation = Quaternion.Euler(new Vector3((offsetTime - quarterDay) / 64000 * 360, 0, 0));
            }
            intensity = 1 - ((halfDay - offsetTime) / halfDay * -1);

            // set fog color
            RenderSettings.fogColor = Color.Lerp(fognight, fogday, intensity * intensity);

            if (offsetTime <= sunsetStart)
            {
                sunLight.intensity = intensity;
            }

            // turn off sun, exit coroutine
            if(offsetTime >= totalNight)
            {
                Debug.Log("Lights off!");
                sunLight.gameObject.SetActive(false);
                isDay = false;
            }

            yield return null;
        }
        yield return null;
    }

    public void turnOnLight()
    {
        isDay = false;
        sunLight.gameObject.SetActive(true);
        sun.rotation = originalRotation;
        setAmbientLighting(1);
        sunLight.intensity = 1;

    }

    // Update is called once per frame
    void Update()
    {
    }
}
