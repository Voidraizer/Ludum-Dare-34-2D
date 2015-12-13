﻿using UnityEngine;
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

    [SerializeField]
    private SpriteRenderer sprendor;

    [SerializeField]
    private float rainCloudFadeInRate = 0.05f;

    private float speedActual = 0f;
    private float maxX = 0f;

    public bool rainCloud = false;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate( speedActual * Time.deltaTime, 0f, 0f );
        if( ( transform.position.x >= maxX ) && ( !rainCloud ) )
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

    public IEnumerator GenerateRainClouds( Color goal )
    {
        bool looping = true;
        float distance = 255f;
        float startTime = Time.time;
        float timeDifference = 0f;
        Color Starting = sprendor.color;
        while( looping )
        {
            //shift color
            timeDifference = rainCloudFadeInRate * ( Time.time - startTime );
            sprendor.color = Color.Lerp( Starting, goal, ( timeDifference / distance ) );
            yield return new WaitForEndOfFrame();
            // when done
            if( ( sprendor.color == goal ) || ( Time.time - startTime >= 15f ) )
            {
                looping = false;
            }
        }
    }

    public IEnumerator DestroyRainClouds()
    {
        bool looping = true;
        float distance = 1f;
        float startTime = Time.time;
        float timeDifference = 0f;
        Color Starting = new Color( sprendor.color.r, sprendor.color.g, sprendor.color.b, 1f );
        Color goal = new Color( sprendor.color.r, sprendor.color.g, sprendor.color.b, 0f );
        while( looping )
        {
            //shift color
            timeDifference = rainCloudFadeInRate * ( Time.time - startTime );
            sprendor.color = Color.Lerp( Starting, goal, ( timeDifference / distance ) );
            yield return new WaitForEndOfFrame();
            // when done
            if( ( sprendor.color.a == 0f ) || ( Time.time - startTime >= 10f ) )
            {
                looping = false;
            }
        }
        Destroy( gameObject );
    }
}
