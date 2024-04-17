using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp : MonoBehaviour
{
    public Vector3 a, b;
    public float t = 1;
   
    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(Vector3.zero, a, Color.yellow);
        Debug.DrawLine(Vector3.zero, b, Color.green);
        for (float i = 0.01f; i <= 1; i += 0.01f)
        {
            Debug.DrawLine(Vector3.zero, Vector3.SlerpUnclamped(a, b, t * i), Color.blue);
        }
        
    }
}
