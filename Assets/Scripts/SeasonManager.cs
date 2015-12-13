using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SeasonManager : MonoBehaviour {

    public float waterSeasonMultiplier = 1.0f;
    public float sunlightSeasonMultiplier = 1.0f;

    [SerializeField]
    private float waterSpringMult = 0f;
    [SerializeField]
    private float waterSummerMult = 0f;
    [SerializeField]
    private float waterFallMult = 0f;
    [SerializeField]
    private float waterWinterMult = 0f;

    [SerializeField]
    private float sunlightSpringMult = 0f;
    [SerializeField]
    private float sunlightSummerMult = 0f;
    [SerializeField]
    private float sunlightFallMult = 0f;
    [SerializeField]
    private float sunlightWinterMult = 0f;

    [SerializeField]
    private Text seasonText;

    [SerializeField]
    private Image Background_Overlay;

    [SerializeField]
    private Sprite Spring;
    [SerializeField]
    private Sprite Fall;
    [SerializeField]
    private Sprite Winter;

    [SerializeField]
    private float OverlayChangeRate = 0.05f;
    [SerializeField]
    private float OverlayTransparency = 0.5f;

    [SerializeField]
    private int cyclesPerSeason = 3;
    private int sunCycles = 0;

    [SerializeField]
    private Grow grow_cs;

    [SerializeField]
    private float springGrowthBonus;
    [SerializeField]
    private float winterGrowthPenalty;
    private float defaultGrowth;

    enum seasons { SPRING, SUMMER, FALL, WINTER };
    seasons season = seasons.SUMMER;

    // Use this for initialization
    void Start () {
        defaultGrowth = grow_cs.growthGlucoseFlat;
        seasonText.text = "Season: " + season;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void IncrementSunCycle()
    {
        if( sunCycles >= cyclesPerSeason )
        {
            sunCycles = 0;
            
            if( season == seasons.WINTER )
            {
                season = seasons.SPRING;
            }
            else
            {
                season++;
            }

            StartCoroutine( TransitionInto( season ) );
        }
        else
        {
            sunCycles++;
        }
    }

    IEnumerator TransitionInto( seasons season )
    {
        bool looping = true;
        float distance = 0f;
        float startTime = 0f;
        float timeDifference = 0f;
        Color Starting = Background_Overlay.color;
        Color Ending = new Color( Background_Overlay.color.r, Background_Overlay.color.g, Background_Overlay.color.b, OverlayTransparency );
        Color Reseting = new Color( Starting.r, Starting.g, Starting.b, 0f );
        seasonText.text = "Season: " + season;

        switch( season )
        {
            case seasons.SPRING:
                Starting = Background_Overlay.color;
                Ending = new Color( Background_Overlay.color.r, Background_Overlay.color.g, Background_Overlay.color.b, OverlayTransparency );
                Reseting = new Color( Starting.r, Starting.g, Starting.b, 0f );
                looping = true;
                distance = Mathf.Abs( Starting.a - Reseting.a );
                startTime = Time.time;
                timeDifference = 0f;
                sunlightSeasonMultiplier = sunlightSpringMult;
                waterSeasonMultiplier = waterSpringMult;
                grow_cs.growthGlucoseFlat = defaultGrowth + springGrowthBonus;
                // grow really fast in spring  -- really slow in winter
                while( looping )
                {
                    //shift color
                    timeDifference = OverlayChangeRate * ( Time.time - startTime );
                    Background_Overlay.color = Color.Lerp( Starting, Reseting, ( timeDifference / distance ) );
                    yield return new WaitForEndOfFrame();
                    // when done
                    if( ( Background_Overlay.color == Reseting ) || ( Time.time - startTime >= 10f ) )
                    {
                        looping = false;
                    }
                }
                Background_Overlay.color = Reseting;
                Background_Overlay.sprite = Spring;
                Background_Overlay.color = Reseting;
                Starting = Background_Overlay.color;
                Ending = new Color( Background_Overlay.color.r, Background_Overlay.color.g, Background_Overlay.color.b, OverlayTransparency );
                Reseting = new Color( Starting.r, Starting.g, Starting.b, 0f );
                looping = true;
                distance = Mathf.Abs( Starting.a - Ending.a );
                startTime = Time.time;
                timeDifference = 0f;
                while( looping )
                {
                    //shift color
                    timeDifference = OverlayChangeRate * ( Time.time - startTime );
                    Background_Overlay.color = Color.Lerp( Starting, Ending, ( timeDifference / distance ) );
                    yield return new WaitForEndOfFrame();
                    // when done
                    if( ( Background_Overlay.color == Ending ) || ( Time.time - startTime >= 10f ) )
                    {
                        looping = false;
                    }
                }
                break;
            case seasons.SUMMER:
                Starting = Background_Overlay.color;
                Ending = new Color( Background_Overlay.color.r, Background_Overlay.color.g, Background_Overlay.color.b, OverlayTransparency );
                Reseting = new Color( Starting.r, Starting.g, Starting.b, 0f );
                looping = true;
                distance = Mathf.Abs( Starting.a - Reseting.a );
                startTime = Time.time;
                timeDifference = 0f;
                sunlightSeasonMultiplier = sunlightSummerMult;
                waterSeasonMultiplier = waterSummerMult;
                grow_cs.growthGlucoseFlat = defaultGrowth;
                while( looping )
                {
                    //shift color
                    timeDifference = OverlayChangeRate * ( Time.time - startTime );
                    Background_Overlay.color = Color.Lerp( Starting, Reseting, ( timeDifference / distance ) );
                    yield return new WaitForEndOfFrame();
                    // when done
                    if( ( Background_Overlay.color == Reseting ) || ( Time.time - startTime >= 10f ) )
                    {
                        looping = false;
                    }
                }
                /*
                Background_Overlay.sprite = Spring;
                looping = true;
                distance = Mathf.Abs( Starting.a - Ending.a );
                startTime = Time.time;
                timeDifference = 0f;
                while( looping )
                {
                    //shift color
                    timeDifference = OverlayChangeRate * ( Time.time - startTime );
                    Background_Overlay.color = Color.Lerp( Starting, Ending, ( timeDifference / distance ) );
                    yield return new WaitForEndOfFrame();
                    // when done
                    if( ( Background_Overlay.color == Ending ) || ( Time.time - startTime >= 5f ) )
                    {
                        looping = false;
                    }
                }*/
                break;
            case seasons.FALL:
                Starting = Background_Overlay.color;
                Ending = new Color( Background_Overlay.color.r, Background_Overlay.color.g, Background_Overlay.color.b, OverlayTransparency );
                Reseting = new Color( Starting.r, Starting.g, Starting.b, 0f );
                looping = true;
                distance = Mathf.Abs( Starting.a - Reseting.a );
                startTime = Time.time;
                timeDifference = 0f;
                sunlightSeasonMultiplier = sunlightFallMult;
                waterSeasonMultiplier = waterFallMult;
                grow_cs.growthGlucoseFlat = defaultGrowth;
                while( looping )
                {
                    //shift color
                    timeDifference = OverlayChangeRate * ( Time.time - startTime );
                    Background_Overlay.color = Color.Lerp( Starting, Reseting, ( timeDifference / distance ) );
                    yield return new WaitForEndOfFrame();
                    // when done
                    if( ( Background_Overlay.color == Reseting ) || ( Time.time - startTime >= 10f ) )
                    {
                        looping = false;
                    }
                }

                Background_Overlay.sprite = Fall;
                Starting = Background_Overlay.color;
                Ending = new Color( Background_Overlay.color.r, Background_Overlay.color.g, Background_Overlay.color.b, OverlayTransparency );
                Reseting = new Color( Starting.r, Starting.g, Starting.b, 0f );
                looping = true;
                distance = Mathf.Abs( Starting.a - Ending.a );
                startTime = Time.time;
                timeDifference = 0f;
                while( looping )
                {
                    //shift color
                    timeDifference = OverlayChangeRate * ( Time.time - startTime );
                    Background_Overlay.color = Color.Lerp( Starting, Ending, ( timeDifference / distance ) );
                    yield return new WaitForEndOfFrame();
                    // when done
                    if( ( Background_Overlay.color == Ending ) || ( Time.time - startTime >= 10f ) )
                    {
                        looping = false;
                    }
                }
                break;
            case seasons.WINTER:
                Starting = Background_Overlay.color;
                Ending = new Color( Background_Overlay.color.r, Background_Overlay.color.g, Background_Overlay.color.b, OverlayTransparency );
                Reseting = new Color( Starting.r, Starting.g, Starting.b, 0f );
                looping = true;
                distance = Mathf.Abs( Starting.a - Reseting.a );
                startTime = Time.time;
                timeDifference = 0f;
                sunlightSeasonMultiplier = sunlightWinterMult;
                waterSeasonMultiplier = waterWinterMult;
                grow_cs.growthGlucoseFlat = defaultGrowth - winterGrowthPenalty;
                while( looping )
                {
                    //shift color
                    timeDifference = OverlayChangeRate * ( Time.time - startTime );
                    Background_Overlay.color = Color.Lerp( Starting, Reseting, ( timeDifference / distance ) );
                    yield return new WaitForEndOfFrame();
                    // when done
                    if( ( Background_Overlay.color == Reseting ) || ( Time.time - startTime >= 10f ) )
                    {
                        looping = false;
                    }
                }

                Background_Overlay.sprite = Winter;
                Starting = Background_Overlay.color;
                Ending = new Color( Background_Overlay.color.r, Background_Overlay.color.g, Background_Overlay.color.b, OverlayTransparency );
                Reseting = new Color( Starting.r, Starting.g, Starting.b, 0f );
                looping = true;
                distance = Mathf.Abs( Starting.a - Ending.a );
                startTime = Time.time;
                timeDifference = 0f;
                while( looping )
                {
                    //shift color
                    timeDifference = OverlayChangeRate * ( Time.time - startTime );
                    Background_Overlay.color = Color.Lerp( Starting, Ending, ( timeDifference / distance ) );
                    yield return new WaitForEndOfFrame();
                    // when done
                    if( ( Background_Overlay.color == Ending ) || ( Time.time - startTime >= 10f ) )
                    {
                        looping = false;
                    }
                }
                break;
            default:
                break;
        }
    }
}
