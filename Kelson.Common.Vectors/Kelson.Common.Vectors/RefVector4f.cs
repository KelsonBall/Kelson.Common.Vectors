using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Kelson.Common.Vectors
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly ref struct RefVector4f
    {
        private readonly float x;
        private readonly float y;
        private readonly float z;
        private readonly float w;        

        public double X => x;
        public double Y => y;
        public double Z => z;
        public double W => w;

        public RefVector4f(in ReadOnlySpan<float> data) => (x, y, z, w) = (data[0], data[1], data[2], data[3]);

        public RefVector4f(float x, float y, float z, float w) => (this.x, this.y, this.z, this.w) = (x, y, z, w);

        public RefVector4f(double x, double y, double z, double w) => (this.x, this.y, this.z, this.w) = ((float)x, (float)y, (float)z, (float)w);

        public static implicit operator Vector4fd(RefVector4f v) => new Vector4fd(v.X, v.Y, v.Z, v.W);

        public override string ToString() => $"<{x},{y},{z},{w}>";

        public unsafe ReadOnlySpan<float> AsSpan()
        {
            fixed (RefVector4f* vec = &this)
                return new ReadOnlySpan<float>(vec, 16);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double MagnitudeSquared() => Dot(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Magnitude() => Math.Sqrt(Dot(this));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Dot(in RefVector4f other) => (X * other.X) + (Y * other.Y) + (Z * other.Z) + (W * other.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefVector4f Add(in RefVector4f other) => new RefVector4f(X + other.X, Y + other.Y, Z + other.Z, W + other.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefVector4f Sub(in RefVector4f other) => new RefVector4f(X - other.X, Y - other.Y, Z - other.Z, W - other.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefVector4f Scale(double scalar) => new RefVector4f(X * scalar, Y * scalar, Z * scalar, W * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefVector4f Unit() => Scale(1d / Magnitude());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double AngularMagnitude(in RefVector4f other) => Math.Acos(Dot(other) / (Magnitude() * other.Magnitude()));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RefVector4f(in (float x, float y, float z, float w) t) => new RefVector4f(t.x, t.y, t.z, t.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RefVector4f(Tuple<double, double, double, double> t) => new RefVector4f(t.Item1, t.Item2, t.Item3, t.Item4);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RefVector4f(Tuple<float, float, float, float> t) => new RefVector4f(t.Item1, t.Item2, t.Item3, t.Item4);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator RefVector4f(double[] t)
        {
            if (t.Length != 4)
                throw new InvalidOperationException($"Array length must be 4 to cast to {nameof(RefVector4f)}");
            return new RefVector4f(t[0], t[1], t[2], t[3]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator RefVector4f(float[] t)
        {
            if (t.Length != 4)
                throw new InvalidOperationException($"Array length must be 4 to cast to {nameof(RefVector4f)}");
            return new RefVector4f(t[0], t[1], t[2], t[3]);
        }

        public static RefVector4f operator -(RefVector4f a, RefVector4f b) => a.Sub(b);
        public static RefVector4f operator -(RefVector4f a) => a.Scale(-1);
        public static RefVector4f operator +(RefVector4f a, RefVector4f b) => a.Sub(b);
        public static double operator *(RefVector4f a, RefVector4f b) => a.Dot(b);
        public static RefVector4f operator *(RefVector4f a, double s) => a.Scale(s);
        public static RefVector4f operator *(double s, RefVector4f b) => b.Scale(s);
        public static RefVector4f operator /(RefVector4f a, double s) => a.Scale(1 / s);
    }
}
