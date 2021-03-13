using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereControl : MonoBehaviour, IControlable
{
    Renderer ourRenderer = new Renderer();
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Rigidbody rigid;
    private VariableManager vm;
    private Vector3 drag_position;

    void Start()
    {
        //reset variables
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        ourRenderer = GetComponent<Renderer>();
        ourRenderer.material.color = Color.white;

        vm = FindObjectOfType<VariableManager>();
        rigid = GetComponent<Rigidbody>();

        drag_position = transform.position;
    }

    void Update()
    {
        if (vm.GetAccel())
        {
            Vector3 tilt = Input.acceleration;

            tilt = Quaternion.Euler(90, 0, 0) * tilt;

            rigid.AddForce(tilt);
        }

        transform.position = Vector3.Lerp(transform.position, drag_position, 0.05f);
    }

    public void Youve_Been_Selected()
    {
        ourRenderer.material.color = Color.red;
    }

    public void Youve_Been_Deselected()
    {
        ourRenderer.material.color = Color.white;
    }

    public void MoveTo(Vector3 dis)
    {
        drag_position = dis;
    }

    public void ScaleTo(float scaler)
    {
        if (scaler != 0)
        {
            if (scaler < 1)
                transform.localScale += Vector3.one * Time.deltaTime;
            else
                transform.localScale -= Vector3.one * Time.deltaTime;
        }
    }

    public void RotateTo(float angle, Quaternion initialRotation)
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, Camera.main.transform.forward);
        transform.rotation = rotation * transform.rotation;
    }

    public Quaternion GetRotation()
    {
        return transform.rotation;
    }

    public void Reset()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}
