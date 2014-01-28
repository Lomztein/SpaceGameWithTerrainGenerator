using UnityEngine;
using System.Collections;

public class LootCredits : MonoBehaviour {

	public GameObject player;
	public float value;
	public float range;

	// Use this for initialization
	void Start () {

		if (!player) {
			player = GameObject.FindGameObjectWithTag("Player");
		}
		transform.localScale *= value/20;
	
	}

	void OnCollisionEnter (Collision other) {
		GameObject og = other.gameObject;
		Debug.Log (og.tag);
		if (og.tag == "Player") {
			PlayerScript ps = og.GetComponent<PlayerScript>();
			ps.stats.credits += value;
			Destroy(gameObject);
		}
	}
}
