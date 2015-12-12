using UnityEngine;
using System.Collections;

public class DayNightManager : MonoBehaviour {

    public float waterTimeMultiplier = 1.0f;
    public float sunlightTimeMultiplier = 1.0f;

    [SerializeField]
    private GameObject Sun;

    [SerializeField]
    private Sprite Background;

    private enum timeOfDay { EARLY_MORNING, MORNING, NOON, EVENING, LATE_NIGHT };

    [SerializeField]
    private timeOfDay dayTime;

	// Use this for initialization
	void Start () {
        dayTime = timeOfDay.LATE_NIGHT;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator TransitionInto( timeOfDay day )
    {

        yield return new WaitForEndOfFrame();

    }

    public void IncrementDayTime()
    {
        if( dayTime == timeOfDay.LATE_NIGHT )
        {
            dayTime = timeOfDay.EARLY_MORNING;
        }
        else
        {
            dayTime++;
        }
        StartCoroutine( TransitionInto( dayTime ) );
    }
}
