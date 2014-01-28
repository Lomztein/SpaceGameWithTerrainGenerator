using UnityEngine;
using System.Collections;

public class EnemyShipAI : MonoBehaviour {

	public float speed;
	public float accSpeed;
	public float maxSpeed;
	public float turnspeed;
	public float range;
	
	public Transform[] weaponSlots;
	public Transform[] equipmentSlots;
	public ParticleSystem[] exhausts;
	public Transform pointer;
	
	public GameObject manager;
	public StatsManager stats;

	public GameObject player;
	public Transform pp;
	public Vector3 targetPos;
	public float angle;

	// Use this for initialization
	void Start () {

		manager = GameObject.FindGameObjectWithTag("Stats");
		stats = manager.GetComponent<StatsManager>();

		if (!player) {
			player = stats.player;
		}

		pp = player.transform;
		Invoke ("GetRandomTargetPos",Random.Range (1.0f,2.0f));
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 targetDirection = targetPos - transform.position;
		Ray ray = new Ray(transform.position,targetDirection * Mathf.Infinity);
		RaycastHit hit;

		if (Physics.Raycast(ray,out hit) && hit.collider.gameObject.tag != "Player") {
			Debug.Log (angle);
			//GetRandomTargetPos();
		}

		Debug.DrawRay (transform.position,targetDirection);

		angle = 0;
		if (angle < 0) {
			rigidbody.AddTorque (new Vector3 (0,0,turnspeed));
		}
		if (angle > 0) {
			rigidbody.AddTorque (new Vector3 (0,0,-turnspeed));
		}

		float distance = Vector3.Distance (transform.position,targetPos);
		if (distance > range) {
			rigidbody.AddForce (transform.forward * accSpeed);
		}else{
			rigidbody.AddForce (-(transform.forward * accSpeed));
		}

		if (Vector3.Distance (transform.position,pp.position) < range) {
			targetPos = pp.position;
		}

		if (hit.collider.gameObject) {
			if (hit.collider.gameObject.tag == "Player") {
				foreach(Transform s in weaponSlots) {
					foreach(Transform w in s) {
						w.SendMessage ("Fire");
					}
				}
			}
		}
	}

	void OnDrawGizmos () {
		Gizmos.DrawSphere (targetPos,0.5f);
	}

	void GetRandomTargetPos () {

		targetPos = transform.position + new Vector3 (Random.Range (-range/2,range/2),Random.Range (-range/2,range/2),transform.position.z);
		Invoke ("GetRandomTargetPos",Random.Range (1.0f,2.0f));
			
	}
}
