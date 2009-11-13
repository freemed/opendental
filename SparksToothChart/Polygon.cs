/*
using System;
using System.Collections.Generic;
using System.Text;

namespace SparksToothChart {
	///<summary>A series of vertices that are all connected into one triangle.  Just used for BU/post.</summary>
	public class Triangle {
		public List<Vertex3f> Vertices;

		///<summary>Specify a series of triangles as a series of points.  It's implied that they are grouped by threes.</summary>
		public Triangle(params float[] coords) {
			Vertices=new List<Vertex3f>();
			Vertex3f vertex=new Vertex3f();
			for(int i=0;i<coords.Length;i++) {
				vertex.X=coords[i];
				i++;
				vertex.Y=coords[i];
				i++;
				vertex.Z=coords[i];
				Vertices.Add(vertex);
				vertex=new Vertex3f();
			}
		}
	}
}*/
