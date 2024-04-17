using UnityEngine;

public class Throw : MonoBehaviour
{
    // �����ʼ�ļ��ٶ�
    public float volacity;
    // ���ٶȷ���
    public Vector3 direction;
    // �̶�ʱ��
    public float time;

    // �������ٶ�ʸ��
    Vector3 G { get;  } = new Vector3(0, -9.81f, 0);
    Vector3 v, positon;

    GameObject standard;

    public static Vector3 pos;
    public static Vector3 staP;

    private void OnEnable()
    {
        Timer.ResetEvent += SetSpeed;
        Timer.ResetEvent += SetPos;
    }

    private void Awake()
    {
        positon = transform.position;
        standard = GameObject.Find("Standard");
    }

    private void Start() => GetPosByTime();

    void FixedUpdate()
    {
        ThrowFall(Time.fixedDeltaTime);
        if(!Mathf.Approximately(transform.position.y , standard.transform.position.y))
        {
            print($"Ŀ�굱ǰλ��{transform.position}");
            print($"��׼λ��{standard.transform.position}");
        }

        pos = transform.position;
        staP = standard.transform.position;
    }

    private void OnDisable()
    {
        Timer.ResetEvent -= SetSpeed;
        Timer.ResetEvent -= SetPos;
    }

    // �����ٶ�ʸ���ͳ�ʼλ��
    private void SetSpeed() => v = volacity * direction.normalized;
    private void SetPos() => transform.position = positon;

    // ���巽��
    private void ThrowFall(float t)
    {
        transform.Translate(v * t);
        v += G * t;
    }

    private void GetPosByTime()
    {
        var pos = transform.position;
        pos += v * time + 0.5f * G * time * time;
        print($"{time}���λ��Ϊ{pos}");
        
        GameObject.Find("Target").transform.position = pos;
    }
}
