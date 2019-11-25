using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Kelson.Common.Vectors
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly partial struct Vector3fd : IVector<Vector3fd>
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

        public Vector3fd(double x, double y, double z) : this((float)x, (float)y, (float)z) { }

        public Vector3fd(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double MagnitudeSquared() => Dot(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Magnitude() => Math.Sqrt(Dot(this));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3fd Cross(in Vector3fd other)
            => new Vector3fd(Y * other.Z - Z * other.Y,
                            Z * other.X - X * other.Z,
                            X * other.Y - Y * other.X);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Dot(in Vector3fd other) => (X * other.X) + (Y * other.Y) + (Z * other.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3fd Add(in Vector3fd other) => new Vector3fd(X + other.X, Y + other.Y, Z + other.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3fd Sub(in Vector3fd other) => new Vector3fd(X - other.X, Y - other.Y, Z - other.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3fd Scale(double scalar) => new Vector3fd(X * scalar, Y * scalar, Z * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3fd Unit() => Scale(1d / Magnitude());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double AngularMagnitude(in Vector3fd other) => Math.Acos(Dot(other) / (Magnitude() * other.Magnitude()));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Angle(in Vector3fd other, in Vector3fd normal)
        {
            if (normal.Cross(this).Dot(other) > 0)
                return AngularMagnitude(other);
            else
                return -AngularMagnitude(other);
        }        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector3fd(in (double x, double y, double z) t) => new Vector3fd(t.x, t.y, t.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector3fd(in (float x, float y, float z) t) => new Vector3fd(t.x, t.y, t.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector3fd(Tuple<double, double, double> t) => new Vector3fd(t.Item1, t.Item2, t.Item3);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector3fd(Tuple<float, float, float> t) => new Vector3fd(t.Item1, t.Item2, t.Item3);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector3fd(double[] t)
        {
            if (t.Length != 3)
                throw new InvalidOperationException($"Array length must be 3 to cast to {nameof(Vector3fd)}");
            return new Vector3fd(t[0], t[1], t[2]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector3fd(float[] t)
        {
            if (t.Length != 3)
                throw new InvalidOperationException($"Array length must be 3 to cast to {nameof(Vector3fd)}");
            return new Vector3fd(t[0], t[1], t[2]);
        }

        public static explicit operator RefVector3f(Vector3fd v) => new RefVector3f(v.X, v.Y, v.Z);

        public static Vector3fd operator -(Vector3fd a, Vector3fd b) => a.Sub(b);
        public static Vector3fd operator -(Vector3fd a) => a.Scale(-1);
        public static Vector3fd operator +(Vector3fd a, Vector3fd b) => a.Sub(b);
        public static double operator *(Vector3fd a, Vector3fd b) => a.Dot(b);
        public static Vector3fd operator *(Vector3fd a, double s) => a.Scale(s);
        public static Vector3fd operator *(double s, Vector3fd b) => b.Scale(s);
        public static Vector3fd operator /(Vector3fd a, double s) => a.Scale(1 / s);

        public static bool operator ==(Vector3fd a, Vector3fd b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        public static bool operator !=(Vector3fd a, Vector3fd b) => a.X != b.X || a.Y != b.Y || a.Z != b.Z;

        public unsafe ReadOnlySpan<float> AsSpan()
        {
            fixed (Vector3fd* data = &this)
                return new ReadOnlySpan<float>(data, 3);
        }        

        public override string ToString() => $"<{x.ToString("#0.##")},{y.ToString("#0.##")},{z.ToString("#0.##")}>";
    }
}
