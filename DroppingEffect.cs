using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DroppingEffect : MonoBehaviour
{
    public VisualEffect _particleEffect;
    public float _treshold;

    public SquachAndStretch _sas;

    private void OnCollisionEnter(Collision collision)
    {
        if (GetComponent<Rigidbody>().velocity.magnitude > _treshold)
        {
            var smoke = Instantiate(_particleEffect, transform.position, Quaternion.identity);
            smoke.Play();
            _sas.PlaySquachAndStretch();
        }
    }
}
