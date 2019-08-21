using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawInstancedTest : MonoBehaviour {

	public bool alphaEnable = false;
	public float speed = 1;
	public Transform target;
	public float angle;
	public int count;
	private uint id;
	// Use this for initialization
	void Awake () {

		DrawInstancedLuaHelper.AddTask_Route(new string[]{"_UV_Scale","_Speed","_AlphaEnable"}, new string[]{"_Color"});
		for (int i = 0; i < count; i++)
		{
			Vector3 pos = Random.insideUnitSphere;
			id = DrawInstancedLuaHelper.AddRenderToTask_Route();
			DrawInstancedLuaHelper.SetPosToTask_Route(id, 0, 0);
			DrawInstancedLuaHelper.SetScaleToTask_Route(id, 10);
			DrawInstancedLuaHelper.SetColorToTask_Route(id, 1,0,0);
		}

		// DrawInstancedLuaHelper.AddTask_Resource();
		// for (int i = 0; i < count; i++)
		// {
		// 	id = DrawInstancedLuaHelper.AddRenderToTask_Resource();
		// 	DrawInstancedLuaHelper.SetPosToTask_Resource(id, 0f, 0f, 0f);
		// 	DrawInstancedLuaHelper.SetRotToTask_Resource(id, 0,0,0);
		// 	DrawInstancedLuaHelper.SetScaleToTask_Resource(id, 1,1,1);
		// }
	}
	
	// Update is called once per frame
	void Update () {
		var pos = target.position;
		angle = -Mathf.Atan2(pos.z,pos.x)*Mathf.Rad2Deg;
		DrawInstancedLuaHelper.SetTRSToTask_Route(id, 0, 0, angle, 10);
		DrawInstancedLuaHelper.SetAlphaEnableToTask_Route(id, alphaEnable);
		DrawInstancedLuaHelper.SetSpeedToTask_Route(id, speed);
		DrawInstancedLuaHelper.SetColorToTask_Route(id, 1,0,0);
		speed = speed + 0.001f;
	}
}
