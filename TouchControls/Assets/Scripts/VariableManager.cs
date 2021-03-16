using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VariableManager : MonoBehaviour
{
    private bool zoom = false, rotate = false, gyro = false, accel = false;
    private List<IControlable> objects;
    public Button zoomBtn,rotateBtn,gyroBtn;

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
            zoomBtn.GetComponent<Image>().color = Color.blue;
        }
        else
        {
            zoom = false;
            zoomBtn.GetComponent<Image>().color = Color.white;
        }
    }

    public void SetRotate()
    {
        if (rotate == false)
        {
            rotate = true;
            zoom = false;
            rotateBtn.GetComponent<Image>().color = Color.blue;
        }
        else
        {
            rotate = false;
            rotateBtn.GetComponent<Image>().color = Color.white;
        }
    }

    public void SetGyro()
    {
        if (gyro == false)
        {
            gyro = true;
            gyroBtn.GetComponent<Image>().color = Color.blue;
        }
        else
        {
            gyro = false;
            gyroBtn.GetComponent<Image>().color = Color.white;
        }
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
