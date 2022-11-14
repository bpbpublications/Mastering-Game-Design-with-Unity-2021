using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    // the objects to animate
    public Transform _topObj;
    public Transform _botObj;

    Vector2 _endTop, _endBot;

    // wait a half second before unhiding
    public float _curTime = -0.5f;

    // by default, transitions take 1 second
    float _endTime = 1f;

    // data for loading a scene post-transition
    public bool _loadScene = false;
    public string _sceneToLoad = "";

    // the singleton instance of this object
    static public SceneTransition Instance;

    private void Awake()
    {
        // make sure transition objects are visible
        _topObj.gameObject.SetActive(true);
        _botObj.gameObject.SetActive(true);

        // store the single instance of the
        // transition object in this scene
        Instance = this;

        // calculate final transition positions
        float offset = Screen.height * 0.75f;
        _endTop = new Vector2(0, offset);
        _endBot = new Vector2(0, -offset);
    }

    void FixedUpdate()
    {
        _curTime += Time.deltaTime;

        if ( _curTime < 0 )
        {
            // buffer before transition starts
            return;
        }
        else if (_curTime > _endTime)
        {
            // reached the end of the transition
            // is it time to load the next scene?
            if (_loadScene)
            {
                SceneManager.LoadScene(_sceneToLoad);
            }

            return;
        }

        // note: linear lerping is boring, so apply
        // an ease-out animation with Mathf.Pow()
        float t = Mathf.Pow(_curTime / _endTime, 4);

        if (_loadScene)
        {
            // move objects into position then load a new scene
            _topObj.localPosition = Vector2.Lerp(_endTop, Vector2.zero, t);
            _botObj.localPosition = Vector2.Lerp(_endBot, Vector2.zero, t);
        }
        else
        {
            // transition objects away to reveal the scene
            _topObj.localPosition = Vector2.Lerp(Vector2.zero, _endTop, t);
            _botObj.localPosition = Vector2.Lerp(Vector2.zero, _endBot, t);
        }
    }

    static public void TransitionToScene( string sceneToLoad )
    {
        Instance._curTime = 0;
        Instance._sceneToLoad = sceneToLoad;
        Instance._loadScene = true;
    }
}
