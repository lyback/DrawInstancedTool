using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawInstancedTest : MonoBehaviour {

	public Mesh mesh;
	public Material mat;
	public int count;
	// Use this for initialization
	void Start () {
		DrawInstancedTool.AddTask("path", mesh, mat, new string[]{"_UV_Scale"}, new string[]{"_Color"});
		
		for (int i = 0; i < count; i++)
		{
			float uv_Scale = Random.Range(1,10);
			Vector3 pos = Random.insideUnitSphere;
			uint id = DrawInstancedTool.AddRenderToTask("path", pos, Quaternion.Euler(new Vector3(90f,Random.Range(0f,360f),0f)), new Vector3(uv_Scale,1,1));
			DrawInstancedTool.SetMatPB_FloatToTask("path", id, "_UV_Scale", uv_Scale);
			DrawInstancedTool.SetMatPB_Vec4ToTask("path", id, "_Color", Random.ColorHSV());
		}

		DrawInstancedTool.AddTask("path2", mesh, mat, new string[]{"_UV_Scale"}, new string[]{"_Color"});
		for (int i = 0; i < count; i++)
		{
			float uv_Scale = Random.Range(1,10);
			Vector3 pos = Random.insideUnitSphere + Vector3.one*10f;
			uint id = DrawInstancedTool.AddRenderToTask("path2", pos, Quaternion.Euler(new Vector3(90f,Random.Range(0f,360f),0f)), new Vector3(uv_Scale,1,1));
			DrawInstancedTool.SetMatPB_FloatToTask("path2", id, "_UV_Scale", uv_Scale);
			DrawInstancedTool.SetMatPB_Vec4ToTask("path2", id, "_Color", Random.ColorHSV());
		}
	}
	
	// // Update is called once per frame
	// void Update () {
	// 	task.Draw();
	// }
}
