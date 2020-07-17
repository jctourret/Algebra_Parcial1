using UnityEngine;
using System;
using CustomMath;
using System.Xml.Schema;

namespace CustomMath {

    public struct Quats : IEquatable<Quats> {

        public const float kEpsilon = 1E-06F;
        public float x;
        public float y;
        public float z;
        public float w;
        // Resumen:
        //     Constructs new Quaternion with given x,y,z,w components.
        // Parámetros:
        //   x:
        //   y:
        //   z:
        //   w:
        public Quats(float x, float y, float z, float w) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        // Resumen:
        //     The identity rotation (Read Only).
        public static Quats identity {
            get {
                return new Quats(0, 0, 0, 1);
            }
        }
        
        // Resumen:
        //     Returns or sets the euler angle representation of the rotation.
        public Vec3 eulerAngles { 
            get{
                float sinX = 2 * (w * x + y * z);
                float cosX = 1 - 2 * (x * x + y * y);
                float eulerX = Mathf.Atan2(sinX, cosX) * Mathf.PI/ 180.0f;
                float sinY = 2 * (w * y - z * x);
                float eulerY;
                if (Mathf.Abs(sinY) >= 1)
                {
                    eulerY = Mathf.PI / 2;
                    if ((eulerY < 0 && sinY > 0) || (eulerY > 0 && sinY < 0))
                    {
                        eulerY = -eulerY;
                    }
                }
                else
                {
                    eulerY = Mathf.Asin(sinY)*Mathf.PI/180.0f;
                }
                float sinZ = 2 * (w * z + x * y);
                float cosZ = 1 - 2 * (y * y + z * z);
                float eulerZ = Mathf.Atan2(sinZ, cosZ) * Mathf.PI/ 180.0f;
                return new Vec3(eulerX, eulerY, eulerZ);
            }
            set
            {
                float cosX = Mathf.Cos(x * 0.5f); // Se hace para evitar las deformaciones que
                float sinX = Mathf.Sin(x * 0.5f); // se producirian por afectar la mitad del cuaternion
                float cosY = Mathf.Cos(y * 0.5f);
                float sinY = Mathf.Sin(y * 0.5f);
                float cosZ = Mathf.Cos(z * 0.5f);
                float sinZ = Mathf.Sin(z * 0.5f);

                w = (cosX * cosY * cosZ + sinX * sinY * sinZ) * 180.0f / Mathf.PI;
                x = (sinX * cosY * cosZ - cosX * sinY * sinZ) * 180.0f / Mathf.PI;
                y = (cosX * sinY * cosZ + sinX * cosY * sinZ) * 180.0f / Mathf.PI;
                z = (cosX * cosY * sinZ - sinX * sinY * cosZ) * 180.0f / Mathf.PI;
            } 
        }
        // Resumen:
        //     Returns this quaternion with a magnitude of 1 (Read Only).
        public Quats normalized {
            get {
                return new Quats(x / Mathf.Sqrt(w * w + x * x + y * y + z * z),
                                 y / Mathf.Sqrt(w * w + x * x + y * y + z * z),
                                 z / Mathf.Sqrt(w * w + x * x + y * y + z * z),
                                 w / Mathf.Sqrt(w * w + x * x + y * y + z * z));
            }
        }
        // Resumen:
        //     Returns the angle in degrees between two rotations a and b.
        // Parámetros:
        //   a:
        //   b:
        public static float Angle(Quats a, Quats b) {
            /*Quats aux = new Quats(-a.x, -a.y, -a.z, a.w);
            aux = aux * b;
            return float angle = 2 * atan2(norm(Q12(2:4)), Q12(1))
            */
            float dot = a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
            float fromMag = Mathf.Sqrt(a.w * a.w + a.x * a.x + a.y * a.y + a.z * a.z);
            float toMag = Mathf.Sqrt(a.w * a.w + b.x * b.x + b.y * b.y + b.z * b.z);
            float cosNumber = dot / (fromMag * toMag);
            float angle = Mathf.Cos(cosNumber);
            return angle;
        }
        // Resumen:
        //     Creates a rotation which rotates angle degrees around axis.
        // Parámetros:
        //     angle:
        //   axis:
        public static Quats AngleAxis(float angle, Vec3 axis) {
            // Cos(angulo/2)+Sin(angulo/2)(I,J,K) <- Formula de quaterniones en base a angulos.
            Quats q;
            float cosAngle = Mathf.Cos(angle / 2);
            float sinAngle = Mathf.Sin(angle / 2);
            q.x = axis.x * sinAngle;
            q.y = axis.y * sinAngle;
            q.z = axis.z * sinAngle;
            q.w = cosAngle;
            return q;
        }
        // Resumen:
        //     The dot product between two rotations.
        // Parámetros:
        //   a:
        //   b:
        public static float Dot(Quats a, Quats b)
        {
            return (a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w);
        }
        // Resumen:
        //     Returns a rotation that rotates z degrees around the z axis, x degrees around
        //     the x axis, and y degrees around the y axis.
        // Parámetros:
        //   euler:
        public static Quats Euler(Vec3 euler)
        {
            /*
             w = c1 c2 c3 - s1 s2 s3
             x = s1 s2 c3 + c1 c2 s3
             y = s1 c2 c3 + c1 s2 s3
             z = c1 s2 c3 - s1 c2 s3
             where:
             c1 = cos(heading / 2) -> y
             c2 = cos(attitude / 2) -> z
             c3 = cos(bank / 2) -> x
             s1 = sin(heading / 2)
             s2 = sin(attitude / 2)
             s3 = sin(bank / 2)
            */
            float cosX = Mathf.Cos(euler.x * 0.5f);
            float sinX = Mathf.Sin(euler.x * 0.5f);
            float cosY = Mathf.Cos(euler.y * 0.5f);
            float sinY = Mathf.Sin(euler.y * 0.5f);
            float cosZ = Mathf.Cos(euler.z * 0.5f);
            float sinZ = Mathf.Sin(euler.z * 0.5f);

            Quats q;
            q.w = (cosY * cosZ * cosX + sinY * sinZ * sinX) * 180.0f / Mathf.PI;
            q.x = (sinY * sinZ * cosX - cosY * cosZ * sinX) * 180.0f / Mathf.PI;
            q.y = (sinY * cosZ * cosX + cosY * sinZ * sinX) * 180.0f / Mathf.PI;
            q.z = (cosY * sinZ * cosX - sinY * cosZ * sinX) * 180.0f / Mathf.PI;
            return q;
        }
        // Resumen:
        //     Returns a rotation that rotates z degrees around the z axis, x degrees around
        //     the x axis, and y degrees around the y axis.
        // Parámetros:
        //   x:
        //   y:
        //   z:
        public static Quats Euler(float x, float y, float z)
        {
            return Euler(new Vec3(x,y,z));
        }
        // Resumen:
        //     Creates a rotation which rotates from fromDirection to toDirection.
        // Parámetros:
        //   fromDirection:
        //   toDirection:
        public static Quats FromToRotation(Vec3 fromDirection, Vec3 toDirection) {
            Vec3 transition = toDirection - fromDirection;
            
            float cosX = Mathf.Cos(transition.x * 0.5f);
            float sinX = Mathf.Sin(transition.x * 0.5f);
            float cosY = Mathf.Cos(transition.y * 0.5f);
            float sinY = Mathf.Sin(transition.y * 0.5f);
            float cosZ = Mathf.Cos(transition.z * 0.5f);
            float sinZ = Mathf.Sin(transition.z * 0.5f);

            Quats q;
            q.w = (cosY * cosZ * cosX + sinY * sinZ * sinX) * 180.0f / Mathf.PI;
            q.x = (sinY * sinZ * cosX - cosY * cosZ * sinX) * 180.0f / Mathf.PI;
            q.y = (sinY * cosZ * cosX + cosY * sinZ * sinX) * 180.0f / Mathf.PI;
            q.z = (cosY * sinZ * cosX - sinY * cosZ * sinX) * 180.0f / Mathf.PI;
            return q;
        }
        //
        // Resumen:
        //     Returns the Inverse of rotation.
        // Parámetros:
        //   rotation:
        public static Quats Inverse(Quats rotation)
        {
            // inverse = conjugate / magnitude
            Quats inverse = new Quats();
            float rotationMag = Mathf.Sqrt((rotation.x * rotation.x) + (rotation.y * rotation.y) + (rotation.z * rotation.z) + (rotation.w * rotation.w));
            // convierte rotationMag a 1/rotationMag.
            float escalarRotMag = 1f / rotationMag;
            inverse.x = -rotation.x * escalarRotMag;
            inverse.y = -rotation.y * escalarRotMag;
            inverse.z = -rotation.z * escalarRotMag;
            inverse.w = rotation.w * escalarRotMag;
            return inverse;
        }

