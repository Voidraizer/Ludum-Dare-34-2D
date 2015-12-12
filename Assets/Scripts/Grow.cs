﻿using UnityEngine;
using System.Collections;

public class Grow : MonoBehaviour
{

    [SerializeField]
    private float growth = 0.0001f;
    [SerializeField]
    private float maxGrowth = 1.0001f;
    [SerializeField]
    private float growthGlucoseFlat = 1.001f;

    [SerializeField]
    private float waterStored = 0f;
    [SerializeField]
    private float sunlightStored = 0f;
    [SerializeField]
    private float glucoseStored = 0f;

    [SerializeField]
    private float waterIncrement = 0.1f;
    [SerializeField]
    private float sunlightIncrement = 0.1f;
    [SerializeField]
    private float glucoseIncrement = 0.1f;
    [SerializeField]
    private float waterIncrement2 = 0.1f;
    [SerializeField]
    private float sunlightIncrement2 = 0.1f;

    [SerializeField]
    private float waterThreshold = 0.75f;
    [SerializeField]
    private float sunlightThreshold = 0.75f;

    [SerializeField]
    private float waterDecrement = 0.05f;
    [SerializeField]
    private float sunlightDecrement = 0.05f;
    [SerializeField]
    private float glucoseDecrement = 0.1f;
    [SerializeField]
    private float glucoseConsumption = 0.005f;
    [SerializeField]
    private float waterDecrement2 = 0.05f;
    [SerializeField]
    private float sunlightDecrement2 = 0.05f;

    [SerializeField]
    private float waterMax = 1f;
    [SerializeField]
    private float sunlightMax = 1f;
    [SerializeField]
    private float glucoseMax = 1f;

    [SerializeField]
    EventText eventText;

    [SerializeField]
    DayNightManager days;
    [SerializeField]
    SeasonManager seasons;

    [SerializeField]
    private float keySwitchTime = 1f;
    private float dKeyPressTime = 0f;
    private float kKeyPressTime = 0f;

    [SerializeField]
    private SpriteRenderer sprender;
    [SerializeField]
    private float deathColorRate = 0.2f;

    private enum Controls { BASIC, EXPANDED };
    private enum DKeyControls { WATER, SUNLIGHT };
    private enum KKeyControls { GLUCOSE, GROW };

    Controls gameControls;
    DKeyControls dKey;
    KKeyControls kKey;

    public float waterRatio;
    public float sunlightRatio;
    public float glucoseRatio;

    private bool dKeySwitched = false;
    private bool kKeySwitched = false;
    public bool dead = false;

    void Start()
    {
        gameControls = Controls.EXPANDED;
        dKey = DKeyControls.SUNLIGHT;
        kKey = KKeyControls.GLUCOSE;
    }

