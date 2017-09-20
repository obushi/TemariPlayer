using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;

public class Temari : MonoBehaviour {

	Rigidbody2D rigid;
	AudioSource audioSource;
	Material material;
	
	[SerializeField]
	GameObject trail;

	List<GameObject> trails;

	public static Mesh TemariMesh;
	public static Vector3[] TemariVertices;
	public static float forceCoef = 0.85f;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();

		rigid.AddForce(new Vector2(Random.Range(-4, 4), 0), ForceMode2D.Impulse);

		gameObject.UpdateAsObservable()
		.Where(_ => gameObject.transform.position.y < -20)
		.Subscribe(g => GameObject.Destroy(gameObject))
		.AddTo(gameObject);

		gameObject.OnCollisionEnter2DAsObservable()
		.Subscribe(
			g => rigid.AddForce(g.contacts.First().normal * g.relativeVelocity.magnitude * forceCoef, ForceMode2D.Impulse)
		);

		gameObject.OnCollisionEnter2DAsObservable()
		.Subscribe(g => FadeOutTrail());

		material = transform.Find("TemariModel").gameObject.transform.Find("mainMeshNode").GetComponent<Renderer>().material;
		material.SetInt("_ColorPattern", Random.Range(0, 5));
		StartCoroutine("FadeIn");
		
		foreach(var i in Enumerable.Range(0, 30))
		{
			int vertexId = Random.Range(0, TemariVertices.Length);
			Instantiate(trail, transform.position + TemariVertices[vertexId], Quaternion.identity, transform);
		}

		trails = new List<GameObject>();
		foreach(Transform child in transform)
		{
			if (child.gameObject.name.Equals("Trail(Clone)"))
			{
				trails.Add(child.gameObject);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator FadeIn() {
		for (float f = 0f; f < 1f; f += 0.01f)
		{
			material.SetFloat("_Fade", f);
			yield return null;
		}
	}

	void FadeOutTrail()
	{
		foreach(var trail in trails)
		{
			if (trail.gameObject.name.StartsWith("Trail"))
			{
				trail.SendMessage("FadeOutTrail");
			}
		}
	}

	/// <summary>
	/// OnGUI is called for rendering and handling GUI events.
	/// This function can be called multiple times per frame (one call per event).
	/// </summary>
	void OnGUI()
	{
		//GUI.TextField(new Rect(10, 50, 200, 20), "forceCoef" + forceCoef.ToString());
	}
}
