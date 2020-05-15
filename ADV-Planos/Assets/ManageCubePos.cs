using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using plane;
using CustomMath;

public class ManageCubePos : MonoBehaviour
{
    public GameObject wallFront;
    public GameObject wallBack;
    public GameObject wallTop;
    public GameObject wallBottom;
    public GameObject wallSideR;
    public GameObject wallSideL;
    public GameObject cube;

    [SerializeField]
    bool inWallFront;
    [SerializeField]
    bool inWallBack;
    [SerializeField]
    bool inWallTop;
    [SerializeField]
    bool inWallBottom;
    [SerializeField]
    bool inWallSideR;
    [SerializeField]
    bool inWallSideL;
    [SerializeField]
    bool inRoom;

    Plane wallFrontPlane;
    Plane wallBackPlane;
    Plane wallTopPlane;
    Plane wallBottomPlane;
    Plane wallSideRPlane;
    Plane wallSideLPlane;
    private void Start()
    {
        wallFrontPlane = new Plane(Vec3.Back, wallFront.transform.position);
        wallBackPlane = new Plane(Vec3.Forward, wallBack.transform.position);
        wallTopPlane = new Plane(Vec3.Down, wallTop.transform.position);
        wallBottomPlane = new Plane(Vec3.Up, wallBottom.transform.position);
        wallSideRPlane = new Plane(Vec3.Left, wallSideR.transform.position);
        wallSideLPlane = new Plane(Vec3.Right, wallSideL.transform.position);
    }

    void Update()
    {
        if (wallFrontPlane.GetSide(cube.transform.position))
        {
            inWallFront = true;
        }
        else
        {
            inWallFront = false;
        }
        if (wallBackPlane.GetSide(cube.transform.position))
        {
            inWallBack = true;
        }
        else
        {
            inWallBack = false;
        }
        if (wallTopPlane.GetSide(cube.transform.position))
        {
            inWallTop = true;
        }
        else
        {
            inWallTop = false;
        }
        if (wallBottomPlane.GetSide(cube.transform.position))
        {
            inWallBottom = true;
        }
        else
        {
            inWallBottom = false;
        }
        if (wallSideRPlane.GetSide(cube.transform.position))
        {
            inWallSideR = true;
        }
        else
        {
            inWallSideR = false;
        }
        if (wallSideLPlane.GetSide(cube.transform.position))
        {
            inWallSideL = true;
        }
        else
        {
            inWallSideL = false;
        }
        if (wallBottomPlane.GetSide(cube.transform.position) &&
            wallTopPlane.GetSide(cube.transform.position) &&
            wallFrontPlane.GetSide(cube.transform.position) &&
            wallBackPlane.GetSide(cube.transform.position) &&
            wallSideRPlane.GetSide(cube.transform.position) &&
            wallSideLPlane.GetSide(cube.transform.position))
        {
            inRoom = true;
        }
        else
        {
            inRoom = false;
        }
    }
}
