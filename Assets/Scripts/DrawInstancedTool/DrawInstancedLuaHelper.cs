using UnityEngine;
using System.Collections.Generic;
public class DrawInstancedLuaHelper
{
    public static Vector3 v3_s = Vector3.zero;
    public static Vector4 v4_s = Vector4.zero;

    #region 行军路线
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
        DrawInstancedTool.AddTask(RouteTaskName, RouteMesh, RouteMat, MatPB_Float, MatPB_Vec4);
    }
    public static void RemoveTask_Route()
    {
        DrawInstancedTool.RemoveTask(RouteTaskName);
    }
    public static uint AddRenderToTask_Route()
    {
        return DrawInstancedTool.AddRenderToTask(RouteTaskName);
    }
    public static void RemoveRenderFromTask_Route(uint id)
    {
        DrawInstancedTool.RemoveRenderFromTask(RouteTaskName, id);
    }
    public static void SetMatPB_FloatToTask_Route(uint id, string pbName, float value)
    {
        DrawInstancedTool.SetMatPB_FloatToTask(RouteTaskName, id, pbName, value);
    }
    public static void SetMatPB_Vec4ToTask_Route(uint id, string pbName, float x, float y, float z, float w)
    {
        v4_s.x = x;
        v4_s.y = y;
        v4_s.z = z;
        v4_s.w = w;
        DrawInstancedTool.SetMatPB_Vec4ToTask(RouteTaskName, id, pbName, v4_s);
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
        SetRotToTask_Route(id, angle);
        SetScaleToTask_Route(id, scale);
    }
    //设置坐标
    public static void SetPosToTask_Route(uint id, float x, float z)
    {
        v3_s.x = x;
        v3_s.y = 0.1f;
        v3_s.z = z;
        DrawInstancedTool.SetPosToTask(RouteTaskName, id, v3_s);
    }
    //设置旋转
    public static void SetRotToTask_Route(uint id, float angle)
    {
        v3_s.x = 90;
        v3_s.y = angle;
        v3_s.z = 0;
        DrawInstancedTool.SetRotToTask(RouteTaskName, id, Quaternion.Euler(v3_s));
    }
    //设置缩放
    public static void SetScaleToTask_Route(uint id, float s)
    {
        v3_s.x = s;
        v3_s.y = Route_Scale;
        v3_s.z = Route_Scale;
        DrawInstancedTool.SetScaleToTask(RouteTaskName, id, v3_s);
        SetMatPB_FloatToTask_Route(id, RouteUVScaleName, s / Route_Scale);
    }
    #endregion

    #region 建筑
    public static Mesh GridItemMesh;
    public static Dictionary<string, Material> GridItemMatDic = new Dictionary<string, Material>();
    public static void AddTask_GridItem(string name)
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
            mat = Resources.Load<Material>("food");
#else
            LoadManager.Instance.LoadAsset("lworld/instanced", name, "mat", typeof(Material), (data) =>
            {
                GridItemMatDic[name] = data as Material;
            }, false);
#endif
            DrawInstancedTool.AddTask(name, GridItemMesh, GridItemMatDic[name]);
        }
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
    public static uint AddRenderToTask_GridItem(string name, float pos_x, float pos_z)
    {
        uint id = DrawInstancedTool.AddRenderToTask(name);
        SetTRSToTask_GridItem(name, id, pos_x, pos_z);
        return id;
    }
    public static void RemoveRenderFromTask_GridItem(string name, uint id)
    {
        DrawInstancedTool.RemoveRenderFromTask(name, id);
    }
    //设置坐标、旋转、缩放
    public static void SetTRSToTask_GridItem(string name, uint id, float pos_x, float pos_z)
    {
        SetPosToTask_GridItem(name, id, pos_x, 0, pos_z);
        SetRotToTask_GridItem(name, id, 45, 45, 0);
        SetScaleToTask_GridItem(name, id, 1, 1, 1);
    }
    //设置坐标
    public static void SetPosToTask_GridItem(string name, uint id, float x, float y, float z)
    {
        v3_s.x = x;
        v3_s.y = y;
        v3_s.z = z;
        DrawInstancedTool.SetPosToTask(name, id, v3_s);
    }
    //设置旋转
    public static void SetRotToTask_GridItem(string name, uint id, float x, float y, float z)
    {
        v3_s.x = x;
        v3_s.y = y;
        v3_s.z = z;
        DrawInstancedTool.SetRotToTask(name, id, Quaternion.Euler(v3_s));
    }
    //设置缩放
    public static void SetScaleToTask_GridItem(string name, uint id, float x, float y, float z)
    {
        v3_s.x = x;
        v3_s.y = y;
        v3_s.z = z;
        DrawInstancedTool.SetScaleToTask(name, id, v3_s);
    }
    #endregion
}