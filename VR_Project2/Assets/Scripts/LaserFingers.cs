using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFingers : MonoBehaviour
{
    public bool isLeft;
    public bool surveyTime;

    public float maxLaserDistance;

    public GameManager gm;

    public Laser laser;

    private string[] tagArray = { "1", "2", "3", "4", "5" };
    private Renderer lastColored;

    // Start is called before the first frame update
    void Start()
    {
        surveyTime = false;
        lastColored = null;
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
        // only use laser fingers when taking a survey
        if (surveyTime)
        {
            laser.gameObject.SetActive(true);
            RaycastHit hit;
            if (Physics.Raycast(new Ray(laser.transform.position, laser.transform.forward), out hit, maxLaserDistance))
            {
                laser.length = hit.distance;    // shortens the laser

                // use index trigger to select an object
                if (getIndexTriggerState() >= .5)
                {
                    for (int i = 0; i < tagArray.Length; i++)
                    {
                        if (hit.collider.CompareTag(tagArray[i]))
                        {
                            Debug.Log("Selected Number " + tagArray[i]);
                            gm.changeColor(hit);
                            break;
                        }else if (hit.collider.CompareTag("next"))
                        {
                            gm.nextQuestion();
                        }
                    }//for
                }
            }
            else
            {
                laser.length = maxLaserDistance;
            }
        }
        else
        {
            laser.gameObject.SetActive(false);
        }
        
    }
}
