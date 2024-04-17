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

    // �����ٶ�ʸ���ͳ�ʼλ��
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

    // ���巽��
    private void ThrowFall(float t)
    {
        transform.Translate(v * t);
        v += G * t;
    }

    private void GetPosByTime()
    {
        var pos = transform.position;
        pos += 0.5f * G * time * time;
        print($"{time}���λ��Ϊ{pos}");
        
        var tar = GameObject.Instantiate(this);
        tar.GetComponent<Throw>().enabled = false;
        tar.transform.position = pos;
    }
}
