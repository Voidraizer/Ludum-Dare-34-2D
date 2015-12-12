using UnityEngine;
using System.Collections;

public class Grow : MonoBehaviour {

    [SerializeField]
    private float growth = 0.0001f;
    [SerializeField]
    private float maxGrowth = 1.0001f;
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
    [SerializeField]
    private float waterMax = 1f;
    [SerializeField]
    private float sunlightMax = 1f;

    public float waterRatio;
    public float sunlightRatio;

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

	// Update is called once per frame
	void Update () {
        //   if( Input.GetKey( KeyCode.G ) )
        //   {
        //       transform.localScale = new Vector3( transform.localScale.x * growth, transform.localScale.y * growth, transform.localScale.z * growth );
        //   }

        waterStored -= waterDecrement;
        sunlightStored -= sunlightDecrement;
        if( Input.GetKeyDown( KeyCode.D ) )
        {
            waterStored += waterIncrement;
        }
        if( Input.GetKeyDown( KeyCode.K ) )
        {
            sunlightStored += sunlightIncrement;
        }

        growth = ( waterStored / waterThreshold ) * ( sunlightStored / sunlightThreshold );
        if( growth < 1f )
        {
            growth = 1f;
        }
        else if( growth > maxGrowth )
        {
            growth = 1.001f;
        }
        if( waterStored < 0f )
        {
            waterStored = 0f;
        }
        else if( waterStored > 1f )
        {
            waterStored = 1f;
        }
        if( sunlightStored < 0f )
        {
            sunlightStored = 0f;
        }
        else if( sunlightStored > 1f )
        {
            sunlightStored = 1f;
        }
        GetBigger( growth );

        waterRatio = waterStored / waterMax;
        sunlightRatio = sunlightStored / sunlightMax;
	}

    void GetBigger( float rate )
    {
        transform.localScale = new Vector3( transform.localScale.x * rate, transform.localScale.y * rate, transform.localScale.z * rate );
    }
}
