using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class AIBrain : MonoBehaviour
{
    #region ** members **
    // the current set of AI actions
    UnityEvent _curAIDirective;

    [SerializeField, Tooltip("Default events for this AI.")]
    UnityEvent _defaultActions;

    [SerializeField, Tooltip("Events to trigger when alerted.")]
    UnityEvent _alertedActions;

    [SerializeField, Tooltip("Events to trigger when hunting the player.")]
    UnityEvent _huntActions;

    [SerializeField, Tooltip("Misc Patterns of AI movement.")]
    public UnityEvent _miscPattern1Actions;
    public UnityEvent _miscPattern2Actions;
    public UnityEvent _miscPattern3Actions;

    // timer for pausing AI logic
    float _pauseTimer = 0;

    // we need quick access to the player object
    PlayerController _playerObject = null;

    #endregion
    private void Start()
    {
        // find the player object in the scene
        _playerObject = GameObject.FindObjectOfType<PlayerController>();

        // set default actions
        _curAIDirective = _defaultActions;
    }
    void Update()
    {
        if (UpdatePausedAI())
            return;

        _curAIDirective.Invoke();
    }
    bool UpdatePausedAI()
    {
        if (_pauseTimer > 0)
        {
            _pauseTimer -= Time.deltaTime;
            _pauseTimer = Mathf.Max(_pauseTimer, 0f);
        }

        return (bool)(_pauseTimer > 0f);
    }

    #region *** AI State ***
    public void SetState_Default() 
    { 
        _curAIDirective = _defaultActions; 
    }

    public void SetState_Hunt() 
    { 
        _curAIDirective = _huntActions; 
    }

    public void SetState_MiscPattern(int pattern) 
    { 
        /* Todo: Ch14 */ 
    }
    
    #endregion

    #region *** AI events ***
    public void Jump( float force )
    {
        GetComponent<Rigidbody>()?.AddForce( new Vector3(0, force, 0) );
    }

    public void AlertIfPlayerNearby(float distance)
    {
        if (CalcDistanceToPlayer() < distance)
            _alertedActions?.Invoke();
    }

    public void PauseAI( float timeInMS )
    {
        _pauseTimer = timeInMS;
    }

    public void UseWeapon() 
    { 
        /* Todo: Ch10 */ 
    }
    

    #endregion

    #region *** Player Hunting ***
    float CalcDistanceToPlayer()
    {
        return Vector3.Distance( transform.position, _playerObject.transform.position );
    }

    Vector3 CalcPlayerPos( bool ignoreY = false )
    {
        Vector3 playerPos = _playerObject.gameObject.transform.position;
        
        if (ignoreY)
            playerPos.y = transform.position.y;

        return playerPos;
    }

    public void LookAtPlayer()
    {
        transform.LookAt(CalcPlayerPos(true));
    }

    public void MoveTowardsPlayer( float speed )
    {
        // move towards the player
        Vector3 playerPos = CalcPlayerPos(true);
        Vector3 newPos = transform.position;

        playerPos.y = transform.position.y;

        newPos += (playerPos - transform.position).normalized * (speed * Time.deltaTime);

        transform.position = newPos;

        // look ahead towards the player
        transform.LookAt(playerPos);
    }

    #endregion

    #region *** NavMesh ***
    public void MoveTowardsPlayerUsingNavMesh()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if ( agent )
        {
            agent.SetDestination( _playerObject.transform.position );
        }
    }

    #endregion

}

