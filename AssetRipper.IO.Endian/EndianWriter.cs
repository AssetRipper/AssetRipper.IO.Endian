using AssetRipper.Primitives;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace AssetRipper.IO.Endian;

public class EndianWriter : BinaryWriter
{
	public EndianWriter(Stream stream, EndianType endianess) : this(stream, endianess, false) { }

	protected EndianWriter(Stream stream, EndianType endianess, bool alignArray) : base(stream, Encoding.UTF8, true)
	{
		EndianType = endianess;
		IsAlignArray = alignArray;
	}

	~EndianWriter()
	{
		Dispose(false);
	}

	public sealed override void Write(byte value)
	{
		base.Write(value);
	}

	public sealed override void Write(byte[] buffer)
	{
		base.Write(buffer);
	}

	public sealed override void Write(byte[] buffer, int index, int count)
	{
		base.Write(buffer, index, count);
	}

	public sealed override void Write(ReadOnlySpan<byte> buffer)
	{
		OutStream.Write(buffer);
	}

	public sealed override void Write(bool value)
	{
		base.Write(value);
	}

	public override void Write(short value)
	{
		base.Write(IsLittleEndian ? value : BinaryPrimitives.ReverseEndianness(value));
	}

	public override void Write(ushort value)
	{
		base.Write(IsLittleEndian ? value : BinaryPrimitives.ReverseEndianness(value));
	}

	public override void Write(int value)
	{
		base.Write(IsLittleEndian ? value : BinaryPrimitives.ReverseEndianness(value));
	}

	public override void Write(uint value)
	{
		base.Write(IsLittleEndian ? value : BinaryPrimitives.ReverseEndianness(value));
	}

	public override void Write(long value)
	{
		base.Write(IsLittleEndian ? value : BinaryPrimitives.ReverseEndianness(value));
	}

	public override void Write(ulong value)
	{
		base.Write(IsLittleEndian ? value : BinaryPrimitives.ReverseEndianness(value));
	}

	public override void Write(Half value)
	{
		Write(BitConverter.HalfToUInt16Bits(value));
	}

	public override void Write(float value)
	{
		Write(BitConverter.SingleToUInt32Bits(value));
	}

	public override void Write(double value)
	{
		Write(BitConverter.DoubleToUInt64Bits(value));
	}

	public override void Write(string value)
	{
		byte[] buffer = Encoding.UTF8.GetBytes(value);
		Write(buffer.Length);
		Write(buffer);
	}

	public void Write(Utf8String value)
	{
		Write(value.Data.Length);
		OutStream.Write(value.Data);
	}

	public void WriteStringZeroTerm(string value)
	{
		Write(Encoding.UTF8.GetBytes(value));
		Write((byte)'\0');
	}

	public void WriteArray(ReadOnlySpan<bool> buffer)
	{
		Write(buffer.Length);

		int index = 0;
		while (index < buffer.Length)
		{
			int left = buffer.Length - index;
			int toWrite = left < BufferSize ? left : BufferSize;
			for (int i = 0; i < toWrite; i++, index++)
			{
				m_buffer[i] = buffer[index] ? (byte)1 : (byte)0;
			}
			Write(m_buffer, 0, toWrite);
		}
		if (IsAlignArray)
		{
			AlignStream();
		}
	}

	public void WriteArray(ReadOnlySpan<char> buffer)
	{
		Write(buffer.Length);

		int index = 0;
		while (index < buffer.Length)
		{
			int left = buffer.Length - index;
			int toWrite = left < BufferSize ? left : BufferSize;
			for (int i = 0; i < toWrite; i += sizeof(char), index++)
			{
				FillInnerBuffer(buffer[index], i);
			}
			Write(m_buffer, 0, toWrite);
		}
		if (IsAlignArray)
		{
			AlignStream();
		}
	}

	public void WriteArray(ReadOnlySpan<sbyte> buffer)
	{
		Write(buffer.Length);

		ReadOnlySpan<byte> span = MemoryMarshal.Cast<sbyte, byte>(buffer);
		Write(span);
		if (IsAlignArray)
		{
			AlignStream();
		}
	}

	public void WriteArray(ReadOnlySpan<byte> buffer)
	{
		Write(buffer.Length);
		Write(buffer);
		if (IsAlignArray)
		{
			AlignStream();
		}
	}

	public void WriteArray(ReadOnlySpan<short> buffer)
	{
		Write(buffer.Length);

		int index = 0;
		while (index < buffer.Length)
		{
			int left = buffer.Length - index;
			int toWrite = left < BufferSize ? left : BufferSize;
			for (int i = 0; i < toWrite; i += sizeof(short), index++)
			{
				FillInnerBuffer(unchecked((ushort)buffer[index]), i);
			}
			Write(m_buffer, 0, toWrite);
		}
		if (IsAlignArray)
		{
			AlignStream();
		}
	}

