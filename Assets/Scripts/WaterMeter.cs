﻿using UnityEngine;
using System.Collections;

public class WaterMeter : MonoBehaviour {

    [SerializeField]
    private Transform ForegroundTransform;

    private Grow grow_cs;

    // Use for assigning objects
    void Awake()
    {
        grow_cs = GameObject.Find( "Tree Parent" ).GetComponent<Grow>();
    }

	// Update is called once per frame
	void Update () {
        ForegroundTransform.localScale = new Vector2( grow_cs.waterRatio, 1f );
	}
}
