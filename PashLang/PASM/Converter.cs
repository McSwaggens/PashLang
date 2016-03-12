using System;
namespace PASM
{
    /// <summary>
    /// Untra Fast conversion
    /// </summary>
    public class Converter
    {
        public static unsafe byte[] int64(long i)
        {
            byte[] arr = new byte[8];
            byte* pi = (byte*)&i;
            arr[0] = pi[0];
            arr[1] = pi[1];
            arr[2] = pi[2];
            arr[3] = pi[3];
            arr[4] = pi[4];
            arr[5] = pi[5];
            arr[6] = pi[6];
            arr[7] = pi[7];
            return arr;
        }
        public static unsafe byte[] int32(int i)
        {
            byte[] arr = new byte[4];
            byte* pi = (byte*)&i;
            arr[0] = pi[0];
            arr[1] = pi[1];
            arr[2] = pi[2];
            arr[3] = pi[3];
            return arr;
        }
        public static unsafe byte[] int16(short i)
        {
            byte[] arr = new byte[2];
            byte* pi = (byte*)&i;
            arr[0] = pi[0];
            arr[1] = pi[1];
            return arr;
        }

        public static short ParseStringToShort(string s)
        {
            short value = 0;
            for (var i = 0; i < s.Length; i++)
            {
                value = (short)(value * 10 + (s[i] - '0'));
            }
            return value;
        }

        public static int ParseStringToInt(string s)
        {
            int value = 0;
            for (var i = 0; i < s.Length; i++)
            {
                value = value * 10 + (s[i] - '0');
            }
            return value;
        }

        public static long ParseStringToLong(string s)
        {
            long value = 0;
            for (var i = 0; i < s.Length; i++)
            {
                value = value * 10 + (s[i] - '0');
            }
            return value;
        }
    }
}