using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Springboard : MonoBehaviour
{
    [SerializeField, Tooltip("Velocity change on the Y axis.")]
    float _upwardsForce = 300f;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hitObj = collision.gameObject;

        if (hitObj != null)
        {
            Rigidbody rb = hitObj.GetComponent<Rigidbody>();
            rb?.AddForce(0, _upwardsForce, 0);
        }
    }
}
