using TMPro;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class StackButton : MonoBehaviour, IDisposable
    {
        #region Variables

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

        }

        public void Dispose()
        {
            _gradeText = null;
        }

        public void SetupButton(string grade)
        {
            _gradeText.text = grade;
        }

        #endregion Functions
    }
}