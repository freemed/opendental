using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBase {
	public class MiscUtils {

		public static List <T> ArrayToList<T> (T[] array){
			List <T> list=new List<T> ();
			for(int i=0;i<array.Length;i++){
				list.Add(array[i]);
			}
			return list;
		}

	}
}
