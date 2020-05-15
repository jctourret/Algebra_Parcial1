using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CustomMath;

namespace plane
{
    public class Planes : MonoBehaviour
    {
        public Planes(Vec3 inNormal, Vec3 inPoint) {    
            {
                normal = inNormal;
                distance = -inNormal.x * inPoint.x - inNormal.y * inPoint.y - inNormal.z * inPoint.z;
            }
        }
        public Planes(Vec3 a, Vec3 b, Vec3 c) {
            Vec3 AB = a - b;
            Vec3 AC = c - a;
            normal = Vec3.Cross(AB, AC);
            distance = Mathf.Abs (-normal.x * a.x - normal.y * a.y - normal.z * a.z);
        }
        public Vec3 normal
        {
            get { return normal; }
            set { normal = value; }
        }
        public float distance
        {
            get {return distance;}
            set {distance = value;}
        }
        public Plane flipped
        {
            get
            {
                return new Plane(-normal,distance);
            }
        }
        public static Plane Translate(Plane plane, Vec3 translation)
        {
            throw new NotImplementedException();
        }
        public Vec3 ClosestPointOnPlane(Vec3 point)
        {
            throw new NotImplementedException();
        }
        public void Flip()
        {
            normal = -normal;
            distance = -distance;
        }
        public float GetDistanceToPoint(Vec3 point)
        {
            return ((Vec3.Dot(point, normal)+distance) / Vec3.Magnitude(point));
        }
        public bool GetSide(Vec3 point)
        {
            if (GetDistanceToPoint(point)>=0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Raycast(Ray ray, out float enter)
        {
            throw new NotImplementedException();
        }

        public bool SameSide(Vec3 inPt0, Vec3 inPt1)
        {
            if (GetDistanceToPoint(inPt0) > 0 && GetDistanceToPoint(inPt1) > 0 ||
                GetDistanceToPoint(inPt0) < 0 && GetDistanceToPoint(inPt1) < 0 ||
                GetDistanceToPoint(inPt0) == 0 && GetDistanceToPoint(inPt1) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Set3Points(Vec3 a, Vec3 b, Vec3 c)
        {
            Vec3 AB = a - b;
            Vec3 AC = c - a;
            normal = Vec3.Cross(AB, AC);
            distance = -normal.x * a.x - normal.y * a.y - normal.z * a.z;
        }
        public void SetNormalAndPosition(Vec3 inNormal, Vec3 inPoint)
        {
            normal = inNormal;
            normal.Normalize();
            distance = -inNormal.x * inPoint.x - inNormal.y * inPoint.y - inNormal.z * inPoint.z;
        }
        public override string ToString()
        {
            throw new NotImplementedException();
        }
        public string ToString(string format)
        {
            throw new NotImplementedException();
        }
        public void Translate(Vec3 translation)
        {
            throw new NotImplementedException();
        }
    }
}
