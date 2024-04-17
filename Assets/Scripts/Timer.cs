using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static event Action ResetEvent;
    float time;


    private void OnEnable() => ResetEvent += SetTimer;

    private void Awake() => Time.timeScale = 0;

    private void Start() => ResetEvent?.Invoke();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Time.timeScale = 1;

        if (Input.GetKeyDown(KeyCode.R)) ResetEvent?.Invoke();

    }

    void FixedUpdate()
    {
        time -= Time.fixedDeltaTime;
        if (time < 0.01f * Time.fixedDeltaTime)
        {   
            Time.timeScale = 0;
            print($"��ʱ������{time}, ��ǰλ�ã�{Throw.pos}, ��׼λ�ã�{Throw.staP}");
        }
    }

    private void OnDisable() => ResetEvent -= SetTimer;

    private void SetTimer() => time = GameObject.Find("Sphere").GetComponent<Throw>().time;
}
