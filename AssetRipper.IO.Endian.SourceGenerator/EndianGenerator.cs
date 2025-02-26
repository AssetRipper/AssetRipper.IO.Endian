using AssetRipper.Text.SourceGeneration;
using Microsoft.CodeAnalysis;
using SGF;
using System.Buffers.Binary;
using System.CodeDom.Compiler;

namespace AssetRipper.IO.Endian.SourceGenerator;

[IncrementalGenerator]
internal sealed class EndianGenerator : IncrementalGenerator
{
	private const string Half = "Half";
	private const string ReaderStructName = "EndianSpanReader";
	private const string WriterStructName = "EndianSpanWriter";
	private const string TargetNamespace = "AssetRipper.IO.Endian";
	private const string BigEndianField = "bigEndian";
	private const string OffsetField = "offset";
	private const string DataField = "data";
	private const string SliceMethod = nameof(ReadOnlySpan<byte>.Slice);

	private static List<(string, string)> EndianTypes { get; } = new()
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
	};

	private static List<(string, string)> OtherTypes { get; } = new()
	{
		(nameof(Boolean), "bool"),
		(nameof(Byte), "byte"),
		(nameof(SByte), "sbyte"),
		(nameof(Char), "char"),
	};

	private static IEnumerable<(string, string)> AllTypes => EndianTypes.Concat(OtherTypes);

	public EndianGenerator() : base(nameof(EndianGenerator))
	{
	}

	public override void OnInitialize(SgfInitializationContext context)
	{
		context.RegisterPostInitializationOutput(AddGeneratedCode);
	}

	private static void AddGeneratedCode(IncrementalGeneratorPostInitializationContext context)
	{
		// Reader
		{
			using StringWriter stringWriter = new() { NewLine = "\n" };
			using IndentedTextWriter writer = IndentedTextWriterFactory.Create(stringWriter);
			DoReaderStruct(writer);

			context.AddSource($"EndianSpanReader.g.cs", stringWriter.ToString());
		}

		// Writer
		{
			using StringWriter stringWriter = new() { NewLine = "\n" };
			using IndentedTextWriter writer = IndentedTextWriterFactory.Create(stringWriter);
			DoWriterStruct(writer);

			context.AddSource($"EndianSpanWriter.g.cs", stringWriter.ToString());
		}
	}

	private static void DoReaderStruct(IndentedTextWriter writer)
	{
		AddHeaderLines(writer);
		writer.WriteLine($"ref partial struct {ReaderStructName}");
		using (new CurlyBrackets(writer))
		{
			writer.WriteLine($"private readonly ReadOnlySpan<byte> {DataField};");
			writer.WriteLine($"private int {OffsetField};");
			writer.WriteLine($"private bool {BigEndianField};");
			AddLengthProperty(writer);
			AddPositionProperty(writer);
			foreach ((string typeName, string keyword) in EndianTypes)
			{
				writer.WriteLineNoTabs();
				AddReadMethod(writer, typeName, keyword);
			}
			foreach ((string typeName, string keyword) in AllTypes)
			{
				writer.WriteLineNoTabs();
				AddTryReadMethod(writer, typeName, keyword);
			}
			writer.WriteLineNoTabs();
			AddGenericReadMethod(writer);
		}
	}

	private static void DoWriterStruct(IndentedTextWriter writer)
	{
		AddHeaderLines(writer);
		writer.WriteLine($"ref partial struct {WriterStructName}");
		using (new CurlyBrackets(writer))
		{
			writer.WriteLine($"private readonly Span<byte> {DataField};");
			writer.WriteLine($"private int {OffsetField};");
			writer.WriteLine($"private bool {BigEndianField};");
			AddLengthProperty(writer);
			AddPositionProperty(writer);
			foreach ((string typeName, string keyword) in EndianTypes)
			{
				writer.WriteLineNoTabs();
				AddWriteMethod(writer, typeName, keyword);
			}
			writer.WriteLineNoTabs();
			AddGenericWriteMethod(writer);
		}
	}

	private static void AddHeaderLines(IndentedTextWriter writer)
	{
		writer.WriteGeneratedCodeWarning();
		writer.WriteUsing("System.Buffers.Binary");
		writer.WriteUsing("System.Runtime.CompilerServices");
		writer.WriteLine();
		writer.WriteFileScopedNamespace(TargetNamespace);
		writer.WriteLine();
	}

	private static void AddLengthProperty(IndentedTextWriter writer)
	{
		writer.WriteLine($"public readonly int Length => {DataField}.{nameof(ReadOnlySpan<byte>.Length)};");
	}

	private static void AddPositionProperty(IndentedTextWriter writer)
	{
		writer.WriteLine("public int Position");
		using (new CurlyBrackets(writer))
		{
			writer.WriteAggressiveInliningAttribute();
			writer.WriteLine($"readonly get => {OffsetField};");
			writer.WriteAggressiveInliningAttribute();
			writer.WriteLine($"set => {OffsetField} = value;");
		}
	}

	/// <summary>
	/// <code>
	/// public int ReadInt32()
	/// {
	///     int result = bigEndian
	///         ? BinaryPrimitives.ReadInt32BigEndian(sourceData.Slice(offset))
	///         : BinaryPrimitives.ReadInt32LittleEndian(sourceData.Slice(offset));
	///     offset += sizeof(int);
	///     return result;
	/// }
	/// </code>
	/// </summary>
	/// <param name="writer"></param>
	/// <param name="typeName"></param>
	/// <param name="returnType"></param>
	private static void AddReadMethod(IndentedTextWriter writer, string typeName, string returnType)
	{
		const string ResultVariable = "result";
		writer.WriteAggressiveInliningAttribute();
		writer.WriteLine($"public {returnType} Read{typeName}()");
		using (new CurlyBrackets(writer))
		{
			writer.WriteLine($"{returnType} {ResultVariable} = {BigEndianField}");
			using (new Indented(writer))
			{
				writer.WriteLine($"? {GetBinaryPrimitivesMethodName(typeName, true, true)}({DataField}.{SliceMethod}({OffsetField}))");
				writer.WriteLine($": {GetBinaryPrimitivesMethodName(typeName, false, true)}({DataField}.{SliceMethod}({OffsetField}));");
			}
			writer.WriteLine($"{OffsetField} += {SizeOfExpression(returnType)};");
			writer.WriteLine($"return {ResultVariable};");
		}
	}

	/// <summary>
	/// <code>
	/// public bool TryReadInt32(out int value) => TryReadPrimitive(out value);
	/// </code>
	/// </summary>
	/// <param name="writer"></param>
	/// <param name="typeName"></param>
	/// <param name="returnType"></param>
	private static void AddTryReadMethod(IndentedTextWriter writer, string typeName, string returnType)
	{
		writer.WriteAggressiveInliningAttribute();
		writer.WriteLine($"public bool TryRead{typeName}(out {returnType} value) => TryReadPrimitive(out value);");
	}

	private static void AddGenericReadMethod(IndentedTextWriter writer)
	{
		writer.WriteSummaryDocumentation("Read a C# primitive type. JIT optimizations should make this as efficient as normal method calls.");
		writer.WriteAggressiveInliningAttribute();
		writer.WriteLine("public T ReadPrimitive<T>() where T : unmanaged");
		using (new CurlyBrackets(writer))
		{
			string elsePrefix = "";
			foreach ((string typeName, string keyword) in AllTypes)
			{
				writer.WriteLine($"{elsePrefix}if (typeof(T) == typeof({keyword}))");
				using (new CurlyBrackets(writer))
				{
					writer.WriteLine($"{keyword} value = Read{typeName}();");
					writer.WriteLine($"return Unsafe.As<{keyword}, T>(ref value);");
				}
				elsePrefix = "else ";
			}
			writer.WriteLine("return default;//Throwing an exception prevents method inlining.");
		}
	}

	/// <summary>
	/// <code>
	/// public void Write(int value)
	/// {
	///     if (bigEndian)
	///     {
	///         BinaryPrimitives.WriteInt32BigEndian(sourceData.Slice(offset), value);
	///     }
	///     else
	///     {
	///         BinaryPrimitives.WriteInt32LittleEndian(sourceData.Slice(offset), value);
	///     }
	///     offset += sizeof(int);
	/// }
	/// </code>
	/// </summary>
	/// <param name="writer"></param>
	/// <param name="typeName"></param>
	/// <param name="parameterType"></param>
	private static void AddWriteMethod(IndentedTextWriter writer, string typeName, string parameterType)
	{
		const string ValueParameter = "value";
		writer.WriteAggressiveInliningAttribute();
		writer.WriteLine($"public void Write({parameterType} {ValueParameter})");
		using (new CurlyBrackets(writer))
		{
			writer.WriteLine($"if ({BigEndianField})");
			using (new CurlyBrackets(writer))
			{
				string methodName = GetBinaryPrimitivesMethodName(typeName, true, false);
				writer.WriteLine($"{methodName}({DataField}.{SliceMethod}({OffsetField}), {ValueParameter});");
			}
			writer.WriteLine("else");
			using (new CurlyBrackets(writer))
			{
				string methodName = GetBinaryPrimitivesMethodName(typeName, false, false);
				writer.WriteLine($"{methodName}({DataField}.{SliceMethod}({OffsetField}), {ValueParameter});");
			}
			writer.WriteLine($"{OffsetField} += {SizeOfExpression(parameterType)};");
		}
	}

	private static void AddGenericWriteMethod(IndentedTextWriter writer)
	{
		writer.WriteSummaryDocumentation("Write a C# primitive type. JIT optimizations should make this as efficient as normal method calls.");
		writer.WriteAggressiveInliningAttribute();
		writer.WriteLine("public void WritePrimitive<T>(T value) where T : unmanaged");
		using (new CurlyBrackets(writer))
		{
			string elsePrefix = "";
			foreach ((string typeName, string keyword) in AllTypes)
			{
				writer.WriteLine($"{elsePrefix}if (typeof(T) == typeof({keyword}))");
				using (new CurlyBrackets(writer))
				{
					writer.WriteLine($"Write(Unsafe.As<T, {keyword}>(ref value));");
				}
				elsePrefix = "else ";
			}
		}
	}

	private static string GetBinaryPrimitivesMethodName(string typeName, bool bigEndian, bool read)
	{
		return (bigEndian, read) switch
		{
			(true, true) => $"{nameof(BinaryPrimitives)}.Read{typeName}BigEndian",
			(false, true) => $"{nameof(BinaryPrimitives)}.Read{typeName}LittleEndian",
			(true, false) => $"{nameof(BinaryPrimitives)}.Write{typeName}BigEndian",
			(false, false) => $"{nameof(BinaryPrimitives)}.Write{typeName}LittleEndian",
		};
	}

	private static string SizeOfExpression(string type)
	{
		return type is nameof(Half) ? "sizeof(ushort)" : $"sizeof({type})";
	}
}
