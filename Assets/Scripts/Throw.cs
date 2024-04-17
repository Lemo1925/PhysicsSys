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
    Vector3 v, positon;

    // 设置速度矢量和初始位置
    private void SetSpeed() => v = volacity * direction.normalized;
    private void SetPos() => transform.position = positon;

    private void OnEnable()
    {
        Timer.ResetEvent += SetSpeed;
        Timer.ResetEvent += SetPos;
    }

    private void Awake() => positon = transform.position;

    private void Start() => GetPosByTime();

    void FixedUpdate() => ThrowFall(Time.fixedDeltaTime);

    private void OnDisable()
    {
        Timer.ResetEvent -= SetSpeed;
        Timer.ResetEvent -= SetPos;
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
        pos += 0.5f * G * time * time;
        print($"{time}秒后，位置为{pos}");
        
        var tar = GameObject.Instantiate(this);
        tar.GetComponent<Throw>().enabled = false;
        tar.transform.position = pos;
    }
}
