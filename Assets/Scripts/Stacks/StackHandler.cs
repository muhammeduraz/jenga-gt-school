using TMPro;
using System;
using UnityEngine;
using Assets.Scripts.Stacks.UI;
using System.Collections.Generic;
using Assets.Scripts.Stacks.Blocks;

namespace Assets.Scripts.Stacks
{
    public class StackHandler : MonoBehaviour, IDisposable
    {
        #region Variables

        private StackGenerator _stackGenerator;
        private List<BlockHandler> _blockHandlerList;

        [SerializeField] private TextMeshPro _gradeText;

        [SerializeField] private Stack _stack;

        #endregion Variables

        #region Properties

        public Stack Stack { get => _stack; set => _stack = value; }
        public StackGenerator StackGenerator { get => _stackGenerator; set => _stackGenerator = value; }

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
            SetGradeText();

            _blockHandlerList = new List<BlockHandler>();
        }

        public void Dispose()
        {
            _blockHandlerList = null;
            _gradeText = null;
            _stack = null;
        }

        private void SetName()
        {
            name = _stack.Grade;
        }

        private void SetGradeText()
        {
            if (_stack != null)
            {
                _gradeText.text = _stack.Grade;
            }
        }

        public void AddBlock(BlockHandler blockHandler)
        {
            _blockHandlerList.Add(blockHandler);
        }

        public void InitializeBlocks()
        {
            BlockHandler blockHandler = null;

            for (int i = 0; i < _blockHandlerList.Count; i++)
            {
                blockHandler = _blockHandlerList[i];

                if (blockHandler != null)
                {
                    blockHandler.Initialize();
                }
            }
        }

        private void RemoveGlassBlocks()
        {
            BlockHandler blockHandler = null;

            for (int i = 0; i < _blockHandlerList.Count; i++)
            {
                blockHandler = _blockHandlerList[i];

                if (blockHandler != null && blockHandler.Block.mastery == 0)
                {
                    blockHandler.gameObject.SetActive(false);
                }
            }
        }

        private void SetPhysic(bool activate)
        {
            BlockHandler blockHandler = null;

            for (int i = 0; i < _blockHandlerList.Count; i++)
            {
                blockHandler = _blockHandlerList[i];

                if (blockHandler != null)
                {
                    blockHandler.SetPhysic(activate);
                }
            }
        }

        public void ActivateTestMyStack()
        {
            RemoveGlassBlocks();
            SetPhysic(true);
        }

        public void ResetStack()
        {
            _blockHandlerList.ForEach(block => Destroy(block.gameObject));
            _blockHandlerList.Clear();

            StackGenerator.GenerateStack(this);
        }

        #endregion Functions
    }
}