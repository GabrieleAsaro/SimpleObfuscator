using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Text;

namespace SimpleObfuscator {
	class Program {
		static void Main(string[] args) {
			var module = ModuleDefMD.Load(Console.ReadLine()); //Getting the file you want to obfuscate
			try {
				foreach (var types in module.GetTypes()) { //Getting all the classes
					foreach (var methods in types.Methods) { //Getting all methods inside the classes
						if (!methods.HasBody)
							continue; //If a method doesn't have anything inside it will just ignore it and pass to other methods
						methods.Body.SimplifyBranches();
						for (int i = 0; i < methods.Body.Instructions.Count; i++) { //For all the instructions of the method:
							if (methods.Body.Instructions[i].OpCode == OpCodes.Ldstr) { //If the instruction is a string(Ldstr = string) if you want to learn opcodes(https://en.wikipedia.org/wiki/List_of_CIL_instructions):
								var stringg = methods.Body.Instructions[i].Operand.ToString(); //Getting the original string
								var newString = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(stringg)); //Converting the original string to Base64(https://en.wikipedia.org/wiki/Base64)
								Console.ForegroundColor = ConsoleColor.Magenta;
								Console.WriteLine("Obfuscating...");
								methods.Body.Instructions[i].OpCode = OpCodes.Nop; //Change the Opcode for the Original Instruction
								methods.Body.Instructions.Insert(i + 1, new Instruction(OpCodes.Call, module.Import(typeof(System.Text.Encoding).GetMethod("get_UTF8", new Type[] { })))); //get Method (get_UTF8) from Type (System.Text.Encoding)
								methods.Body.Instructions.Insert(i + 2, new Instruction(OpCodes.Ldstr, newString)); //add the Encrypted String
								methods.Body.Instructions.Insert(i + 3, new Instruction(OpCodes.Call, module.Import(typeof(System.Convert).GetMethod("FromBase64String", new Type[] { typeof(string) })))); //get Method (FromBase64String) from Type (System.Convert), and arguments for method we will get it using "new Type[] { typeof(string) }"
								methods.Body.Instructions.Insert(i + 4, new Instruction(OpCodes.Callvirt, module.Import(typeof(System.Text.Encoding).GetMethod("GetString", new Type[] { typeof(byte[]) }))));
								i += 4; //skip the Instructions that we have just added
								module.Write(Environment.CurrentDirectory + @"\obfuscated.exe"); //saving the "obfuscated" file
								Console.ForegroundColor = ConsoleColor.Green;
								Console.WriteLine("Obfuscated");
							}
						}
					}
				}
			} catch (Exception ex) {
				Console.WriteLine(ex.Message);
				Console.ReadKey();
			}
		}
	}
}