	public void WriteArray(ReadOnlySpan<ushort> buffer)
	{
		Write(buffer.Length);

		int index = 0;
		while (index < buffer.Length)
		{
			int left = buffer.Length - index;
			int toWrite = left < BufferSize ? left : BufferSize;
			for (int i = 0; i < toWrite; i += sizeof(ushort), index++)
			{
				FillInnerBuffer(buffer[index], i);
			}
			Write(m_buffer, 0, toWrite);
		}
		if (IsAlignArray)
		{
			AlignStream();
		}
	}

	public void WriteArray(ReadOnlySpan<int> buffer)
	{
		Write(buffer.Length);

		int index = 0;
		while (index < buffer.Length)
		{
			int left = buffer.Length - index;
			int toWrite = left < BufferSize ? left : BufferSize;
			for (int i = 0; i < toWrite; i += sizeof(int), index++)
			{
				FillInnerBuffer(unchecked((uint)buffer[index]), i);
			}
			Write(m_buffer, 0, toWrite);
		}
		if (IsAlignArray)
		{
			AlignStream();
		}
	}

	public void WriteArray(ReadOnlySpan<uint> buffer)
	{
		Write(buffer.Length);

		int index = 0;
		while (index < buffer.Length)
		{
			int left = buffer.Length - index;
			int toWrite = left < BufferSize ? left : BufferSize;
			for (int i = 0; i < toWrite; i += sizeof(uint), index++)
			{
				FillInnerBuffer(buffer[index], i);
			}
			Write(m_buffer, 0, toWrite);
		}
		if (IsAlignArray)
		{
			AlignStream();
		}
	}

	public void WriteArray(ReadOnlySpan<long> buffer)
	{
		Write(buffer.Length);

		int index = 0;
		while (index < buffer.Length)
		{
			int left = buffer.Length - index;
			int toWrite = left < BufferSize ? left : BufferSize;
			for (int i = 0; i < toWrite; i += sizeof(long), index++)
			{
				FillInnerBuffer(unchecked((ulong)buffer[index]), i);
			}
			Write(m_buffer, 0, toWrite);
		}
		if (IsAlignArray)
		{
			AlignStream();
		}
	}

	public void WriteArray(ReadOnlySpan<ulong> buffer)
	{
		Write(buffer.Length);

		int index = 0;
		while (index < buffer.Length)
		{
			int left = buffer.Length - index;
			int toWrite = left < BufferSize ? left : BufferSize;
			for (int i = 0; i < toWrite; i += sizeof(ulong), index++)
			{
				FillInnerBuffer(buffer[index], i);
			}
			Write(m_buffer, 0, toWrite);
		}
		if (IsAlignArray)
		{
			AlignStream();
		}
	}

	public void WriteArray(ReadOnlySpan<Half> buffer)
	{
		Write(buffer.Length);
		int index = 0;
		while (index < buffer.Length)
		{
			int left = buffer.Length - index;
			int toWrite = left < BufferSize ? left : BufferSize;
			for (int i = 0; i < toWrite; i += sizeof(ushort), index++)
			{
				FillInnerBuffer(BitConverter.HalfToUInt16Bits(buffer[index]), i);
			}
			Write(m_buffer, 0, toWrite);
		}
		if (IsAlignArray)
		{
			AlignStream();
		}
	}

	public void WriteArray(ReadOnlySpan<float> buffer)
	{
		Write(buffer.Length);

		int index = 0;
		while (index < buffer.Length)
		{
			int left = buffer.Length - index;
			int toWrite = left < BufferSize ? left : BufferSize;
			for (int i = 0; i < toWrite; i += sizeof(float), index++)
			{
				FillInnerBuffer(BitConverter.SingleToUInt32Bits(buffer[index]), i);
			}
			Write(m_buffer, 0, toWrite);
		}
		if (IsAlignArray)
		{
			AlignStream();
		}
	}

	public void WriteArray(ReadOnlySpan<double> buffer)
	{
		Write(buffer.Length);

		int index = 0;
		while (index < buffer.Length)
		{
			int left = buffer.Length - index;
			int toWrite = left < BufferSize ? left : BufferSize;
			for (int i = 0; i < toWrite; i += sizeof(double), index++)
			{
				FillInnerBuffer(BitConverter.DoubleToUInt64Bits(buffer[index]), i);
			}
			Write(m_buffer, 0, toWrite);
		}
		if (IsAlignArray)
		{
			AlignStream();
		}
	}

	public void WriteArray(ReadOnlySpan<string> buffer)
	{
		Write(buffer.Length);

		for (int i = 0; i < buffer.Length; i++)
		{
			string str = buffer[i];
			Write(str);
		}
		if (IsAlignArray)
		{
			AlignStream();
		}
	}

