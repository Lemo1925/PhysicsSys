using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static event Action ResetEvent;
    float time, rigidTime;
    private void OnEnable() => ResetEvent += SetTimer;

    private void Awake() => Time.timeScale = 0;

    private void Start() => ResetEvent?.Invoke();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Time.timeScale = 1;

        if (Input.GetKeyDown(KeyCode.R)) ResetEvent?.Invoke();

        RigidTimer();
    }

    void FixedUpdate()
    {
        time -= Time.fixedDeltaTime;
        if (time < 0.01f * Time.fixedDeltaTime)
        {
            Time.timeScale = 0;
            print($"��ʱ������{time}, ��ǰλ�ã�{Throw.pos}, ��׼λ�ã�{Throw.staP}, ����λ�ã�{Throw.posR}");
        }
    }

    private void OnDisable() => ResetEvent -= SetTimer;

    private void SetTimer()
    {
        time = GameObject.Find("Sphere").GetComponent<Throw>().time;
        rigidTime = time - 0.01f;
    }

    private void RigidTimer()
    {
        rigidTime -= Time.deltaTime;
        if (rigidTime < 0.01f * Time.deltaTime)
        {
            GameObject.Find("Standard").GetComponent<Rigidbody>().isKinematic = true;
            print($"��ʱ������{time}, ��ǰλ�ã�{Throw.pos}, ��׼λ�ã�{Throw.staP}, ����λ�ã�{Throw.posR}");
        }
    }
}
