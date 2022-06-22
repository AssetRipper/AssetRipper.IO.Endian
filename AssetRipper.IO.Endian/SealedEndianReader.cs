using System.IO;
using System.Text;

namespace AssetRipper.IO.Endian
{
	public sealed class SealedEndianReader : EndianReader
	{
		public SealedEndianReader(Stream stream, bool isLittleEndian) : base(stream, isLittleEndian)
		{
		}

		public SealedEndianReader(Stream stream, bool isLittleEndian, Encoding encoding) : base(stream, isLittleEndian, encoding)
		{
		}

		public SealedEndianReader(Stream stream, bool isLittleEndian, Encoding encoding, bool leaveOpen) : base(stream, isLittleEndian, encoding, leaveOpen)
		{
		}
	}
}
