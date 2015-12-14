using UnityEngine;
using System.Collections;

public class RainBehavior : MonoBehaviour {

    [SerializeField]
    private float fallSpeed = 1f;
    [SerializeField]
    private float angle = 1f;
    [SerializeField]
    private float bottomEdge = -10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate( angle * Time.deltaTime, -fallSpeed * Time.deltaTime, 0f );
        if( transform.position.y <= bottomEdge )
        {
            Destroy( gameObject );
        }
	}
}
