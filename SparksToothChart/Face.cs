using System;
using System.Collections.Generic;
using System.Text;

namespace SparksToothChart {
	///<summary>A face is a single polygon, usually a rectangle.  Will soon be only a triangle.  The two lists here have a 1:1 relationship.</summary>
	public class Face {
		//public List<VertexNormal> VertexNormals;
		///<summary>A list of indices to the VertexNormal list contained in the ToothGraphic object.</summary>
		public List<int> IndexListVertices;
		///<summary>A list of indices to the VertexNormal list contained in the ToothGraphic object.</summary>
		public List<int> IndexListNormals;

		public Face() {
			//VertexNormals=new List<VertexNormal>();
			IndexListVertices=new List<int>();
			IndexListNormals=new List<int>();
		}

		/*
		public override string ToString() {
			string retVal="";
			for(int i=0;i<IndexList.Count;i++) {
				if(i>0) {
					retVal+=",";
				}
				retVal+=IndexList[i].ToString();
			}
			return retVal;
		}*/
	}
}
