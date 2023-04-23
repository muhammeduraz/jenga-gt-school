using System;
using UnityEngine;
using Assets.Scripts.Stacks;
using System.Collections.Generic;

namespace Assets.Scripts.Stacks
{
    public class StackUIGenerator : MonoBehaviour, IDisposable
    {
        #region Variables

        [SerializeField] private Transform _buttonParent;
        [SerializeField] private StackButton _stackButtonPrefab;

        #endregion Variables

        #region Unity Functions

        private void OnDestroy()
        {
            Dispose();
        }

        #endregion Unity Functions

        #region Functions

        public void Initialize()
        {

        }

        public void Dispose()
        {

        }

        public void CreateUIElements(List<StackHandler> stackHandlerList)
        {
            StackButton stackButton = null;
            StackHandler stackHandler = null;

            for (int i = 0; i < stackHandlerList.Count; i++)
            {
                stackHandler = stackHandlerList[i];

                if (stackHandler != null)
                {
                    stackButton = Instantiate(_stackButtonPrefab, _buttonParent);
                    stackButton.SetupButton(stackHandler.Stack.Grade);
                }
            }
        }

        #endregion Functions
    }
}