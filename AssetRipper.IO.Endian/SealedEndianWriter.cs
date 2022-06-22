using System.IO;
using System.Text;

namespace AssetRipper.IO.Endian
{
	public sealed class SealedEndianWriter : EndianWriter
	{
		public SealedEndianWriter(Stream stream, bool isLittleEndian) : base(stream, isLittleEndian)
		{
		}

		public SealedEndianWriter(Stream stream, bool isLittleEndian, Encoding encoding) : base(stream, isLittleEndian, encoding)
		{
		}

		public SealedEndianWriter(Stream stream, bool isLittleEndian, Encoding encoding, bool leaveOpen) : base(stream, isLittleEndian, encoding, leaveOpen)
		{
		}
	}
}
