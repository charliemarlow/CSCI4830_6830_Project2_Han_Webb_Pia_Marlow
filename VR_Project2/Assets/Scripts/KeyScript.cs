using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("found");
        if (collision.name.Equals("LockedDoor"))
        {
            Debug.Log("used key!");
            Destroy(gameObject);
        }
    }
}
