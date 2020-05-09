using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathDebbuger.Internals;

namespace MathDebbuger
{
    public static class VectorDebugger
    {
        private static Camera renderCamera;
        private static Dictionary<string, CameraInternals.CameraDebugger> debuggers;
        private static bool showedInEditor = false;
        private static bool showedCoordinates = false;
        private static bool InitDebugger()
        {
            renderCamera = Object.FindObjectOfType<Camera>();
            if (renderCamera)
            {
                debuggers = new Dictionary<string, CameraInternals.CameraDebugger>();
                renderCamera.gameObject.AddComponent<CameraInternals.VectorHandles>();
                return true;
            }
            Debug.LogError("Init Failed: The VectorDebugger needs a Camera in scene to works");
            return false;
        }
        private static bool CheckInited()
        {
            if (renderCamera)
                return true;
            return InitDebugger();
        }
        private static bool KeyAlreadyExist(string key)
        {
            if (debuggers.ContainsKey(key))
            {
                Debug.LogError("Init Failed: The identifier \"" + key + "\" are already in use");
                return true;
            }
            return false;
        }
        private static bool ExistKey(string key)
        {
            if (!debuggers.ContainsKey(key))
            {
                Debug.LogError("Find Identifier Failed: The identifier \"" + key + "\" don't exist");
                return false;
            }
            return true;
        }
        public static void AddVector(Vector3 destinationPosition, string identifier)
        {
            if (!CheckInited() && !KeyAlreadyExist(identifier))
                return;
            CameraInternals.CameraDebugger cameraDebugger = renderCamera.gameObject.AddComponent<CameraInternals.CameraDebugger>();
            cameraDebugger.hideFlags = HideFlags.HideInInspector;
            List<Vector3> positions = new List<Vector3>();
            positions.Add(Vector3.zero);
            positions.Add(destinationPosition);
            cameraDebugger.Init(positions);
            if(showedInEditor)
                cameraDebugger.EnableShowInEditor();
            if(showedCoordinates)
                cameraDebugger.EnableShowCoordinates();
            debuggers.Add(identifier, cameraDebugger);
        }
        public static void AddVector(Vector3 originPosition, Vector3 destinationPosition, string identifier)
        {
            if (!CheckInited() && !KeyAlreadyExist(identifier))
                return;
            CameraInternals.CameraDebugger cameraDebugger = renderCamera.gameObject.AddComponent<CameraInternals.CameraDebugger>();
            cameraDebugger.hideFlags = HideFlags.HideInInspector;
            List<Vector3> positions = new List<Vector3>();
            positions.Add(originPosition);
            positions.Add(destinationPosition);
            cameraDebugger.Init(positions);
            if (showedInEditor)
                cameraDebugger.EnableShowInEditor();
            if (showedCoordinates)
                cameraDebugger.EnableShowCoordinates();
            debuggers.Add(identifier, cameraDebugger);
        }
        public static void AddVector(Vector3 destinationPosition, Color vectorColor, string identifier)
        {
            if (!CheckInited() && !KeyAlreadyExist(identifier))
                return;
            CameraInternals.CameraDebugger cameraDebugger = renderCamera.gameObject.AddComponent<CameraInternals.CameraDebugger>();
            cameraDebugger.hideFlags = HideFlags.HideInInspector;
            List<Vector3> positions = new List<Vector3>();
            positions.Add(Vector3.zero);
            positions.Add(destinationPosition);
            cameraDebugger.Init(positions, vectorColor);
            if (showedInEditor)
                cameraDebugger.EnableShowInEditor();
            if (showedCoordinates)
                cameraDebugger.EnableShowCoordinates();
            debuggers.Add(identifier, cameraDebugger);
        }
        public static void AddVector(Vector3 originPosition, Vector3 destinationPosition, Color vectorColor, string identifier)
        {
            if (!CheckInited() && !KeyAlreadyExist(identifier))
                return;
            CameraInternals.CameraDebugger cameraDebugger = renderCamera.gameObject.AddComponent<CameraInternals.CameraDebugger>();
            cameraDebugger.hideFlags = HideFlags.HideInInspector;
            List<Vector3> positions = new List<Vector3>();
            positions.Add(originPosition);
            positions.Add(destinationPosition);
            cameraDebugger.Init(positions, vectorColor);
            if (showedInEditor)
                cameraDebugger.EnableShowInEditor();
            if (showedCoordinates)
                cameraDebugger.EnableShowCoordinates();
            debuggers.Add(identifier, cameraDebugger);
        }
        public static void AddVectorsSecuence(List<Vector3> positions, bool useTheFirstVertexAsZero, string identifier)
        {
            if (!CheckInited() && !KeyAlreadyExist(identifier))
                return;
            CameraInternals.CameraDebugger cameraDebugger = renderCamera.gameObject.AddComponent<CameraInternals.CameraDebugger>();
            cameraDebugger.hideFlags = HideFlags.HideInInspector;
            if (!useTheFirstVertexAsZero)
                positions.Insert(0, Vector3.zero);
            cameraDebugger.Init(positions);
            if (showedInEditor)
                cameraDebugger.EnableShowInEditor();
            if (showedCoordinates)
                cameraDebugger.EnableShowCoordinates();
            debuggers.Add(identifier, cameraDebugger);
        }
        public static void AddVectorsSecuence(List<Vector3> positions, bool useTheFirstVertexAsZero, Color vectorColor, string identifier)
        {
            if (!CheckInited() && !KeyAlreadyExist(identifier))
                return;
            CameraInternals.CameraDebugger cameraDebugger = renderCamera.gameObject.AddComponent<CameraInternals.CameraDebugger>();
            cameraDebugger.hideFlags = HideFlags.HideInInspector;
            if (!useTheFirstVertexAsZero)
                positions.Insert(0, Vector3.zero);
            cameraDebugger.Init(positions, vectorColor);
            if (showedInEditor)
                cameraDebugger.EnableShowInEditor();
            if (showedCoordinates)
                cameraDebugger.EnableShowCoordinates();
            debuggers.Add(identifier, cameraDebugger);
        }
        public static void UpdatePosition(string identifier, Vector3 newDestinationPosition)
        {
            if (!ExistKey(identifier))
                return;
            List<Vector3> newPositions = new List<Vector3>();
            newPositions.Add(Vector3.zero);
            newPositions.Add(newDestinationPosition);
            debuggers[identifier].UpdateVectors(newPositions);
        }
        public static void UpdatePosition(string identifier, Vector3 newOriginPosition, Vector3 newDestinationPosition)
        {
            if (!ExistKey(identifier))
                return;
            List<Vector3> newPositions = new List<Vector3>();
            newPositions.Add(newOriginPosition);
            newPositions.Add(newDestinationPosition);
            debuggers[identifier].UpdateVectors(newPositions);
        }
        public static void UpdatePositionsSecuence(string identifier, List<Vector3> newPositions)
        {
            if (ExistKey(identifier))
                debuggers[identifier].UpdateVectors(newPositions);
        }
        public static void UpdateColor(string identifier, Color newColor)
        {
            if (ExistKey(identifier))
                debuggers[identifier].UpdateColor(newColor);
        }
        public static void EnableEditorView()
        {
            if (!CheckInited())
                return;
            foreach (KeyValuePair<string, CameraInternals.CameraDebugger> debugger in debuggers)
            {
                debugger.Value.EnableShowInEditor();
            }
            showedInEditor = true;
        }
        public static void DisableEditorView()
        {
            if (!CheckInited())
                return;
            foreach (KeyValuePair<string, CameraInternals.CameraDebugger> debugger in debuggers)
            {
                debugger.Value.DisableShowInEditor();
            }
            showedInEditor = false;

        }
        public static void EnableCoordinates()
        {
            if (!CheckInited())
                return;
            foreach (KeyValuePair<string, CameraInternals.CameraDebugger> debugger in debuggers)
            {
                debugger.Value.EnableShowCoordinates();
            }
            showedCoordinates = true;
        }
        public static void DisableCoordinates()
        {
            if (!CheckInited())
                return;
            foreach (KeyValuePair<string, CameraInternals.CameraDebugger> debugger in debuggers)
            {
                debugger.Value.DisableShowCoordinates();
            }
            showedCoordinates = false;
        }
        public static void SetVectorArrow(VectorArrow arrow)
        {
            if (!CheckInited())
                return;
            foreach (KeyValuePair<string, CameraInternals.CameraDebugger> debugger in debuggers)
            {
                debugger.Value.SetVectorArrow(arrow);
            }
        }
        public static void SetFontSize(int size) 
        {
            if (!CheckInited())
                return;
            foreach (KeyValuePair<string, CameraInternals.CameraDebugger> debugger in debuggers)
            {
                debugger.Value.SetFontSize(size);
            }
        }
        public static void EnableEditorView(string identifier)
        {
            if (ExistKey(identifier))
                debuggers[identifier].EnableShowInEditor();
        }
        public static void DisableEditorView(string identifier)
        {
            if (ExistKey(identifier))
                debuggers[identifier].DisableShowInEditor();
        }
        public static void EnableCoordinates(string identifier)
        {
            if (ExistKey(identifier))
                debuggers[identifier].EnableShowCoordinates();
        }
        public static void DisableCoordinates(string identifier)
        {
            if (ExistKey(identifier))
                debuggers[identifier].DisableShowCoordinates();
        }
        public static void SetVectorArrow(VectorArrow arrow, string identifier)
        {
            if (ExistKey(identifier))
                debuggers[identifier].SetVectorArrow(arrow);
        }
        public static void SetFontSize(int size, string identifier)
        {
            if (ExistKey(identifier))
                debuggers[identifier].SetFontSize(size);
        }
        public static void DeleteVector(string identifier)
        {
            if (ExistKey(identifier))
            {
                debuggers[identifier].Delete();
                debuggers.Remove(identifier);
            }
        }
        public static void TurnOffVector(string identifier)
        {
            if (ExistKey(identifier))
                debuggers[identifier].enabled = false;
        }
        public static void TurnOnVector(string identifier)
        {
            if (ExistKey(identifier))
                debuggers[identifier].enabled = true;
        }

        public static List<Vector3> GetVectorsPositions(string identifier) 
        {
            if (ExistKey(identifier))
                return debuggers[identifier].GetPositions();
            return new List<Vector3>();
        }
    }
}
