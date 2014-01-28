using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
[RequireComponent (typeof(MeshFilter))]
[RequireComponent (typeof(MeshCollider))]
[RequireComponent (typeof(MeshRenderer))]

public class TerrainGenerator : MonoBehaviour {

	public float noiseScale;
	public float heightScale;
	public float maxHeight;
	public int fractalAmount;
	public Vector3 startingPos;

	public int sizeX;
	public int sizeY;
	public float tileSize;
	public int mapDetail;
	public Texture2D heightmap;
	public float terrainOffsetZ;

	Vector3[] verts;
	Vector3[] norms;
	Vector2[] uvs;
	int[] tris;

	void Start () {
		BuildMesh();
	}

	// Use this for initialization
	public void BuildMesh () {

		int numTiles = sizeX * sizeY;
		int numTris = numTiles * 2;

		int vsizeX = sizeX + 1;
		int vsizeY = sizeY + 1;
		int numVerts = vsizeX * vsizeY;

		verts = new Vector3[numVerts];
		norms = new Vector3[numVerts];
		uvs = new Vector2[numVerts];

		tris = new int[numTris * 3];

		int x, y;
		for (y = 0; y < sizeY; y++) {
			for (x = 0; x < sizeX; x++) {
				verts[y * vsizeX + x] = new Vector3(x * tileSize,y * tileSize,0);
				norms[y * vsizeX + x] = -Vector3.forward;
				uvs  [y * vsizeX + x] = new Vector2((float)x / vsizeX,(float)y / vsizeY);
			}
		}

		Debug.Log ("Done vertices!");

		for (y = 0; y < sizeY; y++) {
			for (x = 0; x < sizeX; x++) {
				int squareIndex = y * sizeX + x;
				int triOffset = squareIndex * 6;

				tris[triOffset + 0] = y * vsizeX + x + vsizeX + 0;
				tris[triOffset + 1] = y * vsizeX + x + vsizeX + 1;
				tris[triOffset + 2] = y * vsizeX + x + 0;
				
				tris[triOffset + 3] = y * vsizeX + x + vsizeX + 1;
				tris[triOffset + 4] = y * vsizeX + x + 1;
				tris[triOffset + 5] = y * vsizeX + x + 0;
			}
		}

		Debug.Log ("Done triangles!");


		Mesh newMesh = new Mesh();

		//heightmap = GenerateHeightmap(sizeX,sizeY,mapDetail);
		GenerateHeight();

		Vector3 meshCenter = newMesh.bounds.extents;
		newMesh.vertices = verts;
		newMesh.triangles = tris;
		newMesh.normals = norms;
		newMesh.uv = uvs;
		
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
		MeshCollider meshCollider = GetComponent<MeshCollider>();
		
		meshFilter.mesh = newMesh;
		meshCollider.sharedMesh = newMesh;

		transform.position = meshCenter;

		Debug.Log ("Done mesh!");
	}

	void GenerateHeight () {

		for (int i=0;i<verts.Length;i++) {

			int x = heightmap.width/sizeX * (int)verts[i].x;
			int y = heightmap.height/sizeY * (int)verts[i].y;
			//Debug.Log (x + ", " + y);
			float newZ = heightmap.GetPixel(x, y).grayscale * heightScale;
			verts[i] += new Vector3 (0,0,newZ);

		}

		Debug.Log ("Done height!");

	}

	Texture2D GenerateHeightmap (int texWidth, int texHeight, int factor) {

		Texture2D tex = new Texture2D(texWidth*factor,texHeight*factor);
		for (int y = 0; y < texHeight; y++) {
			for (int x = 0; x < texWidth; x++) {
				float noise = GetPerlinFractal(x,y,fractalAmount);
				tex.SetPixel (x,y,new Color(noise,noise,noise));
			}
		}

		tex.wrapMode = TextureWrapMode.Repeat;
		tex.filterMode = FilterMode.Bilinear;
		tex.Apply ();
		Debug.Log ("Done texture!");
		renderer.material.mainTexture = tex;
		return tex;

	}

	float GetPerlinFractal (int x, int y, int amount) {
		float value = 0;

		int intAmount = amount;
		while (intAmount > 0) {
			value += Mathf.PerlinNoise (x / noiseScale,y / noiseScale);
			intAmount--;
		}
		value /= amount;
		return value;
	}
}
