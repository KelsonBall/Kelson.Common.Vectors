using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Kelson.Common.Vectors
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly partial struct Vector4fd : IVector<Vector4fd>
    {
        //[FieldOffset(0)]
        private readonly float x;
        public double X => x;

        //[FieldOffset(4)]
        private readonly float y;
        public double Y => y;

        //[FieldOffset(8)]
        private readonly float z;
        public double Z => z;

        //[FieldOffset(12)]
        private readonly float w;
        public double W => w;

        public Vector4fd(double x, double y, double z, double w) : this((float)x, (float)y, (float)z, (float)w) { }

        public Vector4fd(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double MagnitudeSquared() => Dot(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Magnitude() => Math.Sqrt(Dot(this));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Dot(in Vector4fd other) => (X * other.X) + (Y * other.Y) + (Z * other.Z) + (W * other.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4fd Add(in Vector4fd other) => new Vector4fd(X + other.X, Y + other.Y, Z + other.Z, W + other.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4fd Sub(in Vector4fd other) => new Vector4fd(X - other.X, Y - other.Y, Z - other.Z, W - other.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4fd Scale(double scalar) => new Vector4fd(X * scalar, Y * scalar, Z * scalar, W * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4fd Unit() => Scale(1d / Magnitude());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double AngularMagnitude(in Vector4fd other) => Math.Acos(Dot(other) / (Magnitude() * other.Magnitude()) );

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector4fd(in (float x, float y, float z, float w) t) => new Vector4fd(t.x, t.y, t.z, t.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector4fd(Tuple<double, double, double, double> t) => new Vector4fd(t.Item1, t.Item2, t.Item3, t.Item4);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector4fd(Tuple<float, float, float, float> t) => new Vector4fd(t.Item1, t.Item2, t.Item3, t.Item4);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector4fd(double[] t)
        {
            if (t.Length != 4)
                throw new InvalidOperationException($"Array length must be 4 to cast to {nameof(Vector4fd)}");
            return new Vector4fd(t[0], t[1], t[2], t[3]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector4fd(float[] t)
        {
            if (t.Length != 4)
                throw new InvalidOperationException($"Array length must be 4 to cast to {nameof(Vector4fd)}");
            return new Vector4fd(t[0], t[1], t[2], t[3]);
        }

        public static implicit operator RefVector4f(Vector4fd v) => new RefVector4f(v.X, v.Y, v.Z, v.W);

        public static Vector4fd operator -(Vector4fd a, Vector4fd b) => a.Sub(b);
        public static Vector4fd operator -(Vector4fd a) => a.Scale(-1);
        public static Vector4fd operator +(Vector4fd a, Vector4fd b) => a.Sub(b);
        public static double operator *(Vector4fd a, Vector4fd b) => a.Dot(b);
        public static Vector4fd operator *(Vector4fd a, double s) => a.Scale(s);
        public static Vector4fd operator *(double s, Vector4fd b) => b.Scale(s);
        public static Vector4fd operator /(Vector4fd a, double s) => a.Scale(1 / s);
        public static bool operator ==(Vector4fd a, Vector4fd b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.W == b.W;
        public static bool operator !=(Vector4fd a, Vector4fd b) => a.X != b.X || a.Y != b.Y || a.Z != b.Z || a.W != b.W;

        public unsafe ReadOnlySpan<float> AsSpan()
        {
            fixed (Vector4fd* data = &this)
                return new ReadOnlySpan<float>(data, 4);
        }

        public override string ToString() => $"<{x},{y},{z},{w}>";
    }
}
