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

		public static string CreateRandomAlphaNumericString(int length){
			string result="";
			Random rand=new Random();
			string randChrs="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			for(int i=0;i<length;i++){
				result+=randChrs[rand.Next(0,randChrs.Length-1)];
			}
			return result;
		}

	}
}
