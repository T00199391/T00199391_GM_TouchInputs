using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VariableManager : MonoBehaviour
{
    private bool zoom = false, rotate = false, gyro = false, accel = false;
    private List<IControlable> objects;

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

    public void SetZoom()
    {
        if (zoom == false)
        {
            zoom = true;
            rotate = false;
        }
    }

    public void SetRotate()
    {
        if (rotate == false)
        {
            rotate = true;
            zoom = false;
        }
    }

    public void SetGyro()
    {
        gyro = !gyro;
    }

    public void SetAccel()
    {
        accel = !accel;
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
