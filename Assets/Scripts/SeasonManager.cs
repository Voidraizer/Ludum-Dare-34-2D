using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SeasonManager : MonoBehaviour {

    public float waterSeasonMultiplier = 1.0f;
    public float sunlightSeasonMultiplier = 1.0f;

    [SerializeField]
    private Text seasonText;

    [SerializeField]
    private int cyclesPerSeason = 3;
    private int sunCycles = 0;

    enum seasons { SPRING, SUMMER, FALL, WINTER };
    seasons season = seasons.SUMMER;

    // Use this for initialization
    void Start () {
	
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
        // create snow overlay/leaves overay/flowers overlay -- fade in and out at start/end of season
        yield return new WaitForEndOfFrame();
    }
}
