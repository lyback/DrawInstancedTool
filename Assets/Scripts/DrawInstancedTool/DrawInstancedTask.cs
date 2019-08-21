using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawInstancedTask
{
    static int maxCount_s = 1023;
    Mesh m_Mesh;
    Material m_Material;
    FastList<Matrix4x4> m_Matrix4x4s = new FastList<Matrix4x4>(maxCount_s);
    Dictionary<string, FastList<float>> m_MatPropertyBlock_Float = new Dictionary<string, FastList<float>>();
    Dictionary<string, FastList<Vector4>> m_MatPropertyBlock_Vec4 = new Dictionary<string, FastList<Vector4>>();
    MaterialPropertyBlock m_MatPB = new MaterialPropertyBlock();
    bool m_IsChangeMatPB_Float = false;
    bool m_IsChangeMatPB_Vec4 = false;

    static Matrix4x4 mat4x4_s = new Matrix4x4();
    public void Init(Mesh mesh, Material material, string[] MatPB_Float = null, string[] MatPB_Vec4 = null)
    {
        m_Mesh = mesh;
        m_Material = material;
        m_MatPropertyBlock_Float.Clear();
        m_MatPropertyBlock_Vec4.Clear();
        if (MatPB_Float != null)
        {
            for (int i = 0; i < MatPB_Float.Length; i++)
            {
                m_MatPropertyBlock_Float.Add(MatPB_Float[i], new FastList<float>(maxCount_s));
            }
        }
        if (MatPB_Vec4 != null)
        {
            for (int i = 0; i < MatPB_Vec4.Length; i++)
            {
                m_MatPropertyBlock_Vec4.Add(MatPB_Vec4[i], new FastList<Vector4>(maxCount_s));
            }
        }
    }

    public uint Add(Vector3 pos, Quaternion rot, Vector3 scale)
    {
        Matrix4x4 mat4x4 = new Matrix4x4();
        Matrix4x4Helper.SetTRS(ref mat4x4, ref pos, ref rot, ref scale);
        foreach (var kv in m_MatPropertyBlock_Float)
        {
            kv.Value.Add(0f);
        }
        foreach (var kv in m_MatPropertyBlock_Vec4)
        {
            kv.Value.Add(Vector4.one);
        }
        return m_Matrix4x4s.Add(mat4x4);
    }
    public void Remove(uint id)
    {
        m_Matrix4x4s.Remove(id);
        foreach (var kv in m_MatPropertyBlock_Float)
        {
            kv.Value.Remove(id);
        }
        foreach (var kv in m_MatPropertyBlock_Vec4)
        {
            kv.Value.Remove(id);
        }
    }
    public void SetPos(uint id, Vector3 pos){
        Matrix4x4 matrix4X4 = mat4x4_s;
        m_Matrix4x4s.GetValue(id, out matrix4X4);
        Matrix4x4Helper.SetMatrixPosition(ref matrix4X4, ref pos);
        m_Matrix4x4s.SetValue(id, matrix4X4);
    }
    public void SetRot(uint id, Quaternion rot){
        Matrix4x4 matrix4X4 = mat4x4_s;
        m_Matrix4x4s.GetValue(id, out matrix4X4);
        Matrix4x4Helper.SetMatrixRotation(ref matrix4X4, ref rot);
        m_Matrix4x4s.SetValue(id, matrix4X4);
    }
    public void SetScale(uint id, Vector3 scale){
        Matrix4x4 matrix4X4 = mat4x4_s;
        m_Matrix4x4s.GetValue(id, out matrix4X4);
        Matrix4x4Helper.SetMatrixScale(ref matrix4X4, ref scale);
        m_Matrix4x4s.SetValue(id, matrix4X4);
    }
    public void SetMatPB_Float(uint id, string name, float value){
        m_MatPropertyBlock_Float[name].SetValue(id, value);
        m_IsChangeMatPB_Float = true;
    }
    public void SetMatPB_Vec4(uint id, string name, Vector4 value){
        m_MatPropertyBlock_Vec4[name].SetValue(id, value);
        m_IsChangeMatPB_Vec4 = true;
    }
    public void Draw()
    {
        if (m_IsChangeMatPB_Float)
        {
            foreach (var kv in m_MatPropertyBlock_Float)
            {
                m_MatPB.SetFloatArray(kv.Key, kv.Value.GetArray());
            }
            m_IsChangeMatPB_Float = false;
        }
        if (m_IsChangeMatPB_Vec4){
            foreach (var kv in m_MatPropertyBlock_Vec4)
            {
                m_MatPB.SetVectorArray(kv.Key, kv.Value.GetArray());
            }
            m_IsChangeMatPB_Vec4 = false;
        }
        int count = m_Matrix4x4s.Count;
        if (count > 0)
        {
            Graphics.DrawMeshInstanced(m_Mesh, 0, m_Material, m_Matrix4x4s.GetArray(), count, m_MatPB);
        }
    }
}