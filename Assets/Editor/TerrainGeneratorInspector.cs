using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(TerrainGenerator))]

public class TerrainGeneratorInspector : Editor {

	public override void OnInspectorGUI () {

		base.OnInspectorGUI();
		if (GUILayout.Button ("Regenerate Mesh")) {
			TerrainGenerator tg = (TerrainGenerator)target;
			tg.BuildMesh();
		}
	}
}