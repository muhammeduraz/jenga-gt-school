using System;
using UnityEngine;
using Assets.Scripts.Stacks;
using System.Collections.Generic;

namespace Assets.Scripts.Stacks
{
    public class StackManager : MonoBehaviour, IDisposable
    {
        #region Variables

        private List<StackHandler> _stackHandlerList;

        [SerializeField] private StackUIGenerator _stackUIGenerator;

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

        public void Initialize()
        {
            _stackHandlerList = new List<StackHandler>();
        }

        public void Dispose()
        {

        }

        public void AddStack(StackHandler stackHandler)
        {
            if (stackHandler == null) return;

            _stackHandlerList.Add(stackHandler);
        }

        public void CreateStackUIElements()
        {
            _stackUIGenerator.CreateUIElements(_stackHandlerList);
        }

        #endregion Functions
    }
}