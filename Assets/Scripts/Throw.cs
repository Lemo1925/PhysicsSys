using UnityEngine;

public class Throw : MonoBehaviour
{
    // 物体初始的加速度
    public float volacity;
    // 加速度方向
    public Vector3 direction;
    // 固定时间
    public float time;

    // 重力加速度矢量
    Vector3 G { get;  } = new Vector3(0, -9.81f, 0);
    Vector3 v, positon, standardPos;

    GameObject standard;

    public static Vector3 pos;
    public static Vector3 staP;
    public static Vector3 posR;

    private void OnEnable()
    {
        Timer.ResetEvent += SetSpeed;
        Timer.ResetEvent += SetPos;
    }

    private void Awake()
    {
        standard = GameObject.Find("Standard");
        positon = transform.position;
        standardPos = standard.transform.position;
    }

    void FixedUpdate()
    {
        ThrowFall(Time.fixedDeltaTime);
        if(!Mathf.Approximately(transform.position.y , standard.transform.position.y))
        {
            print($"目标当前位置{transform.position}");
            print($"标准位置{standard.transform.position}");
        }

        pos = transform.position;
        staP = standard.transform.position;
    }

    private void OnDisable()
    {
        Timer.ResetEvent -= SetSpeed;
        Timer.ResetEvent -= SetPos;
    }

    // 设置速度矢量和初始位置
    private void SetSpeed() => v = volacity * direction.normalized;

    private void SetPos()
    {
        transform.position = positon;
        standard.transform.position = standardPos;
        standard.GetComponent<Rigidbody>().isKinematic = false;
        GetPosByTime();
    }

    // 抛体方程
    private void ThrowFall(float t)
    {
        transform.Translate(v * t);
        v += G * t;
    }

    private void GetPosByTime()
    {
        var pos = transform.position;
        var t = time - 0.01f;
        pos += v * time + 0.5f * G * Mathf.Pow(t, 2);
        print($"{time}秒后，位置为{pos}");
        posR = pos;
        GameObject.Find("Target").transform.position = pos;
    }
}
