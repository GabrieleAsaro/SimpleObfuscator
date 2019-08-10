using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SimpleObfuscator.Core.Utils
{
	internal class Randoms
	{
		/// <summary>
		/// const string chars = "ABCD1234"; || We are putting random letters.
		/// return new string(Enumerable.Repeat(chars, 10).Select(s => s[new Random(Guid.NewGuid().GetHashCode()).Next(s.Length)]).ToArray()); || We are returning a random string with length 10.
		/// </summary>
		public static string RandomString()
		{
			const string chars = "ABCD1234";
			return new string(Enumerable.Repeat(chars, 10)
				.Select(s => s[new Random(Guid.NewGuid().GetHashCode()).Next(s.Length)]).ToArray());
		}

		/// <summary>
		/// var ints = Convert.ToInt32(Regex.Match(Guid.NewGuid().ToString(), @"\d+").Value); || We are generating a random integer.
		/// return new Random(ints).Next(0, 99999999); || We are returning to a random integer, with a minimum value of 0 and a maximum value of 99999999.
		/// </summary>
		public static int RandomInt()
		{
			var ints = Convert.ToInt32(Regex.Match(Guid.NewGuid().ToString(), @"\d+").Value);
			return new Random(ints).Next(0, 99999999);
		}
	}
}