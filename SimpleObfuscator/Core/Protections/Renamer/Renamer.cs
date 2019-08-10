using dnlib.DotNet;
using SimpleObfuscator.Core.Protections.Analyzer;
using SimpleObfuscator.Core.Utils;

namespace SimpleObfuscator.Core.Protections
{
	internal class Renamer : Randoms
	{
		/// <summary>
		/// We are executing the method 'Renamer'. The Renamer will rename name of { Types, Methods, Parameters, Properties, Fields }.
		/// </summary>
		public static void Execute(ModuleDefMD module)
		{
			foreach (var type in module.Types)
			{
				if (CanRename(type))
					type.Name = RandomString();

				foreach (var m in type.Methods)
				{
					if (CanRename(m))
						m.Name = RandomString();
					foreach (var para in m.Parameters)
						para.Name = RandomString();
				}
				foreach (var p in type.Properties)
				{
					if (CanRename(p))
						p.Name = RandomString();
				}
				foreach (var field in type.Fields)
				{
					if (CanRename(field))
						field.Name = RandomString();
				}
			}
		}

		/// <summary>
		/// We are checking with some Analyzers if it is possible to modify a determinate { TypeDef, MethodDef, EventDef, FieldDef }.
		/// return analyze.Execute(obj); || We are returning the execution of the renamer after checking if it is possible to modify a determinate { TypeDef, MethodDef, EventDef, FieldDef }.
		/// </summary>
		public static bool CanRename(object obj)
		{
			iAnalyze analyze = null;
			if (obj is TypeDef)
				analyze = new TypeDefAnalyzer();
			else if (obj is MethodDef)
				analyze = new MethodDefAnalyzer();
			else if (obj is EventDef)
				analyze = new EventDefAnalyzer();
			else if (obj is FieldDef)
				analyze = new FieldDefAnalyzer();
			if (analyze == null)
				return false;
			return analyze.Execute(obj);
		}
	}
}