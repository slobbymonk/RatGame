using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Grabbing : MonoBehaviour
{
    //THE PLAN
    //Shoot lasers out of our eyes
    //We check if lasers hit object tag
    //We set target to object position

    private Camera mainCamera;

    private Vector3 startIKTargetPosition;
    [SerializeField] private Transform ikTarget;
    public Transform targetObject;

    public bool isTryingToHoldObject, isHoldingObject;
    [SerializeField] private Transform holdingTarget;

    [SerializeField] KeyCode grabKey;

    public HandGrab handGrab;

    private WaterSpray _waterSpray;

    private AudioManager _audioManager;

    [SerializeField] LayerMask grabbableLayer;

    private void Start()
    {

        //Get the camera
        mainCamera = Camera.main;
        //Save IK transform position for going back to resting position
        startIKTargetPosition = ikTarget.localPosition;
        //Listen to hand for when target has been held
        handGrab.GrabEvent += GrabTarget;

        _waterSpray = FindObjectOfType<WaterSpray>();

        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    void InputHandler()
    {
        if (Input.GetKey(grabKey) && !_waterSpray._isEquiped)
        {
            isTryingToHoldObject = true;
        }
        else
        {
            isTryingToHoldObject = false;
        }
    }
    public void Update()
    {
        InputHandler();
        if (isTryingToHoldObject && targetObject != null)
        {
            HoldTargetObject();
        }
        else if(targetObject != null && isHoldingObject)
        {
            //Drops object & nullifies target
             DropObject();
        }
        else
        {
            targetObject = null;
        }
        if (isHoldingObject && targetObject != null)
        {
            //Debug.Log("DDDD");
            targetObject.eulerAngles = new Vector3(targetObject.eulerAngles.x, transform.root.GetChild(0).eulerAngles.y + 180, targetObject.eulerAngles.z);
            targetObject.position = holdingTarget.position;
        }
    }
    void DropObject()
    {
        ikTarget.localPosition = startIKTargetPosition;
        targetObject.position = holdingTarget.position;
        targetObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        targetObject.GetComponent<Collider> ().isTrigger = false;
        targetObject.GetComponent<Rigidbody> ().isKinematic = false;
        targetObject = null;
        isHoldingObject = false;
    }
    private void GrabTarget(object sender, EventArgs e)
    {
        if (targetObject == null)
        {
            if(_audioManager != null)
                _audioManager.Play("PickUpItem");
            targetObject = handGrab.grabTargetObject;
        }

    }
    private void HoldTargetObject()
    {
        targetObject.GetComponent<Collider>().isTrigger = true;
        targetObject.GetComponent<Rigidbody>().isKinematic = true;
        targetObject.transform.parent = null;
        isHoldingObject = true;
        ikTarget.position = holdingTarget.position;
        targetObject.position = holdingTarget.position;
    }
}
