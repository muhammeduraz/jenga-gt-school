using System;
using UnityEngine;
using DG.Tweening;
using Assets.Scripts.Inputs;
using Assets.Scripts.Stacks;

namespace Assets.Scripts.Cameras
{
    public class CameraHandler : MonoBehaviour, IDisposable
    {
        #region Variables

        public float rotateSpeed;
        public float rotationLerpSpeed;

        public float zoomSpeed;
        public float zoomLerpSpeed;

        public float minDistance;
        public float maxDistance;

        public Vector3 stackMovementOffset;

        private float _currentDistance;
        private Vector3 _targetRotation;
        private Vector3 _currentRotation;

        public Transform center;
        public InputHandler _inputHandler;

        private Tween _movementTween;

        #endregion Variables

        #region Properties

        private Vector3 CenterDirection { get => (transform.position - center.position).normalized; }

        #endregion Properties

        #region Unity Functions

        private void Awake()
        {
            Initialize();
        }

        private void OnDisable()
        {
            Dispose();
        }

        #endregion Unity Functions

        #region Functions

        private void Initialize()
        {
            _currentDistance = Vector3.Distance(transform.position, center.position);
            _currentDistance = Mathf.Clamp(_currentDistance, minDistance, maxDistance);

            SubscribeEvents(true);
        }

        public void Dispose()
        {
            SubscribeEvents(false);
        }

        private void ZoomInOut(InputData inputData)
        {
            _currentDistance += inputData.mouseScrollDelta.y * zoomSpeed;
            _currentDistance = Mathf.Clamp(_currentDistance, minDistance, maxDistance);

            transform.position = Vector3.Lerp(transform.position, center.position + CenterDirection * _currentDistance, zoomLerpSpeed * Time.deltaTime);
        }

        private void RotateCamera(InputData inputData)
        {
            _targetRotation = inputData.ScreenDelta * rotateSpeed;
            _currentRotation = Vector3.Lerp(_currentRotation, _targetRotation, rotationLerpSpeed * Time.deltaTime);

            center.Rotate(0f, -_currentRotation.x * rotateSpeed, 0f, Space.World);
            center.Rotate(_currentRotation.y * rotateSpeed, 0f, 0f, Space.Self);
        }

        private void SubscribeEvents(bool subscribe)
        {
            if (subscribe)
            {
                _inputHandler.OnLeftFinger += RotateCamera;
                _inputHandler.OnLeftFingerUpdate += ZoomInOut;
            }
            else if (!subscribe)
            {
                _inputHandler.OnLeftFinger -= RotateCamera;
                _inputHandler.OnLeftFingerUpdate -= ZoomInOut;
            }
        }

        public void MoveToStack(StackHandler stackHandler)
        {
            _movementTween?.Kill();
            _movementTween = center.DOMove(stackHandler.transform.position + stackMovementOffset, 1f);
            //_movementTween = center.DOMove(stackHandler.transform.position, 1f).SetUpdate(UpdateType.Late);
        }

        #endregion Functions
    }
}