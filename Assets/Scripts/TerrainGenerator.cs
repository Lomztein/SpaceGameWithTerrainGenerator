using UnityEngine;
using System.Collections;

public class TerrainGenerator : MonoBehaviour {

//	public float noiseScale;
//	public float heightScale;
//	public float maxHeight;
//	public int fractalAmount;

	public int mapSize;
	public int mapDetail;

	public bool generateNew;

	public Vector3[] vtp;

	void Start () {
		GenerateTerrainMesh();
	}

	// Use this for initialization
	void GenerateTerrainMesh () {

		Mesh newMesh = new Mesh();

		int vertCount = ((mapSize+1)*(mapSize+1));
		int tileCount = (mapSize*mapSize);
		int triCount = (tileCount*3);

		/*Debug.Log (vertCount + " verts!");
		Debug.Log (tileCount + " tiles!");
		Debug.Log (triCount/1.5f + " tris!");*/

		Vector3[] verts = new Vector3[vertCount];
		Vector3[] norms = new Vector3[verts.Length];
		int[] tris = new int[triCount*2];

		int index = 0;
		for (int x = 0; x < mapSize+1; x++) {
			for (int y = 0; y < mapSize+1; y++) {
				verts[index] = new Vector3 (x,y,transform.position.z);
				//Debug.Log (verts[index]);
				index++;
			}
		}

		Debug.Log (tris.Length);
		Debug.Log (triCount);

		int squareIndex = 0;
		for (int t = 0; t < tris.Length; t+=6) {

			tris[t] = squareIndex;
			tris[t+1] = squareIndex + 1;
			tris[t+2] = squareIndex + (mapSize) + 1;

			tris[t+3] = squareIndex + 1;
			tris[t+4] = squareIndex + mapSize;
			tris[t+5] = squareIndex + mapSize + 1;

			//Debug.Log (squareIndex);
			squareIndex++;
		
		}

		for (int n = 0; n < norms.Length; n++) {
			norms[n] = Vector3.up;
		}
		
		newMesh.vertices = verts;
		newMesh.triangles = tris;
		newMesh.normals = norms;
			
		MeshFilter meshFilter = GetComponent<MeshFilter>();
//		MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
		MeshCollider meshCollider = GetComponent<MeshCollider>();

		meshFilter.mesh = newMesh;
		meshCollider.sharedMesh = newMesh;

	}

	void OnDrawGizmos () {
		foreach (Vector3 v in vtp) {
			Gizmos.DrawSphere (v,0.25f);
		}
	}

	void Update () {
		if (generateNew == true) {
			GenerateTerrainMesh();
			generateNew = false;
		}
	}
}
