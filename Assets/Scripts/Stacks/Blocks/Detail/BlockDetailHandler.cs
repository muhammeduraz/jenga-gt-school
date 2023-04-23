using System;
using UnityEngine;
using Assets.Scripts.Stacks;
using Assets.Scripts.Inputs;
using Assets.Scripts.Raycasts;
using Assets.Scripts.Stacks.Blocks;

namespace Assets.Scripts.Blocks.Detail
{
    public class BlockDetailHandler : MonoBehaviour, IDisposable
    {
        #region Variables

        private Camera _camera;
        private RaycastOperations _raycastOperations;

        private BlockHandler _currentBlockHandler;

        public InputHandler _inputHandler;

        [SerializeField] private BlockDetailPanel _blockDetailPanel;

        #endregion Variables

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
            _camera = Camera.main;
            _raycastOperations = new RaycastOperations();

            SubscribeEvents(true);
        }

        public void Dispose()
        {
            SubscribeEvents(false);
        }

        private void SubscribeEvents(bool subscribe)
        {
            if (subscribe)
            {
                _inputHandler.OnRightFingerUp += Appear;
                _inputHandler.OnRightFingerUp += Disappear;
            }
            else if (!subscribe)
            {
                _inputHandler.OnRightFingerUp -= Appear;
                _inputHandler.OnRightFingerUp -= Disappear;
            }
        }

        private void Appear(InputData inputData)
        {
            BlockHandler blockHandler = _raycastOperations.GetObjectOfType<BlockHandler>(inputData.screenPosition);

            if (blockHandler == null) return;
            if (blockHandler == _currentBlockHandler) _blockDetailPanel.Disappear();

            Vector3 position = blockHandler.transform.position + (_camera.transform.position - blockHandler.transform.position).normalized * 1f;
            Quaternion rotation = Quaternion.LookRotation((position - _camera.transform.position).normalized);

            _blockDetailPanel.Appear(blockHandler.Block, position, rotation);
        }

        private void Disappear(InputData inputData)
        {
            _currentBlockHandler = _raycastOperations.GetObjectOfType<BlockHandler>(inputData.screenPosition);

            if (_currentBlockHandler != null) return;

            _blockDetailPanel.Disappear();
        }

        #endregion Functions
    }
}