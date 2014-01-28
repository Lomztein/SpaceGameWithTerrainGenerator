using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	public float maxHull;
	public float maxArmor;
	public float hull;
	public float armor;
	public float regenSpeed;
	public float maxRegen;
	public GameObject debris;

	// Use this for initialization
	void Start () {

		if (hull == 0) {
			hull = maxHull;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (hull < maxRegen) {
			hull += regenSpeed * Time.deltaTime;
		}
		if (hull <=0) {
			Destroy(gameObject);
			if (debris) {
				Instantiate(debris,transform.position,transform.rotation);
			}
		}
	}

	public void TakeDamage (float damage, float apFactor) {
		if (armor > 0) {
			armor -= damage*apFactor;
		}else{
			hull -= damage;
		}
	}
}
