using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutable : MonoBehaviour
{
    public GameObject cutVersion;

    public void CutObject()
    {
        Instantiate(cutVersion, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
