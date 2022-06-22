using System;
#if !NET6_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif
using SystemBinaryPrimitives = System.Buffers.Binary.BinaryPrimitives;

namespace AssetRipper.IO.Endian
{
	public static class BinaryPrimitives
	{
		/// <summary>Reads a <see cref="T:System.Double" /> from the beginning of a read-only span of bytes, as big endian.</summary>
		/// <param name="source">The read-only span to read.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="source" /> is too small to contain a <see cref="T:System.Double" />.</exception>
		/// <returns>The big endian value.</returns>
		public static double ReadDoubleBigEndian(ReadOnlySpan<byte> source)
		{
#if NET5_0_OR_GREATER
			return SystemBinaryPrimitives.ReadDoubleBigEndian(source);
#else
			ulong value = SystemBinaryPrimitives.ReadUInt64BigEndian(source);
			return Unsafe.As<ulong, double>(ref value);
#endif
		}

		/// <summary>Reads a <see cref="T:System.Double" /> from the beginning of a read-only span of bytes, as little endian.</summary>
		/// <param name="source">The read-only span to read.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="source" /> is too small to contain a <see cref="T:System.Double" />.</exception>
		/// <returns>The little endian value.</returns>
		public static double ReadDoubleLittleEndian(ReadOnlySpan<byte> source)
		{
#if NET5_0_OR_GREATER
			return SystemBinaryPrimitives.ReadDoubleLittleEndian(source);
#else
			ulong value = SystemBinaryPrimitives.ReadUInt64LittleEndian(source);
			return Unsafe.As<ulong, double>(ref value);
#endif
		}

		/// <summary>Reads a <see cref="T:System.Half" /> from the beginning of a read-only span of bytes, as big endian.</summary>
		/// <param name="source">The read-only span to read.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="source" /> is too small to contain a <see cref="T:System.Half" />.</exception>
		/// <returns>The big endian value.</returns>
		public static Half ReadHalfBigEndian(ReadOnlySpan<byte> source)
		{
#if NET6_0_OR_GREATER
			return SystemBinaryPrimitives.ReadHalfBigEndian(source);
#else
			ushort value = SystemBinaryPrimitives.ReadUInt16BigEndian(source);
			return Unsafe.As<ushort, Half>(ref value);
#endif
		}

		/// <summary>Reads a <see cref="T:System.Half" /> from the beginning of a read-only span of bytes, as little endian.</summary>
		/// <param name="source">The read-only span to read.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="source" /> is too small to contain a <see cref="T:System.Half" />.</exception>
		/// <returns>The little endian value.</returns>
		public static Half ReadHalfLittleEndian(ReadOnlySpan<byte> source)
		{
#if NET6_0_OR_GREATER
			return SystemBinaryPrimitives.ReadHalfLittleEndian(source);
#else
			ushort value = SystemBinaryPrimitives.ReadUInt16LittleEndian(source);
			return Unsafe.As<ushort, Half>(ref value);
#endif
		}

		/// <summary>Reads an <see cref="T:System.Int16" /> from the beginning of a read-only span of bytes, as big endian.</summary>
		/// <param name="source">The read-only span to read.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="source" /> is too small to contain an <see cref="T:System.Int16" />.</exception>
		/// <returns>The big endian value.</returns>
		public static short ReadInt16BigEndian(ReadOnlySpan<byte> source)
		{
			return SystemBinaryPrimitives.ReadInt16BigEndian(source);
		}

		/// <summary>Reads an <see cref="T:System.Int16" /> from the beginning of a read-only span of bytes, as little endian.</summary>
		/// <param name="source">The read-only span to read.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="source" /> is too small to contain an <see cref="T:System.Int16" />.</exception>
		/// <returns>The little endian value.</returns>
		public static short ReadInt16LittleEndian(ReadOnlySpan<byte> source)
		{
			return SystemBinaryPrimitives.ReadInt16LittleEndian(source);
		}

		/// <summary>Reads an <see cref="T:System.Int32" /> from the beginning of a read-only span of bytes, as big endian.</summary>
		/// <param name="source">The read-only span to read.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="source" /> is too small to contain an <see cref="T:System.Int32" />.</exception>
		/// <returns>The big endian value.</returns>
		public static int ReadInt32BigEndian(ReadOnlySpan<byte> source)
		{
			return SystemBinaryPrimitives.ReadInt32BigEndian(source);
		}

		/// <summary>Reads an <see cref="T:System.Int32" /> from the beginning of a read-only span of bytes, as little endian.</summary>
		/// <param name="source">The read-only span to read.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="source" /> is too small to contain an <see cref="T:System.Int32" />.</exception>
		/// <returns>The little endian value.</returns>
		public static int ReadInt32LittleEndian(ReadOnlySpan<byte> source)
		{
			return SystemBinaryPrimitives.ReadInt32LittleEndian(source);
		}

		/// <summary>Reads an <see cref="T:System.Int64" /> from the beginning of a read-only span of bytes, as big endian.</summary>
		/// <param name="source">The read-only span to read.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="source" /> is too small to contain an <see cref="T:System.Int64" />.</exception>
		/// <returns>The big endian value.</returns>
		public static long ReadInt64BigEndian(ReadOnlySpan<byte> source)
		{
			return SystemBinaryPrimitives.ReadInt64BigEndian(source);
		}

