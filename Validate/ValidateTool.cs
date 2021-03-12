using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validate
{
	public class ValidateTool
	{
		public static bool IsValid()
		{
			var list = new List<string>() { "test", "fang" };
			var result = Newtonsoft.Json.JsonConvert.SerializeObject(list);
			return result == null;
		}
	}
}
