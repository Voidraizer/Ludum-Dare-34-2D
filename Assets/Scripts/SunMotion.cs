using UnityEngine;
using System.Collections;

public class SunMotion : MonoBehaviour {

    [SerializeField]
    private Transform[] wayPoints;

    [SerializeField]
    private DayNightManager dayTime;

    [SerializeField]
    private float moveSpeed;
    private float distanceTravelled;
    private float totalDistance;
    private float startTime;

    private int nextWP;
    private int lastWP;

    // Use this for initialization
    void Start () {
        transform.position = wayPoints[0].position;
        nextWP = 1;
        lastWP = 0;
        startTime = Time.time;
        totalDistance = Vector3.Distance( wayPoints[0].position, wayPoints[1].position );
	}
	
	// Update is called once per frame
	void Update () {
        if( transform.position.x >= wayPoints[nextWP].position.x)
        {
            if( nextWP == 7 )
            {
                nextWP = 1;
                lastWP = 0;
                transform.position = wayPoints[0].position;
            }
            else
            {
                lastWP = nextWP;
                nextWP++;
            }
            startTime = Time.time;
            totalDistance = Vector3.Distance( wayPoints[lastWP].position, wayPoints[nextWP].position );
            switch( lastWP )
            {
                case 1:
                    dayTime.IncrementDayTime();
                    break;
                case 2:
                    dayTime.IncrementDayTime();
                    break;
                case 3:
                    dayTime.IncrementDayTime();
                    break;
                case 4:
                    dayTime.IncrementDayTime();
                    break;
                case 6:
                    dayTime.IncrementDayTime();
                    break;
                default:
                    break;
            }
        }
        distanceTravelled = ( Time.time - startTime ) * moveSpeed;
        transform.position = Vector3.Lerp( wayPoints[lastWP].position, wayPoints[nextWP].position, ( distanceTravelled / totalDistance ) );
	}
}
