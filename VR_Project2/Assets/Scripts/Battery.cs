using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{

    private int batteriesUsed;
    public GameManager gm;

    public int getBatteriesUsed()
    {
        return batteriesUsed;
    }
    // Start is called before the first frame update
    void Start()
    {
        batteriesUsed = 0;
        
    }

    // Update is called once per frame
    void Update()
    {

    }

 

    private void OnCollisionEnter(Collision collision) //function to replace batteries
    {
        if (collision.rigidbody == null)
        {
            return;
        }

        Rigidbody rb = collision.rigidbody;
        Flashlight f = rb.GetComponent<Flashlight>();

        if (f == null)
        {
            Debug.Log("Hit By " + rb.name);
            return;
        }
        gm.incrementBatteries();
        Destroy(this.gameObject);
    }
}
