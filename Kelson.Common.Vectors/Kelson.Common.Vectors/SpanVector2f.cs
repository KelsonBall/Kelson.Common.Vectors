using System;
using System.Runtime.CompilerServices;

namespace Kelson.Common.Vectors
{
    public readonly ref struct SpanVector2f
    {
        private readonly ReadOnlySpan<float> data;

        public double X => data[0];
        public double Y => data[1];

        public SpanVector2f(in ReadOnlySpan<float> data) => this.data = data;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double MagnitudeSquared() => Dot(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Magnitude() => Math.Sqrt(Dot(this));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Dot(in SpanVector2f other) => (X * other.X) + (Y * other.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SpanVector2f Add(in SpanVector2f other)
            => new SpanVector2f(new Vector2fd(X + other.X, Y + other.Y).AsSpan());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SpanVector2f Sub(in SpanVector2f other)
            => new SpanVector2f(new Vector2fd(X - other.X, Y - other.Y).AsSpan());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SpanVector2f Scale(double scalar)
            => new SpanVector2f(new Vector2fd(X * scalar, Y * scalar).AsSpan());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SpanVector2f Unit() => Scale(1d / Magnitude());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Angle(in SpanVector2f other) => Math.Acos(Dot(other) / (Magnitude() * other.Magnitude()));

        public override string ToString() => $"<{ data[0]},{ data[1]}>";
    }
}
