using AssetRipper.Text.SourceGeneration;
using Microsoft.CodeAnalysis;
using SGF;
using System.CodeDom.Compiler;

namespace AssetRipper.IO.Endian.Tests.SourceGenerator;

[IncrementalGenerator]
public sealed class TestGenerator : IncrementalGenerator
{
	private const string Half = "Half";
	private static readonly List<(string, string)> list = new()
	{
		(nameof(Int16), "short"),
		(nameof(UInt16), "ushort"),
		(nameof(Int32), "int"),
		(nameof(UInt32), "uint"),
		(nameof(Int64), "long"),
		(nameof(UInt64), "ulong"),
		(nameof(Half), nameof(Half)),
		(nameof(Single), "float"),
		(nameof(Double), "double"),
		(nameof(Boolean), "bool"),
		(nameof(Byte), "byte"),
		(nameof(SByte), "sbyte"),
		(nameof(Char), "char"),
	};

	public TestGenerator() : base(nameof(TestGenerator))
	{
	}

	public override void OnInitialize(SgfInitializationContext context)
	{
		context.RegisterPostInitializationOutput(AddGeneratedCode);
	}

	private static void AddGeneratedCode(IncrementalGeneratorPostInitializationContext context)
	{
		// EndianSpanTests
		{
			using StringWriter stringWriter = new() { NewLine = "\n" };
			using IndentedTextWriter writer = IndentedTextWriterFactory.Create(stringWriter);
			EndianSpanTests(writer);

			context.AddSource($"EndianSpanTests.g.cs", stringWriter.ToString());
		}

		// EndianSpanReaderTests
		{
			using StringWriter stringWriter = new() { NewLine = "\n" };
			using IndentedTextWriter writer = IndentedTextWriterFactory.Create(stringWriter);
			EndianSpanReaderTests(writer);

			context.AddSource($"EndianSpanReaderTests.g.cs", stringWriter.ToString());
		}
	}

	private static void EndianSpanTests(IndentedTextWriter writer)
	{
		writer.WriteGeneratedCodeWarning();
		writer.WriteFileScopedNamespace("AssetRipper.IO.Endian.Tests");
		writer.WriteLine();
		writer.WriteLine($"public partial class EndianSpanTests");
		using (new CurlyBrackets(writer))
		{
			bool first = true;
			foreach ((string typeName, string keyWord) in list)
			{
				if (first)
				{
					first = false;
				}
				else
				{
					writer.WriteLineNoTabs();
				}
				AddTestMethod(writer, typeName, keyWord);
			}
			AddGenericTestMethod(writer);
		}
	}

	/// <summary>
	/// <code>
	/// [Theory]
	/// public void BooleanTest(EndianType endianType)
	/// {
	///     byte[] data = new byte[sizeof(bool)];
	///     bool value1 = RandomData.NextBoolean();
	/// 
	///     EndianSpanWriter writer = new EndianSpanWriter(data, endianType);
	///     Assert.That(writer.Length, Is.EqualTo(sizeof(bool));
	///     writer.Write(value1);
	///     Assert.That(writer.Position, Is.EqualTo(sizeof(bool)));
	/// 
	///     EndianSpanReader reader = new EndianSpanReader(data, endianType);
	///     Assert.That(reader.Length, Is.EqualTo(sizeof(bool));
	///     bool value2 = reader.ReadBoolean();
	///     Assert.That(reader.Position, Is.EqualTo(sizeof(bool)));
	///     Assert.That(value2, Is.EqualTo(value1));
	/// }
	/// </code>
	/// </summary>
	/// <param name="writer"></param>
	/// <param name="typeName"></param>
	/// <param name="parameterType"></param>
	/// <param name="bigEndian"></param>
	private static void AddTestMethod(IndentedTextWriter writer, string typeName, string parameterType)
	{
		const string endianArgumentName = "endianType";
		writer.WriteLine("[Theory]");
		writer.WriteLine($"public void {typeName}Test(EndianType {endianArgumentName})");
		using (new CurlyBrackets(writer))
		{
			writer.WriteLine($"byte[] data = new byte[{SizeOfExpression(parameterType)}];");
			writer.WriteLine($"{parameterType} value1 = RandomData.Next{typeName}();");
			writer.WriteLineNoTabs();
			writer.WriteLine($"EndianSpanWriter writer = new EndianSpanWriter(data, {endianArgumentName});");
			writer.WriteLine($"Assert.That(writer.Length, Is.EqualTo({SizeOfExpression(parameterType)}));");
			writer.WriteLine("writer.Write(value1);");
			writer.WriteLine($"Assert.That(writer.Position, Is.EqualTo({SizeOfExpression(parameterType)}));");
			writer.WriteLineNoTabs();
			writer.WriteLine($"EndianSpanReader reader = new EndianSpanReader(data, {endianArgumentName});");
			writer.WriteLine($"Assert.That(reader.Length, Is.EqualTo({SizeOfExpression(parameterType)}));");
			writer.WriteLine($"{parameterType} value2 = reader.Read{typeName}();");
			writer.WriteLine($"Assert.That(reader.Position, Is.EqualTo({SizeOfExpression(parameterType)}));");
			writer.WriteLine("Assert.That(value2, Is.EqualTo(value1));");
		}
	}

	private static void AddGenericTestMethod(IndentedTextWriter writer)
	{
		writer.WriteLineNoTabs();
		foreach ((_, string keyWord) in list)
		{
			writer.WriteLine($"[TestCase<{keyWord}>(EndianType.LittleEndian)]");
			writer.WriteLine($"[TestCase<{keyWord}>(EndianType.BigEndian)]");
		}
		writer.WriteLine("public partial void TestGenericReadWrite<T>(EndianType endianType) where T : unmanaged;");
	}

	private static void EndianSpanReaderTests(IndentedTextWriter writer)
	{
		writer.WriteGeneratedCodeWarning();
		writer.WriteFileScopedNamespace("AssetRipper.IO.Endian.Tests");
		writer.WriteLine();
		writer.WriteLine($"public partial class EndianSpanReaderTests");
		using (new CurlyBrackets(writer))
		{
			foreach ((string typeName, string keyWord) in list)
			{
				// Try Read Empty Test
				writer.WriteLine("[Theory]");
				writer.WriteLine($"public void TryRead{typeName}_Empty(EndianType endianType)");
				using (new CurlyBrackets(writer))
				{
					writer.WriteLine($"EndianSpanReader reader = new EndianSpanReader([], endianType);");
					writer.WriteLine($"Assert.That(reader.TryRead{typeName}(out _), Is.False);");
				}
				writer.WriteLineNoTabs();

				// Try Read Full Test
				writer.WriteLine("[Theory]");
				writer.WriteLine($"public void TryRead{typeName}_Successful(EndianType endianType)");
				using (new CurlyBrackets(writer))
				{
					writer.WriteLine($"ReadOnlySpan<byte> data = stackalloc byte[{SizeOfExpression(keyWord)}];");
					writer.WriteLine($"EndianSpanReader reader = new EndianSpanReader(data, endianType);");
					writer.WriteLine($"Assert.That(reader.TryRead{typeName}(out _), Is.True);");
				}
				writer.WriteLineNoTabs();
			}
		}
	}

	private static string SizeOfExpression(string type)
	{
		return type is nameof(Half) ? "sizeof(ushort)" : $"sizeof({type})";
	}
}
