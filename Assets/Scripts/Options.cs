using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Options : MonoBehaviour {

    public bool paused = false;

    [SerializeField]
    private Text pausedText;
	
	// Update is called once per frame
	void Update () {
	    if( Input.GetKeyDown(KeyCode.Escape ) )
        {
            if( !paused )
            {
                pausedText.gameObject.SetActive( true );
                Time.timeScale = 0f;
                paused = true;
            }
            else
            {
                pausedText.gameObject.SetActive( false );
                Time.timeScale = 1f;
                paused = false;
            }
        }
	}
}
