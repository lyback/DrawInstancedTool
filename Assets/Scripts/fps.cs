using UnityEngine;
using UnityEngine.SceneManagement;

public class fps : MonoBehaviour {

    //更新的时间间隔
    private float UpdateInterval = 0.5F;
    //最后的时间间隔
    private float _lastInterval;
    //帧[中间变量 辅助]
    private int _frames = 0;
    //当前的帧率
    private float _fps;

    void Start()
    {
        DontDestroyOnLoad(this);
        UpdateInterval = Time.realtimeSinceStartup;
        _frames = 0;
    }
    void OnGUI()
    {
        if (GUI.Button(new Rect(100, 300, 200, 200), "GPU_Instancing"))
        {
            SceneManager.LoadScene("Draw");
        }
        if (GUI.Button(new Rect(100, 800, 200, 200), "GameObject_Instancing"))
        {
            SceneManager.LoadScene("Instanced");
        }
        GUIStyle style = new GUIStyle(); //实例化一个新的GUIStyle，名称为style ，后期使用
        style.fontSize = 50; //字体的大小设置数值越大，字越大，默认颜色为黑色 
        style.normal.textColor=new Color(1,1,1); //设置文本的颜色为 新的颜色(0,0,0)修改值-代表不同的颜色,值为整数 我个人觉得有点像RGB的感觉
        GUI.Label(new Rect(200, 200, 300, 300), "FPS:" + _fps.ToString("f2"), style);
    }
    void Update()
    {
        ++_frames;
        if (Time.realtimeSinceStartup > _lastInterval + UpdateInterval)
        {
            _fps = _frames / (Time.realtimeSinceStartup - _lastInterval);
            _frames = 0;
            _lastInterval = Time.realtimeSinceStartup;
        }
    }

}
