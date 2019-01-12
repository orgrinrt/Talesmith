namespace Talesmith.Core.Utils
{
    public class BitHelper
    {
        public static int Combine(byte b1, byte b2)
        {
            int combined = b1 << 8 | b2;
            return combined;
        }
    }
}