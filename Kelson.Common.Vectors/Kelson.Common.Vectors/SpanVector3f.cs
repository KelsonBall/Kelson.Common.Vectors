using System;
using System.Runtime.CompilerServices;

namespace Kelson.Common.Vectors
{
    public readonly ref struct SpanVector3f
    {
        private readonly ReadOnlySpan<float> data;

        public double X => data[0];
        public double Y => data[1];
        public double Z => data[2];

        public SpanVector3f(in ReadOnlySpan<float> data) => this.data = data;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double MagnitudeSquared() => Dot(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Magnitude() => Math.Sqrt(Dot(this));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Dot(in SpanVector3f other) => (X * other.X) + (Y * other.Y) + (Z * other.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SpanVector3f Add(in SpanVector3f other)
            => new SpanVector3f(new Vector3fd(X + other.X, Y + other.Y, Z + other.Z).AsSpan());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SpanVector3f Sub(in SpanVector3f other)
            => new SpanVector3f(new Vector3fd(X - other.X, Y - other.Y, Z - other.Z).AsSpan());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SpanVector3f Scale(double scalar)
            => new SpanVector3f(new Vector3fd(X * scalar, Y * scalar, Z * scalar).AsSpan());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SpanVector3f Unit() => Scale(1d / Magnitude());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Angle(in SpanVector3f other) => Math.Acos(Dot(other) / (Magnitude() * other.Magnitude()));

        public override string ToString() => $"<{ data[0]},{ data[1]},{data[2]}>";
    }
}
