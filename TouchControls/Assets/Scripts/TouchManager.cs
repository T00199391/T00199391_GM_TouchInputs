using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private Ray ourRay;
    private RaycastHit hitInfo;
    private float tapStartTime, tapTime, initialDistance = 0, newDistance = 0, starting_dis_to_sel_obj, scaler,angle;
    private const float THRESHOLDTAPTIME = 0.05f;
    private Quaternion initialRotation,rotation;
    IControlable selectedObject, obj;
    private enum Gestures { TAP, DRAG, ZOOM, ROTATE, DETERMINING, NONE };
    private Gestures currentGestures;
    private VariableManager vm;
    private CameraControl camera;
    GameObject ourCameraPlane;

    private void Start()
    {
        ourCameraPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ourCameraPlane.transform.position = new Vector3(0, Camera.main.transform.position.y, 0);
        ourCameraPlane.transform.up = (Camera.main.transform.position - ourCameraPlane.transform.position).normalized;

        currentGestures = Gestures.NONE;
        vm = FindObjectOfType<VariableManager>();
        camera = FindObjectOfType<CameraControl>();
    }

    void Update()
    {
        switch (currentGestures)
        {
            case Gestures.NONE:
                if (Input.touchCount > 0)
                {
                    //The Began phase starts here when the first touch is detected so the start time for the method IsTapped had to be moved to here
                    if (Input.touches[0].phase == TouchPhase.Began)
                    {
                        tapStartTime = Time.time;
                    }
                    currentGestures = Gestures.DETERMINING;
                }
                break;
            case Gestures.DETERMINING:
                DetermineAction();
                break;

            case Gestures.TAP:
                TapAction();
                break;

            case Gestures.DRAG:
                if(Input.touchCount == 1)
                {
                    Ray new_pos_ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
                    RaycastHit hit;
                    Physics.Raycast(new_pos_ray, out hit);
                    

                    if(selectedObject is SphereControl)
                    {
                        selectedObject.MoveTo(hit.point);
                    }
                    else
                    {
                        selectedObject.MoveTo(new_pos_ray.GetPoint(starting_dis_to_sel_obj));
                    }
                }                
                break;

            case Gestures.ZOOM:
                IsZooming();
                if(selectedObject == null)
                {
                    camera.ScaleTo(scaler);
                }
                else
                {
                    selectedObject.ScaleTo(scaler);
                }
                if (Input.touchCount <= 1)
                {
                    currentGestures = Gestures.DETERMINING;
                }
                break;
            case Gestures.ROTATE:
                IsRotating();
                if (selectedObject == null)
                {
                    camera.RotateTo(angle, initialRotation);
                }
                else
                {
                    selectedObject.RotateTo(angle, initialRotation);
                }
                if (Input.touchCount <= 1)
                {
                    currentGestures = Gestures.DETERMINING;
                }
                break;
        }

        if (Input.touchCount <= 0)
            currentGestures = Gestures.NONE;
    }

    private void DetermineAction()
    {
        if (Input.touchCount == 1)
        {
            ourRay = Camera.main.ScreenPointToRay(Input.touches[0].position);
            Debug.DrawLine(ourRay.origin, 30 * ourRay.direction);

            if (Physics.Raycast(ourRay, out hitInfo))
            {
                obj = hitInfo.transform.GetComponent<IControlable>();
                if (obj != null && IsTapped())
                {
                    currentGestures = Gestures.TAP;
                }
            }

            if(Input.touches[0].phase == TouchPhase.Moved)
            {
                currentGestures = Gestures.DRAG;
            }
        }

        if (Input.touchCount == 2)
        {
            if (Input.GetTouch(1).phase == TouchPhase.Began)
            {
                initialDistance = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                if(selectedObject == null)
                {
                    initialRotation = camera.GetRotation();
                }
                else
                {
                    initialRotation = selectedObject.GetRotation();
                }
            }

            if (vm.GetRotate())
            {
                IsRotating();
                currentGestures = Gestures.ROTATE;
            }

            if (vm.GetZoom())
            {
                IsZooming();
                currentGestures = Gestures.ZOOM;
            }
        }
    }

    private void TapAction()
    {
        if(selectedObject == null)
        {
            selectedObject = obj;
            selectedObject.Youve_Been_Selected();
        }
        else
        {
            if(selectedObject == obj)
            {
                selectedObject.Youve_Been_Deselected();
                selectedObject = null;
            }
            else
            {
                selectedObject.Youve_Been_Deselected();
                selectedObject = obj;
                selectedObject.Youve_Been_Selected();
            }
        }
        if(obj != null)
        {
            starting_dis_to_sel_obj = Vector3.Distance(Camera.main.transform.position, hitInfo.transform.position);
        }
    }

    private bool IsTapped()
    {
        tapTime = 0;

        if (Input.touches[0].phase == TouchPhase.Began)
        {
            tapStartTime = Time.time;
        }

        if (Input.touches[0].phase == TouchPhase.Ended)
        {
            tapTime = Time.time - tapStartTime;
        }

        if (tapTime <= 0.1f && tapTime != 0 && Input.touches[0].phase != TouchPhase.Moved)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void IsZooming()
    {
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
                newDistance = Vector3.Distance(touch0.position, touch1.position);

                if ((initialDistance > newDistance || initialDistance < newDistance) && newDistance > 0)
                {
                    scaler = initialDistance / newDistance;
                }
            }
        }
    }

    private void IsRotating()
    {
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector3 v = touch1.position - touch0.position;

            if (touch0.phase != TouchPhase.Stationary && touch1.phase != TouchPhase.Stationary)
                angle = Mathf.Rad2Deg * Mathf.Atan2(v.y, v.x);
        }
    }
}
