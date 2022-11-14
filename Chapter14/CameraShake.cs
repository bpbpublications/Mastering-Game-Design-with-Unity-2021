using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    [SerializeField, Tooltip("Magnitude of the shake effect.")]
    float _shake = 0.05f;

    Vector3 _startPos;

    private void Start()
    {
        // store the starting position
        _startPos = transform.position;
    }

    void Update()
    {
        // if enabled, give camera a little shake
        Vector3 newPosition = new Vector3();
        newPosition.x = _startPos.x + Random.Range(-_shake, _shake);
        newPosition.y = _startPos.y + Random.Range(-_shake, _shake);
        newPosition.z = _startPos.z + Random.Range(-_shake, _shake);

        transform.position = newPosition;
    }
}
