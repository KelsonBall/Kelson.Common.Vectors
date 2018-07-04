using FluentAssertions;
using System;
using Xunit;

namespace Kelson.Common.Vectors.Tests
{
    public class Vector3fd_Should
    {
        [Fact]
        public void HaveCorrectValues()
        {
            var v = new Vector3fd(1, 2, 3);
            v.X.Should().BeApproximately(1, double.Epsilon);
            v.Y.Should().BeApproximately(2, double.Epsilon);
            v.Z.Should().BeApproximately(3, double.Epsilon);
        }

        [Fact]
        public void ComputeMagnitudeSquared()
        {
            new Vector3fd(1, 0, 0)
                .MagnitudeSquared()
                .Should()
                .BeApproximately(1, double.Epsilon);

            new Vector3fd(0, 3, 4)
                .MagnitudeSquared()
                .Should()
                .BeApproximately(25, double.Epsilon);
        }

        [Fact]
        public void ComputeMagnitude()
        {
            new Vector3fd(1, 0, 0)
                .Magnitude()
                .Should()
                .BeApproximately(1, double.Epsilon);

            new Vector3fd(3, 0, 4)
                .Magnitude()
                .Should()
                .BeApproximately(5, double.Epsilon);
        }

        [Fact]
        public void ComputeDotProduct()
        {
            new Vector3fd(1, 0, 0)
                .Dot((0, 0, 1))
                .Should()
                .BeApproximately(0, double.Epsilon);

            new Vector3fd(1, 1, 1)
                .Dot((0, 0, -1))
                .Should()
                .BeApproximately(-1, double.Epsilon);

            new Vector3fd(1, 2, 3)
                .Dot((3, 4, 5))
                .Should()
                .BeApproximately(26, double.Epsilon);
        }

        [Fact]
        public void ComputeAddition()
        {
            new Vector3fd(1, 2, 3)
                .Add((3, 4, 5))
                .Should()
                .BeEquivalentTo<Vector3fd>((4, 6, 8));
        }

        [Fact]
        public void ComputeSubtraction()
        {
            new Vector3fd(3, 4, 5)
                .Sub((1, 2, 3))
                .Should()
                .BeEquivalentTo<Vector3fd>((2, 2, 2));
        }

        [Fact]
        public void ComputeScale()
        {
            new Vector3fd(1, 0, 0)
                .Scale(2)
                .Should()
                .BeEquivalentTo<Vector3fd>((2, 0, 0));

            new Vector3fd(-2, 0, 1)
                .Scale(3)
                .Should()
                .BeEquivalentTo<Vector3fd>((-6, 0, 3));
        }

        [Fact]
        public void ComputeUnit()
        {
            var normalized = new Vector3fd(0.1, 0, 0).Unit();
            normalized.X
                    .Should()
                    .BeApproximately(1, double.Epsilon);
            normalized.Y
                    .Should()
                    .Be(0);
            normalized.Z
                    .Should()
                    .Be(0);
        }

        [Fact]
        public void ComputeAngle()
        {
            var v1 = new Vector3fd(1, 0, 0);
            var v2 = new Vector3fd(0, 1, 0);
            v1.Angle(v2, (0, 0, 1)).Should().BeApproximately(Math.PI / 2, double.Epsilon);
            v2.Angle(v1, (0, 0, 1)).Should().BeApproximately(-Math.PI / 2, double.Epsilon);
        }

        [Fact]
        public void ComputeAngularMagnitude()
        {
            var v1 = new Vector3fd(1, 0, 0);
            var v2 = new Vector3fd(0, 1, 0);
            v1.AngularMagnitude(v2).Should().BeApproximately(Math.PI / 2, double.Epsilon);
            v2.AngularMagnitude(v1).Should().BeApproximately(Math.PI / 2, double.Epsilon); // unsigned angle
        }

        [Fact]
        public void ConvertFromValueTupleOfDouble()
        {
            Vector3fd v = (1, 0, -1);
            v.X.Should().Be(1);
            v.Y.Should().Be(0);
            v.Z.Should().Be(-1);
        }

        [Fact]
        public void ConvertFromReferenceTupleOfDouble()
        {
            Vector3fd v = new Tuple<double, double, double>(1, 0, -1);
            v.X.Should().Be(1);
            v.Y.Should().Be(0);
            v.Z.Should().Be(-1);
        }

        [Fact]
        public void ConvertFromValueTupleOfFloat()
        {
            Vector3fd v = (1f, 0f, -1f);
            v.X.Should().Be(1);
            v.Y.Should().Be(0);
            v.Z.Should().Be(-1);
        }

        [Fact]
        public void ConvertFromReferenceTupleOfFloat()
        {
            Vector3fd v = new Tuple<float, float, float>(1f, 0f, -1f);
            v.X.Should().Be(1);
            v.Y.Should().Be(0);
            v.Z.Should().Be(-1);
        }

        [Fact]
        public void ConvertFromDoubleArray()
        {
            var v = (Vector3fd)new double[] { 1, 0 , -1};
            v.X.Should().Be(1);
            v.Y.Should().Be(0);
            v.Z.Should().Be(-1);
        }

        [Fact]
        public void ThrowException_WhenCastingFromDoubleArrayWithoutLength2()
        {
            Vector3fd v;
            ((Action)(() => v = (Vector3fd)new double[] { 1 }))
                .Should()
                .Throw<InvalidOperationException>();
        }

        [Fact]
        public void ConvertFromFloatArray()
        {
            var v = (Vector3fd)new float[] { 1, 0, -1 };
            v.X.Should().Be(1);
            v.Y.Should().Be(0);
            v.Z.Should().Be(-1);
        }

        [Fact]
        public void ThrowException_WhenCastingFromArrayFloatWithoutLength2()
        {
            Vector3fd v;
            ((Action)(() => v = (Vector3fd)new float[] { 1 }))
                .Should()
                .Throw<InvalidOperationException>();
        }

        [Fact]
        public void ConverToSpan()
        {
            Vector3fd v = (1, 2, 3);
            var span = v.AsSpan();
            span[0].Should().Be(1);
            span[1].Should().Be(2);
            span[2].Should().Be(3);
        }
    }
}
