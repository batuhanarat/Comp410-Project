using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{

  [SerializeField] private Transform boxParentTransform;
  private Vector3 _rotationAxis = Vector3.up;
  private float _rotationAngle = 90f;
  public void RotateRight()
  {
    boxParentTransform.Rotate(_rotationAxis,_rotationAngle ,Space.Self);
  }
  public void RotateLeft()
  {
    boxParentTransform.Rotate(_rotationAxis,-_rotationAngle ,Space.Self);

  }
}
