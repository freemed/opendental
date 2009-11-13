using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace SparksToothChart {
	///<summary>For 3D tooth graphics, this is a group of faces within a single tooth.  Different groups can be assigned different colors and visibility.  Groups might include enamel, the filling surfaces, pulp, canals, and cementum.  If a group does not apply, such as a F on a posterior tooth, then that tooth will not have that group.  The code must be resiliant enough to handle missing groups.  We might add more groups later and subdivide existing groups.  For instance pits, grooves, cusps, cervical areas, etc.  Over the years, this could get quite extensive and complex.  We would have to add a table to the database to handle additional groups painted by user.</summary>
	public class ToothGroup {
		///<summary></summary>
		public bool Visible;
		///<summary></summary>
		public Color PaintColor;
		///<summary></summary>
		public ToothGroupType GroupType;
		///<summary>dim 1=the face. dim 2=the vertex. dim 3 always has length=2, with 1st vertex, and 2nd normal.</summary>
		//public int[][][] Faces;
		public List<Face> Faces;
		///<summary>Corresponds to the Faces list. Indicies must be cached to draw DirectX triangles.</summary>
		public IndexBuffer facesDirectX;
		///<summary>Corresponds to the number of indicies referenced by the facesDirectX IndexBuffer. This relates to triangles, not to Polygons as in the Faces list. Must be a multiple of 3.</summary>
		public int NumIndicies=0;

		public ToothGroup() {
			Faces=new List<Face>();
		}

		public void PrepareForDirectX(Device device){
			if(facesDirectX!=null){
				facesDirectX.Dispose();
				facesDirectX=null;
			}
			//Prepare the indicies into an index buffer.
			//When drawing with a single index buffer inside of DirectX, all primitives must be the same type.
			//Furthermore, there are no polygons inside of DirectX, only triangles. Therefore, at this point
			//we break down all faces from polygons into triangles inside of the index buffer so all faces
			//can be drawn using triangles only. This will also allow us to optimize face order later to
			//make the faces display more quickly using caching techniques as well.
			List<int> indexList=new List<int>();
			for(int i=0;i<Faces.Count;i++) {
				for(int j=1;j<Faces[i].IndexList.Count-1;j++) {
					//We create a triangle fan out of the indcies here for simplicity, preserving the original polygon normal directionality.
					indexList.Add(Faces[i].IndexList[0]);
					indexList.Add(Faces[i].IndexList[j]);
					indexList.Add(Faces[i].IndexList[j+1]);
				}
			}
			int[] indicies=indexList.ToArray();
			facesDirectX=new IndexBuffer(typeof(int),indicies.Length,device,Usage.None,Pool.Managed);
			facesDirectX.SetData(indicies,0,LockFlags.None);
			NumIndicies=indicies.Length;
		}

		public override string ToString() {
			return GroupType.ToString()+". Faces:"+Faces.Count.ToString();
		}


	}

	///<summary></summary>
	public enum ToothGroupType{
		///<summary>0- This is areas of the tooth that are not included in any fillings at all.</summary>
		Enamel,
		///<summary>1</summary>
		Cementum,
		///<summary>2</summary>
		M,
		///<summary>3</summary>
		O,
		///<summary>4</summary>
		D,
		///<summary>5</summary>
		B,
		///<summary>6</summary>
		L,
		///<summary>7</summary>
		F,
		///<summary>8</summary>
		I,
		///<summary>9. class V. In addition to B or F</summary>
		V,
		///<summary>10. The pulp chamber and post or buildup.  Because our teeth are not yet transparent, these faces need to be translated forward.</summary>
		Buildup,
		///<summary>Only present in the special implant tooth object.</summary>
		Implant,
		///<summary>Not used. Just a placeholder</summary>
		Canals
	}
}
