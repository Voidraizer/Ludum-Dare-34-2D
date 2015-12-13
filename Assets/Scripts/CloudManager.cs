using UnityEngine;
using System.Collections;

public class CloudManager : MonoBehaviour
{

    [SerializeField]
    private bool raining = false;
    [SerializeField]
    private bool drought = false;
    [SerializeField]
    private bool cloudy = false;

    [SerializeField]
    private GameObject Cloud1PreFab;

    [SerializeField]
    private float heightMin = 3f;
    [SerializeField]
    private float heightMax = 5f;
    [SerializeField]
    private float widthMin = -12f;
    [SerializeField]
    private float widthMax = 12f;

    [SerializeField]
    private float timeMinSpawn = 3f;
    [SerializeField]
    private float timeMaxSpawn = 10f;
    [SerializeField]
    private float timeMinCloudySpawn = 0.3f;
    [SerializeField]
    private float timeMaxCloudySpawn = 3f;

    private float timeLastSpawned = 0f;
    private float timeNextSpawn = 0f;

    GameObject rainCloud;

	// Use this for initialization
	void Start () {
        timeLastSpawned = Time.time;
        timeNextSpawn = timeLastSpawned + Random.Range( timeMinSpawn, timeMaxSpawn );
	}
	
	// Update is called once per frame
	void Update () {
	    if( Time.time >= timeNextSpawn )
        {
            if( !raining && !drought )
            {
                SpawnCloud();
            }
        }
	}

    void SpawnCloud()
    {
        float spawnX = widthMin;
        float spawnY = Random.Range( heightMin, heightMax );

        GameObject temp = (GameObject)Instantiate( Cloud1PreFab, new Vector3( spawnX, spawnY, 70f ), Quaternion.identity );
        CloudBehavior cb = temp.GetComponent<CloudBehavior>();
        cb.SetRandomSpeedSize();
        cb.SetMaxX( widthMax );
        timeLastSpawned = Time.time;
        if( cloudy )
        {
            timeNextSpawn = timeLastSpawned + Random.Range( timeMinCloudySpawn, timeMaxCloudySpawn );
        }
        else
        {
            timeNextSpawn = timeLastSpawned + Random.Range( timeMinSpawn, timeMaxSpawn );
        }
    }

    public void StartRain()
    {
        float spawnX = 0f;
        float spawnY = 3.7f;
        float scaleX = 26.77502f;
        float scaleY = 4.3f;
        Color shade = new Color( 79f, 79f, 83f, 255f );
        raining = true;

        rainCloud = (GameObject)Instantiate( Cloud1PreFab, new Vector3( spawnX, spawnY, 70f ), Quaternion.identity );
        CloudBehavior cb = rainCloud.GetComponent<CloudBehavior>();
        cb.transform.localScale = new Vector3( scaleX, scaleY, 1f );
        cb.rainCloud = true;
        SpriteRenderer cr = rainCloud.GetComponent<SpriteRenderer>();
        cr.color = new Color( shade.r, shade.g, shade.b, 0f );

        StartCoroutine( cb.GenerateRainClouds( shade ) );
    }

    public void EndRain()
    {
        raining = false;
        CloudBehavior cb = rainCloud.GetComponent<CloudBehavior>();
        StartCoroutine( cb.DestroyRainClouds() );
    }

    public void StartCloudy()
    {
        cloudy = true;
        timeNextSpawn = timeLastSpawned + Random.Range( timeMinCloudySpawn, timeMaxCloudySpawn );
    }

    public void EndCloudy()
    {
        cloudy = false;
    }

    public void StartDrought()
    {
        drought = true;
    }

    public void EndDrought()
    {
        drought = false;
    }
}