    // Update is called once per frame
    void Update()
    {
        //   if( Input.GetKey( KeyCode.G ) )
        //   {
        //       transform.localScale = new Vector3( transform.localScale.x * growth, transform.localScale.y * growth, transform.localScale.z * growth );
        //   }

        if( Input.GetKeyDown( KeyCode.G ) )
        {
            if( gameControls == Controls.BASIC )
            {
                gameControls = Controls.EXPANDED;
            }
            else
            {
                gameControls = Controls.BASIC;
            }
        }

        if( gameControls == Controls.BASIC )
        {

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
            else if( waterStored > waterMax )
            {
                waterStored = waterMax;
            }
            if( sunlightStored < 0f )
            {
                sunlightStored = 0f;
            }
            else if( sunlightStored > sunlightMax )
            {
                sunlightStored = sunlightMax;
            }
            GetBigger( growth );
        }
        else if( gameControls == Controls.EXPANDED )  //------------------- EXPANDED ---------------------
        {
            if( Input.GetKeyDown( KeyCode.D ) )
            {
                dKeyPressTime = Time.time + keySwitchTime;
                if( dKey == DKeyControls.WATER )
                {
                    waterStored += waterIncrement2 * seasons.waterSeasonMultiplier * days.waterTimeMultiplier;
                }
                else
                {
                    sunlightStored += sunlightIncrement2;
                }
                dKeySwitched = false;
            }
            if( Input.GetKeyDown( KeyCode.K ) )
            {
                kKeyPressTime = Time.time + keySwitchTime;
                if( kKey == KKeyControls.GLUCOSE )
                {
                    if( ( glucoseStored < glucoseMax - glucoseIncrement ) && ( waterStored > 0f ) && ( sunlightStored > 0f ) )
                    {
                        glucoseStored += glucoseIncrement;
                        waterStored -= waterDecrement2;
                        sunlightStored -= sunlightDecrement2;
                    }
                }
                else
                {
                    if( glucoseStored > 0f )
                    {
                        glucoseStored -= glucoseDecrement;
                        GetBigger( growthGlucoseFlat );
                    }
                }
                kKeySwitched = false;
            }

            if( Input.GetKey( KeyCode.D ) )
            {
                if( Time.time >= dKeyPressTime )
                {
                    if( dKeySwitched == false )
                    {
                        if( dKey == DKeyControls.WATER )
                        {
                            dKey = DKeyControls.SUNLIGHT;
                            eventText.SpawnEventText( "Sunlight" );
                        }
                        else if( dKey == DKeyControls.SUNLIGHT )
                        {
                            dKey = DKeyControls.WATER;
                            eventText.SpawnEventText( "Water" );
                        }
                        dKeySwitched = true;
                    }
                }
            }
            if( Input.GetKey( KeyCode.K ) )
            {
                if( Time.time >= kKeyPressTime )
                {
                    if( kKeySwitched == false )
                    {
                        if( kKey == KKeyControls.GLUCOSE )
                        {
                            kKey = KKeyControls.GROW;
                            eventText.SpawnEventText( "Grow" );
                        }
                        else if( kKey == KKeyControls.GROW )
                        {
                            kKey = KKeyControls.GLUCOSE;
                            eventText.SpawnEventText( "Glucose" );
                        }
                        kKeySwitched = true;
                    }
                }
            }

            if( waterStored < 0f )
            {
                waterStored = 0f;
            }
            else if( waterStored > waterMax )
            {
                waterStored = waterMax;
            }
            if( sunlightStored < 0f )
            {
                sunlightStored = 0f;
            }
            else if( sunlightStored > sunlightMax )
            {
                sunlightStored = sunlightMax;
            }
            if( glucoseStored <= 0f )
            {
                glucoseStored = 0f;
                // herp derp
                if( !dead )
                {
                    dead = true;
                    Die();
                }
            }
            else if( glucoseStored > glucoseMax )
            {
                glucoseStored = glucoseMax;
            }
        }

        waterRatio = waterStored / waterMax;
        sunlightRatio = sunlightStored / sunlightMax;
        glucoseRatio = glucoseStored / glucoseMax;
    }

    void FixedUpdate()
    {
        if( gameControls == Controls.EXPANDED )
        {
            glucoseStored -= glucoseConsumption;
        }
        else
        {
            waterStored -= waterDecrement;
            sunlightStored -= sunlightDecrement;
        }
    }

    void GetBigger( float rate )
    {
        transform.localScale = new Vector3( transform.localScale.x * rate, transform.localScale.y * rate, transform.localScale.z * rate );
    }

    void Die()
    {
        StartCoroutine( DeathTransition() );
    }

    IEnumerator DeathTransition()
    {
        bool looping = true;
        float timeDifference = 0f;
        Color Starting = Color.white;
        Color Ending = Color.black;
        float distance = Vector3.Distance( new Vector3( Starting.r, Starting.g, Starting.b ), new Vector3( Ending.r, Ending.g, Ending.b ) );
        float startTime = Time.time;
        timeDifference = 0f;
        while( looping )
        {
            //shift color
            timeDifference = deathColorRate * ( Time.time - startTime );
            sprender.color = Color.Lerp( Starting, Ending, ( timeDifference / distance ) );
            yield return new WaitForEndOfFrame();
            // when done
            if( ( sprender.color == Ending ) || ( Time.time - startTime >= 5f ) )
            {
                looping = false;
            }
        }
        yield return new WaitForSeconds( 2f );
        Application.LoadLevel( 1 );
    }
}