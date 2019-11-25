using System;
using System.Runtime.CompilerServices;

namespace Kelson.Common.Vectors
{
    public readonly ref struct RefVector2f
    {
        private readonly float x;
        private readonly float y;        

        public double X => x;
        public double Y => y;        

        public RefVector2f(ReadOnlySpan<float> data) => (x, y) = (data[0], data[1]);

        public RefVector2f(float x, float y) => (this.x, this.y) = (x, y);

        public RefVector2f(double x, double y) => (this.x, this.y) = ((float)x, (float)y);

        public static implicit operator Vector2fd(RefVector2f v) => new Vector2fd(v.X, v.Y);

        public override string ToString() => $"<{x},{y}>";

        public unsafe ReadOnlySpan<float> AsSpan()
        {
            fixed (RefVector2f* vec = &this)
                return new ReadOnlySpan<float>(vec, 8);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double MagnitudeSquared() => Dot(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Magnitude() => Math.Sqrt(Dot(this));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Dot(RefVector2f other) => (X * other.X) + (Y * other.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefVector2f Add(RefVector2f other) => new RefVector2f(X + other.X, Y + other.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefVector2f Sub(RefVector2f other) => new RefVector2f(X - other.X, Y - other.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefVector2f Scale(double scalar) => new RefVector2f(X * scalar, Y * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefVector2f Unit() => Scale(1d / Magnitude());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double AngularMagnitude(RefVector2f other) => Math.Acos(Dot(other) / (Magnitude() * other.Magnitude()));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Angle(RefVector2f other) => Math.Atan2(other.Y, other.X) - Math.Atan2(Y, X);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RefVector2f((double x, double y) t) => new RefVector2f(t.x, t.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RefVector2f((float x, float y) t) => new RefVector2f(t.x, t.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RefVector2f(Tuple<double, double> t) => new RefVector2f(t.Item1, t.Item2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RefVector2f(Tuple<float, float> t) => new RefVector2f(t.Item1, t.Item2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator RefVector2f(double[] t)
        {
            if (t.Length != 2)
                throw new InvalidOperationException("Array length must be 2 to cast to RefVector2f");
            return new RefVector2f(t[0], t[1]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator RefVector2f(float[] t)
        {
            if (t.Length != 2)
                throw new InvalidOperationException("Array length must be 2 to cast to RefVector2f");
            return new RefVector2f(t[0], t[1]);
        }

        public static RefVector2f operator -(RefVector2f a, RefVector2f b) => a.Sub(b);
        public static RefVector2f operator -(RefVector2f a) => a.Scale(-1);
        public static RefVector2f operator +(RefVector2f a, RefVector2f b) => a.Sub(b);
        public static double operator *(RefVector2f a, RefVector2f b) => a.Dot(b);
        public static RefVector2f operator *(RefVector2f a, double s) => a.Scale(s);
        public static RefVector2f operator *(double s, RefVector2f b) => b.Scale(s);
        public static RefVector2f operator /(RefVector2f a, double s) => a.Scale(1 / s);
    }
}
