using AssetRipper.Primitives;
using System.Runtime.CompilerServices;

namespace AssetRipper.IO.Endian;

public partial struct EndianSpanReader
{
	public EndianSpanReader(ReadOnlySpan<byte> data, EndianType type)
	{
		offset = 0;
		this.data = data;
		bigEndian = type == EndianType.BigEndian;
	}

	public EndianType Type
	{
		readonly get => bigEndian ? EndianType.BigEndian : EndianType.LittleEndian;
		set => bigEndian = value == EndianType.BigEndian;
	}

	public bool ReadBoolean()
	{
		return ReadByte() != 0;
	}

	public byte ReadByte()
	{
		return data[offset++];
	}

	public sbyte ReadSByte()
	{
		return unchecked((sbyte)ReadByte());
	}

	public char ReadChar()
	{
		return (char)ReadUInt16();
	}

	/// <summary>
	/// Try to read a C# primitive type. JIT optimizations should make this as efficient as normal method calls.
	/// </summary>
	public bool TryReadPrimitive<T>(out T value) where T : unmanaged
	{
		if (HasRemainingBytes(Unsafe.SizeOf<T>()))
		{
			value = ReadPrimitive<T>();
			return true;
		}
		else
		{
			value = default;
			return false;
		}
	}

	public byte[] ReadBytes(int count)
	{
		ThrowIfNegative(count);

		int resultLength = Math.Min(count, Length - Position);
		if (resultLength == 0)
		{
			return Array.Empty<byte>();
		}

		byte[] result = new byte[resultLength];
		data.Slice(Position, resultLength).CopyTo(result);
		Position += resultLength;
		return result;
	}

	public int ReadBytes(Span<byte> buffer)
	{
		int count = Math.Min(buffer.Length, Length - Position);
		data.Slice(Position, count).CopyTo(buffer);
		Position += count;
		return count;
	}

	public ReadOnlySpan<byte> ReadBytesExact(int count)
	{
		ThrowIfNegative(count);
		ReadOnlySpan<byte> sliced = data.Slice(Position, count);
		Position += count;
		return sliced;
	}

	public bool TryReadBytesExact(int count, out ReadOnlySpan<byte> result)
	{
		if (count < 0 || !HasRemainingBytes(count))
		{
			result = default;
			return false;
		}
		result = data.Slice(Position, count);
		Position += count;
		return true;
	}

	public void ReadBytesExact(Span<byte> buffer)
	{
		data.Slice(Position, buffer.Length).CopyTo(buffer);
		Position += buffer.Length;
	}

	public bool TryReadBytesExact(Span<byte> buffer)
	{
		if (TryReadBytesExact(buffer.Length, out ReadOnlySpan<byte> result))
		{
			result.CopyTo(buffer);
			return true;
		}
		return false;
	}

	private static void ThrowIfNegative(int count)
	{
		if (count < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(count), count, "Value cannot be negative.");
		}
	}

	/// <summary>
	/// Read a <see cref="Utf8String"/> from the data.
	/// </summary>
	/// <remarks>
	/// The binary format is a 4-byte integer length, followed by length bytes.
	/// This method does not call <see cref="Align"/>.
	/// </remarks>
	/// <returns>A new <see cref="Utf8String"/> containing the text.</returns>
	public Utf8String ReadUtf8String()
	{
		int length = ReadInt32();
		ReadOnlySpan<byte> byteArray = ReadBytesExact(length);
		return new Utf8String(byteArray);
	}

	/// <summary>
	/// Try to read a <see cref="Utf8String"/> from the data.
	/// </summary>
	/// <remarks>
	/// The binary format is a 4-byte integer length, followed by length bytes.
	/// This method does not call <see cref="Align"/>.
	/// </remarks>
	/// <param name="result">A new <see cref="Utf8String"/> containing the text, if successful.</param>
	/// <returns>True if successful. False otherwise.</returns>
	public bool TryReadUtf8String([NotNullWhen(true)] out Utf8String? result)
	{
		if (!TryReadInt32(out int length))
		{
			result = default;
			return false;
		}
		if (!TryReadBytesExact(length, out ReadOnlySpan<byte> byteArray))
		{
			result = default;
			return false;
		}
		result = new Utf8String(byteArray);
		return true;
	}

	/// <summary>
	/// Read C-like, UTF8-format, zero-terminated string
	/// </summary>
	/// <remarks>
	/// The binary format is a series of UTF8 bytes followed with a zero byte.
	/// This method does not call <see cref="Align"/>.
	/// </remarks>
	/// <returns>A new <see cref="Utf8String"/> containing the text.</returns>
	public Utf8String ReadNullTerminatedString()
	{
		if (!TryReadNullTerminatedString(out Utf8String? result))
		{
			throw new EndOfStreamException("Null termination character not found.");
		}
		return result;
	}

	public bool TryReadNullTerminatedString([NotNullWhen(true)] out Utf8String? result)
	{
		int length = data.Slice(Position).IndexOf((byte)'\0');
		if (length < 0)
		{
			result = default;
			return false;
		}
		ReadOnlySpan<byte> byteArray = ReadBytesExact(length);
		Position += 1;
		result = new Utf8String(byteArray);
		return true;
	}

	/// <summary>
	/// Align the <see cref="Position"/> to a next multiple of 4.
	/// </summary>
	/// <remarks>
	/// If the <see cref="Position"/> is not divisible by 4, this will move it to the next multiple of 4.
	/// </remarks>
	public void Align()
	{
		Position = (Position + 3) & ~3;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private readonly bool HasRemainingBytes(int count)
	{
		return Length - Position >= count;
	}
}
