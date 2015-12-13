using UnityEngine;
using System.Collections;

public class CloudManager : MonoBehaviour {

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

    private float timeLastSpawned = 0f;
    private float timeNextSpawn = 0f;

	// Use this for initialization
	void Start () {
        timeLastSpawned = Time.time;
        timeNextSpawn = timeLastSpawned + Random.Range( timeMinSpawn, timeMaxSpawn );
	}
	
	// Update is called once per frame
	void Update () {
	    if( Time.time >= timeNextSpawn )
        {
            SpawnCloud();
        }
	}

    void SpawnCloud()
    {
        float spawnX = widthMin;
        float spawnY = Random.Range( heightMin, heightMax );

        GameObject temp = (GameObject)Instantiate( Cloud1PreFab, new Vector3( spawnX, spawnY, 0f ), Quaternion.identity );
        CloudBehavior cb = temp.GetComponent<CloudBehavior>();
        cb.SetRandomSpeedSize();
        cb.SetMaxX( widthMax );
        timeLastSpawned = Time.time;
        timeNextSpawn = timeLastSpawned + Random.Range( timeMinSpawn, timeMaxSpawn );
    }
}
