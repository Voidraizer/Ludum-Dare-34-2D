using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextEventScript : MonoBehaviour {

    [SerializeField]
    Text myText;

    [SerializeField]
    private float lifetime = 3.0f;
    [SerializeField]
    private float moveSpeed = 0.1f;

    private float timeStarted = 0f;
    private float timeEnded = 0f;

	// Update is called once per frame
	void Update () {
        transform.Translate( 0f, moveSpeed * Time.deltaTime, 0f );

        if( Time.time > timeEnded )
        {
            StartCoroutine( Die() );
        }
	}

    public void StartLife()
    {
        timeStarted = Time.time;
        timeEnded = timeStarted + lifetime;
        myText.color = new Color( 1f, 1f, 1f, 1f );
    }

    IEnumerator Die()
    {
        bool dying = true;
        while( dying )
        {
            Color myAlpha = myText.color;
            yield return new WaitForEndOfFrame();
            myAlpha.a -= 0.01f;
            myText.color = myAlpha;
            if( myText.color.a <= 0 )
            {
                dying = false;
                Dead();
            }
        }
    }

    void Dead()
    {
        gameObject.SetActive( false );
    }
}
