using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawInstancedTest : MonoBehaviour
{
    public Mesh mesh;
    public Material material;
    private List<List<uint>> m_RenderList = new List<List<uint>>();
    private List<DrawInstancedTask> m_RenderTask = new List<DrawInstancedTask>();
    private void Awake()
    {
        DrawInstancedTool.Init();
    }
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            m_RenderTask.Add(DrawInstancedTool.AddTask("cube"+i, mesh, material));
            m_RenderList.Add(new List<uint>());
        }
        for (int i = 0; i < m_RenderTask.Count; i++)
        {
            for (int j = 0; j < 1023; j++)
            {
                uint id = m_RenderTask[i].Add(Random.insideUnitSphere*5, Vector3.zero, Vector3.one);
                m_RenderList[i].Add(id);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < m_RenderTask.Count; i++)
        {
            for (int j = 0; j < m_RenderList[i].Count; j++)
            {
                m_RenderTask[i].SetPos(m_RenderList[i][j],Random.insideUnitSphere*5);
            }
        }
    }

    private void OnDestroy()
    {
        DrawInstancedTool.RemoveAllTask();
    }
}
