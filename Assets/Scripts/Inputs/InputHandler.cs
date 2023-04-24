using System;
using UnityEngine;

namespace Assets.Scripts.Inputs
{
    public class InputHandler : MonoBehaviour
    {
        #region Events

        public Action<InputData> OnLeftFinger;
        public Action<InputData> OnLeftFingerUpdate;

        public Action<InputData> OnRightFingerUp;

        #endregion Events

        #region Variables

        private bool _enabled;

        private InputData _inputData;

        #endregion Variables

        #region Properties

        public bool Enabled { get => _enabled; set { _enabled = value; enabled = value; } }

        public Vector2 MouseScrollDelta { get => Input.mouseScrollDelta; }

        #endregion Properties

        #region Unity Functions

        private void Update()
        {
            UpdateInput();
        }

        #endregion Unity Functions

        #region Functions

        private void UpdateInput()
        {
            _inputData.screenPosition = Input.mousePosition;
            _inputData.mouseScrollDelta = Input.mouseScrollDelta;

            if (Input.GetMouseButton(0))
            {
                OnLeftFinger?.Invoke(_inputData);
            }

            if (Input.GetMouseButtonUp(1))
            {
                OnRightFingerUp?.Invoke(_inputData);
            }

            OnLeftFingerUpdate?.Invoke(_inputData);

            _inputData.previousScreenPosition = Input.mousePosition;
        }

        #endregion Functions
    }

    public struct InputData
    {
        public Vector3 screenPosition;
        public Vector3 previousScreenPosition;
        public Vector2 mouseScrollDelta;

        public Vector3 ScreenDelta { get => previousScreenPosition - screenPosition; }
    }
}