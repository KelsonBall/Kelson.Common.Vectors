using FluentAssertions;
using System;
using Xunit;

namespace Kelson.Common.Vectors.Tests
{
    public class Vector4fd_Should
    {
        [Fact]
        public void HaveCorrectValues()
        {
            var v = new Vector4fd(1, 2, 3, 4);
            v.X.Should().Be(1);
            v.Y.Should().Be(2);
            v.Z.Should().Be(3);
            v.W.Should().Be(4);
        }

        [Fact]
        public void ComputeMagnitudeSquared()
        {
            new Vector4fd(1, 0, 0, 0)
                .MagnitudeSquared()
                .Should()
                .BeApproximately(1, double.Epsilon);

            new Vector4fd(0, 3, 0, 4)
                .MagnitudeSquared()
                .Should()
                .BeApproximately(25, double.Epsilon);
        }

        [Fact]
        public void ComputeMagnitude()
        {
            new Vector4fd(1, 0, 0, 0)
                .Magnitude()
                .Should()
                .BeApproximately(1, double.Epsilon);

            new Vector4fd(3, 0, 4, 0)
                .Magnitude()
                .Should()
                .BeApproximately(5, double.Epsilon);
        }

        [Fact]
        public void ComputeDotProduct()
        {
            new Vector4fd(1, 0, 0, 0)
                .Dot((0, 0, 0, 1))
                .Should()
                .BeApproximately(0, double.Epsilon);

            new Vector4fd(1, 1, 1, 1)
                .Dot((0, 0, -1, -1))
                .Should()
                .BeApproximately(-2, double.Epsilon);

            new Vector4fd(1, 2, 3, 4)
                .Dot((3, 4, 5, 6))
                .Should()
                .BeApproximately(50, double.Epsilon);
        }

        [Fact]
        public void ComputeAddition()
        {
            new Vector4fd(1, 2, 3, 4)
                .Add((3, 4, 5, 6))
                .Should()
                .BeEquivalentTo<Vector4fd>((4, 6, 8, 10));
        }

        [Fact]
        public void ComputeSubtraction()
        {
            new Vector4fd(3, 4, 5, 6)
                .Sub((1, 2, 3, 4))
                .Should()
                .BeEquivalentTo<Vector4fd>((2, 2, 2, 2));
        }

        [Fact]
        public void ComputeScale()
        {
            new Vector4fd(1, 0, 0, 0)
                .Scale(2)
                .Should()
                .BeEquivalentTo<Vector4fd>((2, 0, 0, 0));

            new Vector4fd(-2, 0, 1, -1)
                .Scale(3)
                .Should()
                .BeEquivalentTo<Vector4fd>((-6, 0, 3, -3));
        }

        [Fact]
        public void ComputeUnit()
        {
            var normalized = new Vector4fd(0.1, 0, 0, 0).Unit();
            normalized.X
                    .Should()
                    .BeApproximately(1, double.Epsilon);
            normalized.Y
                    .Should()
                    .Be(0);
            normalized.Z
                    .Should()
                    .Be(0);
            normalized.W
                    .Should()
                    .Be(0);
        }

        [Fact]
        public void ComputeAngularMagnitude()
        {
            var v1 = new Vector4fd(1, 0, 0, 0);
            var v2 = new Vector4fd(0, 1, 0, 0);
            v1.AngularMagnitude(v2).Should().BeApproximately(Math.PI / 2, double.Epsilon);
            v2.AngularMagnitude(v1).Should().BeApproximately(Math.PI / 2, double.Epsilon); // unsigned angle
        }

        [Fact]
        public void ConvertFromValueTupleOfDouble()
        {
            Vector4fd v = (1, 0, -1, 2);
            v.X.Should().Be(1);
            v.Y.Should().Be(0);
            v.Z.Should().Be(-1);
            v.W.Should().Be(2);
        }

        [Fact]
        public void ConvertFromReferenceTupleOfDouble()
        {
            Vector4fd v = new Tuple<double, double, double, double>(1, 0, -1, 2);
            v.X.Should().Be(1);
            v.Y.Should().Be(0);
            v.Z.Should().Be(-1);
            v.W.Should().Be(2);
        }

        [Fact]
        public void ConvertFromValueTupleOfFloat()
        {
            Vector4fd v = (1f, 0f, -1f, 2f);
            v.X.Should().Be(1);
            v.Y.Should().Be(0);
            v.Z.Should().Be(-1);
            v.W.Should().Be(2);
        }

        [Fact]
        public void ConvertFromReferenceTupleOfFloat()
        {
            Vector4fd v = new Tuple<float, float, float, float>(1, 0, -1, 2);
            v.X.Should().Be(1);
            v.Y.Should().Be(0);
            v.Z.Should().Be(-1);
            v.W.Should().Be(2);
        }

        [Fact]
        public void ConvertFromDoubleArray()
        {
            var v = (Vector4fd)new double[] { 1, 0, -1, 2 };
            v.X.Should().Be(1);
            v.Y.Should().Be(0);
            v.Z.Should().Be(-1);
            v.W.Should().Be(2);
        }

        [Fact]
        public void ThrowException_WhenCastingFromDoubleArrayWithoutLength2()
        {
            Vector4fd v;
            ((Action)(() => v = (Vector4fd)new double[] { 1 }))
                .Should()
                .Throw<InvalidOperationException>();
        }

        [Fact]
        public void ConvertFromFloatArray()
        {
            var v = (Vector4fd)new float[] { 1, 0, -1, 2 };
            v.X.Should().Be(1);
            v.Y.Should().Be(0);
            v.Z.Should().Be(-1);
            v.W.Should().Be(2);
        }

        [Fact]
        public void ThrowException_WhenCastingFromArrayFloatWithoutLength2()
        {
            Vector4fd v;
            ((Action)(() => v = (Vector4fd)new float[] { 1 }))
                .Should()
                .Throw<InvalidOperationException>();
        }

        [Fact]
        public void ConverToSpan()
        {
            Vector4fd v = (1, 2, 3, 4);
            var span = v.AsSpan();
            span[0].Should().Be(1);
            span[1].Should().Be(2);
            span[2].Should().Be(3);
            span[3].Should().Be(4);
        }
    }
}
