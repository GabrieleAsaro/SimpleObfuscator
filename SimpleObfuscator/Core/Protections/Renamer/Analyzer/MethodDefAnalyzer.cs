namespace SimpleObfuscator.Core.Protections.Analyzer
{
	public class MethodDefAnalyzer : iAnalyze
	{
		public override bool Execute(object context)
		{
			dnlib.DotNet.MethodDef method = (dnlib.DotNet.MethodDef)context;
			if (method.IsRuntimeSpecialName)
				return false;
			if (method.DeclaringType.IsForwarder)
				return false;
			return true;
		}
	}
}