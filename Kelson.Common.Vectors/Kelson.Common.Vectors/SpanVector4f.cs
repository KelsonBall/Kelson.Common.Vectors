using System;
using System.Runtime.CompilerServices;

namespace Kelson.Common.Vectors
{
    public readonly ref struct SpanVector4f
    {
        private readonly ReadOnlySpan<float> data;

        public double X => data[0];
        public double Y => data[1];
        public double Z => data[2];
        public double W => data[3];

        public SpanVector4f(in ReadOnlySpan<float> data) => this.data = data;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double MagnitudeSquared() => Dot(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Magnitude() => Math.Sqrt(Dot(this));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Dot(in SpanVector4f other) => (X * other.X) + (Y * other.Y) + (Z * other.Z) + (W * other.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SpanVector4f Add(in SpanVector4f other)
            => new SpanVector4f(new Vector4fd(X + other.X, Y + other.Y, Z + other.Z, W + other.W).AsSpan());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SpanVector4f Sub(in SpanVector4f other)
            => new SpanVector4f(new Vector4fd(X - other.X, Y - other.Y, Z - other.Z, W - other.W).AsSpan());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SpanVector4f Scale(double scalar)
            => new SpanVector4f(new Vector4fd(X * scalar, Y * scalar, Z * scalar, W * scalar).AsSpan());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SpanVector4f Unit() => Scale(1d / Magnitude());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Angle(in SpanVector4f other) => Math.Acos(Dot(other) / (Magnitude() * other.Magnitude()));

        public override string ToString() => $"<{ data[0]},{ data[1]},{data[2]},{data[3]}>";
    }
}
