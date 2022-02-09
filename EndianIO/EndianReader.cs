using System;
using System.Buffers.Binary;
using System.IO;
using System.Text;

namespace AssetRipper.EndianIO
{
	public class EndianReader : BinaryReader
	{
		private bool isBigEndian = false;
		public bool IsLittleEndian
		{
			get => !isBigEndian;
			set => isBigEndian = !value;
		}

		public EndianReader(Stream stream, bool isLittleEndian) : base(stream)
		{
			isBigEndian = !isLittleEndian;
		}
		public EndianReader(Stream stream, bool isLittleEndian, Encoding encoding) : base(stream, encoding)
		{
			isBigEndian = !isLittleEndian;
		}
		public EndianReader(Stream stream, bool isLittleEndian, Encoding encoding, bool leaveOpen) : base(stream, encoding, leaveOpen)
		{
			isBigEndian = !isLittleEndian;
		}

		~EndianReader()
		{
			Dispose(false);
		}

		public void SwapEndianess() => isBigEndian = !isBigEndian;

		public override char ReadChar()
		{
			return (char)ReadUInt16();
		}

		public override short ReadInt16()
		{
			if (isBigEndian)
				return BinaryPrimitives.ReadInt16BigEndian(base.ReadBytes(2));
			else
				return base.ReadInt16();
		}

		public override ushort ReadUInt16()
		{
			if (isBigEndian)
				return BinaryPrimitives.ReadUInt16BigEndian(base.ReadBytes(2));
			else
				return base.ReadUInt16();
		}

		public override int ReadInt32()
		{
			if (isBigEndian)
				return BinaryPrimitives.ReadInt32BigEndian(base.ReadBytes(4));
			else
				return base.ReadInt32();
		}

		public override uint ReadUInt32()
		{
			if (isBigEndian)
				return BinaryPrimitives.ReadUInt32BigEndian(base.ReadBytes(4));
			else
				return base.ReadUInt32();
		}

		public override long ReadInt64()
		{
			if (isBigEndian)
				return BinaryPrimitives.ReadInt64BigEndian(base.ReadBytes(8));
			else
				return base.ReadInt64();
		}

		public override ulong ReadUInt64()
		{
			if (isBigEndian)
				return BinaryPrimitives.ReadUInt64BigEndian(base.ReadBytes(8));
			else
				return base.ReadUInt64();
		}

#if NET6_0_OR_GREATER
		public override Half ReadHalf()
		{
			if (isBigEndian)
				return BinaryPrimitives.ReadHalfBigEndian(base.ReadBytes(2));
			else
				return base.ReadHalf();
		}
#elif NET5_0
		public Half ReadHalf()
		{
			return BitConverterExtensions.ToHalf(ReadUInt16());
		}
#else
		public Half ReadHalf()
		{
			return Half.ToHalf(ReadUInt16());
		}
#endif

		public override float ReadSingle()
		{
			if (isBigEndian)
			{
#if NET5_0_OR_GREATER
				return BinaryPrimitives.ReadSingleBigEndian(base.ReadBytes(4));
#else
				if (BitConverter.IsLittleEndian) 
				{
					byte[] bytes = ReadBytes(4);
					Array.Reverse(bytes);
					return BitConverter.ToSingle(bytes, 0);
				}
				else
				{
					return BitConverter.ToSingle(ReadBytes(4), 0);
				}
#endif
			}
			else
			{ 
				return base.ReadSingle();
			}
		}

		public override double ReadDouble()
		{
			if (isBigEndian)
#if NET5_0_OR_GREATER
				return BinaryPrimitives.ReadDoubleBigEndian(base.ReadBytes(8));
#else
				return BitConverter.Int64BitsToDouble(ReadInt64());
#endif
			else
				return base.ReadDouble();
		}

		public override decimal ReadDecimal()
		{
			if (isBigEndian)
			{
				int[] bits = new int[4];
				bits[0] = ReadInt32();
				bits[1] = ReadInt32();
				bits[2] = ReadInt32();
				bits[3] = ReadInt32();
				return new decimal(bits);
			}
			else
			{
				return base.ReadDecimal();
			}
		}

