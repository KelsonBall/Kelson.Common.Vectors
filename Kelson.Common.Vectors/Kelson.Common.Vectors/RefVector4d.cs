using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Kelson.Common.Vectors
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly ref struct RefVector4d
    {        
        public double X { get; }
        public double Y { get; }
        public double Z { get; }
        public double W { get; }

        public RefVector4d(in ReadOnlySpan<double> data) => (X, Y, Z, W) = (data[0], data[1], data[2], data[3]);

        public RefVector4d(double x, double y, double z, double w) => (X, Y, Z, W) = (x, y, z, w);

        public static implicit operator Vector4fd(RefVector4d v) => new Vector4fd(v.X, v.Y, v.Z, v.W);

        public override string ToString() => $"<{X},{Y},{Z},{W}>";

        public unsafe ReadOnlySpan<double> AsSpan()
        {
            fixed (RefVector4d* vec = &this)
                return new ReadOnlySpan<double>(vec, 32);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double MagnitudeSquared() => Dot(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Magnitude() => Math.Sqrt(Dot(this));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Dot(in RefVector4d other) => (X * other.X) + (Y * other.Y) + (Z * other.Z) + (W * other.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefVector4d Add(in RefVector4d other) => new RefVector4d(X + other.X, Y + other.Y, Z + other.Z, W + other.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefVector4d Sub(in RefVector4d other) => new RefVector4d(X - other.X, Y - other.Y, Z - other.Z, W - other.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefVector4d Scale(double scalar) => new RefVector4d(X * scalar, Y * scalar, Z * scalar, W * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefVector4d Unit() => Scale(1d / Magnitude());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double AngularMagnitude(in RefVector4d other) => Math.Acos(Dot(other) / (Magnitude() * other.Magnitude()));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RefVector4d(in (float x, float y, float z, float w) t) => new RefVector4d(t.x, t.y, t.z, t.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RefVector4d(Tuple<double, double, double, double> t) => new RefVector4d(t.Item1, t.Item2, t.Item3, t.Item4);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RefVector4d(Tuple<float, float, float, float> t) => new RefVector4d(t.Item1, t.Item2, t.Item3, t.Item4);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator RefVector4d(double[] t)
        {
            if (t.Length != 4)
                throw new InvalidOperationException($"Array length must be 4 to cast to {nameof(RefVector4d)}");
            return new RefVector4d(t[0], t[1], t[2], t[3]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator RefVector4d(float[] t)
        {
            if (t.Length != 4)
                throw new InvalidOperationException($"Array length must be 4 to cast to {nameof(RefVector4d)}");
            return new RefVector4d(t[0], t[1], t[2], t[3]);
        }

        public static RefVector4d operator -(RefVector4d a, RefVector4d b) => a.Sub(b);
        public static RefVector4d operator -(RefVector4d a) => a.Scale(-1);
        public static RefVector4d operator +(RefVector4d a, RefVector4d b) => a.Sub(b);
        public static double operator *(RefVector4d a, RefVector4d b) => a.Dot(b);
        public static RefVector4d operator *(RefVector4d a, double s) => a.Scale(s);
        public static RefVector4d operator *(double s, RefVector4d b) => b.Scale(s);
        public static RefVector4d operator /(RefVector4d a, double s) => a.Scale(1 / s);
    }
}
