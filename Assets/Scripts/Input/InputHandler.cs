using System;
using UnityEngine;

namespace Assets.GameAssets.Scripts
{
    public class InputHandler : MonoBehaviour, IDisposable
    {
        #region Events

        //public Action<InputData> OnLeftFingerDown;
        public Action<InputData> OnLeftFinger;
        public Action<InputData> OnLeftFingerUpdate;
        //public Action<InputData> OnLeftFingerUp;

        //public Action<InputData> OnRightFingerDown;
        //public Action<InputData> OnRightFinger;
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

        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            UpdateInput();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        #endregion Unity Functions

        #region Functions

        private void Initialize()
        {

        }

        public void Dispose()
        {

        }

        private void UpdateInput()
        {
            _inputData.screenPosition = Input.mousePosition;
            _inputData.mouseScrollDelta = Input.mouseScrollDelta;

            //if (Input.GetMouseButtonDown(0))
            //{
            //    OnLeftFingerDown?.Invoke(_inputData);
            //}
            if (Input.GetMouseButton(0))
            {
                OnLeftFinger?.Invoke(_inputData);
            }
            //else if (Input.GetMouseButtonUp(0))
            //{
            //    OnLeftFingerUp?.Invoke(_inputData);
            //}

            //if (Input.GetMouseButtonDown(1))
            //{
            //    OnRightFingerDown?.Invoke(_inputData);
            //}
            //else if (Input.GetMouseButton(1))
            //{
            //    OnRightFinger?.Invoke(_inputData);
            //}
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