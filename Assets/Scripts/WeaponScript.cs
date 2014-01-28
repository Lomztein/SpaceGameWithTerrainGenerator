using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {

	public string faction;
	public Transform parentUnit;

	public GameObject bulletType;
	public GameObject fireParticle;
	public bool reloaded = true;
	public float reloadTime;
	public float bulletForce;
	public float inaccuracy;
	public float damage;
	public float apFactor;
	public int upgrade;
	public int amount = 1;

	public void Start () {

		parentUnit = transform.parent;
		while (parentUnit.parent) {
			parentUnit = parentUnit.parent;
		}
		if (!parentUnit.parent) {
			faction = parentUnit.tag;
		}
	}

	public void Fire () {

		if (reloaded == true) {
			Instantiate(fireParticle,transform.position,transform.rotation);
			int internalAmount = amount;
			reloaded = false;
			Invoke ("Reload",reloadTime);
			while (internalAmount > 0) {
				GameObject newBullet = (GameObject)Instantiate (bulletType,transform.position,transform.rotation);
				newBullet.rigidbody.AddForce (transform.forward * bulletForce * Random.Range (0.9f,1.1f));
				newBullet.rigidbody.AddForce (transform.up * Random.Range (-inaccuracy,inaccuracy));
				BulletScript newScript = newBullet.GetComponent<BulletScript>();
				newScript.damage = damage + (upgrade * damage/2);
				newScript.apFactor = apFactor;
				newScript.faction = faction;
				internalAmount--;
			}
		}
	}

	public void Reload () {

		reloaded = true;
	
	}
}