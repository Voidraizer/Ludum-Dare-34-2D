using UnityEngine;
using System.Collections;

public class CloudBehavior : MonoBehaviour {

    [SerializeField]
    private float speedMin = 1f;
    [SerializeField]
    private float speedMax = 2f;

    [SerializeField]
    private float sizeXMin = 1f;
    [SerializeField]
    private float sizeXMax = 2f;
    [SerializeField]
    private float sizeYMin = 1f;
    [SerializeField]
    private float sizeYMax = 2f;

    private float speedActual = 0f;
    private float maxX = 0f;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate( speedActual * Time.deltaTime, 0f, 0f );
        if( transform.position.x >= maxX )
        {
            Destroy( gameObject );
        }
	}

    public void SetRandomSpeedSize()
    {
        speedActual = Random.Range( speedMin, speedMax );
        float sizeX = Random.Range( sizeXMin, sizeXMax );
        float sizeY = Random.Range( sizeYMin, sizeYMax );
        transform.localScale = new Vector3( sizeX, sizeY, 1f );
    }

    public void SetMaxX( float max )
    {
        maxX = max;
    }
}
