using System;
using System.Collections.Generic;
using Runtime.Data.UnityObjects;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

         private bool _isAvailableForTouch, _isFirstTimeTouchTaken, _isTouching;
         
        private Vector2? _mousePosition;

        #endregion

        #endregion

        private void Awake()
        {
            _isAvailableForTouch = false;
            
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onRestartLevel += OnEnableInput;
            InputSignals.Instance.onEnableInput += OnEnableInput;
            InputSignals.Instance.onDisableInput += OnDisableInput;
        }

        private void OnDisableInput()
        {
            _isAvailableForTouch = false;
        }

        private void OnEnableInput()
        {
            _isAvailableForTouch = true;
        }

        private void OnReset()
        {
            _isAvailableForTouch = false;
            //_isFirstTimeTouchTaken = false;
            _isTouching = false;
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onRestartLevel -= OnEnableInput;

            InputSignals.Instance.onEnableInput -= OnEnableInput;
            InputSignals.Instance.onDisableInput -= OnDisableInput;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void Update()
        {
            if (!_isAvailableForTouch) return;


            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    ObjectType myEnum;
                    string tag = hit.collider.gameObject.tag;
                    if (Enum.TryParse(tag, out myEnum))
                    {
                        Debug.Log("Tag as enum: " + myEnum);
                        Debug.Log(myEnum.GetType());

                        BagSignals.Instance.onItemSelected?.Invoke(myEnum);
                        //Debug.Log("Tag as enum: " + myEnum);
                    }
                     Destroy(hit.collider.gameObject); 

                   
                }
                    
                
            }
        }

    }
}