using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// When the user presses the 'Start Game' button,
    /// we need to load the MainGame scene.
    /// </summary>
    public void onPressStartGameBtn()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
