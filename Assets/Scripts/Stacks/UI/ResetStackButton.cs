using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stacks.UI
{
    public class ResetStackButton : MonoBehaviour, IDisposable
    {
        #region Variables

        [SerializeField] private Button _button;
        [SerializeField] private StackManager _stackManager;

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
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(OnButtonClicked);
        }

        public void Dispose()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void OnButtonClicked()
        {
            _stackManager.CurrentStackHandler.ResetStack();
        }

        #endregion Functions
    }
}