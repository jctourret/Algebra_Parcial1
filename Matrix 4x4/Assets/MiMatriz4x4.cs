using CustomMath;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomMath
{

    public struct MiMatriz4x4{
        public float r0c0;
        public float r1c0;
        public float r2c0;
        public float r3c0;
        public float r0c1;
        public float r1c1;
        public float r2c1;
        public float r3c1;
        public float r0c2;
        public float r1c2;
        public float r2c2;
        public float r3c2;
        public float r0c3;
        public float r1c3;
        public float r2c3;
        public float r3c3;

        public MiMatriz4x4(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3)
        {
            r0c0 = column0.x; r0c1 = column1.x; r0c2 = column2.x; r0c3 = column3.x;
            r1c0 = column0.y; r1c1 = column1.y; r1c2 = column2.y; r1c3 = column3.y;
            r2c0 = column0.z; r2c1 = column1.z; r2c2 = column2.z; r2c3 = column3.z;
            r3c0 = column0.w; r3c1 = column1.w; r3c2 = column2.w; r3c3 = column3.w;
        }

        public static MiMatriz4x4 zero { 
            get{
                return new MiMatriz4x4(
                   new Vector4(0.0f, 0.0f, 0.0f, 0.0f),
                   new Vector4(0.0f, 0.0f, 0.0f, 0.0f),
                   new Vector4(0.0f, 0.0f, 0.0f, 0.0f),
                   new Vector4(0.0f, 0.0f, 0.0f, 0.0f));
            } 
        }
        //
        // Resumen:
        //     Returns the identity matrix (Read Only).
        public static MiMatriz4x4 identity
        {
            get
            {
                return new MiMatriz4x4(
                   new Vector4(1.0f,0.0f,0.0f,0.0f),
                   new Vector4(0.0f, 1.0f, 0.0f, 0.0f),
                   new Vector4(0.0f, 0.0f, 1.0f, 0.0f),
                   new Vector4(0.0f, 0.0f, 0.0f, 1.0f));
            }
        }

        public static MiMatriz4x4 Rotate(Quats q){
            //Manipulacion algebraica de p' = q*p*q^-1
            MiMatriz4x4 m = zero;
            m.r0c0 = 1.0f - (2.0f * q.y * q.y) - (2.0f * q.z * q.z);
            m.r0c1 = 2.0f * q.x * q.y - 2.0f * q.z * q.w;
            m.r0c2 = 2.0f * q.x * q.z + 2.0f * q.y * q.w;
            m.r0c3 = 0.0f;

            m.r1c0 = 2.0f * q.x * q.y + 2.0f * q.z * q.w;
            m.r1c1 = 1 - 2.0f * q.x * q.z - 2.0f * q.z * q.z;
            m.r1c2 = 2.0f * q.y * q.z - 2.0f * q.x * q.w;
            m.r1c3 = 0.0f;

            m.r2c0 = 2.0f * q.x * q.z - 2.0f * q.y * q.w;
            m.r2c1 = 2.0f * q.y * q.z + 2.0f * q.x * q.w;
            m.r2c2 = 1.0f - 2.0f * q.x * q.x - 2.0f * q.y * q.y;
            m.r2c3 = 0.0f;
            
            m.r3c0 = 0.0f;
            m.r3c1 = 0.0f;
            m.r3c2 = 0.0f;
            m.r3c3 = 1.0f;

            return m;
        }
        // Resumen:
        // Creates a scaling matrix.
        // Parámetros:
        // vector
        public static MiMatriz4x4 Scale(Vec3 vector){
            MiMatriz4x4 m = new MiMatriz4x4();
            m.r0c0 = vector.x; m.r0c1 = 0.0f; m.r0c2 = 0.0f; m.r0c3 = 0.0f;
            m.r1c0 = 0.0f; m.r1c1 = vector.y; m.r1c2 = 0.0f; m.r1c3 = 0.0f;
            m.r2c0 = 0.0f; m.r2c1 = 0.0f; m.r2c2 = vector.z; m.r2c3 = 0.0f;
            m.r3c0 = 0.0f; m.r3c1 = 0.0f; m.r3c2 = 0.0f; m.r3c3 = 1.0f;
            return m;
        }
        // Resumen:
        //     Creates a translation matrix.
        // Parámetros:
        // vector
        public static MiMatriz4x4 Translate(Vec3 vector){
            MiMatriz4x4 m = new MiMatriz4x4();
            m.r0c0 = 1.0f; m.r0c1 = 0.0f; m.r0c2 = 0.0f; m.r0c3 = vector.x;
            m.r1c0 = 0.0f; m.r1c1 = 1.0f; m.r1c2 = 0.0f; m.r1c3 = vector.y;
            m.r2c0 = 0.0f; m.r2c1 = 0.0f; m.r2c2 = 1.0f; m.r2c3 = vector.z;
            m.r3c0 = 0.0f; m.r3c1 = 0.0f; m.r3c2 = 0.0f; m.r3c3 = 1.0f;
            return m;
        }
        public static MiMatriz4x4 TRS(Vec3 pos, Quats q, Vec3 s){
            MiMatriz4x4 T = Translate(pos);
            MiMatriz4x4 R = Rotate(q);
            MiMatriz4x4 S = Scale(s);
            MiMatriz4x4 TRS = T * R * S;
            return TRS;
        }
        public void SetTRS(Vec3 pos, Quats q, Vec3 s){
            this = TRS(pos,q,s);
        }
        public override string ToString(){
            return (
              "r0c0  " + r0c0 + "  r0c1  " + r0c1 +"  r0c2  " + r0c2 + "  r0c3  " + r0c3 +
            "\nr1c0  " + r1c0 + "  r1c1  " + r1c1 + "  r1c2  " + r1c2 + "  r1c3  " + r1c3 +
            "\nr2c0  " + r2c0 + "  r2c1  " + r2c1 + "  r2c2  " + r2c2 + "  r2c3  " + r2c3 +
            "\nr3c0  " + r3c0 + "  r3c1  " + r3c1 + "  r3c2  " + r3c2 + "  r3c3  " + r3c3);
        }
        public static Vector4 operator *(MiMatriz4x4 lhs, Vector4 vector){
            Vector4 result = Vector4.zero;

            result.x = lhs.r0c0 * vector.x + lhs.r0c1 * vector.y + lhs.r0c2 * vector.z + lhs.r0c3 * vector.w;
            result.y = lhs.r1c0 * vector.x + lhs.r0c1 * vector.y + lhs.r1c2 * vector.z + lhs.r1c3 * vector.w;
            result.z = lhs.r2c0 * vector.x + lhs.r0c1 * vector.y + lhs.r2c2 * vector.z + lhs.r2c3 * vector.w;
            result.w = lhs.r3c0 * vector.x + lhs.r0c1 * vector.y + lhs.r3c2 * vector.z + lhs.r3c3 * vector.w;

            return result;
        }
        public static MiMatriz4x4 operator *(MiMatriz4x4 lhs, MiMatriz4x4 rhs){
            MiMatriz4x4 result = zero;
            result.r0c0 = (lhs.r0c0 * rhs.r0c0 + lhs.r0c1 * rhs.r1c0 + lhs.r0c2 * rhs.r2c0 + lhs.r0c3 * rhs.r3c0);
            result.r1c0 = (lhs.r1c0 * rhs.r0c0 + lhs.r1c1 * rhs.r1c0 + lhs.r1c2 * rhs.r2c0 + lhs.r1c3 * rhs.r3c0);
            result.r2c0 = (lhs.r2c0 * rhs.r0c0 + lhs.r2c1 * rhs.r1c0 + lhs.r2c2 * rhs.r2c0 + lhs.r2c3 * rhs.r3c0);
            result.r3c0 = (lhs.r3c0 * rhs.r0c0 + lhs.r3c1 * rhs.r1c0 + lhs.r3c2 * rhs.r2c0 + lhs.r3c3 * rhs.r3c0);
            
            result.r0c1 = (lhs.r0c0 * rhs.r0c1 + lhs.r0c1 * rhs.r1c1 + lhs.r0c2 * rhs.r1c2 + lhs.r0c3 * rhs.r3c1);
            result.r1c1 = (lhs.r1c0 * rhs.r0c1 + lhs.r1c1 * rhs.r1c1 + lhs.r1c2 * rhs.r2c1 + lhs.r1c3 * rhs.r3c1);
            result.r2c1 = (lhs.r2c0 * rhs.r0c1 + lhs.r2c1 * rhs.r1c1 + lhs.r2c2 * rhs.r2c1 + lhs.r2c3 * rhs.r3c1);
            result.r3c1 = (lhs.r3c0 * rhs.r0c1 + lhs.r3c1 * rhs.r1c1 + lhs.r3c2 * rhs.r2c1 + lhs.r3c3 * rhs.r3c1);

            result.r0c2 = (lhs.r0c0 * rhs.r0c2 + lhs.r0c1 * rhs.r1c2 + lhs.r0c2 * rhs.r2c2 + lhs.r0c3 * rhs.r3c2);
            result.r1c2 = (lhs.r1c0 * rhs.r0c2 + lhs.r1c1 * rhs.r1c2 + lhs.r0c2 * rhs.r2c2 + lhs.r1c3 * rhs.r3c2);
            result.r2c2 = (lhs.r2c0 * rhs.r0c2 + lhs.r2c1 * rhs.r1c2 + lhs.r0c2 * rhs.r2c2 + lhs.r2c3 * rhs.r3c2);
            result.r3c2 = (lhs.r3c0 * rhs.r0c2 + lhs.r3c1 * rhs.r1c2 + lhs.r0c2 * rhs.r2c2 + lhs.r3c3 * rhs.r3c2);

            result.r0c3 = (lhs.r0c0 * rhs.r0c3 + lhs.r0c1 * rhs.r1c3 + lhs.r0c2 * rhs.r2c3 + lhs.r0c3 * rhs.r3c3);
            result.r1c3 = (lhs.r1c0 * rhs.r0c3 + lhs.r1c1 * rhs.r1c3 + lhs.r0c2 * rhs.r2c3 + lhs.r1c3 * rhs.r3c3);
            result.r2c3 = (lhs.r2c0 * rhs.r0c3 + lhs.r2c1 * rhs.r1c3 + lhs.r0c2 * rhs.r2c3 + lhs.r2c3 * rhs.r3c3);
            result.r3c3 = (lhs.r3c0 * rhs.r0c3 + lhs.r3c1 * rhs.r1c3 + lhs.r0c2 * rhs.r2c3 + lhs.r3c3 * rhs.r3c3);

            return result;
        }
        public static bool operator ==(MiMatriz4x4 lhs, MiMatriz4x4 rhs){
            if (lhs.r0c0 == rhs.r0c0 && lhs.r0c1 == rhs.r0c1 && lhs.r0c2 == rhs.r0c2 && lhs.r0c3 == rhs.r0c3 &&
                lhs.r1c0 == rhs.r1c0 && lhs.r1c1 == rhs.r1c1 && lhs.r1c2 == rhs.r1c2 && lhs.r1c3 == rhs.r1c3 &&
                lhs.r2c0 == rhs.r2c0 && lhs.r2c1 == rhs.r2c1 && lhs.r2c2 == rhs.r2c2 && lhs.r2c3 == rhs.r2c3 &&
                lhs.r3c0 == rhs.r3c0 && lhs.r3c1 == rhs.r3c1 && lhs.r3c2 == rhs.r3c2 && lhs.r3c3 == rhs.r3c3){
                return true;
            }
            else{
                return false;
            }
        }
        public static bool operator !=(MiMatriz4x4 lhs, MiMatriz4x4 rhs){
            if (!(lhs == rhs)){
                return true;
            }
            else {
                return false;
            }
        }
    }
}