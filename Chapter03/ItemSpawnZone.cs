using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnZone : MonoBehaviour
{
    [SerializeField, Tooltip("Prefab to spawn in this zone.")]
    private GameObject _itemToSpawn;

    [SerializeField, Tooltip("Number of items to spawn.")]
    private float _itemCount = 30;

    [SerializeField, Tooltip("The area to spawn these items.")]
    private BoxCollider _spawnZone;

    void Start()
    {
        // spawn the items within this area
        for (int i = 0; i < _itemCount; i++)
        {
            SpawnItemAtRandomPosition();
        }
    }

    void SpawnItemAtRandomPosition()
    {
        Vector3 randomPos;

        // randomize location based on the size of the associated BoxCollider
        randomPos.x = Random.Range(_spawnZone.bounds.min.x,
                _spawnZone.bounds.max.x);

        randomPos.y = Random.Range(_spawnZone.bounds.min.y,
       _spawnZone.bounds.max.y);

        randomPos.z = Random.Range(_spawnZone.bounds.min.z, _spawnZone.bounds.max.z);

        // spawn the item prefab at this position
        Instantiate(_itemToSpawn, randomPos, Quaternion.identity);
    }
}
