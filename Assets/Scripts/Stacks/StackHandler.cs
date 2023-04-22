using UnityEngine;

namespace Assets.Scripts.Stacks
{
    public class StackHandler : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Stack _stack;

        #endregion Variables

        #region Properties

        public Stack Stack { get => _stack; set => _stack = value; }

        #endregion Properties

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

        private void Dispose()
        {
            
        }

        #endregion Functions
    }
}