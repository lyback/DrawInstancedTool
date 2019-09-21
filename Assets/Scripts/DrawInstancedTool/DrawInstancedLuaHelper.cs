using UnityEngine;
using System.Collections.Generic;
public class DrawInstancedLuaHelper
{
    public static Vector3 v3_s = Vector3.zero;
    public static Vector3 v3_s_2 = Vector3.zero;
    public static Vector4 v4_s = Vector4.zero;

    #region 行军路线
    public static DrawInstancedTask RouteTask;
    public static string RouteTaskName = "LArmyRoute";
    public static string RouteColorName = "_Color";
    public static string RouteUVScaleName = "_UV_Scale";
    public static string RouteUVSpeedName = "_Speed";
    public static string RouteAlphaEnableName = "_AlphaEnable";
    public static Mesh RouteMesh = null;
    public static Material RouteMat = null;
    public static float Route_Scale = 0.4f;
    public static void AddTask_Route(string[] MatPB_Float, string[] MatPB_Vec4)
    {
        if (RouteMesh == null)
        {
            RouteMesh = new Mesh();
            RouteMesh.vertices = new Vector3[4]
            {
                new Vector3(0, -0.5f, 0),
                new Vector3(1, -0.5f, 0),
                new Vector3(0, 0.5f, 0),
                new Vector3(1, 0.5f, 0)
            };
            RouteMesh.uv = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(0, 1),
                new Vector2(1, 1)
            };
            RouteMesh.triangles = new int[6]
            {
                // lower left triangle
                0, 2, 1,
                // upper right triangle
                2, 3, 1
            };
        }
        if (RouteMat == null)
        {
#if TEST
            RouteMat = Resources.Load<Material>("path_instanced");
#else
            LoadManager.Instance.LoadAsset("lworld/instanced", "path_instanced", "mat", typeof(Material), (data) =>
            {
                RouteMat = data as Material;
            }, false);
#endif
        }
        RouteTask = DrawInstancedTool.AddTask(RouteTaskName, RouteMesh, RouteMat, MatPB_Float, MatPB_Vec4);
    }
    public static void RemoveTask_Route()
    {
        DrawInstancedTool.RemoveTask(RouteTaskName);
    }
    public static uint AddRenderToTask_Route()
    {
        return RouteTask.Add();
    }
    public static void RemoveRenderFromTask_Route(uint id)
    {
        RouteTask.Remove(id);
    }
    public static void SetMatPB_FloatToTask_Route(uint id, string pbName, float value)
    {
        RouteTask.SetMatPB_Float(id, pbName, value);
    }
    public static void SetMatPB_Vec4ToTask_Route(uint id, string pbName, float x, float y, float z, float w)
    {
        v4_s.x = x;
        v4_s.y = y;
        v4_s.z = z;
        v4_s.w = w;
        RouteTask.SetMatPB_Vec4(id, pbName, v4_s);
    }
    //设置是否启用虚线
    public static void SetAlphaEnableToTask_Route(uint id, bool isEnable)
    {
        SetMatPB_FloatToTask_Route(id, RouteAlphaEnableName, isEnable ? 1f : 0f);
    }
    //设置速度
    public static void SetSpeedToTask_Route(uint id, float speed)
    {
        SetMatPB_FloatToTask_Route(id, RouteUVSpeedName, speed);
    }
    //设置颜色
    public static void SetColorToTask_Route(uint id, float r, float g, float b)
    {
        SetMatPB_Vec4ToTask_Route(id, RouteColorName, r, g, b, 1f);
    }
    //设置坐标、旋转、缩放
    public static void SetTRSToTask_Route(uint id, float pos_x, float pos_z, float angle, float scale)
    {
        SetPosToTask_Route(id, pos_x, pos_z);
        SetRotAndScaleToTask_Route(id, angle, scale);
    }
    //设置坐标
    public static void SetPosToTask_Route(uint id, float x, float z)
    {
        v3_s.x = x;
        v3_s.y = 0.1f;
        v3_s.z = z;
        RouteTask.SetPos(id,v3_s);
    }
    //设置旋转和缩放（不能单独设置旋转，若缩放不为等比例缩放，结果是错误的）
    public static void SetRotAndScaleToTask_Route(uint id, float angle, float s)
    {
        v3_s_2.x = 90;
        v3_s_2.y = angle;
        v3_s_2.z = 0;
        v3_s.x = s;
        v3_s.y = Route_Scale;
        v3_s.z = Route_Scale;
        RouteTask.SetRotAndScale(id, v3_s_2, v3_s);
        SetMatPB_FloatToTask_Route(id, RouteUVScaleName, s / Route_Scale);
    }
    //设置缩放
    public static void SetScaleToTask_Route(uint id, float s)
    {
        v3_s.x = s;
        v3_s.y = Route_Scale;
        v3_s.z = Route_Scale;
        RouteTask.SetScale(id, v3_s);
        SetMatPB_FloatToTask_Route(id, RouteUVScaleName, s / Route_Scale);
    }
    #endregion

    #region 建筑
    public static Mesh GridItemMesh;
    public static List<DrawInstancedTask> GridItemTasks = new List<DrawInstancedTask>();
    public static Dictionary<string, Material> GridItemMatDic = new Dictionary<string, Material>();
    public static int AddTask_GridItem(string name)
    {
        if (GridItemMesh == null)
        {
            GridItemMesh = new Mesh();
            GridItemMesh.vertices = new Vector3[4]
            {
                new Vector3(-0.5f, 0, 0),
                new Vector3(0.5f, 0, 0),
                new Vector3(-0.5f, 1, 0),
                new Vector3(0.5f, 1, 0)
            };
            GridItemMesh.uv = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(0, 1),
                new Vector2(1, 1)
            };
            GridItemMesh.triangles = new int[6]
            {
                // lower left triangle
                0, 2, 1,
                // upper right triangle
                2, 3, 1
            };
        }
        if (!GridItemMatDic.ContainsKey(name))
        {
#if TEST
            GridItemMatDic[name] = Resources.Load<Material>(name);
#else
            LoadManager.Instance.LoadAsset("lworld/instanced", name, "mat", typeof(Material), (data) =>
            {
                GridItemMatDic[name] = data as Material;
            }, false);
#endif
            GridItemTasks.Add(DrawInstancedTool.AddTask(name, GridItemMesh, GridItemMatDic[name]));
            return GridItemTasks.Count - 1; 
        }
        return -1;
    }
    public static void RemoveTask_GridItem(string name)
    {
        DrawInstancedTool.RemoveTask(name);
        GridItemMatDic.Remove(name);
    }
    public static void RemoveAllTask_GridItem()
    {
        foreach (var kv in GridItemMatDic)
        {
            DrawInstancedTool.RemoveTask(kv.Key);
        }
        GridItemMatDic.Clear();
    }
    public static uint AddRenderToTask_GridItem(int taskIndex, float pos_x, float pos_y, float pos_z)
    {
        uint id = GridItemTasks[taskIndex].Add();
        SetTRSToTask_GridItem(taskIndex, id, pos_x, pos_y, pos_z, 45, 45, 0, 1, 1, 1);
        return id;
    }
    public static void RemoveRenderFromTask_GridItem(int taskIndex, uint id)
    {
        GridItemTasks[taskIndex].Remove(id);
    }
    //设置坐标、旋转、缩放
    public static void SetTRSToTask_GridItem(int taskIndex, uint id, float pos_x, float pos_y, float pos_z, float rot_x, float rot_y, float rot_z, float scale_x, float scale_y, float scale_z)
    {
        SetPosToTask_GridItem(taskIndex, id, pos_x, pos_y, pos_z);
        SetRotAndScaleToTask_GridItem(taskIndex, id, rot_x, rot_y, rot_z, scale_x, scale_y, scale_z);
    }
    //设置坐标
    public static void SetPosToTask_GridItem(int taskIndex, uint id, float x, float y, float z)
    {
        v3_s.x = x;
        v3_s.y = y;
        v3_s.z = z;
        GridItemTasks[taskIndex].SetPos(id, v3_s);
    }
    //设置旋转
    public static void SetRotAndScaleToTask_GridItem(int taskIndex, uint id, float x, float y, float z, float scale_x, float scale_y, float scale_z)
    {
        v3_s_2.x = x;
        v3_s_2.y = y;
        v3_s_2.z = z;
        v3_s.x = scale_x;
        v3_s.y = scale_y;
        v3_s.z = scale_z;
        GridItemTasks[taskIndex].SetRotAndScale(id, v3_s_2, v3_s);
    }
    //设置缩放
    public static void SetScaleToTask_GridItem(int taskIndex, uint id, float x, float y, float z)
    {
        v3_s.x = x;
        v3_s.y = y;
        v3_s.z = z;
        GridItemTasks[taskIndex].SetScale(id, v3_s);
    }
    #endregion
}