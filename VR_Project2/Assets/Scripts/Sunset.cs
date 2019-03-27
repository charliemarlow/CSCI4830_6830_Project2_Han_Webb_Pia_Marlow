using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunset : MonoBehaviour
{
    public float timeOffset;
    public float time;
    public Transform sun;
    public Light sunLight;
    public float intensity;
    public Color fogday = Color.grey;
    public Color fognight = Color.black;

    public int speed;
    public int days;
    public float ambientLightMultiplier;
    public float ambientIntensity;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(sunset());
    }

    IEnumerator sunset()
    {
        while (true)
        {
            time += Time.deltaTime * speed;
            float offsetTime = time + timeOffset;
            Debug.Log("Do something");
            if(offsetTime >= 54000 && offsetTime <= 58400) {
                Debug.Log("Ambient light multiplier" + ambientLightMultiplier);
                ambientLightMultiplier +=  offsetTime /100000;
                ambientIntensity = 1 / ambientLightMultiplier;
                RenderSettings.reflectionIntensity = 1 / ambientLightMultiplier;
                Debug.Log("Ambient light multiplier" + ambientLightMultiplier);
                Debug.Log("Reflection intensity " + 1 / ambientLightMultiplier);
                RenderSettings.ambientIntensity = 1 / ambientLightMultiplier;
            }else if(offsetTime >= 58400 && ambientLightMultiplier > 0) {
                ambientIntensity -= 1 / Mathf.Log(offsetTime, 2) ;
                Debug.Log("Go black");
                RenderSettings.reflectionIntensity = ambientIntensity;
                RenderSettings.ambientIntensity = ambientIntensity;
            }else if(offsetTime >= 58400) {

                Debug.Log("Go black");
                RenderSettings.reflectionIntensity = 0;
                RenderSettings.ambientIntensity = 0;
            }

            float secondsInADay = 60 * 60 * 24;
            float halfDay = secondsInADay / 2;
            float quarterDay = halfDay / 2;
            /*
            if (time > secondsInADay)
            {
                days += 1;
                time = 0;
            }*/

            sun.rotation = Quaternion.Euler(new Vector3((offsetTime - quarterDay) / 64000 * 360, 0, 0));
            /*
            if (time < halfDay)
            {
                intensity = 1 - (halfDay - offsetTime) / halfDay;
            }
            else
            {
            */
                intensity = 1 - ((halfDay - offsetTime) / halfDay * -1);
            //}

            RenderSettings.fogColor = Color.Lerp(fognight, fogday, intensity * intensity);
            sunLight.intensity = intensity;
            yield return null;
        }
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
