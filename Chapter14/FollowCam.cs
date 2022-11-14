using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField, Tooltip("The object to follow.")]
    private GameObject _camTarget;

    [SerializeField, Tooltip("Target offset.")]
    private Vector3 _targetOffset;

    [SerializeField, Tooltip("The height off the ground to follow from.")]
    private float _camHeight = 10;

    [SerializeField, Tooltip("The distance from the target to follow from.")]
    private float _camDistance = -15;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_camTarget)
            return;

        Vector3 targetPos = _camTarget.transform.position;
        targetPos += _targetOffset;

        targetPos.y += _camHeight;
        targetPos.z += _camDistance;

        // move camera towards target position
        Vector3 camPos = transform.position;
        transform.position = Vector3.Lerp( transform.position, targetPos, Time.deltaTime * 5.0f );
        
    }
}
