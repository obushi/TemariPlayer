using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TemarisController : MonoBehaviour {

	[SerializeField]
	GameObject temari;

	// Use this for initialization
	void Start () {
		Temari.TemariMesh = temari.transform.Find("TemariModel").gameObject.transform.Find("mainMeshNode").GetComponent<MeshFilter>().sharedMesh;
		Temari.TemariVertices = Temari.TemariMesh.vertices;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindGameObjectsWithTag("Temari").Count() < 5)
		{
			Instantiate<GameObject>(temari, new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(5, 7)), Quaternion.Euler(Random.insideUnitSphere * Random.Range(-90, 90)));
		}

		if (Input.GetKeyDown(KeyCode.Keypad8))
		{
			Temari.forceCoef += 0.05f;
			Debug.Log("Temari force" + Temari.forceCoef.ToString());
		}
		else if (Input.GetKeyDown(KeyCode.Keypad2))
		{
			Temari.forceCoef -= 0.05f;
			Debug.Log("Temari force" + Temari.forceCoef.ToString());
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			temari.GetComponent<Rigidbody2D>().sharedMaterial.bounciness += 0.05f;
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			temari.GetComponent<Rigidbody2D>().sharedMaterial.bounciness -= 0.05f;
		}
		
	}

	/// <summary>
	/// OnGUI is called for rendering and handling GUI events.
	/// This function can be called multiple times per frame (one call per event).
	/// </summary>
	void OnGUI()
	{
		//GUI.TextField(new Rect(10, 10, 200, 20), "Temari force" + temari.GetComponent<Rigidbody2D>().sharedMaterial.bounciness.ToString());
	}
}
