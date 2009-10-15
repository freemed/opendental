using System;
using System.Collections.Generic;
using System.Text;

namespace SparksToothChart {
	///<summary>An RctLine is a series of point that are all connected into one continuous line to represent a root canal.  There can be more than one RctLine </summary>
	public class RctLine {
		public List<RctPoint> RctPoints;

		///<summary>Specify a line as a series of points.  It's implied that they are grouped by threes.</summary>
		public RctLine(params float[] coords) {
			RctPoints=new List<RctPoint>();
			RctPoint point=new RctPoint();
			for(int i=0;i<coords.Length;i++) {
				point.X=coords[i];
				i++;
				point.Y=coords[i];
				i++;
				point.Z=coords[i];
				RctPoints.Add(point);
				point=new RctPoint();
			}
		}
	}
}
