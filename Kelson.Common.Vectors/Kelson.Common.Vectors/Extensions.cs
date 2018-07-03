using System;

namespace Kelson.Common.Vectors
{
    public static class Extensions
    {
        [Obsolete("Tested as actually being SLOWER than 1/Math.Sqrt(value) on .net 4.6.1 x64 by around 15%")]
        // https://cs.uwaterloo.ca/~m32rober/rsqrt.pdf
        public static unsafe double FastInverseSqrt(this double value)
        {
            double xhalf = 0.5f * value;
            long i = *(long*)&value;
            i = 0x5FE6EB50C7B537A9 - (i >> 1);
            value = *(double*)&i;
            value = value * (1.5f - xhalf * value * value);
            return value;
        }
    }
}
