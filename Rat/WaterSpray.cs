using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaterSpray : MonoBehaviour
{
    [SerializeField] private ParticleSystem _sprayParticle;

    [SerializeField] private float _sprayRadius, _sprayDistance;

    public PlayerInput playerInput;

    public bool _isEquiped;

    [SerializeField] private GameObject _bottleMesh;

    public LayerMask _ratLayer;

    public AudioClip[] _spraySound;

    private AudioSource _audioSource;

    private void Awake()
    {
        playerInput = new PlayerInput();
        _audioSource= GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Action.Spray.performed += OnSpray;
        playerInput.Action.Equip.performed += OnEquip;
    }

    private void OnDisable()
    {
        playerInput.Disable();
        playerInput.Action.Spray.performed -= OnSpray;
        playerInput.Action.Equip.performed -= OnEquip;
    }
    private void OnEquip(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_isEquiped)
        {
            _bottleMesh.SetActive(false);
            _isEquiped = false;
        }
        else
        {
            _bottleMesh.SetActive(true);
            _isEquiped = true;
        }
    }
    private void OnSpray(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_isEquiped)
        {
            _sprayParticle.Play();
            _audioSource.clip = _spraySound[GetRandomSound(_spraySound.Length)];
            _audioSource.Play();

            RaycastHit hit;
            if (Physics.SphereCast(Camera.main.transform.position, _sprayRadius, Camera.main.transform.forward, out hit, _sprayDistance, _ratLayer))
            {
                hit.transform.gameObject.GetComponent<RatBehaviour>().SprayRat();
            }
        }
    }
    int GetRandomSound(int soundListLength)
    {
        return Random.Range(0, soundListLength);
    }
}
