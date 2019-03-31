using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public Rigidbody rb;
    public Transform holder;
    private Vector3 positionHolder;
    private Quaternion rotationOffset;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(holder != null)
        {
            this.transform.position = holder.localToWorldMatrix.MultiplyPoint(positionHolder);
            this.transform.rotation = holder.transform.rotation;
        }
    }

    public void pickedUp(Transform t)
    {
        positionHolder = t.worldToLocalMatrix.MultiplyPoint(this.transform.position);
        rb.isKinematic = true;
        holder = t;
    }

    public void released(Transform t)
    {
        if(t == holder)
        {
            rb.isKinematic = false;
            holder = null;
        }
    }
}
