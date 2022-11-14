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

    // --- added for chapter 5 --
    [SerializeField, Tooltip("How should these objects be organized when spawned?")]
    private SpawnShape _spawnShape;
    private enum SpawnShape
    {
        Random,
        Circle,
        Grid,
        Count
    }

    [SerializeField, Tooltip("Speed that this group of objects will rotate.")]
    private Vector3 _rotationSpeed;

    void Start()
    {
        // instanciate the objects according to spawn shape
        if (_spawnShape == SpawnShape.Circle)
        {
            SpawnObjectsInCircle();
        }
        else
        {
            for (int i = 0; i < _itemCount; i++)
                SpawnItemAtRandomPosition();
        }

        getMinCodeEntryTime(3, 3, new int[] { 1, 2, 3 });
    }


    public long getMinCodeEntryTime(int N, int M, int[] C)
    {

        long secondsOut = calcSecondsFromAtoB(1, C[1], N);

        for (int i = 0; i < M - 1; i++)
        {
            secondsOut += calcSecondsFromAtoB(C[i], C[i + 1], N);
        }

        Debug.Log("Total Seconds = " + secondsOut);

        return secondsOut;
    }

    private long calcSecondsFromAtoB(int A, int B, int N)
    {
        Debug.Log( "Find the seconds to spin from " + A + " to " + B );

        if (A == B)
            return 0;

        long option1 = A - B;
        if (option1 < 0)
            option1 *= -1;

        long option2;
        if (A > B)
            option2 = ((N - A) + B) - 1;
        else
            option2 = ((N - B) + A) - 1;

        Debug.Log("calcSecondsFromAtoB() found the values " + option1 + " and " + option2 );

        return (long) Mathf.Min(option1, option2);
    }

    void Update()
    {
        // calculate the new rotation
        // by taking the old rotation
        // and applying the speed parameter
        Vector3 newRot = transform.localEulerAngles;
        newRot += _rotationSpeed * Time.deltaTime;
        transform.localEulerAngles = newRot;
    }

    void SpawnItemAtRandomPosition()
    {
        // start off with the position of the zone
        Vector3 randomPosition;

        // now randomize location based on the size of the associated BoxCollider
        randomPosition.x = Random.Range(_spawnZone.bounds.min.x, _spawnZone.bounds.max.x);
        randomPosition.y = Random.Range(_spawnZone.bounds.min.y, _spawnZone.bounds.max.y);
        randomPosition.z = Random.Range(_spawnZone.bounds.min.z, _spawnZone.bounds.max.z);

        // spawn the item prefab at this position
        Instantiate(_itemToSpawn, randomPosition, Quaternion.identity);
    }

    /// <summary>
    /// Go through all the objects and spawn them in a circle.
    /// Radius is determined by the size of the spawn zone collider.
    /// </summary>
    void SpawnObjectsInCircle()
    {
        List<bool> seatTaken = new List<bool>();

        float radius = _spawnZone.bounds.size.x / 2;
        Transform parent = this.gameObject.transform;

        for (int i = 0; i < _itemCount; i++)
        {
            // get the position on the circle to spawn this object
            float angle = i * Mathf.PI * 2 / _itemCount;
            Vector3 pos = Vector3.zero;
            pos.x = Mathf.Cos(angle);
            pos.z = Mathf.Sin(angle);
            pos *= radius;

            // spawn as a child of the parent object
            GameObject newObj = Instantiate(_itemToSpawn, parent);
            newObj.transform.localPosition = pos;
        }
    }
}
