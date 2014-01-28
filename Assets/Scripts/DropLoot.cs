using UnityEngine;
using System.Collections;

public class DropLoot : MonoBehaviour {

	public GameObject[] drops;
	public int minAmount;
	public int maxAmount;
	public float minValue;
	public float maxValue;

	void OnDestroy () {

		int amount = Random.Range(minAmount,maxAmount+1);
		int ia = amount;
		while (ia > 0) {
			Vector3 ranPos = new Vector3 (Random.Range (0,0.2f),Random.Range (0,0.2f));
			GameObject nl = (GameObject)Instantiate(drops[Random.Range (0,drops.Length)],transform.position + ranPos,Quaternion.identity);
			LootCredits cs = nl.GetComponent<LootCredits>();
			ia--;
			if (cs) {
				cs.value = Random.Range (minValue,maxValue);
			}
		}
	}
}