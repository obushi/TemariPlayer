using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Sent when an incoming collider makes contact with this object's
	/// collider (2D physics only).
	/// </summary>
	/// <param name="other">The Collision2D data associated with this collision.</param>
	void OnCollisionEnter2D(Collision2D other)
	{
		other.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(-other.relativeVelocity, (Vector2)other.transform.position, ForceMode2D.Impulse);
	}
}
