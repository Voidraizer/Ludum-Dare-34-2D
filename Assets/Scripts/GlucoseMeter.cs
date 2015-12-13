using UnityEngine;
using System.Collections;

public class GlucoseMeter : MonoBehaviour
{

    [SerializeField]
    private Transform ForegroundTransform;

    private Grow grow_cs;

    // Use for assigning objects
    void Awake()
    {
        grow_cs = GameObject.Find( "Tree Parent" ).GetComponent<Grow>();
    }

    // Update is called once per frame
    void Update()
    {
        ForegroundTransform.localScale = new Vector2( grow_cs.glucoseRatio, 1f );
    }
}
