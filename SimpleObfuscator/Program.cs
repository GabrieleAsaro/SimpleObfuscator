using dnlib.DotNet;
using SimpleObfuscator.Core.Protections;
using SimpleObfuscator.Core.Protections.AddRandoms;
using System;

internal class Program
{
	/// <summary>
	/// ModuleDefMD module = ModuleDefMD.Load(Console.ReadLine()); || We are getting the file path by reading the console.
	/// Execute(module); || We are obfuscating the file.
	/// module.Write(Environment.CurrentDirectory + @"\protected.exe"); || We are rewriting the file in the current directory.
	/// </summary>
	private static void Main()
	{
		ModuleDefMD module = ModuleDefMD.Load(Console.ReadLine());
		Execute(module);
		module.Write(Environment.CurrentDirectory + @"\protected.exe");
	}

	/// <summary>
	/// Renamer.Execute(module); || We are exectuing the obfuscation method 'Renamer'.
	/// RandomOutlinedMethods.Execute(module); || We are exectuing the obfuscation method 'RandomOutlinedMethods'.
	/// </summary>
	private static void Execute(ModuleDefMD module)
	{
		Renamer.Execute(module: module);
		RandomOutlinedMethods.Execute(module: module);
	}
}