		public bool[] ReadBooleanArray() => ReadBooleanArray(true);
		public bool[] ReadBooleanArray(bool align)
		{
			int count = ReadInt32();
			int index = 0;
			bool[] array = count == 0 ? Array.Empty<bool>() : new bool[count];
			while (index < count)
			{
				try
				{
					array[index] = ReadBoolean();
				}
				catch (Exception ex)
				{
					throw new Exception($"End of stream. Read {index}, expected {count} elements", ex);
				}
				index++;
			}
			if (align)
			{
				AlignStream();
			}
			return array;
		}

		public char[] ReadCharArray() => ReadCharArray(true);
		public char[] ReadCharArray(bool align)
		{
			int count = ReadInt32();
			int index = 0;
			char[] array = count == 0 ? Array.Empty<char>() : new char[count];
			while (index < count)
			{
				try
				{
					array[index] = ReadChar();
				}
				catch (Exception ex)
				{
					throw new Exception($"End of stream. Read {index}, expected {count} elements", ex);
				}
				index++;
			}
			if (align)
			{
				AlignStream();
			}
			return array;
		}

		public byte[] ReadByteArray() => ReadByteArray(true);
		public byte[] ReadByteArray(bool align)
		{
			int count = ReadInt32();
			int index = 0;
			byte[] array = count == 0 ? Array.Empty<byte>() : new byte[count];
			while (index < count)
			{
				int read = Read(array, index, count - index);
				if (read == 0)
				{
					throw new Exception($"End of stream. Read {index}, expected {count} elements");
				}
				index += read;
			}
			if (align)
			{
				AlignStream();
			}
			return array;
		}

		public sbyte[] ReadSByteArray() => ReadSByteArray(true);
		public sbyte[] ReadSByteArray(bool align)
		{
			int count = ReadInt32();
			int index = 0;
			sbyte[] array = count == 0 ? Array.Empty<sbyte>() : new sbyte[count];
			while (index < count)
			{
				try
				{
					array[index] = ReadSByte();
				}
				catch (Exception ex)
				{
					throw new Exception($"End of stream. Read {index}, expected {count} elements", ex);
				}
				index++;
			}
			if (align)
			{
				AlignStream();
			}
			return array;
		}

		public short[] ReadInt16Array() => ReadInt16Array(true);
		public short[] ReadInt16Array(bool align)
		{
			int count = ReadInt32();
			int index = 0;
			short[] array = count == 0 ? Array.Empty<short>() : new short[count];
			while (index < count)
			{
				try
				{
					array[index] = ReadInt16();
				}
				catch (Exception ex)
				{
					throw new Exception($"End of stream. Read {index}, expected {count} elements", ex);
				}
				index++;
			}
			if (align)
			{
				AlignStream();
			}
			return array;
		}

		public ushort[] ReadUInt16Array() => ReadUInt16Array(true);
		public ushort[] ReadUInt16Array(bool align)
		{
			int count = ReadInt32();
			int index = 0;
			ushort[] array = count == 0 ? Array.Empty<ushort>() : new ushort[count];
			while (index < count)
			{
				try
				{
					array[index] = ReadUInt16();
				}
				catch (Exception ex)
				{
					throw new Exception($"End of stream. Read {index}, expected {count} elements", ex);
				}
				index++;
			}
			if (align)
			{
				AlignStream();
			}
			return array;
		}

		public int[] ReadInt32Array() => ReadInt32Array(true);
		public int[] ReadInt32Array(bool align)
		{
			int count = ReadInt32();
			int index = 0;
			int[] array = count == 0 ? Array.Empty<int>() : new int[count];
			while (index < count)
			{
				try
				{
					array[index] = ReadInt32();
				}
				catch (Exception ex)
				{
					throw new Exception($"End of stream. Read {index}, expected {count} elements", ex);
				}
				index++;
			}
			if (align)
			{
				AlignStream();
			}
			return array;
		}