	public void WriteEndian<T>(T value) where T : IEndianWritable
	{
		value.Write(this);
	}

	public void WriteEndianArray<T>(T[] buffer) where T : IEndianWritable
	{
		Write(buffer.Length);

		for (int i = 0; i < buffer.Length; i++)
		{
			T t = buffer[i];
			t.Write(this);
		}
		if (IsAlignArray)
		{
			AlignStream();
		}
	}

	public void WriteEndianArray<T>(T[][] buffer) where T : IEndianWritable
	{
		Write(buffer.Length);

		for (int i = 0; i < buffer.GetLength(0); i++)
		{
			WriteEndianArray(buffer[i]);
		}
		if (IsAlignArray)
		{
			AlignStream();
		}
	}

	public void AlignStream()
	{
		long newPosition = (BaseStream.Position + 3) & ~3;
		if (newPosition > BaseStream.Length)
		{
			BaseStream.Position = BaseStream.Length;
			while (BaseStream.Position < newPosition)
			{
				Write((byte)0);
			}
		}
		else
		{
			BaseStream.Position = newPosition;
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected void FillInnerBuffer(ushort value, int offset = 0)
	{
		if (EndianType == EndianType.LittleEndian)
		{
			m_buffer[offset + 0] = unchecked((byte)(value >> 0));
			m_buffer[offset + 1] = unchecked((byte)(value >> 8));
		}
		else
		{
			m_buffer[offset + 1] = unchecked((byte)(value >> 0));
			m_buffer[offset + 0] = unchecked((byte)(value >> 8));
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected void FillInnerBuffer(int value, int offset = 0)
	{
		if (EndianType == EndianType.LittleEndian)
		{
			m_buffer[offset + 0] = unchecked((byte)(value >> 0));
			m_buffer[offset + 1] = unchecked((byte)(value >> 8));
			m_buffer[offset + 2] = unchecked((byte)(value >> 16));
			m_buffer[offset + 3] = unchecked((byte)(value >> 24));
		}
		else
		{
			m_buffer[offset + 3] = unchecked((byte)(value >> 0));
			m_buffer[offset + 2] = unchecked((byte)(value >> 8));
			m_buffer[offset + 1] = unchecked((byte)(value >> 16));
			m_buffer[offset + 0] = unchecked((byte)(value >> 24));
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected void FillInnerBuffer(uint value, int offset = 0)
	{
		if (EndianType == EndianType.LittleEndian)
		{
			m_buffer[offset + 0] = unchecked((byte)(value >> 0));
			m_buffer[offset + 1] = unchecked((byte)(value >> 8));
			m_buffer[offset + 2] = unchecked((byte)(value >> 16));
			m_buffer[offset + 3] = unchecked((byte)(value >> 24));
		}
		else
		{
			m_buffer[offset + 3] = unchecked((byte)(value >> 0));
			m_buffer[offset + 2] = unchecked((byte)(value >> 8));
			m_buffer[offset + 1] = unchecked((byte)(value >> 16));
			m_buffer[offset + 0] = unchecked((byte)(value >> 24));
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected void FillInnerBuffer(ulong value, int offset = 0)
	{
		if (EndianType == EndianType.LittleEndian)
		{
			m_buffer[offset + 0] = unchecked((byte)(value >> 0));
			m_buffer[offset + 1] = unchecked((byte)(value >> 8));
			m_buffer[offset + 2] = unchecked((byte)(value >> 16));
			m_buffer[offset + 3] = unchecked((byte)(value >> 24));
			m_buffer[offset + 4] = unchecked((byte)(value >> 32));
			m_buffer[offset + 5] = unchecked((byte)(value >> 40));
			m_buffer[offset + 6] = unchecked((byte)(value >> 48));
			m_buffer[offset + 7] = unchecked((byte)(value >> 56));
		}
		else
		{
			m_buffer[offset + 7] = unchecked((byte)(value >> 0));
			m_buffer[offset + 6] = unchecked((byte)(value >> 8));
			m_buffer[offset + 5] = unchecked((byte)(value >> 16));
			m_buffer[offset + 4] = unchecked((byte)(value >> 24));
			m_buffer[offset + 3] = unchecked((byte)(value >> 32));
			m_buffer[offset + 2] = unchecked((byte)(value >> 40));
			m_buffer[offset + 1] = unchecked((byte)(value >> 48));
			m_buffer[offset + 0] = unchecked((byte)(value >> 56));
		}
	}

	public EndianType EndianType { get; }

	private bool IsLittleEndian => EndianType is EndianType.LittleEndian;

	public bool IsAlignArray { get; }

	protected const int BufferSize = 4096;

	protected readonly byte[] m_buffer = new byte[BufferSize];
}
