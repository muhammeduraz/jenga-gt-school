using UnityEngine;

namespace Assets.Scripts.Stacks
{
    public class BlockHandler : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Renderer _renderer;
        [SerializeField] private BoxCollider _boxCollider;

        [SerializeField] private Block _block;

        #endregion Variables

        #region Properties

        public BoxCollider BoxCollider { get => _boxCollider; }

        public Block Block { get => _block; set => _block = value; }

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