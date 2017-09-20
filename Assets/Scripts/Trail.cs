using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class Trail : MonoBehaviour {

	TrailRenderer trailRenderer;

	// Use this for initialization
	void Start () {
		trailRenderer = GetComponent<TrailRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FadeOutTrail() {
		trailRenderer.Clear();
	}
}
