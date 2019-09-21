using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstancedTest : MonoBehaviour {
	public GameObject obj;
	private List<Transform> m_RenderList = new List<Transform>();
	// Use this for initialization
	void Start () {
		for (int i = 0; i < 5000; i++)
		{
            var res = GameObject.Instantiate(obj);
            res.transform.position = Random.insideUnitSphere*5;
            m_RenderList.Add(res.transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < m_RenderList.Count; i++)
		{
			m_RenderList[i].position = Random.insideUnitSphere*5;
		}
	}
}
