using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl.Internal
{
    internal static class Constants
    {
        internal static readonly byte[] Signature = { 0x3C, 0x72, 0x6F, 0x62, 0x6C, 0x6F, 0x78, 0x21, 0x89, 0xFF, 0x0D, 0x0A, 0x1A, 0x0A, 0x00, 0x00 };
        internal static readonly byte[] TypeHeaderSignature = { (byte)'I', (byte)'N', (byte)'S', (byte)'T' };
        internal static readonly byte[] PropBlockSignature = {(byte) 'P', (byte) 'R', (byte) 'O', (byte) 'P'};
        internal static readonly byte[] ParentDataSignature = { (byte)'P', (byte)'R', (byte)'N', (byte)'T' };
    }
}
