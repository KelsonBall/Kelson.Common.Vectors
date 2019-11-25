using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Kelson.Common.Vectors
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly ref partial struct RefVector2d
    {        
        public double X { get; }
        public double Y { get; }

        public RefVector2d(in ReadOnlySpan<double> data) => (X, Y) = (data[0], data[1]);

        public RefVector2d(double x, double y) => (X, Y) = (x, y);

        public static implicit operator Vector2fd(RefVector2d v) => new Vector2fd(v.X, v.Y);

        public override string ToString() => $"<{X},{Y}>";

        public unsafe ReadOnlySpan<double> AsSpan()
        {
            fixed (RefVector2d* vec = &this)
                return new ReadOnlySpan<double>(vec, 16);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double MagnitudeSquared() => Dot(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Magnitude() => Math.Sqrt(Dot(this));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Dot(in RefVector2d other) => (X * other.X) + (Y * other.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefVector2d Add(in RefVector2d other) => new RefVector2d(X + other.X, Y + other.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefVector2d Sub(in RefVector2d other) => new RefVector2d(X - other.X, Y - other.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefVector2d Scale(double scalar) => new RefVector2d(X * scalar, Y * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefVector2d Unit() => Scale(1d / Magnitude());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double AngularMagnitude(in RefVector2d other) => Math.Acos(Dot(other) / (Magnitude() * other.Magnitude()));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Angle(in RefVector2d other) => Math.Atan2(other.Y, other.X) - Math.Atan2(Y, X);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RefVector2d(in (double x, double y) t) => new RefVector2d(t.x, t.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RefVector2d(in (float x, float y) t) => new RefVector2d(t.x, t.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RefVector2d(Tuple<double, double> t) => new RefVector2d(t.Item1, t.Item2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RefVector2d(Tuple<float, float> t) => new RefVector2d(t.Item1, t.Item2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator RefVector2d(double[] t)
        {
            if (t.Length != 2)
                throw new InvalidOperationException("Array length must be 2 to cast to RefVector2d");
            return new RefVector2d(t[0], t[1]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator RefVector2d(float[] t)
        {
            if (t.Length != 2)
                throw new InvalidOperationException("Array length must be 2 to cast to RefVector2d");
            return new RefVector2d(t[0], t[1]);
        }


        public static RefVector2d operator -(RefVector2d a, RefVector2d b) => a.Sub(b);
        public static RefVector2d operator -(RefVector2d a) => a.Scale(-1);
        public static RefVector2d operator +(RefVector2d a, RefVector2d b) => a.Sub(b);
        public static double operator *(RefVector2d a, RefVector2d b) => a.Dot(b);
        public static RefVector2d operator *(RefVector2d a, double s) => a.Scale(s);
        public static RefVector2d operator *(double s, RefVector2d b) => b.Scale(s);
        public static RefVector2d operator /(RefVector2d a, double s) => a.Scale(1 / s);
    }
}
