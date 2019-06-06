using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SimpleObfuscator.Core.Utils
{
	class Randoms
	{
		public static string RandomString()
		{
			const string chars = "ABCD1234";
			return new string(Enumerable.Repeat(chars, 10)
				.Select(s => s[new Random(Guid.NewGuid().GetHashCode()).Next(s.Length)]).ToArray());
		}

		public static int RandomInt()
		{
			var ints = Convert.ToInt32(Regex.Match(Guid.NewGuid().ToString(), @"\d+").Value);
			return new Random(ints).Next(0, 99999999);
		}
	}
}