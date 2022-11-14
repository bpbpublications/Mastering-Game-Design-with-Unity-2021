using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchfireBlock : MonoBehaviour
{
    [SerializeField, Tooltip("Seconds until fire goes out.")]
    float _fireTimer = 0;

    [SerializeField, Tooltip("Seconds applied when re-lit.")]
    float _maxTimer = 15;

    [SerializeField, Tooltip("The fire particle effect.")]
    ParticleSystem _fireParticle;

    // number of blocks in the level that are currently lit
    static int s_numLitBlocks = 0;

    // Start is called before the first frame update
    void Start()
    {
        EnableFireEffects(false);
        EnableSceneLights(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_fireTimer > 0f)
        {
            _fireTimer -= Time.deltaTime;
            if (_fireTimer < 0)
            {
                // fire has gone out
                AdjustFireBlockCount(-1);
                EnableFireEffects(false);
            }
        }
    }

    void EnableFireEffects(bool status)
    {
        // grab the particle effect emitter
        // and enable (or disable) it
        var emit = _fireParticle.emission;
        emit.enabled = status;
    }

    void OnTriggerEnter(Collider collider)
    {
        // did we collide with a flamable object?
        if (collider.gameObject.GetComponent<FireObject>())
        {
            // re-light the pyre
            if (_fireTimer <= 0)
            {
                AdjustFireBlockCount(1);
                EnableFireEffects(true);
            }

            _fireTimer = _maxTimer;
        }
    }

    static public void AdjustFireBlockCount(int change)
    {
        // update number of 'lit' torchfire
        // blocks in the scene
        s_numLitBlocks += change;

        // enable / disable the lights based on
        // the number of lit blocks
        bool lightStatus = true;
        if (s_numLitBlocks <= 0)
            lightStatus = false;

        EnableSceneLights(lightStatus);
    }

    static public void EnableSceneLights(bool status)
    { 
        Light[] lights = FindObjectsOfType<Light>();
        foreach (Light l in lights)
        {
            if (l.type == LightType.Directional)
                l.enabled = status;
        }
    }
}
