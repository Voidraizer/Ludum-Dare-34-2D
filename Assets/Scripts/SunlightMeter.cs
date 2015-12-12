using UnityEngine;
using System.Collections;

public class SunlightMeter : MonoBehaviour
{

    [SerializeField]
    private Transform ForegroundTransform;

    private Grow grow_cs;

    // Use for assigning objects
    void Awake()
    {
        grow_cs = GameObject.Find( "Tree" ).GetComponent<Grow>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ForegroundTransform.localScale = new Vector2( grow_cs.sunlightRatio, 1f );
    }
}
