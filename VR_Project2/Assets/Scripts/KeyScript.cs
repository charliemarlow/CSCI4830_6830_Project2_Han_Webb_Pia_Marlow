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
        if (collision.name.Equals("LockedDoor"))
        {
            collision.name = "Door";
            Debug.Log("used key!");
            Destroy(gameObject);
        }
    }
}
