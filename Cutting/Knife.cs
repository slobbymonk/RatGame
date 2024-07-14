using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public AudioSource _source;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Cutable>())
        {
            _source.Play();
            collision.gameObject.GetComponent<Cutable>().CutObject();
        }
    }
}
