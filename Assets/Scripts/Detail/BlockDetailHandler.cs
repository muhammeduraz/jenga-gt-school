using System;
using UnityEngine;
using Assets.Scripts.Stacks;
using Assets.Scripts.Stacks.Blocks;

namespace Assets.GameAssets.Scripts
{
    public class BlockDetailHandler : MonoBehaviour, IDisposable
    {
        #region Variables

        private Camera _camera;
        private RaycastOperations _raycastOperations;

        public InputHandler _inputHandler;

        [SerializeField] private BlockDetailPanel _blockDetailPanel;

        #endregion Variables

        #region Properties



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

            Vector3 position = blockHandler.transform.position + (_camera.transform.position - blockHandler.transform.position).normalized * 1f;
            Quaternion rotation = Quaternion.LookRotation((position - _camera.transform.position).normalized);

            _blockDetailPanel.Appear(blockHandler.Block, position, rotation);
        }

        private void Disappear(InputData inputData)
        {
            BlockHandler blockHandler = _raycastOperations.GetObjectOfType<BlockHandler>(inputData.screenPosition);

            if (blockHandler != null) return;

            _blockDetailPanel.Disappear();
        }

        #endregion Functions
    }
}