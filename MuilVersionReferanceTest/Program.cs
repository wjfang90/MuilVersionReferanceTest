using EsDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuilVersionReferanceTest
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				//依赖newtonsoft.json 10.0.1
				EsHelper.Init("192.168.0.168", 9200);

				//依赖newtonsoft.json 6.0.8
				Validate.ValidateTool.IsValid();

				//依赖newtonsoft.json 10.0.1
				EsHelper.Init("192.168.0.168", 9200);

			}
			catch (Exception ex)
			{
				Console.WriteLine("failure:");
				Console.WriteLine(ex.Message);
			}

			Console.WriteLine("referance muti version assembly success");

			Console.ReadKey();
		}
	}
	
}
