using UnityEngine;
using System.Collections;

public class HEMeshConstructor : MonoBehaviour {

	public string fileName;

	// Use this for initialization
	void Start () {

		MeshFilter mf = GetComponent<MeshFilter> ();
		Mesh mesh = new Mesh ();

		FileReader reader = new FileReader ();

		if (reader.load (fileName)) 
		{
			Polygon poly = reader.poly;

			Vector3[] vecVerts = new Vector3[] {};
			int[] triangles = new int[] {};

			int i = 0;
			foreach (Vertex vert in poly.vertices)
			{
				vecVerts[i] = vert.asVector();
				i++;
			}

			foreach (Face face in poly.faces) 
			{
				Vertex[] verts = face.getVertices();
				for(int k = 0; i < 3; i++)
				{
					triangles[triangles.Length] = verts[i].index - 1;
				}
			}

			mesh.vertices = vecVerts;
			mesh.triangles = triangles;
			mesh.RecalculateNormals ();

			mf.mesh = mesh;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}