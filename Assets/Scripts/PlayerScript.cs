using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float speed;
	public float accSpeed;
	public float maxSpeed;
	public float turnspeed;

	public Transform[] weaponSlots;
	public Transform[] equipmentSlots;
	public ParticleSystem[] exhausts;

	public GameObject manager;
	public StatsManager stats;

	// Use this for initialization
	void Start () {

		manager = GameObject.FindGameObjectWithTag("Stats");
		stats = manager.GetComponent<StatsManager>();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (stats.mouseControl == false) {

			//Rotate craft
			Vector3 torque = new Vector3 (0,0,Input.GetAxis ("Horizontal"));
			rigidbody.AddTorque(torque * turnspeed * Time.deltaTime);

			//Speed up craft
			speed = Vector3.Distance (new Vector3(0,0,0),rigidbody.velocity);
			rigidbody.AddForce(transform.right * Input.GetAxis ("Vertical") * Time.deltaTime * accSpeed);

			//Brake craft
			if (Input.GetButton ("Brake")) {
				rigidbody.velocity /= accSpeed;
			}

			/*foreach (ParticleSystem e in exhausts) {
				if (Input.GetAxis ("Vertical") > 0) {
					e
				}else{
					e.isPlaying = false;
				}
			}*/
		
			if (Input.GetButton ("Jump")) {
				foreach(Transform s in weaponSlots) {
					foreach(Transform w in s) {
						w.SendMessage ("Fire");
					}
				}
			}
		}
	}
}
