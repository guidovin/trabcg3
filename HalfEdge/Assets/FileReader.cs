using System;
using System.Text;
using System.IO;  
using UnityEngine;
using System.Collections;

public class FileReader {

	public string fileName;
	public string[] entries;
	public Polygon poly;

		
	private bool Load(string fileName)
	{
		// Tipo de leitura que o leitor está fazendo:
		// false => vertices
		// true  => half edges
		bool readingType = false;

		int[] nexts = new int[] {0};
		int[] twins = new int[] {0};

		// Handle any problems that might arise when reading the text
		try
		{
			string line;
			// Create a new StreamReader, tell it which file to read and what encoding the file
			// was saved as
			StreamReader theReader = new StreamReader(fileName, Encoding.Default);
			// Immediately clean up the reader after this block of code is done.
			// You generally use the "using" statement for potentially memory-intensive objects
			// instead of relying on garbage collection.
			// (Do not confuse this with the using directive for namespace at the 
			// beginning of a class!)
			using (theReader)
			{
				do // WHILE there's lines left in the text file, do this:
				{
					line = theReader.ReadLine();

					if (line == "#VERTICES"){
						readingType = false;
					}
					else if(line == "#HALFEDGES") {
						readingType = true;
					}
					else if (line != null)
					{
						string[] entries = line.Split(' '); //separa os valores na linha por espaço

						if (entries.Length > 0)
						{
							int index = int.Parse(entries[0]);

							if(!readingType) // trata vertices
							{
								
								poly.vertices[index] = 
									new Vertex(
										float.Parse(entries[1]),
										float.Parse(entries[2]), 
										float.Parse(entries[3]) 
									);
							}
							else //trata half edges
							{
								int vertexIndex = int.Parse(entries[1]);
								int twinIndex  = int.Parse(entries[2]);
								int nextIndex  = int.Parse(entries[3]);
								int faceIndex = int.Parse(entries[4]);

								HEdge hEdge = new HEdge();

								hEdge.vert = poly.vertices[vertexIndex];

								twins[index] = twinIndex;
								nexts[index] = nextIndex;

								if(poly.faces[faceIndex] != null) 
									hEdge.face = poly.faces[faceIndex];
								else
									hEdge.face = poly.faces[faceIndex] = new Face(hEdge);

								poly.vertices[vertexIndex].hEdge = 
									poly.halfEdges[index] = 
										hEdge;
							}
						}
					}
				}
				while (line != null);

				// Done reading, close the reader
				theReader.Close();

				for(int i = 1; i < twins.Length; i++)
				{
					poly.halfEdges[i].twin = poly.halfEdges[twins[i]];
					poly.halfEdges[i].next = poly.halfEdges[nexts[i]];
				}

				return true;
			}
		}
		catch (Exception e)
		{
		// If anything broke in the try block, we throw an exception with information
		// on what didn't work

			Console.WriteLine("{0}\n", e.Message);
			return false;
		}
	}
}
