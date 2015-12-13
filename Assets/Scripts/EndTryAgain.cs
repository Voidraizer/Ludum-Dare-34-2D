using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndTryAgain : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TryAgain()
    {
        SceneManager.LoadScene( 0 );
    }
}
