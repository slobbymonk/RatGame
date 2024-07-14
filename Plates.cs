using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plates : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plate"))
        {
            transform.parent = collision.transform;
            GetComponent<Collider>().isTrigger = true;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
