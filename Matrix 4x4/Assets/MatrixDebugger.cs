using UnityEditor;
using UnityEngine;
using CustomMath;

public class MatrixDebugger : EditorWindow
{
	public GameObject target;
	GUIStyle style = new GUIStyle();
	[MenuItem("Window/MatrixDebugger")]
	static void Init()
	{
		MatrixDebugger window = (MatrixDebugger)EditorWindow.GetWindow(typeof(MatrixDebugger));
		window.Show();
	}

	private void OnGUI()
	{
		target = EditorGUILayout.ObjectField(target, typeof(GameObject), true) as GameObject;
		if (target != null)
		{
			target.transform.position = EditorGUILayout.Vector3Field("Position: ", target.transform.position);
			target.transform.rotation = Quaternion.Euler(EditorGUILayout.Vector3Field("Rotation: ", target.transform.rotation.eulerAngles));
			target.transform.localScale = EditorGUILayout.Vector3Field("Scale: ", target.transform.localScale);
			style.fontSize = 10;
			EditorGUILayout.LabelField("Translation matrix", style);
			DrawMatrix(Matrix4x4.Translate(target.transform.position));
			
			EditorGUILayout.LabelField("Rotation matrix", style);
			DrawMatrix(Matrix4x4.Rotate(target.transform.rotation));
			
			EditorGUILayout.LabelField("Scale matrix", style);
			DrawMatrix(Matrix4x4.Scale(target.transform.localScale));
			
			EditorGUILayout.LabelField("TRS matrix",style);
			DrawMatrix(Matrix4x4.TRS(target.transform.position, target.transform.rotation, target.transform.localScale));

			
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Translation matrix", style);
			DrawMatrix(MiMatriz4x4.Translate(target.transform.position));
			
			EditorGUILayout.LabelField("Rotation matrix", style);
			DrawMatrix(MiMatriz4x4.Rotate(target.transform.rotation));
			
			EditorGUILayout.LabelField("Scale matrix", style);
			DrawMatrix(MiMatriz4x4.Scale(target.transform.localScale));
			
			EditorGUILayout.LabelField("TRS matrix", style);
			DrawMatrix(MiMatriz4x4.TRS(target.transform.position, target.transform.rotation, target.transform.localScale));
		}
	}

	private void DrawMatrix(Matrix4x4 matrix)
	{
		GUIStyle style = new GUIStyle();
		style.fontSize = 10;
		EditorGUILayout.LabelField("| " + matrix.m00.ToString("00.00") + " " + matrix.m01.ToString("00.00") + " " + matrix.m02.ToString("00.00") + " " + matrix.m03.ToString("00.00") + " |", style);
		EditorGUILayout.LabelField("| " + matrix.m10.ToString("00.00") + " " + matrix.m11.ToString("00.00") + " " + matrix.m12.ToString("00.00") + " " + matrix.m13.ToString("00.00") + " |", style);
		EditorGUILayout.LabelField("| " + matrix.m20.ToString("00.00") + " " + matrix.m21.ToString("00.00") + " " + matrix.m22.ToString("00.00") + " " + matrix.m23.ToString("00.00") + " |", style);
		EditorGUILayout.LabelField("| " + matrix.m30.ToString("00.00") + " " + matrix.m31.ToString("00.00") + " " + matrix.m32.ToString("00.00") + " " + matrix.m33.ToString("00.00") + " |", style);
	}
	private void DrawMatrix(MiMatriz4x4 matrix)
	{
		GUIStyle style = new GUIStyle();
		style.fontSize = 10;
		EditorGUILayout.LabelField("| " + matrix.r0c0.ToString("00.00") + " " + matrix.r0c1.ToString("00.00") + " " + matrix.r0c2.ToString("00.00") + " " + matrix.r0c3.ToString("00.00") + " |", style);
		EditorGUILayout.LabelField("| " + matrix.r1c0.ToString("00.00") + " " + matrix.r1c1.ToString("00.00") + " " + matrix.r1c2.ToString("00.00") + " " + matrix.r1c3.ToString("00.00") + " |", style);
		EditorGUILayout.LabelField("| " + matrix.r2c0.ToString("00.00") + " " + matrix.r2c1.ToString("00.00") + " " + matrix.r2c2.ToString("00.00") + " " + matrix.r2c3.ToString("00.00") + " |", style);
		EditorGUILayout.LabelField("| " + matrix.r3c0.ToString("00.00") + " " + matrix.r3c1.ToString("00.00") + " " + matrix.r3c2.ToString("00.00") + " " + matrix.r3c3.ToString("00.00") + " |", style);
	}
}
