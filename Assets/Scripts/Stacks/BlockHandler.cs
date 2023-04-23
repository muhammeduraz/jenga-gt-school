using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stacks
{
    public class BlockHandler : MonoBehaviour
    {
        #region Variables

        [SerializeField] private TextMeshPro _textLeft;
        [SerializeField] private TextMeshPro _textRight;

        [SerializeField] private Renderer _renderer;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private BoxCollider _boxCollider;

        [SerializeField] private Block _block;

        #endregion Variables

        #region Properties

        public Renderer Renderer { get => _renderer; }
        public Rigidbody Rigidbody { get => _rigidbody; }
        public BoxCollider BoxCollider { get => _boxCollider; }

        public Block Block { get => _block; set => _block = value; }

        #endregion Properties

        #region Unity Functions

        private void OnDestroy()
        {
            Dispose();
        }

        #endregion Unity Functions

        #region Functions

        public void Initialize()
        {
            SetName();
            SetupBlock();
        }

        private void Dispose()
        {
            
        }

        private void SetName()
        {
            name = $"Block_{_block.id}";
        }

        private void SetText(string text)
        {
            _textLeft.text = text;
            _textRight.text = text;
        }

        public void SetupBlock()
        {
            switch (_block.mastery)
            {
                case 0:
                    SetText("Not Learned");
                    SetText("Not Learned");
                    break;
                case 1:
                    SetText("Learned");
                    SetText("Learned");
                    break;
                case 2:
                    SetText("Mastered");
                    SetText("Mastered");
                    break;
                default:
                    SetText("None");
                    SetText("None");
                    break;
            }
        }

        public void SetPhysic(bool activate)
        {
            if (_rigidbody == null) return;

            _rigidbody.isKinematic = !activate;
        }

        #endregion Functions
    }
}