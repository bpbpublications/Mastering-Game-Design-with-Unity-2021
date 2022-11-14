using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTimer : MonoBehaviour
{

    [SerializeField, Tooltip("Current timer.")]
    float _curTimer = 0;

    [SerializeField, Tooltip("Seconds between toggling of objects.")]
    float _timerGoal = 3;

    [SerializeField, Tooltip("Objects to toggle on/off.")]
    List<GameObject> _toggleObjs;

    void Update()
    {
        // if there are no objects to toggle
        // then don't bother with the countdown logic
        if (_toggleObjs == null)
            return;

        // increment timer
        _curTimer += Time.deltaTime;
        
        // have we met our goal?
        if ( _curTimer > _timerGoal )
        {
            // reset timer
            _curTimer = 0;

            // go through objects and toggle on/off
            for ( int i = 0; i < _toggleObjs.Count; i++ )
            {
                bool newVal = !_toggleObjs[i].activeSelf;
                _toggleObjs[i].SetActive(newVal);
            }
        }
    }
}
