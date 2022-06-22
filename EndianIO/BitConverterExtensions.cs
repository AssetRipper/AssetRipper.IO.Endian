using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AssetRipper.EndianIO
{
	internal static class BitConverterExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ushort ToUInt16(Half value)
		{
			return Unsafe.As<Half, ushort>(ref value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint ToUInt32(float value)
		{
			return Unsafe.As<float, uint>(ref value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong ToUInt64(double value)
		{
			return unchecked((ulong)BitConverter.DoubleToInt64Bits(value));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Half ToHalf(ushort value)
		{
			return Unsafe.As<ushort, Half>(ref value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float ToSingle(uint value)
		{
			return Unsafe.As<uint, float>(ref value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double ToDouble(ulong value)
		{
			return BitConverter.Int64BitsToDouble(unchecked((long)value));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void GetBytes(ushort value, byte[] buffer, int offset)
		{
			buffer[offset + 0] = unchecked((byte)(value >> 0));
			buffer[offset + 1] = unchecked((byte)(value >> 8));
		}

		public static void GetBytes(uint value, byte[] buffer, int offset)
		{
			buffer[offset + 0] = unchecked((byte)(value >> 0));
			buffer[offset + 1] = unchecked((byte)(value >> 8));
			buffer[offset + 2] = unchecked((byte)(value >> 16));
			buffer[offset + 3] = unchecked((byte)(value >> 24));
		}
	}
}