        //
        // Resumen:
        //     Interpolates between a and b by t and normalizes the result afterwards. The parameter
        //     t is clamped to the range [0, 1].
        // Parámetros:
        //   a:
        //   b:
        //   t:
        public static Quats Lerp(Quats a, Quats b, float t)
        {
            Mathf.Clamp(t,0,1);
            float time = t;
            float timeLeft = 1.0f - time;
            Quats qActual = new Quats();
            // Coseno de angulo entre vectores
            float dot = (((a.x * b.x) + (a.y * b.y)) + (a.z * b.z)) + (a.w * b.w);
            if (dot >= 0.0f)
            {
                qActual.x = (timeLeft * a.x) + (time * b.x);
                qActual.y = (timeLeft * a.y) + (time * b.y);
                qActual.z = (timeLeft * a.z) + (time * b.z);
                qActual.w = (timeLeft * a.w) + (time * b.w);
            }
            else
            {
                qActual.x = (timeLeft * a.x) - (time * b.x);
                qActual.y = (timeLeft * a.y) - (time * b.y);
                qActual.z = (timeLeft * a.z) - (time * b.z);
                qActual.w = (timeLeft * a.w) - (time * b.w);
            }
            float qActualSqrd = (((qActual.x * qActual.x) + (qActual.y * qActual.y)) + (qActual.z * qActual.z)) + (qActual.w * qActual.w);
            float Normalize = 1.0f / (float)Math.Sqrt(qActualSqrd);
            qActual.x *= Normalize;
            qActual.y *= Normalize;
            qActual.z *= Normalize;
            qActual.w *= Normalize;
            return qActual;
        }
        //
        // Resumen:
        //     Interpolates between a and b by t and normalizes the result afterwards. The parameter
        //     t is not clamped.
        // Parámetros:
        //   a:
        //   b:
        //   t:
        public static Quats LerpUnclamped(Quats a, Quats b, float t)
        {
            float time = t;
            float timeLeft = 1.0f - time;
            Quats qActual = new Quats();
            // Coseno de angulo entre vectores
            float dot = (((a.x * b.x) + (a.y * b.y)) + (a.z * b.z)) + (a.w * b.w);
            if (dot >= 0.0f)
            {
                qActual.x = (timeLeft * a.x) + (time * b.x);
                qActual.y = (timeLeft * a.y) + (time * b.y);
                qActual.z = (timeLeft * a.z) + (time * b.z);
                qActual.w = (timeLeft * a.w) + (time * b.w);
            }
            else
            {
                qActual.x = (timeLeft * a.x) - (time * b.x);
                qActual.y = (timeLeft * a.y) - (time * b.y);
                qActual.z = (timeLeft * a.z) - (time * b.z);
                qActual.w = (timeLeft * a.w) - (time * b.w);
            }
            float qActualSqrd = (((qActual.x * qActual.x) + (qActual.y * qActual.y)) + (qActual.z * qActual.z)) + (qActual.w * qActual.w);
            float Normalize = 1.0f / (float)Math.Sqrt(qActualSqrd);
            qActual.x *= Normalize;
            qActual.y *= Normalize;
            qActual.z *= Normalize;
            qActual.w *= Normalize;
            return qActual;
        }
        // Resumen:
        //     Creates a rotation with the specified forward and upwards directions.
        // Parámetros:
        //   forward:
        //     The direction to look in.
        //   upwards:
        //     The vector that defines in which direction up is.
        public static Quats LookRotation(Vec3 forward)
        {
            // Al no saber up se toma inicialmente la default de vec3
            Vec3 up = Vec3.Up;
            forward.Normalize();
            // Se saca right a partir de up y forward y luego se lo normaliza.
            Vec3 upForwardCross = Vec3.Cross(up, forward);
            Vec3 right = new Vec3();
            right.x = upForwardCross.x / (Mathf.Sqrt(upForwardCross.x * upForwardCross.x + upForwardCross.y * upForwardCross.y + upForwardCross.z * upForwardCross.z));
            right.y = upForwardCross.y / (Mathf.Sqrt(upForwardCross.x * upForwardCross.x + upForwardCross.y * upForwardCross.y + upForwardCross.z * upForwardCross.z));
            right.z = upForwardCross.z / (Mathf.Sqrt(upForwardCross.x * upForwardCross.x + upForwardCross.y * upForwardCross.y + upForwardCross.z * upForwardCross.z));
            // se reemplaza el up default por uno usando los otros vectores.
            up.x = forward.y * right.y - right.z * forward.z;
            up.y = forward.z * right.z - forward.x * right.x;
            up.z = forward.x * right.x - forward.y * right.y;
            // Suma total?
            float totalSum = right.x + up.y + forward.z;
            Quats q = new Quats();
            
            if (totalSum > 0f){
                float sqrtTotalSum = Mathf.Sqrt(totalSum + 1.0f);
                q.w = sqrtTotalSum * 0.5f;
                sqrtTotalSum = 0.5f / sqrtTotalSum;
                q.x = (up.z - forward.y) * sqrtTotalSum;
                q.y = (forward.x - right.z) * sqrtTotalSum;
                q.z = (right.y - up.x) * sqrtTotalSum;
                return q;
            }
            if ((right.x >= up.y) && (right.x >= forward.z))
            {
                float num7 = Mathf.Sqrt(((1.0f + right.x) - up.y) - forward.z);
                float num4 = 0.5f / num7;
                q.x = 0.5f * num7;
                q.y = (right.y + up.x) * num4;
                q.z = (right.z + forward.x) * num4;
                q.w = (up.z - forward.y) * num4;
                return q;
            }
            if (up.y > forward.z)
            {
                float num6 = (float)System.Math.Sqrt(((1f + up.y) - right.x) - forward.z);
                float num3 = 0.5f / num6;
                q.x = (up.x + right.y) * num3;
                q.y = 0.5f * num6;
                q.z = (forward.y + up.z) * num3;
                q.w = (forward.x - right.z) * num3;
                return q;
            }
            float num5 = Mathf.Sqrt(((1f + forward.x) - right.x) - up.y);
            float num2 = 0.5f / num5;
            q.x = (forward.x + right.z) * num2;
            q.y = (forward.y + up.z) * num2;
            q.z = 0.5f * num5;
            q.w = (right.y - up.x) * num2;
            return q;
        }
        // Resumen:
        //     Creates a rotation with the specified forward and upwards directions.
        // Parámetros:
        //   forward:
        //     The direction to look in.
        //   upwards:
        //     The vector that defines in which direction up is.
        public static Quats LookRotation(Vec3 forward, Vec3 upwards) {
            throw new NotImplementedException();
        }
        // Resumen:
        //     Converts this quaternion to one with the same orientation but with a magnitude
        //     of 1.
        // Parámetros:
        //   q:
        public static Quats Normalize(Quats q) {
            return new Quats(q.x / Mathf.Sqrt(q.w * q.w + q.x * q.x + q.y * q.y + q.z * q.z),
                              q.y / Mathf.Sqrt(q.w * q.w + q.x * q.x + q.y * q.y + q.z * q.z),
                              q.z / Mathf.Sqrt(q.w * q.w + q.x * q.x + q.y * q.y + q.z * q.z),
                              q.w / Mathf.Sqrt(q.w * q.w + q.x * q.x + q.y * q.y + q.z * q.z));
        }
        // Resumen:
        //     Rotates a rotation from towards to.
        // Parámetros:
        //   from:
        //   to:
        //   maxDegreesDelta:
        public static Quats RotateTowards(Quats from, Quats to, float maxDegreesDelta){
            float angulo = Angle(from, to);
            if (angulo == 0f)
            {
                return to;
            }
            float t = Mathf.Min(1f, maxDegreesDelta / angulo);
            return SlerpUnclamped(from, to, t);
        }
        // Resumen:
        //     Spherically interpolates between a and b by t. The parameter t is clamped to
        //     the range [0, 1].
        // Parámetros:
        //   a:
        //   b:
        //   t:
        public static Quats Slerp(Quats a, Quats b, float t){
            Mathf.Clamp(t,0,1);
            Quats qActual;
            float time = t;
            a.Normalize();
            b.Normalize();
            // Coseno del angulo entre vectores
            float dot = Dot(a, b);
            // si da 1 abs son iguales, devolver.
            if (Mathf.Abs(dot) >= 1.0f){
                return a;
            } 
            // Slerp no funciona con cosenos del angulo menores a 0, revertir origen y conseno del angulo.
            if (dot < 0.0f)
            {
                a = new Quats(-a.x, -a.y, -a.z, -a.w);
                dot = -dot;
            }
            // Arcocoseno del coseno del angulo entre vectores = angulo entre vectores.
            float anguloEntreVectores = Mathf.Acos(dot);
            float anguloActual = anguloEntreVectores * time;
            float senoAngActual = Mathf.Sin(anguloActual);
            float senoAngEntreVecs = Mathf.Sin(anguloEntreVectores);
            // Quaternion resultado= ('origen'*(sin((1.0f-time)'Angulo')+ 'objetivo'(time*'Angulo')) / sin('Angulo'))
            float escalar1 = Mathf.Sin(1.0f - time)*anguloEntreVectores/senoAngEntreVecs;
            float escalar2 = Mathf.Sin(anguloActual)/senoAngActual;
            qActual.w = (a.w * escalar1 + b.w * escalar2);
            qActual.x = (a.x * escalar1 + b.x * escalar2);
            qActual.y = (a.y * escalar1 + b.y * escalar2);
            qActual.z = (a.z * escalar1 + b.z * escalar2);
            return qActual;
        }
        // Resumen:
        //     Spherically interpolates between a and b by t. The parameter t is not clamped.
        // Parámetros:
        //   a:
        //   b:
        //   t:
        public static Quats SlerpUnclamped(Quats a, Quats b, float t) {
            Quats qActual;
            float time = t;
            a.Normalize();
            b.Normalize();
            // Coseno del angulo entre vectores
            float dot = Quats.Dot(a, b);
            // si da 1 abs son iguales, devolver.
            if (Mathf.Abs(dot) >= 1.0f)
            {
                qActual = a;
            }
            // Slerp no funciona con cosenos del angulo menores a 0, revertir origen y conseno del angulo.
            if (dot < 0.0f)
            {
                a = new Quats(-a.x, -a.y, -a.z, -a.w);
                dot = -dot;
            }
            // Arcocoseno del coseno del angulo entre vectores = angulo entre vectores.
            float anguloEntreVectores = Mathf.Acos(dot);
            float anguloActual = anguloEntreVectores * time;
            float senoAngActual = Mathf.Sin(anguloActual);
            float senoAngEntreVecs = Mathf.Sin(anguloEntreVectores);
            // Quaternion resultado= ('origen'*(sin((1.0f-t)'Angulo')+ 'objetivo'(t*'Angulo')) / sin('Angulo'))
            float escalar1 = Mathf.Sin(1.0f - time) * anguloEntreVectores / senoAngEntreVecs;
            float escalar2 = Mathf.Sin(anguloActual) / senoAngActual;
            qActual.w = (a.w * escalar1 + b.w * escalar2);
            qActual.x = (a.x * escalar1 + b.x * escalar2);
            qActual.y = (a.y * escalar1 + b.y * escalar2);
            qActual.z = (a.z * escalar1 + b.z * escalar2);
            return qActual;
        }
        public bool Equals(Quats other) {
            if (x == other.x && y == other.y && z == other.z && w == other.w)
            {
                return true;
            }
            else {
                return false;
            }
        }
        public override bool Equals(object other) {
            if (this == (Quats)other){
                return true;
            }
            else{
                return false;
            }
        }
        public override int GetHashCode() {
            return GetHashCode();
        }
        public void Normalize() {
            new Quats(x / Mathf.Sqrt(x * x + y * y + z * z + w * w),
                     y / Mathf.Sqrt(x * x + y * y + z * z + w * w),
                     z / Mathf.Sqrt(x * x + y * y + z * z + w * w), 
                     w / Mathf.Sqrt(x * x + y * y + z * z + w * w));
        }
        // Resumen:
        //     Set x, y, z and w components of an existing Quaternion.
        // Parámetros:
        //   newX:
        //   newY:
        //   newZ:
        //   newW:
        public void Set(float newX, float newY, float newZ, float newW) {
            x = newX;
            y = newY;
            z = newZ;
            w = newW;
        }
        // Resumen:
        //     Creates a rotation which rotates from fromDirection to toDirection.
        // Parámetros:
        //   fromDirection:
        //   toDirection:
        public void SetFromToRotation(Vec3 fromDirection, Vec3 toDirection)
        {
            this = FromToRotation(fromDirection,toDirection);
        }
        // Resumen:
        //     Creates a rotation with the specified forward and upwards directions.
        // Parámetros:
        //   view:
        //     The direction to look in.
        //   up:
        //     The vector that defines in which direction up is.
        public void SetLookRotation(Vec3 view, Vec3 up)
        {
            this = LookRotation(view,up);
        }
        // Resumen:
        //     Creates a rotation with the specified forward and upwards directions.
        // Parámetros:
        //   view:
        //     The direction to look in.
        public void SetLookRotation(Vec3 view) {
            this = LookRotation(view);
        }
        
