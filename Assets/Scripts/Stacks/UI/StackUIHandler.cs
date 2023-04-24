using System;
using UnityEngine;
using Assets.Scripts.Cameras;
using System.Collections.Generic;

namespace Assets.Scripts.Stacks.UI
{
    public class StackUIHandler : MonoBehaviour, IDisposable
    {
        #region Variables

        private CameraHandler _cameraHandler;

        [SerializeField] private StackUIGenerator _stackUIGenerator;

        #endregion Variables

        #region Unity Functions

        private void Awake()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        #endregion Unity Functions

        #region Functions

        private void Initialize()
        {
            _cameraHandler = Camera.main.GetComponent<CameraHandler>();
        }

        public void Dispose()
        {

        }

        public void CreateStackUIElements(List<StackHandler> stackHandlerList)
        {
            _stackUIGenerator.CreateStackUIElements(stackHandlerList, this);
        }

        public void MoveCamera(StackHandler stackHandler)
        {
            _cameraHandler.MoveToStack(stackHandler);
        }

        #endregion Functions
    }
}