using System;
using System.Linq;
using dnlib.DotNet;

class Program {

	static void Main(string[] args) {
		var module = ModuleDefMD.Load(Console.ReadLine());
		Execute(module);
		module.Write(Environment.CurrentDirectory + @"\obfuscated.exe");
	}

	public static void Execute(ModuleDefMD md) {
		foreach (var type in md.GetTypes()) {
			type.Name = RandomString();

			foreach (var methods in type.Methods) {
				methods.Name = RandomString();

			foreach (var parameters in methods.Parameters)
				parameters.Name = RandomString();
			}

			foreach (var ty in type.Properties) {
				ty.Name = RandomString();
			}

			foreach (var field in type.Fields) {
				field.Name = RandomString();
			}
		}
	}

	private static string RandomString() {
		const string chars = "ABCD1234";
		return new string(Enumerable.Repeat(chars, 10)
			.Select(s => s[new Random(Guid.NewGuid().GetHashCode()).Next(s.Length)]).ToArray());
	}
}