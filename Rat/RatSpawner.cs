using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _ratPrefab;

    public void SpawnRat(Transform parent)
    {
        Instantiate(_ratPrefab, transform.position, Quaternion.identity, parent);
    }
}
