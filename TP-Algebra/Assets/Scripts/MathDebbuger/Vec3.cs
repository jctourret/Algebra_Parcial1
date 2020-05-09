using UnityEngine;
using System;
namespace CustomMath
{
    public struct Vec3 : IEquatable<Vec3>
    {
        #region Variables
        public float x;
        public float y;
        public float z;

        public float sqrMagnitude { get { return (x * x + y * y + z * z); } }
        public Vector3 normalized { get { return new Vec3(x / magnitude, y / magnitude, z / magnitude); } }
        public float magnitude { get { return Mathf.Sqrt(x * x + y * y + z * z); } }
        #endregion

        #region Default Values
        public static Vec3 Zero { get { return new Vec3(0.0f, 0.0f, 0.0f); } }
        public static Vec3 One { get { return new Vec3(1.0f, 1.0f, 1.0f); } }
        public static Vec3 Forward { get { return new Vec3(0.0f, 0.0f, 1.0f); } }
        public static Vec3 Back { get { return new Vec3(0.0f, 0.0f, -1.0f); } }
        public static Vec3 Right { get { return new Vec3(1.0f, 0.0f, 0.0f); } }
        public static Vec3 Left { get { return new Vec3(-1.0f, 0.0f, 0.0f); } }
        public static Vec3 Up { get { return new Vec3(0.0f, 1.0f, 0.0f); } }
        public static Vec3 Down { get { return new Vec3(0.0f, -1.0f, 0.0f); } }
        public static Vec3 PositiveInfinity { get { return new Vec3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity); } }
        public static Vec3 NegativeInfinity { get { return new Vec3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity); } }
        #endregion                                                                                                                                                                               

        #region Constructors
        public Vec3(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0.0f;
        }

        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vec3(Vec3 v3)
        {
            this.x = v3.x;
            this.y = v3.y;
            this.z = v3.z;
        }

        public Vec3(Vector3 v3)
        {
            this.x = v3.x;
            this.y = v3.y;
            this.z = v3.z;
        }

        public Vec3(Vector2 v2)
        {
            this.x = v2.x;
            this.y = v2.y;
            this.z = 0.0f;
        }
        #endregion

        #region Operators
        public static bool operator ==(Vec3 left, Vec3 right)
        {
            bool equalsX = left.x == right.x;
            bool equalsY = left.y == right.y;
            bool equalsZ = left.z == right.z;
            return (equalsX && equalsY && equalsZ);
        }
        public static bool operator !=(Vec3 left, Vec3 right)
        {
            return !(left == right);
        }

        public static Vec3 operator +(Vec3 leftV3, Vec3 rightV3)
        {
            return new Vec3(leftV3.x + rightV3.x, leftV3.y + rightV3.y, leftV3.z + rightV3.z);
        }

        public static Vec3 operator -(Vec3 leftV3, Vec3 rightV3)
        {
            return new Vec3(leftV3.x - rightV3.x, leftV3.y - rightV3.y, leftV3.z - rightV3.z);
        }

        public static Vec3 operator -(Vec3 v3)
        {
            return new Vec3(-v3.x, -v3.y, v3.z);
        }

        public static Vec3 operator *(Vec3 v3, float scalar)
        {
            return new Vec3(v3.x * scalar, v3.y * scalar, v3.z * scalar);
        }
        public static Vec3 operator *(float scalar, Vec3 v3)
        {
            return new Vec3(v3.x * scalar, v3.y * scalar, v3.z * scalar);
        }
        public static Vec3 operator /(Vec3 v3, float scalar)
        {
            return new Vec3(v3.x / scalar, v3.y / scalar, v3.z / scalar);
        }

        public static implicit operator Vector3(Vec3 v3)
        {
            return new Vector3(v3.x, v3.y, v3.z);
        }

        public static implicit operator Vector2(Vec3 v2)
        {
            return new Vector3(v2.x, v2.y, 0.0f);
        }
        #endregion

        #region Functions
        public override string ToString()
        {
            return "X = " + x.ToString() + "   Y = " + y.ToString() + "   Z = " + z.ToString();
        }
        public static float Angle(Vec3 from, Vec3 to)
        {
            float dot = from.x * to.x + from.y * to.y + from.z * to.z;
            float fromMag = Mathf.Sqrt(from.x * from.x + from.y * from.y + from.z * from.z);
            float toMag = Mathf.Sqrt(to.x * to.x + to.y * to.y + to.z * to.z);
            float cosNumber = dot / (fromMag * toMag);
            float angle = Mathf.Cos(cosNumber);
            return angle;
        }
        public static Vec3 ClampMagnitude(Vec3 vector, float maxLength)
        {
            if (SqrMagnitude(vector) > maxLength * maxLength)
            {
                float normalizedX = vector.x / Magnitude(vector);
                float normalizedY = vector.y / Magnitude(vector);
                float normalizedZ = vector.z / Magnitude(vector);
                return new Vec3(normalizedX * maxLength, normalizedY * maxLength, normalizedZ * maxLength);
            }
            return vector;
        }
        public static float Magnitude(Vec3 vector)
        {
            return Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
        }
        public static Vec3 Cross(Vec3 a, Vec3 b)
        {
            float newX = a.y * b.y - b.z * a.z;
            float newY = a.z * b.z - a.x * b.x;
            float newZ = a.x * b.x - a.y * b.y;
            return new Vec3(newX, newY, newZ);
        }
        public static float Distance(Vec3 a, Vec3 b)
        {
            float difX = a.x - b.x;
            float difY = a.y - b.y;
            float difZ = a.z - b.z;
            return Mathf.Sqrt(difX * difX + difY * difY + difZ * difZ);
        }
        public static float Dot(Vec3 a, Vec3 b)
        {
            return (a.x * b.x + a.y * b.y + a.z * b.z);
        }
        public static Vec3 Lerp(Vec3 a, Vec3 b, float t)
        {
            if (t < 0)
            {
                t = 0;
            }
            if (t > 1)
            {
                t = 1;
            }
            float newX = b.x - (a.x * t);
            float newY = b.y - (a.y * t);
            float newZ = b.z - (a.z  * t);
            return new Vec3(newX, newY, newZ);
        }
        public static Vec3 LerpUnclamped(Vec3 a, Vec3 b, float t)
        {
            float newX = a.x + (b.x - a.x) * t;
            float newY = a.y + (b.y - a.y) * t;
            float newZ = a.z + (b.z - a.z) * t;
            return new Vec3(newX, newY, newZ);
        }
        public static Vec3 Max(Vec3 a, Vec3 b)
        {
            float newX;
            float newY;
            float newZ;
            if (a.x > b.x)
            {
                newX = a.x;
            }
            else
            {
                newX = b.x;
            }
            if (a.y > b.y)
            {
                newY = a.y;
            }
            else
            {
                newY = b.y;
            }
            if (a.z > b.z)
            {
                newZ = a.z;
            }
            else
            {
                newZ = b.z;
            }
            return new Vec3(newX, newY, newZ);
        }
        public static Vec3 Min(Vec3 a, Vec3 b)
        {
            float newX;
            float newY;
            float newZ;
            if (a.x < b.x)
            {
                newX = a.x;
            }
            else
            {
                newX = b.x;
            }
            if (a.y < b.y)
            {
                newY = a.y;
            }
            else
            {
                newY = b.y;
            }
            if (a.z > b.z)
            {
                newZ = a.z;
            }
            else
            {
                newZ = b.z;
            }
            return new Vec3(newX, newY, newZ);
        }
        public static float SqrMagnitude(Vec3 vector)
        {
            return (vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
        }
        public static Vec3 Project(Vec3 vector, Vec3 onNormal)
        {
            return (Vec3.Dot(vector, onNormal) / Vec3.Dot(vector, vector)) * onNormal;
        }
        public static Vec3 Reflect(Vec3 inDirection, Vec3 inNormal)
        {
            throw new NotImplementedException();
            float angle = Angle(inDirection,inNormal);
            float recAngle = 180-angle;
            inDirection.x = -inDirection.x;
            inDirection.y = -inDirection.y;
            inDirection.z = -inDirection.z;
        }
        public void Set(float newX, float newY, float newZ)
        {
            new Vec3(newX, newY, newZ);
        }
        public void Scale(Vec3 a, Vec3 b)
        {
            new Vec3(a.x * b.x, a.y * b.y, a.z * b.z);
        }
        public void Scale(Vec3 scale)
        {
            new Vec3(x * scale.x, y * scale.y, z * scale.z);
        }
        public void Normalize()
        {
            new Vec3(x / (Mathf.Sqrt(x * x + y * y + z * z)), y / (Mathf.Sqrt(x * x + y * y + z * z)), z / (Mathf.Sqrt(x * x + y * y + z * z)));
        }

        #endregion

        #region Internals
        public override bool Equals(object other)
        {
            if (!(other is Vec3)) return false;
            return Equals((Vec3)other);
        }

        public bool Equals(Vec3 other)
        {
            return x == other.x && y == other.y && z == other.z;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2);
        }
        #endregion
    }
}