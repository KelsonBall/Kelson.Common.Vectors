using System;
using Xunit;
using FluentAssertions;

namespace Kelson.Common.Vectors.Tests
{
    public class Vector2fd_Should
    {
        [Fact]
        public void HaveCorrectValues()
        {
            var v = new Vector2fd(1, 2);
            v.X.Should().BeApproximately(1, double.Epsilon);
            v.Y.Should().BeApproximately(2, double.Epsilon);
        }

        [Fact]
        public void ComputeAddition()
        {
            var v1 = new Vector2fd(1, 2);
            var v2 = new Vector2fd(3, 4);
            v1.Add(v2).Should().BeEquivalentTo(new Vector2fd(4, 6));
        }

        [Fact]
        public void ComputeSubtraction()
        {
            var v1 = new Vector2fd(1, 2);
            var v2 = new Vector2fd(3, 4);
            v2.Sub(v1).Should().BeEquivalentTo(new Vector2fd(2, 2));
        }

        [Fact]
        public void ComputeDotProduct()
        {
            var v1 = new Vector2fd(1, 2);
            var v2 = new Vector2fd(3, 4);
            v1.Dot(v2).Should().BeApproximately(11, double.Epsilon);
        }

        [Fact]
        public void ComputeAngle()
        {
            var v1 = new Vector2fd(1, 0);
            var v2 = new Vector2fd(0, 1);
            v1.Angle(v2).Should().BeApproximately(Math.PI / 2, double.Epsilon);
            v2.Angle(v1).Should().BeApproximately(-Math.PI / 2, double.Epsilon);
        }

        [Fact]
        public void ComputeAngularMagnitude()
        {
            var v1 = new Vector2fd(1, 0);
            var v2 = new Vector2fd(0, 1);
            v1.Angle(v2).Should().BeApproximately(Math.PI / 2, double.Epsilon);
            v2.Angle(v1).Should().BeApproximately(Math.PI / 2, double.Epsilon); // unsigned angle
        }
    }
}
