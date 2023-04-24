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
            }
            else if (!subscribe)
            {
                _inputHandler.OnRightFingerUp -= Appear;
            }
        }

        private void Appear(InputData inputData)
        {
            BlockHandler blockHandler = _raycastOperations.GetObjectOfType<BlockHandler>(inputData.screenPosition);

            if (blockHandler == null)
            {
                _blockDetailPanel.Disappear();
                _currentBlockHandler = null;
                return;
            }

            if (blockHandler == _currentBlockHandler)
            {
                _blockDetailPanel.Disappear();
                _currentBlockHandler = null;
                return;
            }

            Vector3 position = blockHandler.transform.position + (_camera.transform.position - blockHandler.transform.position).normalized * 1.5f;
            Quaternion rotation = Quaternion.LookRotation((position - _camera.transform.position).normalized);

            _currentBlockHandler = blockHandler;
            _blockDetailPanel.Appear(blockHandler.Block, position, rotation);
        }

        #endregion Functions
    }
}