using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EventText : MonoBehaviour {

    public float waterEventMod = 0f;
    public float sunlightEventMod = 0f;
    
    [SerializeField]
    private Text[] eventText;

    [SerializeField]
    private Text statEventText;

    [SerializeField]
    private Transform eventTextSpawn;

    [SerializeField]
    private CloudManager cloudManager;

    [SerializeField]
    private bool sendEvent = false;
    [SerializeField]
    private bool endEvent = false;

    [SerializeField]
    private float eventBreakMin = 30f;
    [SerializeField]
    private float eventBreakMax = 60f;
    [SerializeField]
    private float eventStartMin = 60f;
    [SerializeField]
    private float eventStartMax = 120f;
    [SerializeField]
    private float eventDurationMin = 10f;
    [SerializeField]
    private float eventDurationMax = 30f;

    [SerializeField]
    private string[] events;

    private float eventDurationActual;
    private float eventBreakActual;
    private float eventStartActual;

    private float previousEventEnd = 100f;
    private bool cleanedUpPreviousEvent = true;

    private enum eventName { RAIN, DROUGHT, CLOUDY };

    [SerializeField]
    private eventName eventActual;
    private eventName lastEvent;

    void Start()
    {
        eventActual = (eventName)Random.Range( 0, events.Length );
        eventDurationActual = Random.Range( eventDurationMin, eventDurationMax );
        eventBreakActual = Time.time + Random.Range( eventStartMin, eventStartMax );

        cleanedUpPreviousEvent = true;
        previousEventEnd = 100f;
        endEvent = false;

        statEventText.text = "Event: None";
    }
    

	// Update is called once per frame
	void Update ()
    {
        if( ( Time.time >= eventBreakActual ) || sendEvent )
        {
            SpawnEventText( events[(int)eventActual] );
            previousEventEnd = eventBreakActual + eventDurationActual;
            cleanedUpPreviousEvent = false;
            switch( eventActual )
            {
                case eventName.RAIN:
                    SpawnEventText( events[(int)eventActual] );
                    cloudManager.StartRain();
                    statEventText.text = "Event: " + events[(int)eventActual];
                    waterEventMod = 0.08f;
                    sunlightEventMod = -0.08f;
                    break;
                case eventName.DROUGHT:
                    SpawnEventText( events[(int)eventActual] );
                    cloudManager.StartDrought();
                    statEventText.text = "Event: " + events[(int)eventActual];
                    waterEventMod = -0.08f;
                    sunlightEventMod = 0.08f;
                    break;
                case eventName.CLOUDY:
                    SpawnEventText( events[(int)eventActual] );
                    cloudManager.StartCloudy();
                    statEventText.text = "Event: " + events[(int)eventActual];
                    waterEventMod = 0.02f;
                    sunlightEventMod = -0.02f;
                    break;
                default:
                    break;
            }
            sendEvent = false;
            lastEvent = eventActual;
            eventBreakActual = Time.time + eventDurationActual + Random.Range( eventBreakMin, eventBreakMax );
            eventActual = (eventName)Random.Range( 0, events.Length );
            eventDurationActual = Random.Range( eventDurationMin, eventDurationMax );
        }

        if( ( ( Time.time >= previousEventEnd ) && ( !cleanedUpPreviousEvent ) ) || ( endEvent ) )
        {
            string message = "";
            switch( lastEvent )
            {
                case eventName.RAIN:
                    message = events[(int)lastEvent] + " has ended";
                    SpawnEventText( message );
                    cloudManager.EndRain();
                    statEventText.text = "Event: None";
                    waterEventMod = 0f;
                    sunlightEventMod = 0f;
                    break;
                case eventName.DROUGHT:
                    message = events[(int)lastEvent] + " has ended";
                    SpawnEventText( message );
                    cloudManager.EndDrought();
                    statEventText.text = "Event: None";
                    waterEventMod = 0f;
                    sunlightEventMod = 0f;
                    break;

                case eventName.CLOUDY:
                    message = events[(int)lastEvent] + " has ended";
                    SpawnEventText( message );
                    cloudManager.EndCloudy();
                    statEventText.text = "Event: None";
                    waterEventMod = 0f;
                    sunlightEventMod = 0f;
                    break;
                default:
                    break;
            }
            endEvent = false;
            cleanedUpPreviousEvent = true;
        }

        // Spawn test event in editor
        //if( sendEvent )
        //{
        //    eventActual = eventName.RAIN;
        //    SpawnEventText( events[(int)eventActual] );
        //    cloudManager.SpawnRainCloud();

        //    waterEventMod = 0.08f;
        //    sunlightEventMod = -0.08f;

        //    sendEvent = false;
        //}	
	}

    public void SpawnEventText( string textEvent )
    {
        Text temp = eventText[FindText()];
        temp.text = textEvent;
        temp.transform.position = eventTextSpawn.position;
        temp.gameObject.SetActive( true );
        TextEventScript textScript = temp.GetComponent<TextEventScript>();
        textScript.StartLife();
    }

    private int FindText()
    {
        int result = -1;
        for( int i = 0; i < eventText.Length; i++ )
        {
            if( !eventText[i].gameObject.activeInHierarchy )
            {
                result = i;
                break;
            }
        }
        return result;
    }
}