		/// <summary>Reads an <see cref="T:System.Int64" /> from the beginning of a read-only span of bytes, as little endian.</summary>
		/// <param name="source">The read-only span to read.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="source" /> is too small to contain an <see cref="T:System.Int64" />.</exception>
		/// <returns>The little endian value.</returns>
		public static long ReadInt64LittleEndian(ReadOnlySpan<byte> source)
		{
			return SystemBinaryPrimitives.ReadInt64LittleEndian(source);
		}

		/// <summary>Reads a <see cref="T:System.Single" /> from the beginning of a read-only span of bytes, as big endian.</summary>
		/// <param name="source">The read-only span to read.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="source" /> is too small to contain a <see cref="T:System.Single" />.</exception>
		/// <returns>The big endian value.</returns>
		public static float ReadSingleBigEndian(ReadOnlySpan<byte> source)
		{
#if NET5_0_OR_GREATER
			return SystemBinaryPrimitives.ReadSingleBigEndian(source);
#else
			return BitConverterExtensions.ToSingle(SystemBinaryPrimitives.ReadUInt32BigEndian(source));
#endif
		}

		/// <summary>Reads a <see cref="T:System.Single" /> from the beginning of a read-only span of bytes, as little endian.</summary>
		/// <param name="source">The read-only span to read.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="source" /> is too small to contain a <see cref="T:System.Single" />.</exception>
		/// <returns>The little endian value.</returns>
		public static float ReadSingleLittleEndian(ReadOnlySpan<byte> source)
		{
#if NET5_0_OR_GREATER
			return SystemBinaryPrimitives.ReadSingleLittleEndian(source);
#else
			return BitConverterExtensions.ToSingle(SystemBinaryPrimitives.ReadUInt32LittleEndian(source));
#endif
		}

		/// <summary>Reads a <see cref="T:System.UInt16" /> from the beginning of a read-only span of bytes, as big endian.</summary>
		/// <param name="source">The read-only span to read.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="source" /> is too small to contain a <see cref="T:System.UInt16" />.</exception>
		/// <returns>The big endian value.</returns>
		public static ushort ReadUInt16BigEndian(ReadOnlySpan<byte> source)
		{
			return SystemBinaryPrimitives.ReadUInt16BigEndian(source);
		}

		/// <summary>Reads a <see cref="T:System.UInt16" /> from the beginning of a read-only span of bytes, as little endian.</summary>
		/// <param name="source">The read-only span to read.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="source" /> is too small to contain a <see cref="T:System.UInt16" />.</exception>
		/// <returns>The little endian value.</returns>
		public static ushort ReadUInt16LittleEndian(ReadOnlySpan<byte> source)
		{
			return SystemBinaryPrimitives.ReadUInt16LittleEndian(source);
		}

		/// <summary>Reads a <see cref="T:System.UInt32" /> from the beginning of a read-only span of bytes, as big endian.</summary>
		/// <param name="source">The read-only span to read.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="source" /> is too small to contain a <see cref="T:System.UInt32" />.</exception>
		/// <returns>The big endian value.</returns>
		public static uint ReadUInt32BigEndian(ReadOnlySpan<byte> source)
		{
			return SystemBinaryPrimitives.ReadUInt32BigEndian(source);
		}

		/// <summary>Reads a <see cref="T:System.UInt32" /> from the beginning of a read-only span of bytes, as little endian.</summary>
		/// <param name="source">The read-only span of bytes to read.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="source" /> is too small to contain a <see cref="T:System.UInt32" />.</exception>
		/// <returns>The little endian value.</returns>
		public static uint ReadUInt32LittleEndian(ReadOnlySpan<byte> source)
		{
			return SystemBinaryPrimitives.ReadUInt32LittleEndian(source);
		}

		/// <summary>Reads a <see cref="T:System.UInt64" /> from the beginning of a read-only span of bytes, as big endian.</summary>
		/// <param name="source">The read-only span of bytes to read.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="source" /> is too small to contain a <see cref="T:System.UInt64" />.</exception>
		/// <returns>The big endian value.</returns>
		public static ulong ReadUInt64BigEndian(ReadOnlySpan<byte> source)
		{
			return SystemBinaryPrimitives.ReadUInt64BigEndian(source);
		}

		/// <summary>Reads a <see cref="T:System.UInt64" /> from the beginning of a read-only span of bytes, as little endian.</summary>
		/// <param name="source">The read-only span of bytes to read.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="source" /> is too small to contain a <see cref="T:System.UInt64" />.</exception>
		/// <returns>The little endian value.</returns>
		public static ulong ReadUInt64LittleEndian(ReadOnlySpan<byte> source)
		{
			return SystemBinaryPrimitives.ReadUInt64LittleEndian(source);
		}

		/// <summary>Reverses a primitive value by performing an endianness swap of the specified <see cref="T:System.Byte" /> value, which effectively does nothing for a <see cref="T:System.Byte" />.</summary>
		/// <param name="value">The value to reverse.</param>
		/// <returns>The passed-in value, unmodified.</returns>
		public static byte ReverseEndianness(byte value)
		{
			return SystemBinaryPrimitives.ReverseEndianness(value);
		}

