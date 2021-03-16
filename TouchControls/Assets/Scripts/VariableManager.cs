using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VariableManager : MonoBehaviour
{
    private bool zoom = false, rotate = false, gyro = false, accel = false;
    private List<IControlable> objects;
    public Button zoomBtn,rotateBtn,gyroBtn,accelBtn;

    private void Start()
    {
        //https://answers.unity.com/questions/863509/how-can-i-find-all-objects-that-have-a-script-that.html
        objects = new List<IControlable>();
        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var rootGameObject in rootGameObjects)
        {
            IControlable[] childrenInterfaces = rootGameObject.GetComponentsInChildren<IControlable>();
            foreach (var childInterface in childrenInterfaces)
            {
                objects.Add(childInterface);
            }
        }
    }

    void Update()
    {
        if(zoom)
            zoomBtn.GetComponent<Image>().color = Color.blue;
        else
            zoomBtn.GetComponent<Image>().color = Color.white;

        if(rotate)
            rotateBtn.GetComponent<Image>().color = Color.blue;
        else
            rotateBtn.GetComponent<Image>().color = Color.white;

        if(gyro)
            gyroBtn.GetComponent<Image>().color = Color.blue;
        else
            gyroBtn.GetComponent<Image>().color = Color.white;

        if (accel)
            accelBtn.GetComponent<Image>().color = Color.blue;
        else
            accelBtn.GetComponent<Image>().color = Color.white;
    }

    public void SetZoom()
    {
        if (zoom == false)
        {
            zoom = true;
            rotate = false;
        }
        else
        {
            zoom = false;
        }
    }

    public void SetRotate()
    {
        if (rotate == false)
        {
            rotate = true;
            zoom = false;
        }
        else
        {
            rotate = false;
        }
    }

    public void SetGyro()
    {
        if (gyro == false)
        {
            gyro = true;
            accel = false;
        }
        else
        {
            gyro = false;
        }
    }

    public void SetAccel()
    {
        if (accel == false)
        {
            accel = true;
            gyro = false;
        }
        else
        {
            accel = false;
        }
    }

    public bool GetZoom()
    {
        return zoom;
    }

    public bool GetRotate()
    {
        return rotate;
    }

    public bool GetGyro()
    {
        return gyro;
    }

    public bool GetAccel()
    {
        return accel;
    }

    public void ResetVariables()
    {
        foreach(IControlable o in objects)
        {
            o.Reset();
        }
    }
}
