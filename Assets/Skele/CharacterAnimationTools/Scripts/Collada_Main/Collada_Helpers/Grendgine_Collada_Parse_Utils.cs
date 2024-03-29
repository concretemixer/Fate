using System;

namespace grendgine_collada
{
	public class Grendgine_Collada_Parse_Utils
	{
		public static int[] String_To_Int(string int_array)
		{
			string[] str = int_array.Split(delis, StringSplitOptions.RemoveEmptyEntries);
			int[] array = new int[str.Length];
			try
			{
				for (long i = 0; i < str.Length; i++)
				{
					array[i] = Convert.ToInt32(str[i]);
				}
			}
			catch (Exception e)
			{
                Dbg.Log(e.ToString());
				//System.Console.WriteLine(e.ToString());
    //            System.Console.WriteLine();
    //            System.Console.WriteLine(int_array);
			}
			return array;
		}
		
		public static float[] String_To_Float(string float_array)
		{
            string[] str = float_array.Split(delis, StringSplitOptions.RemoveEmptyEntries);
			float[] array = new float[str.Length];
			try
			{
				for (long i = 0; i < str.Length; i++)
				{
					array[i] = Convert.ToSingle(str[i]);
				}
			}
			catch (Exception e)
			{
                Dbg.Log(e.ToString());
                //System.Console.WriteLine(e.ToString());
                //System.Console.WriteLine();
                //System.Console.WriteLine(float_array);
			}
			return array;
		}
	
		public static bool[] String_To_Bool(string bool_array)
		{
            string[] str = bool_array.Split(delis, StringSplitOptions.RemoveEmptyEntries);
			bool[] array = new bool[str.Length];
			try
			{
				for (long i = 0; i < str.Length; i++)
				{
					array[i] = Convert.ToBoolean(str[i]);
				}
			}
			catch (Exception e)
			{
                Dbg.Log(e.ToString());

                //System.Console.WriteLine(e.ToString());
                //System.Console.WriteLine();
                //System.Console.WriteLine(bool_array);
			}
			return array;
		}

        private static readonly char[] delis = new char[] { ' ', '\n', '\r' };
		
	}
}