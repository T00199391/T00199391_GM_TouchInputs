using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour,IControlable
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private VariableManager vm;
    public GameObject panel;

    private Vector3 drag_position;

    void Start()
    {
        //reset variables
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        vm = FindObjectOfType<VariableManager>();
    }

    void Update()
    {
        Input.gyro.enabled = vm.GetGyro();
        if (Input.gyro.enabled)
        {
            transform.Rotate(new Vector3(-Input.gyro.rotationRateUnbiased.x, -Input.gyro.rotationRateUnbiased.y, -Input.gyro.rotationRateUnbiased.z), Space.Self);
        }
    }

    public Quaternion GetRotation()
    {
        return transform.rotation;
    }

    public void MoveTo(Vector3 dis)
    {
        if(Input.touchCount == 1)
        {
            if (Input.touches[0].position.x < 1000)
                transform.position += Vector3.left * 5 * Time.deltaTime;
            else if (Input.touches[0].position.x >= 1000)
                transform.position += Vector3.right * 5 * Time.deltaTime;
            if (Input.touches[0].position.y < 500)
                transform.position += -Vector3.up * 5 * Time.deltaTime;
            else if (Input.touches[0].position.y >= 500)
                transform.position += Vector3.up * 5 * Time.deltaTime;
        }
    }

    public void Reset()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }

    public void RotateTo(float angle, Quaternion initialRotation)
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, -Camera.main.transform.forward);
        transform.rotation = rotation * initialRotation;
    }

    public void ScaleTo(float scaler)
    {
        if (scaler != 0)
        {
            if (scaler < 1)
                transform.Translate(Vector3.forward * 0.1f);
            else
                transform.Translate(Vector3.back * 0.1f);
        }
    }

    public void Youve_Been_Deselected()
    {
        panel.SetActive(false);
    }

    public void Youve_Been_Selected()
    {
        panel.SetActive(true);
    }
}
