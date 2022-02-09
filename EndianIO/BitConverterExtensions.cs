using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AssetRipper.EndianIO
{
	internal static class BitConverterExtensions
	{
		[StructLayout(LayoutKind.Explicit)]
		private struct FloatUIntUnion
		{
			[FieldOffset(0)]
			public uint Int;
			[FieldOffset(0)]
			public float Float;
		}

		[StructLayout(LayoutKind.Explicit)]
		private struct HalfUShortUnion
		{
			[FieldOffset(0)]
			public ushort Short;
			[FieldOffset(0)]
			public Half Half;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ushort ToUInt16(Half value)
		{
			return new HalfUShortUnion { Half = value }.Short;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint ToUInt32(float value)
		{
			return new FloatUIntUnion { Float = value }.Int;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong ToUInt64(double value)
		{
			return unchecked((ulong)BitConverter.DoubleToInt64Bits(value));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Half ToHalf(ushort value)
		{
			return new HalfUShortUnion { Short = value }.Half;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float ToSingle(uint value)
		{
			return new FloatUIntUnion { Int = value }.Float;
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
