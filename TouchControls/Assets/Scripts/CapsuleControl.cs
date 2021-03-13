using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleControl : MonoBehaviour, IControlable
{
    Renderer ourRenderer = new Renderer();
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 drag_position;

    void Start()
    {
        //reset variables
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        ourRenderer = GetComponent<Renderer>();
        ourRenderer.material.color = Color.white;

        drag_position = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, drag_position, 0.05f);
    }

    public void Youve_Been_Selected()
    {
        ourRenderer.material.color = Color.cyan;
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