        public void ToAngleAxis(out float angle, out Vec3 axis) {
            throw new NotImplementedException();
        }
        // Resumen:
        //     Returns a nicely formatted string of the Quaternion.
        // Parámetros:
        //   format:
        public string ToString(string format)
        {
            return string.Format("({0}, {1}, {2}, {3})", x.ToString(format), y.ToString(format), z.ToString(format), w.ToString(format));
        }
        // Resumen:
        //     Returns a nicely formatted string of the Quaternion.
        // Parámetros:
        //   format:
        public override string ToString() {
            return string.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", x, y, z, w);
        }
        public static Vec3 operator *(Quats rotation, Vec3 point) {
            float sinX = 2 * (rotation.w * rotation.x + rotation.y * rotation.z);
            float cosX = 1 - 2 * (rotation.x * rotation.x + rotation.y * rotation.y);
            float eulerX = Mathf.Atan2(sinX, cosX) * Mathf.PI / 180.0f;
            float sinY = 2 * (rotation.w * rotation.y - rotation.z * rotation.x);
            float eulerY;
            if (Mathf.Abs(sinY) >= 1){
                eulerY = Mathf.PI / 2;
                if ((eulerY < 0 && sinY > 0) || (eulerY > 0 && sinY < 0)){
                    eulerY = -eulerY;
                }
            }
            else{
                eulerY = Mathf.Asin(sinY) * Mathf.PI / 180.0f;
            }
            float sinZ = 2 * (rotation.w * rotation.z + rotation.x * rotation.y);
            float cosZ = 1 - 2 * (rotation.y * rotation.y + rotation.z * rotation.z);
            float eulerZ = Mathf.Atan2(sinZ, cosZ) * Mathf.PI / 180.0f;
            Vec3 aux = new Vec3(eulerX,eulerY,eulerZ);
            Vec3 result = Vec3.Cross(aux,point);
            return result;
        }
        public static Quats operator *(Quats lhs, Quats rhs) {
            Quats resultado;
            float x1 = lhs.x;
            float y1 = lhs.y;
            float z1 = lhs.z;
            float w1 = lhs.w;
            float x2 = rhs.x;
            float y2 = rhs.y;
            float z2 = rhs.z;
            float w2 = rhs.w;
            resultado.x = ((x1 * w2) + (x1 * w2)) + (y1 * z2) - (z1 * y2);
            resultado.y = ((y1 * w2) - (x1 * z2)) + (y1 * w2) + (z1 * x2);
            resultado.z = ((z1 * w2) + (x1 * y1)) + (y1 * x2) - (z1 * w2);
            resultado.w = (w1 * w2) - ((x1 * x2) - (y1 * y2)) - (z1 * z2);
            return resultado;
        }
        public static bool operator ==(Quats lhs, Quats rhs) {
            if (lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z && lhs.w == rhs.w)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator !=(Quats lhs, Quats rhs) {
            if (!(lhs == rhs))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}