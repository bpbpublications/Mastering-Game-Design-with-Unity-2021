using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructTimer : MonoBehaviour
{
    [SerializeField, Tooltip("Seconds until this object self-destructs")]
    float _countdownTimer = 1.5f;
    
    void Update()
    {
        _countdownTimer -= Time.deltaTime;
        if (_countdownTimer <= 0)
            Destroy(gameObject);
    }
}
