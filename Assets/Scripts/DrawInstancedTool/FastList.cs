using System;
using System.Collections.Generic;

public class FastList<T>
{
    T[] m_Array;
    Dictionary<uint, int> m_IdtoIndex;
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
    }

    public uint Add(T item)
    {
        m_Array[m_Count] = item;
        m_Id += 1;
        m_IdtoIndex.Add(m_Id, m_Count);
        m_Count += 1;
        return m_Id;
    }
    public void Remove(uint id)
    {
        int index;
        if (m_IdtoIndex.TryGetValue(id, out index))
        {
            if (index == m_Count - 1)
            {
                m_Array[m_Count - 1] = default(T);
                m_Count = 0;
            }
            else
            {
                m_Array[index] = m_Array[m_Count - 1];
                m_Array[m_Count - 1] = default(T);
                m_Count -= 1;
            }
            m_IdtoIndex.Remove(id);
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