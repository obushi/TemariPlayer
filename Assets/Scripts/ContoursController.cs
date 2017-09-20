using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using UdpReceiverUniRx;
using System;

public class ContoursController : MonoBehaviour {

	public UdpReceiverRx udpReceiver;
	private IObservable<UdpState> myUdpSequence;
	static readonly int MaxContourCount = 10;
	
	[SerializeField]
	GameObject contour;

	GameObject[] contours;
	PolygonCollider2D[] polygonCollier2Ds;

	// Use this for initialization
	void Start () {
		contours = new GameObject[MaxContourCount];
		polygonCollier2Ds = new PolygonCollider2D[MaxContourCount];
		for (int i = 0; i < MaxContourCount; i++)
		{
			contours[i] = Instantiate(contour, Vector3.zero, Quaternion.identity);
			contours[i].transform.parent = transform;
			polygonCollier2Ds[i] = contours[i].GetComponent<PolygonCollider2D>();
			polygonCollier2Ds[i].points = new Vector2[] {};
		}

		
		string[] udpMsg;
		Vector2[] vertices;
		int index;
		

		myUdpSequence = udpReceiver._udpSequence;
		myUdpSequence
		.ObserveOnMainThread()
		.Subscribe(x =>{
		// Debug.Log(x.UdpMsg);
		{
			// Vector2[] contourVertices = x.UdpMsg.Split('|')
			// 							        .Skip(1)
			// 							        // .TakeWhile(t => t != string.Empty)
			// 							        .Select(v => new Vector2(float.Parse(v.Split(',')[0]), float.Parse(v.Split(',')[1])))
			// 							        .ToArray();

			// int index = int.Parse(x.UdpMsg.Split('|').First());
			// contours[index].GetComponent<PolygonCollider2D>().points = contourVertices;

			udpMsg = x.UdpMsg.Split('|');
			index = int.Parse(udpMsg.First());

			if (udpMsg.Count() == 1)
			{
				polygonCollier2Ds[index].points = new Vector2[]{};
			}

			else
			{
				vertices = new Vector2[udpMsg.Count() - 1];
				for (int i = 1; i < udpMsg.Count(); i++)
				{
					var coords = udpMsg[i].Split(',');
					vertices[i-1] = new Vector2(float.Parse(coords[0]), float.Parse(coords[1]));
				}
				polygonCollier2Ds[index].points = vertices;
			}
		}

			// if (x.UdpMsg.StartsWith("[Current]"))
			// {
			// 	int n;
			// 	var temarisInfo = x.UdpMsg.Split('|').Skip(1).TakeWhile(t => t != string.Empty).ToArray();
			// 	foreach (var temariInfo in temarisInfo)
			// 	{
					
			// 		var t = temariInfo.Split(',');
			// 		int index = int.Parse(t[0]);
			// 		Vector3 position = new Vector3(float.Parse(t[1]), float.Parse(t[2]));
			// 		// Debug.Log(index.ToString() + position.ToString());
			// 		contours[index].transform.position = position;
			// 	}
			// }

			// else
			// {
			// 	int n;
			// 	var ids = x.UdpMsg.Split('|').Where(i => int.TryParse(i, out n)).Select(i => int.Parse(i)).ToArray();
			// 	foreach (var i in ids)
			// 	{
			// 		bool isHidden = x.UdpMsg.StartsWith("[Inserted]");

			// 		if (isHidden)
			// 		{

			// 		}
			// 		else
			// 		{
			// 			contours[i].transform.position = new Vector3(0, 10, 0);
			// 		}

			// 		contours[i].SetActive(isHidden);
			// 	}
			// }

			// IEnumerable<int> temariId = x.UdpMsg.Split(',').Where(s => s.Is)
		})
		.AddTo(this);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			contour.GetComponent<PolygonCollider2D>().sharedMaterial.bounciness += 0.05f;
			Debug.Log("Contour Bounceness" + contour.GetComponent<PolygonCollider2D>().sharedMaterial.bounciness.ToString());
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			contour.GetComponent<PolygonCollider2D>().sharedMaterial.bounciness -= 0.05f;
			Debug.Log("Contour Bounceness" + contour.GetComponent<PolygonCollider2D>().sharedMaterial.bounciness.ToString());
		}
	}

	/// <summary>
	/// OnGUI is called for rendering and handling GUI events.
	/// This function can be called multiple times per frame (one call per event).
	/// </summary>
	void OnGUI()
	{
		//GUI.TextField(new Rect(10, 30, 200, 20), "Contour Bounceness" + contour.GetComponent<PolygonCollider2D>().sharedMaterial.bounciness.ToString());
	}
}
