using TMPro;
using System;
using UnityEngine;

namespace Assets.Scripts.Stacks.Blocks
{
    public class BlockHandler : MonoBehaviour, IDisposable
    {
        #region Variables

        [SerializeField] private TextMeshPro _textLeft;
        [SerializeField] private TextMeshPro _textRight;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private BoxCollider _boxCollider;

        [SerializeField] private Block _block;

        #endregion Variables

        #region Properties

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

        public void Dispose()
        {
            _textLeft = null;
            _textRight = null;

            _boxCollider = null;

            _block = null;
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