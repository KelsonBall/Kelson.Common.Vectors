using System;

namespace Kelson.Common.Vectors
{
    /// <summary>
    /// 2D vector (X, Y) backed by floats
    /// </summary>
    public readonly partial struct Vector2fd
    {
        public static Vector2fd FromPolar(double theta, double radius)
            => new Vector2fd(Math.Cos(theta) * radius, Math.Sin(theta) * radius);
    }

    /// <summary>
    /// 3D vector (X, Y, Z) backed by floats
    /// </summary>
    public readonly partial struct Vector3fd
    {
        public Vector3fd(in Vector2fd xy, double z) : this(xy.X, xy.Y, z) { }
        public Vector3fd(double x, in Vector2fd yz) : this(x, yz.X, yz.Y) { }        
    }

    /// <summary>
    /// 4D vector (X, Y, Z, W) backed by floats
    /// </summary>
    public readonly partial struct Vector4fd
    {
        public Vector4fd(in Vector2fd xy, in Vector2fd zw) : this(xy.X, xy.Y, zw.X, zw.Y) { }
        public Vector4fd(double x, in Vector2fd yz, double w) : this(x, yz.X, yz.Y, w) { }
        public Vector4fd(double x, in Vector3fd yzw) : this(x, yzw.X, yzw.Y, yzw.Z) { }
        public Vector4fd(in Vector3fd xyz, double w) : this(xyz.X, xyz.Y, xyz.Z, w) { }
    }

    public static class VectorConstruction
    {
        public static Vector2fd vec(float x, float y) => new Vector2fd(x, y);
        public static Vector3fd vec(float x, float y, float z) => new Vector3fd(x, y, z);
        public static Vector4fd vec(float x, float y, float z, float w) => new Vector4fd(x, y, z, w);        

        public static RefVector2f rvec(float x, float y) => new RefVector2f(x, y);
        public static RefVector3f rvec(float x, float y, float z) => new RefVector3f(x, y, z);
        public static RefVector4f rvec(float x, float y, float z, float w) => new RefVector4f(x, y, z, w);
        public static RefVector2d rvec(double x, double y) => new RefVector2d(x, y);
        public static RefVector3d rvec(double x, double y, double z) => new RefVector3d(x, y, z);
        public static RefVector4d rvec(double x, double y, double z, double w) => new RefVector4d(x, y, z, w);
    }
}
