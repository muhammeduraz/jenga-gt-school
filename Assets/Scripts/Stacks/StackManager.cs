using System;
using UnityEngine;
using Assets.Scripts.Stacks;
using Assets.Scripts.Stacks.UI;
using System.Collections.Generic;

namespace Assets.Scripts.Stacks
{
    public class StackManager : MonoBehaviour, IDisposable
    {
        #region Variables

        private StackHandler _currentStackHandler;
        private List<StackHandler> _stackHandlerList;

        [SerializeField] private StackUIHandler _stackUIHandler;
        [SerializeField] private StackGenerator _stackGenerator;

        #endregion Variables

        #region Properties

        public StackHandler CurrentStackHandler { get => _currentStackHandler; set => _currentStackHandler = value; }

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

            _stackGenerator.Initialize();
            StartCoroutine(_stackGenerator.GenerateStacks(this));
        }

        public void LateInitialize()
        {
            _currentStackHandler = _stackHandlerList[0];
            _stackUIHandler.CreateStackUIElements(_stackHandlerList);
        }

        public void Dispose()
        {

        }

        public void AddStack(StackHandler stackHandler)
        {
            if (stackHandler == null) return;

            _stackHandlerList.Add(stackHandler);
        }

        #endregion Functions
    }
}