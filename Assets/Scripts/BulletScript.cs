using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public string faction;

	public GameObject particleType;
	public float damage;
	public float apFactor;

	void OnTriggerEnter (Collider other) {

		if (other.gameObject.tag != faction) {
			Hit (other.gameObject,damage,apFactor);
			//Debug.Log (name + " hit " + other.gameObject.name);
		}
	}

	void Hit (GameObject hit, float d, float a) {

		Destroy(gameObject);
		Instantiate(particleType,transform.position,Quaternion.identity);
		HealthScript hh = hit.GetComponent<HealthScript>();
		if (hit.rigidbody) {
			hit.rigidbody.AddForceAtPosition(rigidbody.velocity,transform.position);
		}
		if (hh) {
			hh.TakeDamage(d,a);
		}
	}
}