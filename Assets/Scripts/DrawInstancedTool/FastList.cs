using System;
using System.Collections.Generic;

public class FastList<T>
{
    T[] m_Array;
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
    public FastList(int count)
    {
        m_Id = 0;
        m_Count = 0;
        m_Array = new T[count];
        m_IdtoIndex = new Dictionary<uint, int>(count);
        m_IndextoId = new Dictionary<int, uint>(count);
    }

    public uint Add(T item)
    {
        m_Array[m_Count] = item;
        m_IdtoIndex.Add(m_Id, m_Count);
        m_IndextoId.Add(m_Count, m_Id);
        m_Id++;
        m_Count++;
        return m_Id-1;
    }
    public void Remove(uint id)
    {
        int index_d;
        int index_l = m_Count - 1;
        if (m_IdtoIndex.TryGetValue(id, out index_d))
        {
            if (index_d == index_l)
            {
                m_Array[index_d] = default(T);
            }
            else
            {
                m_Array[index_d] = m_Array[index_l];
                m_Array[index_l] = default(T);
                
                uint id_l = m_IndextoId[index_l];
                m_IdtoIndex[id_l] = index_d;
                m_IndextoId[index_d] = id_l;
            }
            m_Count -= 1;
            m_IdtoIndex.Remove(id);
            m_IndextoId.Remove(index_l);
        }
    }
    public T[] GetArray(){
        return m_Array;
    }
    public void GetValue(uint id, out T value){
        value = m_Array[m_IdtoIndex[id]];
    }
    public void SetValue(uint id, T value){
        m_Array[m_IdtoIndex[id]] = value;
    }
}