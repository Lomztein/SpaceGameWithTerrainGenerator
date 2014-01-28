using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public GameObject player;
	private Transform pt;

	// Use this for initialization
	void Start () {

		pt = player.transform;
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 newPos = new Vector3 (pt.position.x,pt.position.y,transform.position.z) + player.rigidbody.velocity/10;
		transform.position = newPos;
	
	}
}
