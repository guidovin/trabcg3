using UnityEngine;
using System.Collections;

public class Polygon {
	public Vertex[] vertices;
	public HEdge[] halfEdges;
	public Face[] faces;
}

public class Vertex {

	//Vars
	public int index;
	public float x, y, z;
	public HEdge hEdge;

	//Functions

	//Constructor
	public Vertex(float x, float y, float z){
		this.x = x;		
		this.y = y;		
		this.z = z;
	}

	//Getter of vertex position on vector3 form
	public Vector3 asVector()
	{
		return new Vector3 (x,y,z);
	}
}


public class Face {

	public int index;
	public HEdge hEdge;

	public Face(HEdge hEdge)
	{
		this.hEdge = hEdge;
	}
}

public class HEdge {

	//Vars --
	public int index;
	public Vertex vert; //origin vertex
	public HEdge twin; //complementary pair
	public HEdge next; //next
	public Face face;

	//Constructors --

	public HEdge()
	{
	}

	public HEdge(Vertex vert, HEdge twin, HEdge next, Face face)
	{
		this.vert = vert;
		this.twin = twin;
		this.next = next;
		this.face = face;
	}

	// Esses são os exemplos mais simples q eu já fiz:

	public Vertex[] getVertExtremes()
	{
		Vertex[] vertices;
		vertices [0] = this.vert;
		vertices [1] = this.next.vert;
	}

	public Face[] getFaces()
	{
		Face[] faces;
		faces [0] = this.face;
		faces [0] = this.twin.face;
		return faces;
	}


	//Complete as funções a partir daqui (só no corpo das func, 
	// não recebe parametro pq isso é método de instancia, ou seja vc quer encontrar os referentes ao "this") :

	public Vertex[] getNeighborVertices()
	{
	}

	public HEdge[] getNeighborHEdges()
	{
	}

	public Face[] getNeighborFaces()
	{
	}
}
