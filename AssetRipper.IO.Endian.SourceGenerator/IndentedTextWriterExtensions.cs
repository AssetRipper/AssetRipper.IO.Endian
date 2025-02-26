using System.CodeDom.Compiler;

namespace AssetRipper.IO.Endian.SourceGenerator;

internal static class IndentedTextWriterExtensions
{
	public static void WriteAggressiveInliningAttribute(this IndentedTextWriter writer)
	{
		writer.WriteLine("[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]");
	}
}