		/// <summary>Reverses a primitive value by performing an endianness swap of the specified <see cref="T:System.Int16" /> value.</summary>
		/// <param name="value">The value to reverse.</param>
		/// <returns>The reversed value.</returns>
		public static short ReverseEndianness(short value)
		{
			return SystemBinaryPrimitives.ReverseEndianness(value);
		}

		/// <summary>Reverses a primitive value by performing an endianness swap of the specified <see cref="T:System.Int32" /> value.</summary>
		/// <param name="value">The value to reverse.</param>
		/// <returns>The reversed value.</returns>
		public static int ReverseEndianness(int value)
		{
			return SystemBinaryPrimitives.ReverseEndianness(value);
		}

		/// <summary>Reverses a primitive value by performing an endianness swap of the specified <see cref="T:System.Int64" /> value.</summary>
		/// <param name="value">The value to reverse.</param>
		/// <returns>The reversed value.</returns>
		public static long ReverseEndianness(long value)
		{
			return SystemBinaryPrimitives.ReverseEndianness(value);
		}

		/// <summary>Reverses a primitive value by performing an endianness swap of the specified <see cref="T:System.SByte" /> value, which effectively does nothing for an <see cref="T:System.SByte" />.</summary>
		/// <param name="value">The value to reverse.</param>
		/// <returns>The passed-in value, unmodified.</returns>
		public static sbyte ReverseEndianness(sbyte value)
		{
			return SystemBinaryPrimitives.ReverseEndianness(value);
		}

		/// <summary>Reverses a primitive value by performing an endianness swap of the specified <see cref="T:System.UInt16" /> value.</summary>
		/// <param name="value">The value to reverse.</param>
		/// <returns>The reversed value.</returns>
		public static ushort ReverseEndianness(ushort value)
		{
			return SystemBinaryPrimitives.ReverseEndianness(value);
		}

		/// <summary>Reverses a primitive value by performing an endianness swap of the specified <see cref="T:System.UInt32" /> value.</summary>
		/// <param name="value">The value to reverse.</param>
		/// <returns>The reversed value.</returns>
		public static uint ReverseEndianness(uint value)
		{
			return SystemBinaryPrimitives.ReverseEndianness(value);
		}

		/// <summary>Reverses a primitive value by performing an endianness swap of the specified <see cref="T:System.UInt64" /> value.</summary>
		/// <param name="value">The value to reverse.</param>
		/// <returns>The reversed value.</returns>
		public static ulong ReverseEndianness(ulong value)
		{
			return SystemBinaryPrimitives.ReverseEndianness(value);
		}

