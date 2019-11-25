using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Kelson.Common.Vectors
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly ref struct RefVector3d
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public RefVector3d(in ReadOnlySpan<double> data) => (X, Y, Z) = (data[0], data[1], data[2]);        

        public RefVector3d(double x, double y, double z) => (X, Y, Z) = (x, y, z);

        public static implicit operator Vector3fd(RefVector3d v) => new Vector3fd(v.X, v.Y, v.Z);

        public override string ToString() => $"<{X.ToString("#0.##")},{Y.ToString("#0.##")},{Z.ToString("#0.##")}>";

        public unsafe ReadOnlySpan<double> AsSpan()
        {
            fixed (RefVector3d* vec = &this)
                return new ReadOnlySpan<double>(vec, 24);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double MagnitudeSquared() => Dot(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Magnitude() => Math.Sqrt(Dot(this));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefVector3d Cross(in RefVector3d other)
            => new RefVector3d(Y * other.Z - Z * other.Y,
                            Z * other.X - X * other.Z,
                            X * other.Y - Y * other.X);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Dot(in RefVector3d other) => (X * other.X) + (Y * other.Y) + (Z * other.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefVector3d Add(in RefVector3d other) => new RefVector3d(X + other.X, Y + other.Y, Z + other.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefVector3d Sub(in RefVector3d other) => new RefVector3d(X - other.X, Y - other.Y, Z - other.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefVector3d Scale(double scalar) => new RefVector3d(X * scalar, Y * scalar, Z * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefVector3d Unit() => Scale(1d / Magnitude());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double AngularMagnitude(in RefVector3d other) => Math.Acos(Dot(other) / (Magnitude() * other.Magnitude()));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Angle(in RefVector3d other, in RefVector3d normal)
        {
            if (normal.Cross(this).Dot(other) > 0)
                return AngularMagnitude(other);
            else
                return -AngularMagnitude(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RefVector3d(in (double x, double y, double z) t) => new RefVector3d(t.x, t.y, t.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RefVector3d(in (float x, float y, float z) t) => new RefVector3d(t.x, t.y, t.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RefVector3d(Tuple<double, double, double> t) => new RefVector3d(t.Item1, t.Item2, t.Item3);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RefVector3d(Tuple<float, float, float> t) => new RefVector3d(t.Item1, t.Item2, t.Item3);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator RefVector3d(double[] t)
        {
            if (t.Length != 3)
                throw new InvalidOperationException($"Array length must be 3 to cast to {nameof(RefVector3d)}");
            return new RefVector3d(t[0], t[1], t[2]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator RefVector3d(float[] t)
        {
            if (t.Length != 3)
                throw new InvalidOperationException($"Array length must be 3 to cast to {nameof(RefVector3d)}");
            return new RefVector3d(t[0], t[1], t[2]);
        }

        public static RefVector3d operator -(RefVector3d a, RefVector3d b) => a.Sub(b);
        public static RefVector3d operator -(RefVector3d a) => a.Scale(-1);
        public static RefVector3d operator +(RefVector3d a, RefVector3d b) => a.Sub(b);
        public static double operator *(RefVector3d a, RefVector3d b) => a.Dot(b);
        public static RefVector3d operator *(RefVector3d a, double s) => a.Scale(s);
        public static RefVector3d operator *(double s, RefVector3d b) => b.Scale(s);
        public static RefVector3d operator /(RefVector3d a, double s) => a.Scale(1 / s);
    }
}
