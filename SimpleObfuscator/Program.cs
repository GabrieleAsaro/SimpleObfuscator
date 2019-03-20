using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Text;

namespace SimpleObfuscator {
	class Program {
		static void Main(string[] args) {

			//Getting the file you want to obfuscate
			var module = ModuleDefMD.Load(Console.ReadLine());

			try {

				//Getting all the classes
				foreach (var types in module.GetTypes()) {

					//Getting all methods inside the classes
					foreach (var methods in types.Methods) {

						//If a method doesn't have anything inside it will just ignore it and pass to other methods
						if (!methods.HasBody)
							continue;

						methods.Body.SimplifyBranches();

						//For all the instructions of the method:
						for (int i = 0; i < methods.Body.Instructions.Count; i++) {

							//If the instruction is a string(Ldstr = string) if you want to learn opcodes(https://en.wikipedia.org/wiki/List_of_CIL_instructions):
							if (methods.Body.Instructions[i].OpCode == OpCodes.Ldstr) {

								//Getting the original string
								var stringg = methods.Body.Instructions[i].Operand.ToString();

								//Converting the original string to Base64(https://en.wikipedia.org/wiki/Base64)
								var newString = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(stringg));

								Console.ForegroundColor = ConsoleColor.Magenta;
								Console.WriteLine("Obfuscating...");

								//Change the Opcode for the Original Instruction
								methods.Body.Instructions[i].OpCode = OpCodes.Nop;

								//get Method (get_UTF8) from Type (System.Text.Encoding)
								methods.Body.Instructions.Insert(i + 1, new Instruction(OpCodes.Call, module.Import(typeof(System.Text.Encoding).GetMethod("get_UTF8", new Type[] { }))));

								//add the Encrypted String
								methods.Body.Instructions.Insert(i + 2, new Instruction(OpCodes.Ldstr, newString));

								//get Method (FromBase64String) from Type (System.Convert), and arguments for method we will get it using "new Type[] { typeof(string) }"
								methods.Body.Instructions.Insert(i + 3, new Instruction(OpCodes.Call, module.Import(typeof(System.Convert).GetMethod("FromBase64String", new Type[] { typeof(string) }))));

								//get Method (GetString) from Type (System.Text.Encoding)
								methods.Body.Instructions.Insert(i + 4, new Instruction(OpCodes.Callvirt, module.Import(typeof(System.Text.Encoding).GetMethod("GetString", new Type[] { typeof(byte[]) }))));

								//skip the Instructions that we have just added
								i += 4;

								//saving the "obfuscated" file
								module.Write(Environment.CurrentDirectory + @"\obfuscated.exe");

								Console.ForegroundColor = ConsoleColor.Green;
								Console.WriteLine("Obfuscated");
							}
						}
					}
				}
			} catch (Exception ex) {

				//In case it gives some errors
				Console.WriteLine(ex.Message);
				Console.ReadKey();
			}
		}
	}
}