		/// <summary>Reads a <see cref="T:System.Double" /> from the beginning of a read-only span of bytes, as big endian.</summary>
		/// <param name="source">The read-only span of bytes to read.</param>
		/// <param name="value">When this method returns, contains the value read out of the read-only span of bytes, as big endian.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain a <see cref="T:System.Double" />; otherwise, <see langword="false" />.</returns>
		public static bool TryReadDoubleBigEndian(ReadOnlySpan<byte> source, out double value)
		{
#if NET5_0_OR_GREATER
			return SystemBinaryPrimitives.TryReadDoubleBigEndian(source, out value);
#else
			if (SystemBinaryPrimitives.TryReadUInt64BigEndian(source, out ulong bits))
			{
				value = Unsafe.As<ulong, double>(ref bits);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
#endif
		}

		/// <summary>Reads a <see cref="T:System.Double" /> from the beginning of a read-only span of bytes, as little endian.</summary>
		/// <param name="source">The read-only span of bytes to read.</param>
		/// <param name="value">When this method returns, contains the value read out of the read-only span of bytes, as little endian.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain a <see cref="T:System.Double" />; otherwise, <see langword="false" />.</returns>
		public static bool TryReadDoubleLittleEndian(ReadOnlySpan<byte> source, out double value)
		{
#if NET5_0_OR_GREATER
			return SystemBinaryPrimitives.TryReadDoubleLittleEndian(source, out value);
#else
			if(SystemBinaryPrimitives.TryReadUInt64LittleEndian(source, out ulong bits))
			{
				value = Unsafe.As<ulong, double>(ref bits);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
#endif
		}

		/// <summary>Reads a <see cref="T:System.Half" /> from the beginning of a read-only span of bytes, as big endian.</summary>
		/// <param name="source">The read-only span of bytes to read.</param>
		/// <param name="value">When this method returns, the value read out of the read-only span of bytes, as big endian.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain a <see cref="T:System.Half" />; otherwise, <see langword="false" />.</returns>
		public static bool TryReadHalfBigEndian(ReadOnlySpan<byte> source, out Half value)
		{
#if NET6_0_OR_GREATER
			return SystemBinaryPrimitives.TryReadHalfBigEndian(source, out value);
#else
			if (SystemBinaryPrimitives.TryReadUInt16BigEndian(source, out ushort bits))
			{
				value = Unsafe.As<ushort, Half>(ref bits);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
#endif
		}

		/// <summary>Reads a <see cref="T:System.Half" /> from the beginning of a read-only span of bytes, as little endian.</summary>
		/// <param name="source">The read-only span of bytes to read.</param>
		/// <param name="value">When this method returns, the value read out of the read-only span of bytes, as little endian.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain a <see cref="T:System.Half" />; otherwise, <see langword="false" />.</returns>
		public static bool TryReadHalfLittleEndian(ReadOnlySpan<byte> source, out Half value)
		{
#if NET6_0_OR_GREATER
			return SystemBinaryPrimitives.TryReadHalfLittleEndian(source, out value);
#else
			if (SystemBinaryPrimitives.TryReadUInt16LittleEndian(source, out ushort bits))
			{
				value = Unsafe.As<ushort, Half>(ref bits);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
#endif
		}

		/// <summary>Reads an <see cref="T:System.Int16" /> from the beginning of a read-only span of bytes, as big endian.</summary>
		/// <param name="source">The read-only span of bytes to read.</param>
		/// <param name="value">When this method returns, the value read out of the read-only span of bytes, as big endian.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain an <see cref="T:System.Int16" />; otherwise, <see langword="false" />.</returns>
		public static bool TryReadInt16BigEndian(ReadOnlySpan<byte> source, out short value)
		{
			return SystemBinaryPrimitives.TryReadInt16BigEndian(source, out value);
		}

		/// <summary>Reads an <see cref="T:System.Int16" /> from the beginning of a read-only span of bytes, as little endian.</summary>
		/// <param name="source">The read-only span of bytes to read.</param>
		/// <param name="value">When this method returns, the value read out of the read-only span of bytes, as little endian.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain an <see cref="T:System.Int16" />; otherwise, <see langword="false" />.</returns>
		public static bool TryReadInt16LittleEndian(ReadOnlySpan<byte> source, out short value)
		{
			return SystemBinaryPrimitives.TryReadInt16LittleEndian(source, out value);
		}

		/// <summary>Reads an <see cref="T:System.Int32" /> from the beginning of a read-only span of bytes, as big endian.</summary>
		/// <param name="source">The read-only span of bytes to read.</param>
		/// <param name="value">When this method returns, the value read out of the read-only span of bytes, as big endian.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain an <see cref="T:System.Int32" />; otherwise, <see langword="false" />.</returns>
		public static bool TryReadInt32BigEndian(ReadOnlySpan<byte> source, out int value)
		{
			return SystemBinaryPrimitives.TryReadInt32BigEndian(source, out value);
		}

		/// <summary>Reads an <see cref="T:System.Int32" /> from the beginning of a read-only span of bytes, as little endian.</summary>
		/// <param name="source">The read-only span of bytes to read.</param>
		/// <param name="value">When this method returns, the value read out of the read-only span of bytes, as little endian.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain an <see cref="T:System.Int32" />; otherwise, <see langword="false" />.</returns>
		public static bool TryReadInt32LittleEndian(ReadOnlySpan<byte> source, out int value)
		{
			return SystemBinaryPrimitives.TryReadInt32LittleEndian(source, out value);
		}

		/// <summary>Reads an <see cref="T:System.Int64" /> from the beginning of a read-only span of bytes, as big endian.</summary>
		/// <param name="source">The read-only span of bytes to read.</param>
		/// <param name="value">When this method returns, the value read out of the read-only span of bytes, as big endian.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain an <see cref="T:System.Int64" />; otherwise, <see langword="false" />.</returns>
		public static bool TryReadInt64BigEndian(ReadOnlySpan<byte> source, out long value)
		{
			return SystemBinaryPrimitives.TryReadInt64BigEndian(source, out value);
		}

		/// <summary>Reads an <see cref="T:System.Int64" /> from the beginning of a read-only span of bytes, as little endian.</summary>
		/// <param name="source">The read-only span of bytes to read.</param>
		/// <param name="value">When this method returns, the value read out of the read-only span of bytes, as little endian.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain an <see cref="T:System.Int64" />; otherwise, <see langword="false" />.</returns>
		public static bool TryReadInt64LittleEndian(ReadOnlySpan<byte> source, out long value)
		{
			return SystemBinaryPrimitives.TryReadInt64LittleEndian(source, out value);
		}

		/// <summary>Reads a <see cref="T:System.Single" /> from the beginning of a read-only span of bytes, as big endian.</summary>
		/// <param name="source">The read-only span of bytes to read.</param>
		/// <param name="value">When this method returns, contains the value read out of the read-only span of bytes, as big endian.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain a <see cref="T:System.Single" />; otherwise, <see langword="false" />.</returns>
		public static bool TryReadSingleBigEndian(ReadOnlySpan<byte> source, out float value)
		{
#if NET5_0_OR_GREATER
			return SystemBinaryPrimitives.TryReadSingleBigEndian(source, out value);
#else
			if (SystemBinaryPrimitives.TryReadUInt32BigEndian(source, out uint bits))
			{
				value = Unsafe.As<uint, float>(ref bits);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
#endif
		}

		/// <summary>Reads a <see cref="T:System.Single" /> from the beginning of a read-only span of bytes, as little endian.</summary>
		/// <param name="source">The read-only span of bytes to read.</param>
		/// <param name="value">When this method returns, contains the value read out of the read-only span of bytes, as little endian.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain a <see cref="T:System.Single" />; otherwise, <see langword="false" />.</returns>
		public static bool TryReadSingleLittleEndian(ReadOnlySpan<byte> source, out float value)
		{
#if NET5_0_OR_GREATER
			return SystemBinaryPrimitives.TryReadSingleLittleEndian(source, out value);
#else
			if (SystemBinaryPrimitives.TryReadUInt32LittleEndian(source, out uint bits))
			{
				value = Unsafe.As<uint, float>(ref bits);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
#endif
		}

		/// <summary>Reads a <see cref="T:System.UInt16" /> from the beginning of a read-only span of bytes, as big endian.</summary>
		/// <param name="source">The read-only span of bytes to read.</param>
		/// <param name="value">When this method returns, the value read out of the read-only span of bytes, as big endian.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain a <see cref="T:System.UInt16" />; otherwise, <see langword="false" />.</returns>
		public static bool TryReadUInt16BigEndian(ReadOnlySpan<byte> source, out ushort value)
		{
			return SystemBinaryPrimitives.TryReadUInt16BigEndian(source, out value);
		}

		/// <summary>Reads a <see cref="T:System.UInt16" /> from the beginning of a read-only span of bytes, as little endian.</summary>
		/// <param name="source">The read-only span of bytes to read.</param>
		/// <param name="value">When this method returns, the value read out of the read-only span of bytes, as little endian.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain a <see cref="T:System.UInt16" />; otherwise, <see langword="false" />.</returns>
		public static bool TryReadUInt16LittleEndian(ReadOnlySpan<byte> source, out ushort value)
		{
			return SystemBinaryPrimitives.TryReadUInt16LittleEndian(source, out value);
		}

		/// <summary>Reads a <see cref="T:System.UInt32" /> from the beginning of a read-only span of bytes, as big endian.</summary>
		/// <param name="source">The read-only span of bytes to read.</param>
		/// <param name="value">When this method returns, the value read out of the read-only span of bytes, as big endian.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain a <see cref="T:System.UInt32" />; otherwise, <see langword="false" />.</returns>
		public static bool TryReadUInt32BigEndian(ReadOnlySpan<byte> source, out uint value)
		{
			return SystemBinaryPrimitives.TryReadUInt32BigEndian(source, out value);
		}

		/// <summary>Reads a <see cref="T:System.UInt32" /> from the beginning of a read-only span of bytes, as little endian.</summary>
		/// <param name="source">The read-only span of bytes to read.</param>
		/// <param name="value">When this method returns, the value read out of the read-only span of bytes, as little endian.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain a <see cref="T:System.UInt32" />; otherwise, <see langword="false" />.</returns>
		public static bool TryReadUInt32LittleEndian(ReadOnlySpan<byte> source, out uint value)
		{
			return SystemBinaryPrimitives.TryReadUInt32LittleEndian(source, out value);
		}

		/// <summary>Reads a <see cref="T:System.UInt64" /> from the beginning of a read-only span of bytes, as big endian.</summary>
		/// <param name="source">The read-only span of bytes to read.</param>
		/// <param name="value">When this method returns, the value read out of the read-only span of bytes, as big endian.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain a <see cref="T:System.UInt64" />; otherwise, <see langword="false" />.</returns>
		public static bool TryReadUInt64BigEndian(ReadOnlySpan<byte> source, out ulong value)
		{
			return SystemBinaryPrimitives.TryReadUInt64BigEndian(source, out value);
		}

		/// <summary>Reads a <see cref="T:System.UInt64" /> from the beginning of a read-only span of bytes, as little endian.</summary>
		/// <param name="source">The read-only span of bytes to read.</param>
		/// <param name="value">When this method returns, the value read out of the read-only span of bytes, as little endian.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain a <see cref="T:System.UInt64" />; otherwise, <see langword="false" />.</returns>
		public static bool TryReadUInt64LittleEndian(ReadOnlySpan<byte> source, out ulong value)
		{
			return SystemBinaryPrimitives.TryReadUInt64LittleEndian(source, out value);
		}

		/// <summary>Writes a <see cref="T:System.Double" /> into a span of bytes, as big endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as big endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain a <see cref="T:System.Double" />; otherwise, <see langword="false" />.</returns>
		public static bool TryWriteDoubleBigEndian(Span<byte> destination, double value)
		{
#if NET5_0_OR_GREATER
			return SystemBinaryPrimitives.TryWriteDoubleBigEndian(destination, value);
#else
			ulong bits = Unsafe.As<double, ulong>(ref value);
			return SystemBinaryPrimitives.TryWriteUInt64BigEndian(destination, bits);
#endif
		}

		/// <summary>Writes a <see cref="T:System.Double" /> into a span of bytes, as little endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as little endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain a <see cref="T:System.Double" />; otherwise, <see langword="false" />.</returns>
		public static bool TryWriteDoubleLittleEndian(Span<byte> destination, double value)
		{
#if NET5_0_OR_GREATER
			return SystemBinaryPrimitives.TryWriteDoubleLittleEndian(destination, value);
#else
			ulong bits = Unsafe.As<double, ulong>(ref value);
			return SystemBinaryPrimitives.TryWriteUInt64LittleEndian(destination, bits);
#endif
		}

		/// <summary>Writes a <see cref="T:System.Half" /> into a span of bytes, as big endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as big endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain a <see cref="T:System.Half" />; otherwise, <see langword="false" />.</returns>
		public static bool TryWriteHalfBigEndian(Span<byte> destination, Half value)
		{
#if NET6_0_OR_GREATER
			return SystemBinaryPrimitives.TryWriteHalfBigEndian(destination, value);
#else
			ushort bits = Unsafe.As<Half, ushort>(ref value);
			return SystemBinaryPrimitives.TryWriteUInt16BigEndian(destination, bits);
#endif
		}

		/// <summary>Writes a <see cref="T:System.Half" /> into a span of bytes, as little endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as little endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain a <see cref="T:System.Half" />; otherwise, <see langword="false" />.</returns>
		public static bool TryWriteHalfLittleEndian(Span<byte> destination, Half value)
		{
#if NET6_0_OR_GREATER
			return SystemBinaryPrimitives.TryWriteHalfLittleEndian(destination, value);
#else
			ushort bits = Unsafe.As<Half, ushort>(ref value);
			return SystemBinaryPrimitives.TryWriteUInt16LittleEndian(destination, bits);
#endif
		}

		/// <summary>Writes an <see cref="T:System.Int16" /> into a span of bytes, as big endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as big endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain an <see cref="T:System.Int16" />; otherwise, <see langword="false" />.</returns>
		public static bool TryWriteInt16BigEndian(Span<byte> destination, short value)
		{
			return SystemBinaryPrimitives.TryWriteInt16BigEndian(destination, value);
		}

		/// <summary>Writes an <see cref="T:System.Int16" /> into a span of bytes, as little endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as little endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain an <see cref="T:System.Int16" />; otherwise, <see langword="false" />.</returns>
		public static bool TryWriteInt16LittleEndian(Span<byte> destination, short value)
		{
			return SystemBinaryPrimitives.TryWriteInt16LittleEndian(destination, value);
		}

		/// <summary>Writes an <see cref="T:System.Int32" /> into a span of bytes, as big endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as big endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain an <see cref="T:System.Int32" />; otherwise, <see langword="false" />.</returns>
		public static bool TryWriteInt32BigEndian(Span<byte> destination, int value)
		{
			return SystemBinaryPrimitives.TryWriteInt32BigEndian(destination, value);
		}

		/// <summary>Writes an <see cref="T:System.Int32" /> into a span of bytes, as little endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as little endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain an <see cref="T:System.Int32" />; otherwise, <see langword="false" />.</returns>
		public static bool TryWriteInt32LittleEndian(Span<byte> destination, int value)
		{
			return SystemBinaryPrimitives.TryWriteInt32LittleEndian(destination, value);
		}

		/// <summary>Writes an <see cref="T:System.Int64" /> into a span of bytes, as big endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as big endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain an <see cref="T:System.Int64" />; otherwise, <see langword="false" />.</returns>
		public static bool TryWriteInt64BigEndian(Span<byte> destination, long value)
		{
			return SystemBinaryPrimitives.TryWriteInt64BigEndian(destination, value);
		}

		/// <summary>Writes an <see cref="T:System.Int64" /> into a span of bytes, as little endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as little endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain an <see cref="T:System.Int64" />; otherwise, <see langword="false" />.</returns>
		public static bool TryWriteInt64LittleEndian(Span<byte> destination, long value)
		{
			return SystemBinaryPrimitives.TryWriteInt64LittleEndian(destination, value);
		}

		/// <summary>Writes a <see cref="T:System.Single" /> into a span of bytes, as big endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as big endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain a <see cref="T:System.Single" />; otherwise, <see langword="false" />.</returns>
		public static bool TryWriteSingleBigEndian(Span<byte> destination, float value)
		{
#if NET5_0_OR_GREATER
			return SystemBinaryPrimitives.TryWriteSingleBigEndian(destination, value);
#else
			uint bits = Unsafe.As<float, uint>(ref value);
			return SystemBinaryPrimitives.TryWriteUInt32BigEndian(destination, bits);
#endif
		}

		/// <summary>Writes a <see cref="T:System.Single" /> into a span of bytes, as little endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as little endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain a <see cref="T:System.Single" />; otherwise, <see langword="false" />.</returns>
		public static bool TryWriteSingleLittleEndian(Span<byte> destination, float value)
		{
#if NET5_0_OR_GREATER
			return SystemBinaryPrimitives.TryWriteSingleLittleEndian(destination, value);
#else
			uint bits = Unsafe.As<float, uint>(ref value);
			return SystemBinaryPrimitives.TryWriteUInt32LittleEndian(destination, bits);
#endif
		}

		/// <summary>Writes a <see cref="T:System.UInt16" /> into a span of bytes, as big endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as big endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain a <see cref="T:System.UInt16" />; otherwise, <see langword="false" />.</returns>
		public static bool TryWriteUInt16BigEndian(Span<byte> destination, ushort value)
		{
			return SystemBinaryPrimitives.TryWriteUInt16BigEndian(destination, value);
		}

		/// <summary>Writes a <see cref="T:System.UInt16" /> into a span of bytes, as little endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as little endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain a <see cref="T:System.UInt16" />; otherwise, <see langword="false" />.</returns>
		public static bool TryWriteUInt16LittleEndian(Span<byte> destination, ushort value)
		{
			return SystemBinaryPrimitives.TryWriteUInt16LittleEndian(destination, value);
		}

		/// <summary>Writes a <see cref="T:System.UInt32" /> into a span of bytes, as big endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as big endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain a <see cref="T:System.UInt32" />; otherwise, <see langword="false" />.</returns>
		public static bool TryWriteUInt32BigEndian(Span<byte> destination, uint value)
		{
			return SystemBinaryPrimitives.TryWriteUInt32BigEndian(destination, value);
		}

		/// <summary>Writes a <see cref="T:System.UInt32" /> into a span of bytes, as little endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as little endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain a <see cref="T:System.UInt32" />; otherwise, <see langword="false" />.</returns>
		public static bool TryWriteUInt32LittleEndian(Span<byte> destination, uint value)
		{
			return SystemBinaryPrimitives.TryWriteUInt32LittleEndian(destination, value);
		}

		/// <summary>Writes a <see cref="T:System.UInt64" /> into a span of bytes, as big endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as big endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain a <see cref="T:System.UInt64" />; otherwise, <see langword="false" />.</returns>
		public static bool TryWriteUInt64BigEndian(Span<byte> destination, ulong value)
		{
			return SystemBinaryPrimitives.TryWriteUInt64BigEndian(destination, value);
		}

		/// <summary>Writes a <see cref="T:System.UInt64" /> into a span of bytes, as little endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as little endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <returns>
		///   <see langword="true" /> if the span is large enough to contain a <see cref="T:System.UInt64" />; otherwise, <see langword="false" />.</returns>
		public static bool TryWriteUInt64LittleEndian(Span<byte> destination, ulong value)
		{
			return SystemBinaryPrimitives.TryWriteUInt64LittleEndian(destination, value);
		}

		/// <summary>Writes a <see cref="T:System.Double" /> into a span of bytes, as big endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as big endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="destination" /> is too small to contain a <see cref="T:System.Double" />.</exception>
		public static void WriteDoubleBigEndian(Span<byte> destination, double value)
		{
#if NET5_0_OR_GREATER
			SystemBinaryPrimitives.WriteDoubleBigEndian(destination, value);
#else
			ulong bits = Unsafe.As<double, ulong>(ref value);
			SystemBinaryPrimitives.WriteUInt64BigEndian(destination, bits);
#endif
		}

		/// <summary>Writes a <see cref="T:System.Double" /> into a span of bytes, as little endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as little endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="destination" /> is too small to contain a <see cref="T:System.Double" />.</exception>
		public static void WriteDoubleLittleEndian(Span<byte> destination, double value)
		{
#if NET5_0_OR_GREATER
			SystemBinaryPrimitives.WriteDoubleLittleEndian(destination, value);
#else
			ulong bits = Unsafe.As<double, ulong>(ref value);
			SystemBinaryPrimitives.WriteUInt64LittleEndian(destination, bits);
#endif
		}

		/// <summary>Writes a <see cref="T:System.Half" /> into a span of bytes, as big endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as big endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="destination" /> is too small to contain a <see cref="T:System.Half" />.</exception>
		public static void WriteHalfBigEndian(Span<byte> destination, Half value)
		{
#if NET6_0_OR_GREATER
			SystemBinaryPrimitives.WriteHalfBigEndian(destination, value);
#else
			ushort bits = Unsafe.As<Half, ushort>(ref value);
			SystemBinaryPrimitives.WriteUInt16BigEndian(destination, bits);
#endif
		}

		/// <summary>Writes a <see cref="T:System.Half" /> into a span of bytes, as little endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as little endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="destination" /> is too small to contain a <see cref="T:System.Half" />.</exception>
		public static void WriteHalfLittleEndian(Span<byte> destination, Half value)
		{
#if NET6_0_OR_GREATER
			SystemBinaryPrimitives.WriteHalfLittleEndian(destination, value);
#else
			ushort bits = Unsafe.As<Half, ushort>(ref value);
			SystemBinaryPrimitives.WriteUInt16LittleEndian(destination, bits);
#endif
		}

		/// <summary>Writes an <see cref="T:System.Int16" /> into a span of bytes, as big endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as big endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="destination" /> is too small to contain an <see cref="T:System.Int16" />.</exception>
		public static void WriteInt16BigEndian(Span<byte> destination, short value)
		{
			SystemBinaryPrimitives.WriteInt16BigEndian(destination, value);
		}

		/// <summary>Writes an <see cref="T:System.Int16" /> into a span of bytes, as little endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as little endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="destination" /> is too small to contain an <see cref="T:System.Int16" />.</exception>
		public static void WriteInt16LittleEndian(Span<byte> destination, short value)
		{
			SystemBinaryPrimitives.WriteInt16LittleEndian(destination, value);
		}

		/// <summary>Writes an <see cref="T:System.Int32" /> into a span of bytes, as big endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as big endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="destination" /> is too small to contain an <see cref="T:System.Int32" />.</exception>
		public static void WriteInt32BigEndian(Span<byte> destination, int value)
		{
			SystemBinaryPrimitives.WriteInt32BigEndian(destination, value);
		}

		/// <summary>Writes an <see cref="T:System.Int32" /> into a span of bytes, as little endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as little endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="destination" /> is too small to contain an <see cref="T:System.Int32" />.</exception>
		public static void WriteInt32LittleEndian(Span<byte> destination, int value)
		{
			SystemBinaryPrimitives.WriteInt32LittleEndian(destination, value);
		}

		/// <summary>Writes an <see cref="T:System.Int64" /> into a span of bytes, as big endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as big endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="destination" /> is too small to contain an <see cref="T:System.Int64" />.</exception>
		public static void WriteInt64BigEndian(Span<byte> destination, long value)
		{
			SystemBinaryPrimitives.WriteInt64BigEndian(destination, value);
		}

		/// <summary>Writes an <see cref="T:System.Int64" /> into a span of bytes, as little endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as little endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="destination" /> is too small to contain an <see cref="T:System.Int64" />.</exception>
		public static void WriteInt64LittleEndian(Span<byte> destination, long value)
		{
			SystemBinaryPrimitives.WriteInt64LittleEndian(destination, value);
		}

		/// <summary>Writes a <see cref="T:System.Single" /> into a span of bytes, as big endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as big endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="destination" /> is too small to contain a <see cref="T:System.Single" />.</exception>
		public static void WriteSingleBigEndian(Span<byte> destination, float value)
		{
#if NET5_0_OR_GREATER
			SystemBinaryPrimitives.WriteSingleBigEndian(destination, value);
#else
			uint bits = Unsafe.As<float, uint>(ref value);
			SystemBinaryPrimitives.WriteUInt32BigEndian(destination, bits);
#endif
		}

		/// <summary>Writes a <see cref="T:System.Single" /> into a span of bytes, as little endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as little endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="destination" /> is too small to contain a <see cref="T:System.Single" />.</exception>
		public static void WriteSingleLittleEndian(Span<byte> destination, float value)
		{
#if NET5_0_OR_GREATER
			SystemBinaryPrimitives.WriteSingleLittleEndian(destination, value);
#else
			uint bits = Unsafe.As<float, uint>(ref value);
			SystemBinaryPrimitives.WriteUInt32LittleEndian(destination, bits);
#endif
		}

		/// <summary>Writes a <see cref="T:System.UInt16" /> into a span of bytes, as big endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as big endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="destination" /> is too small to contain a <see cref="T:System.UInt16" />.</exception>
		public static void WriteUInt16BigEndian(Span<byte> destination, ushort value)
		{
			SystemBinaryPrimitives.WriteUInt16BigEndian(destination, value);
		}

		/// <summary>Writes a <see cref="T:System.UInt16" /> into a span of bytes, as little endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as little endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="destination" /> is too small to contain a <see cref="T:System.UInt16" />.</exception>
		public static void WriteUInt16LittleEndian(Span<byte> destination, ushort value)
		{
			SystemBinaryPrimitives.WriteUInt16LittleEndian(destination, value);
		}

		/// <summary>Writes a <see cref="T:System.UInt32" /> into a span of bytes, as big endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as big endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="destination" /> is too small to contain a <see cref="T:System.UInt32" />.</exception>
		public static void WriteUInt32BigEndian(Span<byte> destination, uint value)
		{
			SystemBinaryPrimitives.WriteUInt32BigEndian(destination, value);
		}

		/// <summary>Writes a <see cref="T:System.UInt32" /> into a span of bytes, as little endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as little endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="destination" /> is too small to contain a <see cref="T:System.UInt32" />.</exception>
		public static void WriteUInt32LittleEndian(Span<byte> destination, uint value)
		{
			SystemBinaryPrimitives.WriteUInt32LittleEndian(destination, value);
		}

		/// <summary>Writes a <see cref="T:System.UInt64" /> into a span of bytes, as big endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as big endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="destination" /> is too small to contain a <see cref="T:System.UInt64" />.</exception>
		public static void WriteUInt64BigEndian(Span<byte> destination, ulong value)
		{
			SystemBinaryPrimitives.WriteUInt64BigEndian(destination, value);
		}

		/// <summary>Writes a <see cref="T:System.UInt64" /> into a span of bytes, as little endian.</summary>
		/// <param name="destination">The span of bytes where the value is to be written, as little endian.</param>
		/// <param name="value">The value to write into the span of bytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="destination" /> is too small to contain a <see cref="T:System.UInt64" />.</exception>
		public static void WriteUInt64LittleEndian(Span<byte> destination, ulong value)
		{
			SystemBinaryPrimitives.WriteUInt64LittleEndian(destination, value);
		}
	}
}
