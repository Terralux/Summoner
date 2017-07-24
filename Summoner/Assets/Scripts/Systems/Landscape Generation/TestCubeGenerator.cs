using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCubeGenerator : MonoBehaviour {

	private MeshGenerator mg;
	public int configuration = 0;
	private MeshFilter mf;

	public bool neighbouringCubesActive = false;

	void Awake(){
		mg = new MeshGenerator();
		mf = GetComponent<MeshFilter>();

		if(mf == null){
			mf = gameObject.AddComponent<MeshFilter>();
		}
		CreateCube();
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			configuration++;
			CreateCube();
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			configuration--;
			CreateCube();
		}

		if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)){
			CreateCube();
		}

		if(Input.GetKeyDown(KeyCode.Return)){
			neighbouringCubesActive = !neighbouringCubesActive;
			CreateCube();
		}
	}

	void CreateCube(){
		mg.vertices.Clear();
		mg.triangles.Clear();

		bool tfl = false;
		bool tfr = false;
		bool tbr = false;
		bool tbl = false;
		bool bfl = false;
		bool bfr = false;
		bool bbr = false;
		bool bbl = false;

		int tempConfig = configuration;

		if(tempConfig >= 128){
			tempConfig -= 128;
			bbl = true;
		}
		if(tempConfig >= 64){
			tempConfig -= 64;
			bbr = true;
		}
		if(tempConfig >= 32){
			tempConfig -= 32;
			bfr = true;
		}
		if(tempConfig >= 16){
			tempConfig -= 16;
			bfl = true;
		}
		if(tempConfig >= 8){
			tempConfig -= 8;
			tbl = true;
		}
		if(tempConfig >= 4){
			tempConfig -= 4;
			tbr = true;
		}
		if(tempConfig >= 2){
			tempConfig -= 2;
			tfr = true;
		}
		if(tempConfig >= 1){
			tempConfig -= 1;
			tfl = true;
		}

		Cube testCube = new Cube(
			new ControlNode(new Vector3(0, 0, 0), tfl, 1),
			new ControlNode(new Vector3(1, 0, 0), tfr, 1),
			new ControlNode(new Vector3(1, 0, -1), tbr, 1),
			new ControlNode(new Vector3(0, 0, -1), tbl, 1),
			new ControlNode(new Vector3(0, -1, 0), bfl, 1),
			new ControlNode(new Vector3(1, -1, 0), bfr, 1),
			new ControlNode(new Vector3(1, -1, -1), bbr, 1),
			new ControlNode(new Vector3(0, -1, -1), bbl, 1)
		);

		if(neighbouringCubesActive){
			mg.CreateMeshUsingSwitchCase(
				new Cube(
					new ControlNode(new Vector3(0, 0, 0), tfl, 1),
					new ControlNode(new Vector3(1, 0, 0), tfr, 1),
					new ControlNode(new Vector3(1, 0, -1), tbr, 1),
					new ControlNode(new Vector3(0, 0, -1), tbl, 1),
					new ControlNode(new Vector3(0, -1, 0), bfl, 1),
					new ControlNode(new Vector3(1, -1, 0), bfr, 1),
					new ControlNode(new Vector3(1, -1, -1), bbr, 1),
					new ControlNode(new Vector3(0, -1, -1), bbl, 1)
				), new Cube(), new Cube(), new Cube(), new Cube(), new Cube(), new Cube()
			);
		}else{
			mg.CreateMeshUsingSwitchCase(
				new Cube(
					new ControlNode(new Vector3(0, 0, 0), tfl, 1),
					new ControlNode(new Vector3(1, 0, 0), tfr, 1),
					new ControlNode(new Vector3(1, 0, -1), tbr, 1),
					new ControlNode(new Vector3(0, 0, -1), tbl, 1),
					new ControlNode(new Vector3(0, -1, 0), bfl, 1),
					new ControlNode(new Vector3(1, -1, 0), bfr, 1),
					new ControlNode(new Vector3(1, -1, -1), bbr, 1),
					new ControlNode(new Vector3(0, -1, -1), bbl, 1)
				), testCube, testCube, testCube, testCube, testCube, testCube
			);
		}

		Mesh mesh = new Mesh();
		mf.mesh = mesh;

		mesh.vertices = mg.vertices.ToArray();
		mesh.triangles = mg.triangles.ToArray();
		mesh.RecalculateNormals();
	}
}