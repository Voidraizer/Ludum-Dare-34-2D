using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    [SerializeField]
    private AudioSource audorce;

    [SerializeField]
    private float timeMin = 0f;
    [SerializeField]
    private float timeMax = 100f;

	// Use this for initialization
	void Start () {
        audorce.time = Random.Range( timeMin, timeMax );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
