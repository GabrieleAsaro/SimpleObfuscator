using System;
using dnlib.DotNet;
using SimpleObfuscator.Core.Protections;
using SimpleObfuscator.Core.Protections.AddRandoms;

class Program {

	static void Main() {
		var module = ModuleDefMD.Load(Console.ReadLine());
		Execute(module);
		module.Write(Environment.CurrentDirectory + @"\protected.exe");
	}

	static void Execute(ModuleDefMD module)
	{
		Renamer.Execute(module);
		RandomOutlinedMethods.Execute(module);
	}
}