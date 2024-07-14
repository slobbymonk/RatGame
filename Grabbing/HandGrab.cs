using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandGrab : MonoBehaviour
{
    public event EventHandler GrabEvent;
    public Transform grabTargetObject;
    [SerializeField] LayerMask grabbableLayer;

    private void OnTriggerStay(Collider other)
    {
        if((grabbableLayer & (1 << other.gameObject.layer)) != 0)
        {
            if(grabTargetObject == null || Vector3.Distance(grabTargetObject.position, transform.position) >
                Vector3.Distance(other.transform.position, transform.position))
            {
                grabTargetObject = other.transform;
            }
            GrabEvent?.Invoke(this, EventArgs.Empty);
        }
    }
    
}
