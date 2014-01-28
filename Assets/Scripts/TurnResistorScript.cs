using UnityEngine;
using System.Collections;

public class TurnResistorScript : MonoBehaviour {

	public Transform parentUnit;
	public float stabilityFactor;

	// Use this for initialization
	void Start () {

		parentUnit = transform;
		while (parentUnit.parent) {
			parentUnit = parentUnit.parent;
		}
	}
	
	// Update is called once per frame
	void Update () {

		parentUnit.rigidbody.angularDrag = stabilityFactor;
	
	}
}
