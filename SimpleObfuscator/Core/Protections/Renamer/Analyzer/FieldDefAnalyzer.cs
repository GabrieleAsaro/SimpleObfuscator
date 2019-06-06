namespace SimpleObfuscator.Core.Protections.Analyzer
{
	public class FieldDefAnalyzer : iAnalyze
	{
		public override bool Execute(object context)
		{
			dnlib.DotNet.FieldDef field = (dnlib.DotNet.FieldDef)context;
			if (field.IsRuntimeSpecialName)
				return false;
			if (field.IsLiteral && field.DeclaringType.IsEnum)
				return false;
			return true;
		}
	}
}