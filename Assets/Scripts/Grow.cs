using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Grow : MonoBehaviour
{

    [SerializeField]
    private float growth = 0.0001f;
    [SerializeField]
    private float maxGrowth = 1.0001f;

    public float growthGlucoseFlat = 1.001f;

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
    private Text waterBonus;
    [SerializeField]
    private Text sunlightBonus;

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
    private bool won = false;

    void Start()
    {
        gameControls = Controls.EXPANDED;
        dKey = DKeyControls.SUNLIGHT;
        kKey = KKeyControls.GLUCOSE;
    }

    // Update is called once per frame
    void Update()
    {

        float waterTotal = waterIncrement2 + seasons.waterSeasonMultiplier + days.waterTimeMultiplier;
        float sunlightTotal = sunlightIncrement2 + seasons.sunlightSeasonMultiplier + days.sunlightTimeMultiplier;
        string waterMod = "";
        string sunlightMod = "";

        // Water
        if( waterTotal >= 0.016f )
        {
            waterMod = "Much greater";
        }
        else if( waterTotal >= 0.012f )
        {
            waterMod = "Little greater";
        }
        else if( waterTotal >= 0.008f )
        {
            waterMod = "No bonus";
        }
        else if( waterTotal >= 0.002f )
        {
            waterMod = "Reduced";
        }
        else if( waterTotal == 0f )
        {
            waterMod = "No gain";
        }
        else
        {
            waterMod = "Something's wrong";
        }

        // sunlight
        if( sunlightTotal >= 0.016f )
        {
            sunlightMod = "Much greater";
        }
        else if( sunlightTotal >= 0.012f )
        {
            sunlightMod = "Little greater";
        }
        else if( sunlightTotal >= 0.008f )
        {
            sunlightMod = "No bonus";
        }
        else if( sunlightTotal >= 0.002f )
        {
            sunlightMod = "Reduced";
        }
        else if( sunlightTotal == 0f )
        {
            sunlightMod = "No gain";
        }
        else
        {
            sunlightMod = "Something's wrong";
        }

        waterBonus.text = "Water Collection: " + waterMod;
        sunlightBonus.text = "Sunlight Collection: " + sunlightMod;

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
            // Cheat key because I'm tired of pushing the buttons 10000x to test
            if( Input.GetKey( KeyCode.C ) )
            {
                GetBigger( growthGlucoseFlat );
            }
            if( Input.GetKeyDown( KeyCode.D ) )
            {
                dKeyPressTime = Time.time + keySwitchTime;
                if( dKey == DKeyControls.WATER )
                {
                    waterStored += waterIncrement2 + seasons.waterSeasonMultiplier + days.waterTimeMultiplier;
                }
                else
                {
                    sunlightStored += sunlightIncrement2 + seasons.sunlightSeasonMultiplier + days.sunlightTimeMultiplier;
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

        // Win Condition
        if( transform.localScale.y >= 25 )
        {
            if( !won )
            {
                won = true;
                Win();
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
        Vector3 oldPos = new Vector3( transform.position.x, transform.position.y, transform.position.z );
        transform.localScale = new Vector3( transform.localScale.x * rate, transform.localScale.y * rate, transform.localScale.z * rate );
        Vector3 newPos = new Vector3( transform.position.x, transform.position.y, transform.position.z );
        Vector3 diff = new Vector3( transform.position.x, oldPos.y - ( oldPos.y - newPos.y ) / 2f, transform.position.z );
        transform.position = diff;
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
            transform.localScale = transform.localScale * 0.99f;
            yield return new WaitForEndOfFrame();
            // when done
            
            if( transform.localScale.y <= 1.5f )
            {
                looping = false;
            }
        }
        SceneManager.LoadScene( 1 );
    }

    void Win()
    {
        StartCoroutine( WinTransition() );
    }

    IEnumerator WinTransition()
    {
        for( int i = 0; i < 9; i++ )
        {
            sprender.flipY = !sprender.flipY;
            yield return new WaitForSeconds( .2f );
        }
        yield return new WaitForSeconds( 1f );
        SceneManager.LoadScene( 2 );
    }
}