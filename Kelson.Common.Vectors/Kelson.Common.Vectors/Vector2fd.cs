using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Kelson.Common.Vectors
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Vector2fd : IVector<Vector2fd>
    {
        public readonly double x;
        public double X => x;

        public readonly double y;
        public double Y => y;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2fd(double x, double y) : this((float)x, (float)y) { }

        public Vector2fd(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double MagnitudeSquared() => Dot(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Magnitude() => Math.Sqrt(Dot(this));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Dot(in Vector2fd other) => (X * other.X) + (Y * other.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2fd Add(in Vector2fd other) => new Vector2fd(X + other.X, Y + other.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2fd Sub(in Vector2fd other) => new Vector2fd(X - other.X, Y - other.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2fd Scale(double scalar) => new Vector2fd(X * scalar, Y * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2fd Unit() => Scale(1d / Magnitude());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double AngularMagnitude(in Vector2fd other) => Math.Acos(Dot(other) / (Magnitude() * other.Magnitude()));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Angle(in Vector2fd other)
        {
            if (Dot(new Vector2fd(other.Y, other.X)) > 0)
                return AngularMagnitude(other);
            else
                return -AngularMagnitude(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector2fd(in (double x, double y) t) => new Vector2fd(t.x, t.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector2fd(in (float x, float y) t) => new Vector2fd(t.x, t.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector2fd(Tuple<double, double> t) => new Vector2fd(t.Item1, t.Item2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector2fd(Tuple<float, float> t) => new Vector2fd(t.Item1, t.Item2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector2fd(double[] t)
        {
            if (t.Length != 2)
                throw new InvalidOperationException("Array length must be 2 to cast to Vector2fd");
            return new Vector2fd(t[0], t[1]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector2fd(float[] t)
        {
            if (t.Length != 2)
                throw new InvalidOperationException("Array length must be 2 to cast to Vector2fd");
            return new Vector2fd(t[0], t[1]);
        }

        public unsafe ReadOnlySpan<float> AsSpan()
        {
            fixed (void* data = &this)
                return new ReadOnlySpan<float>(data, 2);
        }

        public override string ToString() => $"<{x},{y}>";
    }
}
