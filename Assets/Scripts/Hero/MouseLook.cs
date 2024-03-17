using System;
using Unity.Mathematics;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
   [SerializeField] 
   private Transform _head;
   
   [SerializeField] 
   private Transform _cameraPoint;

   [SerializeField] 
   private Rigidbody _rigidbody;

   [SerializeField] 
   private float _mouseSensitivity = 2f;

   [SerializeField] 
   private float _minHeadAngle = -90f;
   
   [SerializeField] 
   private float _maxHeadAngle = 90f;

   private float _rotateY;
   private float _rotateX;

   private void Start()
   {
      if (Camera.main != null)
      {
         var transformCamera = Camera.main.transform;
         transformCamera.parent = _cameraPoint;
         transformCamera.localPosition = Vector3.zero;
         transformCamera.localRotation = quaternion.identity;
      }
      else
      {
         throw new NullReferenceException("Камера не найдена");
      }
   }

   public void RotateX(float value)
   {
      _rotateX = Math.Clamp(_rotateX + value * _mouseSensitivity, _minHeadAngle, _maxHeadAngle);
      _head.localEulerAngles = new Vector3(_rotateX, 0, 0);
   }
   
   public void RotateY(float value)
   {
      _rotateY += value * _mouseSensitivity;
   }

   private void FixedUpdate()
   {
      _rigidbody.angularVelocity = new Vector3(0, _rotateY, 0);
      _rotateY = 0;
   }
}
