using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneOnCollision : MonoBehaviour
{
    [SerializeField, Tooltip("Name of scene to load.")]
    private string _sceneToLoad;

    [SerializeField, Tooltip("Seconds between collision and load.")]
    private float _transitionTime = 1f;

    private bool _hasCollided = false;

    void Update()
    {
        if (_hasCollided )
        {
            _transitionTime -= Time.deltaTime;
            if (_transitionTime <= 0f)
            {
                // time to load the scene
                // remember: this scene needs to be added
                // to the Build Settings 'Scenes' list !
                //SceneManager.LoadScene(_sceneToLoad, LoadSceneMode.Single);
                SceneTransition.TransitionToScene( _sceneToLoad );
                enabled = false;
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<PlayerController>() )
        {
            // the player has collided with this obj
            _hasCollided = true;
        }
    }
}
