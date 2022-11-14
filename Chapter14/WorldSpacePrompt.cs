using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldSpacePrompt : MonoBehaviour
{
    [TextArea, SerializeField, Tooltip ("The text to display.")]
    private string _promptText;

    [SerializeField, Tooltip("The TextMesh UI Object.")]
    public TextMeshProUGUI _textObj;

    [SerializeField, Tooltip("The Prompt Parent to toggle.")]
    public GameObject _promptBG;

    // Start is called before the first frame update
    void Start()
    {
        _textObj.SetText( _promptText );
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if the player collides with this object,
        // unhide up the prompt text
        if ( collision.gameObject.GetComponent<PlayerController>() )
            _promptBG.SetActive(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        // if the player contact ends,
        // then hide up the prompt text
        if (collision.gameObject.GetComponent<PlayerController>())
            _promptBG.SetActive(false);
    }

}