		public uint[] ReadUInt32Array() => ReadUInt32Array(true);
		public uint[] ReadUInt32Array(bool align)
		{
			int count = ReadInt32();
			int index = 0;
			uint[] array = count == 0 ? Array.Empty<uint>() : new uint[count];
			while (index < count)
			{
				try
				{
					array[index] = ReadUInt32();
				}
				catch (Exception ex)
				{
					throw new Exception($"End of stream. Read {index}, expected {count} elements", ex);
				}
				index++;
			}
			if (align)
			{
				AlignStream();
			}
			return array;
		}

		public long[] ReadInt64Array() => ReadInt64Array(true);
		public long[] ReadInt64Array(bool align)
		{
			int count = ReadInt32();
			int index = 0;
			long[] array = count == 0 ? Array.Empty<long>() : new long[count];
			while (index < count)
			{
				try
				{
					array[index] = ReadInt64();
				}
				catch (Exception ex)
				{
					throw new Exception($"End of stream. Read {index}, expected {count} elements", ex);
				}
				index++;
			}
			if (align)
			{
				AlignStream();
			}
			return array;
		}

		public ulong[] ReadUInt64Array() => ReadUInt64Array(true);
		public ulong[] ReadUInt64Array(bool align)
		{
			int count = ReadInt32();
			int index = 0;
			ulong[] array = count == 0 ? Array.Empty<ulong>() : new ulong[count];
			while (index < count)
			{
				try
				{
					array[index] = ReadUInt64();
				}
				catch (Exception ex)
				{
					throw new Exception($"End of stream. Read {index}, expected {count} elements", ex);
				}
				index++;
			}
			if (align)
			{
				AlignStream();
			}
			return array;
		}

		public float[] ReadSingleArray() => ReadSingleArray(true);
		public float[] ReadSingleArray(bool align)
		{
			int count = ReadInt32();
			int index = 0;
			float[] array = count == 0 ? Array.Empty<float>() : new float[count];
			while (index < count)
			{
				try
				{
					array[index] = ReadSingle();
				}
				catch (Exception ex)
				{
					throw new Exception($"End of stream. Read {index}, expected {count} elements", ex);
				}
				index++;
			}
			if (align)
			{
				AlignStream();
			}
			return array;
		}

		public double[] ReadDoubleArray() => ReadDoubleArray(true);
		public double[] ReadDoubleArray(bool align)
		{
			int count = ReadInt32();
			int index = 0;
			double[] array = count == 0 ? Array.Empty<double>() : new double[count];
			while (index < count)
			{
				try
				{
					array[index] = ReadDouble();
				}
				catch (Exception ex)
				{
					throw new Exception($"End of stream. Read {index}, expected {count} elements", ex);
				}
				index++;
			}
			if (align)
			{
				AlignStream();
			}
			return array;
		}

		public string[] ReadStringArray() => ReadStringArray(true);
		public string[] ReadStringArray(bool align)
		{
			int count = ReadInt32();
			string[] array = count == 0 ? Array.Empty<string>() : new string[count];
			for (int i = 0; i < count; i++)
			{
				string value = ReadString();
				array[i] = value;
			}
			if (align)
			{
				AlignStream();
			}
			return array;
		}

		public T ReadEndian<T>() where T : IEndianReadable, new()
		{
			T t = new T();
			t.Read(this);
			return t;
		}

		public T[] ReadEndianArray<T>(bool align) where T : IEndianReadable, new()
		{
			int count = ReadInt32();
			T[] array = count == 0 ? Array.Empty<T>() : new T[count];
			for (int i = 0; i < count; i++)
			{
				T t = new T();
				t.Read(this);
				array[i] = t;
			}
			if (align)
			{
				AlignStream();
			}
			return array;
		}

		public T[][] ReadEndianArrayArray<T>(bool align) where T : IEndianReadable, new()
		{
			int count = ReadInt32();
			T[][] array = count == 0 ? Array.Empty<T[]>() : new T[count][];
			for (int i = 0; i < count; i++)
			{
				T[] innerArray = ReadEndianArray<T>(align);
				array[i] = innerArray;
			}
			if (align)
			{
				AlignStream();
			}
			return array;
		}

		public void AlignStream()
		{
			BaseStream.Position = (BaseStream.Position + 3) & ~3;
		}
	}
}
