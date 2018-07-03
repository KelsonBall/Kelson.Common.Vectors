using System.Runtime.CompilerServices;

namespace Kelson.Common.Vectors
{
    public interface IVector<TSelf>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        double MagnitudeSquared();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        double Magnitude();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        double Dot(in TSelf other);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        TSelf Add(in TSelf other);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        TSelf Sub(in TSelf other);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        TSelf Scale(double scalar);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        double AngularMagnitude(in TSelf other);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        TSelf Unit();
    }
}
