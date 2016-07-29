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

	public HEdge[] getNeighborHEdges()//eu supus que eram arestas vizinhas a um vertice mas para pegar os vizinhos de uma aresta basta encontrar as arestas vizinhas a ambos os vertices da aresta usando hedge.vert e hedge.next.vert. e ignorar a aresta original(q seria contada) para as arestas vizinhas a uma face fazemos para todas as suas arestas começando e terminando na mesma aresta na iteração.
	{
		int i = 0;
		HEdge[] a;
		HEdge HE;
		HE = this.hEdge;
		while(TRUE){
			if(HE == a[0] && i != 0){
			
				break;			
			}
			a[i]=HE;
			i++;
			HE = HE.twin;
			a[i] = HE; //conta as twins como vizinhas tbm mas da pra tirar
			i++;
			HE = HE.next;
		}
		return a;	
	}
	
	public Vertex[] getNeighborVertices()// mesma logica da função anterior, supus que eram vizinhos de um vertice mas para os vertices vizinhos de uma aresta aplica-se o algoritmo para os dois vertices e ignora o vizinho q compartilha a aresta alvo(usando novamente hedge.vert e hedge.next.vert e para os vertices "vizinhos" a uma face aplicamos o alg para cada aresta de f (usando f.hEdge e sucessivos f.hEdge.next) 
	{	
		int i = 0;
		Vertex[] v;
		HEdge HE;
		HE = this.hEdge; //ou HE = this.hEdge.twin n sei se funciona
		HE = HE.twin;	 //
		while(TRUE){
			if(HE.vert == v[0] && i != 0){
			
				break;			
			}
			v[i] = HE.vert;
			i++;
			HE = HE.next.twin;//separar se n funcionar
		}
		return v;	
	}
	public Face[] getNeighborFaces() 
	{
		int i = 0;
		Face[] faces;
		HEdge HE;
		HE = this.hEdge; //ou HE = this.hEdge.twin n sei se funciona
		HE = HE.twin;	 //
		while(TRUE){
			if(HE == this.hEdge && i != 0){
			
				break;			
			}
			faces[i] = HE.face;
			i++;
			HE = HE.next.twin;//separar se n funcionar
		}
		return faces;	
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
		faces [1] = this.twin.face; 
		return faces;
	}


	//Complete as funções a partir daqui (só no corpo das func, 
	// não recebe parametro pq isso é método de instancia, ou seja vc quer encontrar os referentes ao "this") :

	

	
	public HEdge[] getNeighborHEdges()//para arestas
	{
		Vertex v1, v2;
		v1= this.vert;
		v2 = this.next.vert;
		
		int i = 0;
		HEdge[] a;
		HEdge HE;
		HE = v1.hEdge;
		if((HE.vert == v1 && HE.next.vert == v2) || (HE.vert== v2 && HE.next.vert == v1)){
			a[i]=HE;
			i++;
		}
		while(TRUE){
			if(HE == a[0] && i != 0){
			
				break;			
			}
			if((HE.vert == v1 && HE.next.vert == v2) || (HE.vert== v2 && HE.next.vert == v1)){
				a[i]=HE;
				i++;
			}

			HE = HE.twin;

			if(!((HE.vert == v1 && HE.next.vert == v2) || (HE.vert== v2 && HE.next.vert == v1))){
				a[i]=HE;
				i++;
			}
			HE = HE.next;
		} 
		int k = 0;
		HE = v2.hEdge;
		while(TRUE){
			if(HE == a[i] && k != 0){
			
				break;			
			}
			if(!((HE.vert == v1 && HE.next.vert == v2) || (HE.vert== v2 && HE.next.vert == v1))){
				a[i+k]=HE;
				k++;
			}

			HE = HE.twin;

			if((HE.vert == v1 && HE.next.vert == v2) || (HE.vert== v2 && HE.next.vert == v1)){
				a[i+k]=HE;
				k++;
			}
			HE = HE.next;
		}


		return a;	
	}

	public Vertex[] getNeighborVertices()// mesma logica da função anterior, supus que eram vizinhos de um vertice mas para os vertices vizinhos de uma aresta aplica-se o algoritmo para os dois vertices e ignora o vizinho q compartilha a aresta alvo(usando novamente hedge.vert e hedge.next.vert e para os vertices "vizinhos" a uma face aplicamos o alg para cada aresta de f (usando f.hEdge e sucessivos f.hEdge.next) 
	{	
		Vertex v1, v2;
		v1 = this.vert;
                v2 = this.twin.vert;
		int k = 0;
		int i = 0;
		Vertex[] v;
		HEdge HE;
		HE = v1.hEdge; //ou HE = this.hEdge.twin n sei se funciona
		HE = HE.twin;	 //
		while(TRUE){
			if(HE.vert == v[0] && i != 0){
			
				break;			
			}
			if(!(HE.vert == v2)){
				v[i] = HE.vert;
				i++;
			}
			HE = HE.next.twin;//separar se n funcionar
		}
		while(TRUE){
			if(HE.vert == v[i] && k != 0){
			
				break;			
			}
			if(!(HE.vert == v1)){
				v[i+k] = HE.vert;
				k++;
			}
			HE = HE.next.twin;//separar se n funcionar
		}
		
		return v;	
	}
	
	public Face[] getNeighborFaces() 
	{
		
	}
}
