using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Entity;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.CustomInputAction
{
    /// <summary>
    /// 일반 NewInputSystem은 Pressed 콜백을 지원하지 않아서 이를 구현하기 위해 랩핑한 클래스 
    /// </summary>
    public class AdvancedInputAction
    {
        public InputAction inputAction;

        public InputActionLog startedLog;
        public InputActionLog performedLog;
        public InputActionLog cancledLog;

        private Action<InputAction> pressedActions;
        private MonoBehaviour _owner;

        private Coroutine pressedRoutine;

        public AdvancedInputAction(InputAction inputAction, MonoBehaviour owner)
        {
            this.inputAction = inputAction;
            this.inputAction.started += StartedLog;
            this.inputAction.performed += PerformedLog;
            this.inputAction.canceled += CancledLog;
            this._owner = owner;

            startedLog = new InputActionLog(0, 0, "", "");
            performedLog = new InputActionLog(0, 0, "", "");
            cancledLog = new InputActionLog(0, 0, "", "");
        }

        private void StartedLog(InputAction.CallbackContext context)
        {
            startedLog.LogUpdate(context);
        }

        private void PerformedLog(InputAction.CallbackContext context)
        {
            performedLog.LogUpdate(context);
        }

        private void CancledLog(InputAction.CallbackContext context)
        {
            cancledLog.LogUpdate(context);
        }

        public void BindPressedAction(Action<InputAction> action)
        {
            pressedActions += action;
            if (pressedRoutine == null)
            {
                pressedRoutine = _owner.StartCoroutine(CO_PressedRoutine());
            }
        }

        public void RemoveBindingPressedAction(Action<InputAction> action)
        {
            pressedActions -= action;
        }

        private IEnumerator CO_PressedRoutine()
        {
            while (true)
            {
                if (inputAction.IsPressed() && pressedActions != null)
                {
                    pressedActions.Invoke(inputAction);
                }

                if (pressedActions == null || pressedActions.GetInvocationList().Length == 0)
                {
                    break;
                }
                yield return null;
            }

            pressedRoutine = null;
        }
    }

    public struct InputActionLog
    {
        public float beforeTime;
        public float recentlyTime;
        public object beforeContext;
        public object recentlyContext;

        public InputActionLog(float beforeTime, float recentlyTime, object beforeContext, object recentlyContext)
        {
            this.beforeTime = beforeTime;
            this.recentlyTime = recentlyTime;
            this.beforeContext = beforeContext;
            this.recentlyContext = recentlyContext;
        }

        public void LogUpdate(InputAction.CallbackContext context)
        {
            beforeTime = recentlyTime;
            recentlyTime = Time.time;
            beforeContext = recentlyContext;
            recentlyContext = context.ReadValueAsObject();
        }
    }
}