using System;

namespace Kelson.Common.Vectors
{
    public readonly partial struct Vector2fd
    {
        public static Vector2fd FromPolar(double theta, double radius)
            => new Vector2fd(Math.Cos(theta) * radius, Math.Sin(theta) * radius);
    }

    public readonly partial struct Vector3fd
    {
        public Vector3fd(in Vector2fd xy, double z) : this(xy.X, xy.Y, z) { }
        public Vector3fd(double x, in Vector2fd yz) : this(x, yz.X, yz.Y) { }        
    }

    public readonly partial struct Vector4fd
    {
        public Vector4fd(in Vector2fd xy, in Vector2fd zw) : this(xy.X, xy.Y, zw.X, zw.Y) { }
        public Vector4fd(double x, in Vector2fd yz, double w) : this(x, yz.X, yz.Y, w) { }
        public Vector4fd(double x, in Vector3fd yzw) : this(x, yzw.X, yzw.Y, yzw.Z) { }
        public Vector4fd(in Vector3fd xyz, double w) : this(xyz.X, xyz.Y, xyz.Z, w) { }
    }
}
