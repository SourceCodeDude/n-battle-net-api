using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Test
{
	class Util
	{
		public static string ReadData(string file)
		{            
			using(StreamReader sr = new StreamReader(File.Open(file, FileMode.Open)))
            {
                return sr.ReadToEnd();
            }
		}
	}
}
