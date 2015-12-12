using UnityEngine;
using System.Collections;

public class Grow : MonoBehaviour {

    [SerializeField]
    private float growth = 0.0001f;
    [SerializeField]
    private float waterStored = 0f;
    [SerializeField]
    private float sunlightStored = 0f;
    [SerializeField]
    private float waterIncrement = 0.1f;
    [SerializeField]
    private float sunlightIncrement = 0.1f;
    [SerializeField]
    private float waterThreshold = 0.75f;
    [SerializeField]
    private float sunlightThreshold = 0.75f;
    [SerializeField]
    private float waterDecrement = 0.05f;
    [SerializeField]
    private float sunlightDecrement = 0.05f;

    //public float waterSaved
    //{
    //    get
    //    {
    //        return waterStored;
    //    }
    //    set
    //    {
    //        if( ( waterSaved + value <= 1.0f ) && ( waterSaved - value >= 0f ) )
    //        {
    //            waterSaved = value;
    //        }
    //    }
    //}


    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //   if( Input.GetKey( KeyCode.G ) )
        //   {
        //       transform.localScale = new Vector3( transform.localScale.x * growth, transform.localScale.y * growth, transform.localScale.z * growth );
        //   }

        waterStored -= waterDecrement;
        sunlightStored -= sunlightDecrement;
        if( Input.GetKeyDown( KeyCode.W ) )
        {
            waterStored += waterIncrement;
        }
        if( Input.GetKeyDown( KeyCode.S ) )
        {
            sunlightStored += sunlightIncrement;
        }

        growth = ( waterStored / waterThreshold ) + ( sunlightStored / sunlightThreshold );
        if( growth < 1 )
        {
            growth = 1;
        }
        else if( growth > 1.001f )
        {
            growth = 1.001f;
        }
        if( waterStored < 0 )
        {
            waterStored = 0f;
        }
        if( sunlightStored < 0 )
        {
            sunlightStored = 0f;
        }
        GetBigger( growth );
	}

    void GetBigger( float rate )
    {
        transform.localScale = new Vector3( transform.localScale.x * rate, transform.localScale.y * rate, transform.localScale.z * rate );
    }
}
