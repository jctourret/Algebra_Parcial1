using System.Collections.Generic;
using UnityEngine;
using MathDebbuger;
using CustomMath;
using UnityEngine.XR.WSA.Input;

public class Tester : MonoBehaviour
{
    public Vector3 ejerA;
    public Vector3 ejerB;

    Vec3 result;
    float timer = 0;

    public enum Ejercicios
    {
        uno,
        dos,
        tres,
        cuatro,
        cinco,
        seis,
        siete,
        ocho,
        nueve,
        diez,
    }

    public Ejercicios ejer = 0;

    void Start()
    {
        VectorDebugger.EnableCoordinates();
        List<Vector3> vectors = new List<Vector3>();
        VectorDebugger.AddVector(ejerA, Color.black, "elNegro");
        VectorDebugger.AddVector(ejerB, Color.yellow, "elAmarillo");
        vectors.Add(new Vec3(10.0f, 0.0f, 0.0f));
        vectors.Add(new Vec3(10.0f, 10.0f, 0.0f));
        vectors.Add(new Vec3(20.0f, 10.0f, 0.0f));
        vectors.Add(new Vec3(20.0f, 20.0f, 0.0f));
        VectorDebugger.AddVectorsSecuence(vectors, false, Color.red, "secuencia");
        VectorDebugger.AddVector(new Vector3(10, 10, 0), Color.blue, "elAzul");
        VectorDebugger.AddVector(Vector3.down * 7, Color.green, "elVerde");
        VectorDebugger.AddVector(result, Color.white, "elBlanco");
        VectorDebugger.EnableEditorView();
    }

    // Update is called once per frame
    void Update()
    {
        Vec3 A = new Vec3(ejerA);
        Vec3 B = new Vec3(ejerB);
        
        if (Input.GetKeyDown(KeyCode.O))
        {
            VectorDebugger.TurnOffVector("elAzul");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            VectorDebugger.TurnOnVector("elAzul");
        }
        switch (ejer) {
            case Ejercicios.uno:
            result = A + B;
            break;
            case Ejercicios.dos:
            result = A - B;
            break;
            case Ejercicios.tres:
            //result = Vec3.Scale(A,B);
            break;
            case Ejercicios.cuatro:
            result = Vec3.Cross(A,B);
            break;
            case Ejercicios.cinco:
            timer += Time.deltaTime;
            result = Vec3.Lerp(A,B,timer);
            if (timer > 1) { timer = 0; }
            break;
            case Ejercicios.seis:
            result = Vec3.Max(A,B);
            break;
            case Ejercicios.siete:
            result = Vec3.Project(A,B);
            break;
            case Ejercicios.ocho:
            Vec3 suma = new Vec3(A + B);
            // Esta clampeado por la magnitud de la suma?
            result = Vec3.ClampMagnitude(suma, suma.normalized*);
            break;
            case Ejercicios.nueve:
            // result = ;
            break;
            case Ejercicios.diez:
            float t = 0;
            t += Time.deltaTime;
            result = Vec3.LerpUnclamped(A,B,t);
            break;
        }
        VectorDebugger.UpdatePosition("elBlanco", result);
    }
}