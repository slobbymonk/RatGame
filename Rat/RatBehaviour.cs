using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class RatBehaviour : MonoBehaviour
{
    public NavMeshAgent _agent;

    private Transform _target;

    private Vector3 _spawnPosition;

    [SerializeField] private Transform _holdingPosition;

    public AudioClip[] _grabbingSomethingSound;

    private AudioSource _audioSource;

    private enum States
    {
        GoingToFood,
        RunningWithFood,
        Scared
    }
    private States _state;

    private Animator _anim;

    private float _waitASecond;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        _spawnPosition = transform.position;
        FindTargetFood();
        _agent.SetDestination(_target.position);
        _anim = GetComponent<Animator>();

        _anim.SetTrigger("IsSneaking");
    }

    private void Update()
    {
        switch (_state)
        {
            case States.GoingToFood:
                if (IsAtTarget(_target.position))
                    PickUpFood();
                _waitASecond = 0;
                break;
            case States.RunningWithFood:
                _agent.SetDestination(_spawnPosition);
                if(_waitASecond > 1)
                {
                    if (IsAtTarget(_spawnPosition))
                        Unalive();
                    _waitASecond= 0;
                }
                else
                {
                    _waitASecond += Time.deltaTime;
                }
                //if (IsAtTarget(_spawnPosition))
                break;
            case States.Scared:
                if (IsAtTarget(_spawnPosition))
                    Unalive();
                break;
        }
    }
    int GetRandomSound(int soundListLength)
    {
        return Random.Range(0, soundListLength);
    }
    void PickUpFood()
    {
        _state= States.RunningWithFood;

        _anim.SetTrigger("HasFood");

        _audioSource.clip = _grabbingSomethingSound[GetRandomSound(_grabbingSomethingSound.Length)];
        _audioSource.Play();

        _target.parent = _holdingPosition;
        _target.localPosition = Vector3.zero;
        _target.GetComponent<Collider>().isTrigger = true;
        _target.GetComponent<Rigidbody>().isKinematic = true;
    }
    void DropFood()
    {
        _target.transform.parent = null;
        _target.GetComponent<Collider>().isTrigger = false;
        _target.GetComponent<Rigidbody>().isKinematic = false;
    }
    bool IsAtTarget(Vector3 target)
    {
        _agent.SetDestination(target);
        if (_agent.remainingDistance <= _agent.stoppingDistance)
            return true;
        else
            return false;
    }
    void FindTargetFood()
    {
        var allFood = GameObject.FindGameObjectsWithTag("Food");
        _target = allFood[Random.Range(0, allFood.Length)].transform;
    }

    void Unalive()
    {
        Destroy(gameObject);
    }
    public void SprayRat()
    {
        _state = States.Scared;
        _anim.SetTrigger("IsRunning");
        DropFood();
    }
}
