using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Kelson.Common.Vectors
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly ref struct RefVector3f
    {
        private readonly float x;
        private readonly float y;
        private readonly float z;

        public double X => x;
        public double Y => y;
        public double Z => z;

        public RefVector3f(in ReadOnlySpan<float> data) => (x, y, z) = (data[0], data[1], data[2]);

        public RefVector3f(float x, float y, float z) => (this.x, this.y, this.z) = (x, y, z);

        public RefVector3f(double x, double y, double z) => (this.x, this.y, this.z) = ((float)x, (float)y, (float)z);

        public static implicit operator Vector3fd(RefVector3f v) => new Vector3fd(v.X, v.Y, v.Z);

        public override string ToString() => $"<{x.ToString("#0.##")},{y.ToString("#0.##")},{z.ToString("#0.##")}>";

        public unsafe ReadOnlySpan<float> AsSpan()
        {
            fixed (RefVector3f* vec = &this)
                return new ReadOnlySpan<float>(vec, 12);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double MagnitudeSquared() => Dot(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Magnitude() => Math.Sqrt(Dot(this));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefVector3f Cross(in RefVector3f other)
            => new RefVector3f(Y * other.Z - Z * other.Y,
                            Z * other.X - X * other.Z,
                            X * other.Y - Y * other.X);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Dot(in RefVector3f other) => (X * other.X) + (Y * other.Y) + (Z * other.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefVector3f Add(in RefVector3f other) => new RefVector3f(X + other.X, Y + other.Y, Z + other.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefVector3f Sub(in RefVector3f other) => new RefVector3f(X - other.X, Y - other.Y, Z - other.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefVector3f Scale(double scalar) => new RefVector3f(X * scalar, Y * scalar, Z * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefVector3f Unit() => Scale(1d / Magnitude());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double AngularMagnitude(in RefVector3f other) => Math.Acos(Dot(other) / (Magnitude() * other.Magnitude()));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Angle(in RefVector3f other, in RefVector3f normal)
        {
            if (normal.Cross(this).Dot(other) > 0)
                return AngularMagnitude(other);
            else
                return -AngularMagnitude(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RefVector3f(in (double x, double y, double z) t) => new RefVector3f(t.x, t.y, t.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RefVector3f(in (float x, float y, float z) t) => new RefVector3f(t.x, t.y, t.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RefVector3f(Tuple<double, double, double> t) => new RefVector3f(t.Item1, t.Item2, t.Item3);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RefVector3f(Tuple<float, float, float> t) => new RefVector3f(t.Item1, t.Item2, t.Item3);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator RefVector3f(double[] t)
        {
            if (t.Length != 3)
                throw new InvalidOperationException($"Array length must be 3 to cast to {nameof(RefVector3f)}");
            return new RefVector3f(t[0], t[1], t[2]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator RefVector3f(float[] t)
        {
            if (t.Length != 3)
                throw new InvalidOperationException($"Array length must be 3 to cast to {nameof(RefVector3f)}");
            return new RefVector3f(t[0], t[1], t[2]);
        }

        public static RefVector3f operator -(RefVector3f a, RefVector3f b) => a.Sub(b);
        public static RefVector3f operator -(RefVector3f a) => a.Scale(-1);
        public static RefVector3f operator +(RefVector3f a, RefVector3f b) => a.Sub(b);
        public static double operator *(RefVector3f a, RefVector3f b) => a.Dot(b);
        public static RefVector3f operator *(RefVector3f a, double s) => a.Scale(s);
        public static RefVector3f operator *(double s, RefVector3f b) => b.Scale(s);
        public static RefVector3f operator /(RefVector3f a, double s) => a.Scale(1 / s);
        public static bool operator ==(RefVector3f a, RefVector3f b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        public static bool operator !=(RefVector3f a, RefVector3f b) => a.X != b.X || a.Y != b.Y || a.Z != b.Z;
    }
}
