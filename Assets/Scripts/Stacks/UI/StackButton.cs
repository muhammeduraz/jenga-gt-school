using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stacks.UI
{
    public class StackButton : MonoBehaviour, IDisposable
    {
        #region Variables

        private StackHandler _stackHandler;
        private StackUIHandler _stackUIHandler;

        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _gradeText;

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
            _button = null;
            _gradeText = null;

            _button.onClick.RemoveAllListeners();
        }

        public void SetupButton(StackUIHandler stackUIHandler, StackHandler stackHandler)
        {
            _stackHandler = stackHandler;
            _stackUIHandler = stackUIHandler;

            _gradeText.text = _stackHandler.Stack.Grade;
        }

        private void OnButtonClicked()
        {
            _stackUIHandler.MoveCamera(_stackHandler);
        }

        #endregion Functions
    }
}