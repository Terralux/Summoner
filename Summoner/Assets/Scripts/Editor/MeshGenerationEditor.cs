using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MeshGenerationEditor : EditorWindow {

	public int width = 5;
	public int depth = 5;
	public int height = 5;

	public string seed = "Hello world!";
	public bool useRandomSeed = true;

	[Range(0,100)]
	public int randomFillPercent = 53;

	int[,] map;

	MeshFilter target;

	// Add menu named "My Window" to the Window menu
	[MenuItem("Window/Mesh Generation Tester")]
	static void Init()
	{
		// Get existing open window or if none, make a new one:
		MeshGenerationEditor window = (MeshGenerationEditor)EditorWindow.GetWindow(typeof(MeshGenerationEditor));
		window.Show();
	}

	void OnGUI()
	{
		GUILayout.Label("Target Object", EditorStyles.boldLabel);
		target = (MeshFilter) EditorGUILayout.ObjectField (target, typeof(MeshFilter));

		GUILayout.Label("Grid Size", EditorStyles.boldLabel);

		width = EditorGUILayout.IntSlider ("Width", width, 2, 30);
		depth = EditorGUILayout.IntSlider ("Depth", depth, 2, 30);
		height = EditorGUILayout.IntSlider ("Height", height, 2, 30);

		useRandomSeed = EditorGUILayout.BeginToggleGroup("Select a Seed", useRandomSeed);

		seed = EditorGUILayout.TextField ("Seed", seed);

		EditorGUILayout.EndToggleGroup();

		randomFillPercent = EditorGUILayout.IntSlider ("Fill percentage", randomFillPercent, 0, 100);

		EditorGUILayout.Space ();

		if (GUILayout.Button ("Generate")) {
			MapGenerator mapGen = new MapGenerator ();
			mapGen.width = width;
			mapGen.depth = depth;
			mapGen.height = height;

			mapGen.seed = useRandomSeed ? seed : System.DateTime.Now.Millisecond.ToString ();
			mapGen.useRandomSeed = useRandomSeed;

			mapGen.randomFillPercent = randomFillPercent;

			mapGen.Create (target);
		}
	}
}