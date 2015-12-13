using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DayNightManager : MonoBehaviour {

    public float waterTimeMultiplier = 1.0f;
    public float sunlightTimeMultiplier = 1.0f;

    [SerializeField]
    private float waterEarlyMorning = 0f;
    [SerializeField]
    private float waterMorning = 0f;
    [SerializeField]
    private float waterNoon = 0f;
    [SerializeField]
    private float waterEvening = 0f;
    [SerializeField]
    private float waterLateEvening = 0f;

    [SerializeField]
    private float sunlightEarlyMorning = 0f;
    [SerializeField]
    private float sunlightMorning = 0f;
    [SerializeField]
    private float sunlightNoon = 0f;
    [SerializeField]
    private float sunlightEvening = 0f;
    [SerializeField]
    private float sunlightLateEvening = 0f;

    [SerializeField]
    private Text dayTimeText;

    [SerializeField]
    private GameObject Sun;

    [SerializeField]
    private Image Background;

    [SerializeField]
    private SpriteRenderer sprender;

    [SerializeField]
    private Grow grow_cs;

    [SerializeField]
    private SeasonManager season;

    private enum timeOfDay { EARLY_MORNING, MORNING, NOON, EVENING, LATE_NIGHT };

    [SerializeField]
    private timeOfDay dayTime;

    [SerializeField]
    private float conversionRate = 1f;

    [SerializeField]
    private Color earlyMorning;
    [SerializeField]
    private Color morning;
    [SerializeField]
    private Color noon;
    [SerializeField]
    private Color evening;
    [SerializeField]
    private Color lateEvening;

    // Use this for initialization
    void Start () {
        dayTime = timeOfDay.LATE_NIGHT;
        dayTimeText.text = "Time: " + dayTime;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator TransitionInto( timeOfDay day )
    {
        bool looping = true;
        float distance = 0f;
        float startTime = 0f;
        float timeDifference = 0f;
        Color Starting = Background.color;
        dayTimeText.text = "Time: " + day;

        switch( day )
        {
            case timeOfDay.EARLY_MORNING:
                looping = true;
                distance = Vector3.Distance( new Vector3( Starting.r, Starting.g, Starting.b ), new Vector3( earlyMorning.r, earlyMorning.g, earlyMorning.b ) );
                startTime = Time.time;
                timeDifference = 0f;
                sunlightTimeMultiplier = sunlightEarlyMorning;
                waterTimeMultiplier = waterEarlyMorning;
                while( looping )
                {
                    //shift color
                    timeDifference = conversionRate * ( Time.time - startTime );
                    Background.color = Color.Lerp( Starting, earlyMorning, ( timeDifference / distance ) );
                    if( !grow_cs.dead )
                    {
                        sprender.color = Background.color;
                    }
                    yield return new WaitForEndOfFrame();
                    // when done
                    if( ( Background.color == earlyMorning ) || ( Time.time - startTime >= 5f ) )
                    {
                        looping = false;
                    }
                }
                break;
            case timeOfDay.MORNING:
                looping = true;
                distance = Vector3.Distance( new Vector3( Starting.r, Starting.g, Starting.b ), new Vector3( morning.r, morning.g, morning.b ) );
                startTime = Time.time;
                timeDifference = 0f;
                sunlightTimeMultiplier = sunlightMorning;
                waterTimeMultiplier = waterMorning;
                while( looping )
                {
                    //shift color
                    timeDifference = conversionRate * ( Time.time - startTime );
                    Background.color = Color.Lerp( Starting, morning, ( timeDifference / distance ) );
                    if( !grow_cs.dead )
                    {
                        sprender.color = Background.color;
                    }
                    yield return new WaitForEndOfFrame();
                    // when done
                    if( ( Background.color == morning ) || ( Time.time - startTime >= 5f ) )
                    {
                        looping = false;
                    }
                }
                break;
            case timeOfDay.NOON:
                looping = true;
                distance = Vector3.Distance( new Vector3( Starting.r, Starting.g, Starting.b ), new Vector3( noon.r, noon.g, noon.b ) );
                startTime = Time.time;
                timeDifference = 0f;
                sunlightTimeMultiplier = sunlightNoon;
                waterTimeMultiplier = waterNoon;
                while( looping )
                {
                    //shift color
                    timeDifference = conversionRate * ( Time.time - startTime );
                    Background.color = Color.Lerp( Starting, noon, ( timeDifference / distance ) );
                    if( !grow_cs.dead )
                    {
                        sprender.color = Background.color;
                    }
                    yield return new WaitForEndOfFrame();
                    // when done
                    if( ( Background.color == noon ) || ( Time.time - startTime >= 5f ) )
                    {
                        looping = false;
                    }
                }
                break;
            case timeOfDay.EVENING:
                looping = true;
                distance = Vector3.Distance( new Vector3( Starting.r, Starting.g, Starting.b ), new Vector3( evening.r, evening.g, evening.b ) );
                startTime = Time.time;
                timeDifference = 0f;
                sunlightTimeMultiplier = sunlightEvening;
                waterTimeMultiplier = -waterEvening;
                while( looping )
                {
                    //shift color
                    timeDifference = conversionRate * ( Time.time - startTime );
                    Background.color = Color.Lerp( Starting, evening, ( timeDifference / distance ) );
                    if( !grow_cs.dead )
                    {
                        sprender.color = Background.color;
                    }
                    yield return new WaitForEndOfFrame();
                    // when done
                    if( ( Background.color == evening ) || ( Time.time - startTime >= 5f ) )
                    {
                        looping = false;
                    }
                }
                break;
            case timeOfDay.LATE_NIGHT:
                looping = true;
                distance = Vector3.Distance( new Vector3( Starting.r, Starting.g, Starting.b ), new Vector3( lateEvening.r, lateEvening.g, lateEvening.b ) );
                startTime = Time.time;
                timeDifference = 0f;
                sunlightTimeMultiplier = sunlightLateEvening;
                waterTimeMultiplier = waterLateEvening;
                while( looping )
                {
                    //shift color
                    timeDifference = conversionRate * ( Time.time - startTime );
                    Background.color = Color.Lerp( Starting, lateEvening, ( timeDifference / distance ) );
                    if( !grow_cs.dead )
                    {
                        sprender.color = Background.color;
                    }
                    yield return new WaitForEndOfFrame();
                    // when done
                    if( ( Background.color == lateEvening ) || ( Time.time - startTime >= 5f ) )
                    {
                        looping = false;
                    }
                }
                break;
            default:
                break;
        }
    }

    public void IncrementDayTime()
    {
        if( dayTime == timeOfDay.LATE_NIGHT )
        {
            dayTime = timeOfDay.EARLY_MORNING;
            season.IncrementSunCycle();
        }
        else
        {
            dayTime++;
        }
        StartCoroutine( TransitionInto( dayTime ) );
    }
}
