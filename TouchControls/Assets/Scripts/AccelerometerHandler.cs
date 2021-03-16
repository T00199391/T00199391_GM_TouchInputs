using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerHandler : MonoBehaviour
{
    private float speed = 5.0f;
    private VariableManager vm;

    void Start()
    {
        vm = FindObjectOfType<VariableManager>();
    }

    void Update()
    {
        if (vm.GetAccel())
        {
            Vector3 dir = Vector3.zero;

            dir.x = Input.acceleration.x;
            dir.z = Input.acceleration.y;

            if (dir.sqrMagnitude > 1)
                dir.Normalize();

            dir *= Time.deltaTime;

            transform.Translate(dir * speed);
        }
    }
}
