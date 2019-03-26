using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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

        Destroy(this.gameObject);
    }
}
