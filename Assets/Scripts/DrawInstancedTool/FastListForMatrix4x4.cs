using System;
using UnityEngine;
using System.Collections.Generic;

public class FastListForMatrix4x4
{
    Matrix4x4[] m_Array;
    Dictionary<uint, int> m_IdtoIndex;
    Dictionary<int, uint> m_IndextoId;
    int m_Count;
    public int Count
    {
        get
        {
            return m_Count;
        }
    }
    uint m_Id;
    public FastListForMatrix4x4(int count)
    {
        m_Id = 0;
        m_Count = 0;
        m_Array = new Matrix4x4[count];
        m_IdtoIndex = new Dictionary<uint, int>(count);
        m_IndextoId = new Dictionary<int, uint>(count);
    }

    public uint Add(Matrix4x4 item)
    {
        m_Array[m_Count] = item;
        m_IdtoIndex.Add(m_Id, m_Count);
        m_IndextoId.Add(m_Count, m_Id);
        m_Id++;
        m_Count++;
        return m_Id - 1;
    }
    public void Remove(uint id)
    {
        int index_d;
        int index_l = m_Count - 1;
        if (m_IdtoIndex.TryGetValue(id, out index_d))
        {
            if (index_d == index_l)
            {
                m_Array[index_d] = default(Matrix4x4);
            }
            else
            {
                m_Array[index_d] = m_Array[index_l];
                m_Array[index_l] = default(Matrix4x4);

                uint id_l = m_IndextoId[index_l];
                m_IdtoIndex[id_l] = index_d;
                m_IndextoId[index_d] = id_l;
            }
            m_Count -= 1;
            m_IdtoIndex.Remove(id);
            m_IndextoId.Remove(index_l);
        }
    }
    public Matrix4x4[] GetArray()
    {
        return m_Array;
    }
    public void GetValue(uint id, ref Matrix4x4 value)
    {
        value = m_Array[m_IdtoIndex[id]];
    }
    public void SetValue(uint id, Matrix4x4 value)
    {
        m_Array[m_IdtoIndex[id]] = value;
    }
    public void SetPos(uint id, Vector3 pos)
    {
        Matrix4x4Helper.SetMatrixPosition(ref m_Array[m_IdtoIndex[id]], pos);
    }
    public void SetRotAndScale(uint id, Vector3 rot, Vector3 scale)
    {
        Matrix4x4Helper.SetRS(ref m_Array[m_IdtoIndex[id]], rot, scale);
    }
    public void SetRotAndResetScale(uint id, Vector3 rot)
    {
        Matrix4x4Helper.SetMatrixRotationAndResetScale(ref m_Array[m_IdtoIndex[id]], rot);
    }
    public void SetScaleAndResetRot(uint id, Vector3 scale)
    {
        Matrix4x4Helper.SetMatrixScaleAndResetRot(ref m_Array[m_IdtoIndex[id]], scale);
    }
}