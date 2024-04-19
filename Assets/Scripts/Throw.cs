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

    private void SetPos()
    {
        transform.position = positon;
        standard.transform.position = standardPos;
        standard.GetComponent<Rigidbody>().isKinematic = false;
        GetPosByTime();
    }

    // ���巽��
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
        print($"{time}���λ��Ϊ{pos}");
        posR = pos;
        GameObject.Find("Target").transform.position = pos;
    }
}
