using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatManager : MonoBehaviour
{
    [SerializeField] private RatSpawner[] _ratSpawners;

    [SerializeField] private Vector2 _minMaxSpawnDelay;
    [HideInInspector] public float _currentSpawnDelay;

    private void Update()
    {
        if (CanSpawnARat())
        {
            _ratSpawners[RandomRatSpawner()].SpawnRat(transform);

            SetSpawnDelay();
        }
        else
        {
            _currentSpawnDelay -= Time.deltaTime;
        }
    }
    int RandomRatSpawner()
    {
        return Random.Range(0, _ratSpawners.Length);
    }
    private bool CanSpawnARat()
    {
        if (_currentSpawnDelay <= 0)
            return true;
        else
            return false;
    }

    float SetSpawnDelay()
    {
        return _currentSpawnDelay = Random.Range(_minMaxSpawnDelay.x, _minMaxSpawnDelay.y);
    }
}
