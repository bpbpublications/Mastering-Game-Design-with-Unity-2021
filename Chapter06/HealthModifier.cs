using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModifier : MonoBehaviour
{

    [SerializeField, Tooltip("Change to health when applied to an object.")]
    float _healthChange = 0;

    [SerializeField, Tooltip("The class of object that should be damaged.")]
    DamageTarget _applyToTarget = DamageTarget.Player;

    public enum DamageTarget
    {
        Player,
        Enemies,
        All,
        None
    }

    [SerializeField, Tooltip("Should this object self-destruct on collision?")]
    bool _destroyOnCollision = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider collider)
    {
        GameObject hitObj = collider.gameObject;

        // get the HealthManager of the object we've hit
        HealthManager healthManager = hitObj.GetComponent<HealthManager>();
        if (healthManager && IsValidTarget(hitObj))
        {
            // apply the damage as negative health to this object
            healthManager.AdjustCurHealth(_healthChange);

            // should we self-destruct after dealing damage?
            if (_destroyOnCollision)
                GameObject.Destroy(gameObject);
        }
    }

    bool IsValidTarget(GameObject possibleTarget)
    {
        if (_applyToTarget == DamageTarget.All)
            return true;

        else if (_applyToTarget == DamageTarget.None)
            return false;

        else if (_applyToTarget == DamageTarget.Player &&
                 possibleTarget.GetComponent<PlayerController>())
            return true;

        else if (_applyToTarget == DamageTarget.Enemies &&
                 possibleTarget.GetComponent<AIBrain>())
            return true;

        // not a valid target
        return false;
    }
}
