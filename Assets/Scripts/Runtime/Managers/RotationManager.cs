using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{

    [SerializeField] private Transform boxParentTransform;
    private Vector3 _rotationAxis = Vector3.up;
    private float _rotationAngle = 90f;
    private float _currentRotation = 0f;
    private float _rotationTime = 1f;
    private short _rotationDirection = 1; // 1 if right, -1 if left
    private float _currentRotationTime = 0f; // between 0 and _rotationTime
    private bool _isRotating = false;
 
    public void RotateRight()
    {
        //boxParentTransform.Rotate(_rotationAxis,_rotationAngle ,Space.Self);
        if (_isRotating) return;
        _isRotating = true;
        _rotationDirection = 1;
    }
    public void RotateLeft()
    {
        //boxParentTransform.Rotate(_rotationAxis,-_rotationAngle ,Space.Self);

        if (_isRotating) return;
        _isRotating = true;
        _rotationDirection = -1;

    }

    private void Update()
    {
        if (!_isRotating) return;

        if (_currentRotationTime >= _rotationTime)
        {
            FinishRotation();
        }
    
        float rotationAngle = Mathf.Lerp(_currentRotation, _rotationAngle * _rotationDirection, Time.deltaTime);
        boxParentTransform.Rotate(_rotationAxis,rotationAngle ,Space.Self);

        _currentRotationTime += Time.deltaTime;
    


    }

    private void FinishRotation()
    {
        _isRotating = false;
        _currentRotationTime = 0;
        _currentRotation = 0f;
    }
}