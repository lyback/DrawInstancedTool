using UnityEngine;
using System.Collections.Generic;
public class DrawInstancedTool : MonoBehaviour
{
    private static DrawInstancedTool _instance;
    public static DrawInstancedTool Instance
    {
        get { return Init(); }
    }

    public static DrawInstancedTool Init()
    {
        if (_instance == null)
        {
            GameObject go = new GameObject("~DrawInstancedTool");
            _instance = go.AddComponent<DrawInstancedTool>();
            DontDestroyOnLoad(go);
        }
        return _instance;
    }
    public Dictionary<string, DrawInstancedTask> m_TastDic = new Dictionary<string, DrawInstancedTask>();

    public static void AddTask(string taskName, Mesh mesh, Material material, string[] MatPB_Float = null, string[] MatPB_Vec4 = null)
    {
        DrawInstancedTask task;
        if (Instance.m_TastDic.TryGetValue(taskName, out task))
        {
            task.Init(mesh, material, MatPB_Float, MatPB_Vec4);
        }
        else
        {
            task = new DrawInstancedTask();
            Instance.m_TastDic.Add(taskName, task);
            task.Init(mesh, material, MatPB_Float, MatPB_Vec4);
        }
    }
    public static void RemoveTask(string taskName)
    {
        if (Instance.m_TastDic.ContainsKey(taskName))
        {
            Instance.m_TastDic.Remove(taskName);
        }
    }
    public static void RemoveAllTask(){
        Instance.m_TastDic.Clear();
    }

    public static uint AddRenderToTask(string taskName, Vector3 pos, Quaternion rot, Vector3 scale)
    {
        return Instance.m_TastDic[taskName].Add(pos, rot, scale);
    }
    public static uint AddRenderToTask(string taskName, Vector3 pos, Quaternion rot)
    {
        return Instance.m_TastDic[taskName].Add(pos, rot, Vector3.one);
    }
    public static uint AddRenderToTask(string taskName, Vector3 pos, Vector3 scale)
    {
        return Instance.m_TastDic[taskName].Add(pos, Quaternion.identity, scale);
    }
    public static uint AddRenderToTask(string taskName, Vector3 pos)
    {
        return Instance.m_TastDic[taskName].Add(pos, Quaternion.identity, Vector3.one);
    }
    public static uint AddRenderToTask(string taskName)
    {
        return Instance.m_TastDic[taskName].Add(Vector3.zero, Quaternion.identity, Vector3.one);
    }

    public static void SetMatPB_FloatToTask(string taskName, uint id, string pbName, float value)
    {
        Instance.m_TastDic[taskName].SetMatPB_Float(id, pbName, value);
    }
    public static void SetMatPB_Vec4ToTask(string taskName, uint id, string pbName, Vector4 value)
    {
        Instance.m_TastDic[taskName].SetMatPB_Vec4(id, pbName, value);
    }
    public static void SetPosToTask(string taskName, uint id, Vector3 pos)
    {
        Instance.m_TastDic[taskName].SetPos(id, pos);
    }
    public static void SetRotToTask(string taskName, uint id, Quaternion rot)
    {
        Instance.m_TastDic[taskName].SetRot(id, rot);
    }
    public static void SetScaleToTask(string taskName, uint id, Vector3 scale)
    {
        Instance.m_TastDic[taskName].SetScale(id, scale);
    }

    public static void RemoveRenderFromTask(string taskName, uint id)
    {
        Instance.m_TastDic[taskName].Remove(id);
    }

    void Update(){
        foreach (var kv in Instance.m_TastDic)
        {
            kv.Value.Draw();
        }
    }
}