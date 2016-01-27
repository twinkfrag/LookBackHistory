using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookBackHistory.Utils
{
	public static class StringEx
	{
		public static string GetOrDefault(this string data, string orElse = null)
		{
			return !string.IsNullOrEmpty(data) ? data : (orElse ?? string.Empty);
		}
	}
}
