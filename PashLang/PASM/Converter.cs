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

        public static unsafe byte[] int64(ulong i)
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

        public static unsafe byte[] float4(float value)
        {
            uint val = *((uint*)&value);
            return new byte[4] {
                    (byte)(val & 0xFF),
                    (byte)((val >> 8) & 0xFF),
                    (byte)((val >> 16) & 0xFF),
                    (byte)((val >> 24) & 0xFF) };
        }

        public static unsafe byte[] double8(double value)
        {
            ulong val = *((ulong*)&value);
            return new byte[8] {
                    (byte)(val & 0xFF),
                    (byte)((val >> 8) & 0xFF),
                    (byte)((val >> 16) & 0xFF),
                    (byte)((val >> 24) & 0xFF),
                    (byte)((val >> 32) & 0xFF),
                    (byte)((val >> 40) & 0xFF),
                    (byte)((val >> 48) & 0xFF),
                    (byte)((val >> 56) & 0xFF)
            };
        }

        public static unsafe byte[] int32(uint i)
        {
            byte[] arr = new byte[4];
            byte* pi = (byte*)&i;
            arr[0] = pi[0];
            arr[1] = pi[1];
            arr[2] = pi[2];
            arr[3] = pi[3];
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

        public static unsafe byte[] int16(ushort i)
        {
            byte[] arr = new byte[2];
            byte* pi = (byte*)&i;
            arr[0] = pi[0];
            arr[1] = pi[1];
            return arr;
        }

        //Unsigned

        public static ushort ParseStringToUShort(string s)
        {
            ushort value = 0;
            for (int i = 0; i < s.Length; i++)
            {
                value = (ushort)(value * 10 + (s[i] - '0'));
            }
            return value;
        }

        public static uint ParseStringToUInt(string s)
        {
            uint value = 0;
            for (int i = 0; i < s.Length; i++)
            {
                value = (uint)(value * 10 + (s[i] - '0'));
            }
            return value;
        }

        public static ulong ParseStringToULong(string s)
        {
            ulong value = 0;
            for (int i = 0; i < s.Length; i++)
            {
                value = (value * 10 + (ulong)(s[i] - '0'));
            }
            return value;
        }

        //Signed

        public static short ParseStringToShort(string s)
        {
            short value = 0;
            for (int i = 0; i < s.Length; i++)
            {
                value = (short)(value * 10 + (s[i] - '0'));
            }
            return value;
        }

        public static int ParseStringToInt(string s)
        {
            int value = 0;
            for (int i = 0; i < s.Length; i++)
            {
                value = (value * 10 + (s[i] - '0'));
            }
            return value;
        }

        public static long ParseStringToLong(string s)
        {
            long value = 0;
            for (int i = 0; i < s.Length; i++)
            {
                value = value * 10 + (s[i] - '0');
            }
            return value;
        }

        public static short ParseStringToShort_NEG_CHECK(string s)
        {
            bool negative = false;
            negative = s[0] == '-';

            short value = 0;
            for (int i = negative ? 1 : 0; i < s.Length; i++)
            {
                value = (short)(value * 10 + (s[i] - '0'));
            }
            return negative ? (short)-value : value;
        }

        public static int ParseStringToInt_NEG_CHECK(string s)
        {
            bool negative = false;
            negative = s[0] == '-';

            int value = 0;
            for (int i = negative ? 1 : 0; i < s.Length; i++)
            {
                value = value * 10 + (s[i] - '0');
            }
            return negative ? value * -1 : value;
        }

        public static long ParseStringToLong_NEG_CHECK(string s)
        {
            bool negative = false;
            negative = s[0] == '-';

            long value = 0;
            for (int i = negative ? 1 : 0; i < s.Length; i++)
            {
                value = value * 10 + (s[i] - '0');
            }
            return negative ? -value : value;
        }
    }
}