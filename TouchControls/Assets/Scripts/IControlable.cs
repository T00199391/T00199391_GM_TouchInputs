using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IControlable
{
    void Youve_Been_Selected();
    void Youve_Been_Deselected();
    void MoveTo(Vector3 dis);
    void ScaleTo(float scaler);
    void RotateTo(float angle, Quaternion initialRotation);
    Quaternion GetRotation();
    void Reset();
}