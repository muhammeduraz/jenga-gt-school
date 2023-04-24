using System;
using UnityEngine;
using Assets.Scripts.API;
using System.Collections;
using Assets.Scripts.Stacks.UI;
using System.Collections.Generic;
using Assets.Scripts.Stacks.Blocks;

namespace Assets.Scripts.Stacks
{
    public class StackGenerator : MonoBehaviour, IDisposable
    {
        #region Variables

        private APIHelper _apiHelper;
        private List<Stack> _stackList;
        private StackManager _stackManager;

        [SerializeField] private BlockDataSO _blockDataSO;
        [SerializeField] private StackHandler _stackPrefab;

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
            _apiHelper = new APIHelper();
        }

        public void Dispose()
        {
            _apiHelper = null;
        }

        private void SetStackPosition(Vector3 direction, int multiplier, float amount, StackHandler stackHandler)
        {
            Vector3 stackPos = direction * amount * multiplier;
            stackHandler.transform.position = stackPos;
        }

        private void SetBlockPositionAndRotation(Vector3 center, int j, BlockHandler blockHandler)
        {
            Quaternion rot = Quaternion.Euler(((j / 3) % 2) * 90f * Vector3.up);
            blockHandler.transform.rotation = rot;

            Vector3 pos = center;
            pos += (((j % 3)) == 2 ? 1 : ((j % 3) % 2)) * 0.05f * Mathf.Pow(-1, (j % 3)) * blockHandler.transform.right + blockHandler.transform.right * Mathf.Pow(-1, (j % 3)) * (((j % 3)) == 2 ? 1 : ((j % 3) % 2)) * blockHandler.BoxCollider.bounds.size.x;
            pos += (j / 3) * blockHandler.BoxCollider.bounds.size.y * Vector3.up;

            blockHandler.transform.position = pos;
        }

        public IEnumerator GenerateStacks(StackManager stackManager)
        {
            yield return _apiHelper.GetStackList();
            _stackList = _apiHelper.stackList;

            _stackManager = stackManager;

            Stack loopStack = null;
            Block loopBlock = null;

            StackHandler tempStackHandler = null;
            BlockHandler tempBlockHandler = null;

            for (int i = 0; i < _stackList.Count; i++)
            {
                loopStack = _stackList[i];

                if (loopStack != null)
                {
                    tempStackHandler = Instantiate(_stackPrefab, _stackManager.transform, true);
                    SetStackPosition(Vector3.right, i, 5f, tempStackHandler);
                    tempStackHandler.Stack = loopStack;
                    tempStackHandler.Initialize();
                    tempStackHandler.StackGenerator = this;

                    _stackManager.AddStack(tempStackHandler);

                    for (int j = 0; j < loopStack.BlockList.Count; j++)
                    {
                        loopBlock = loopStack.BlockList[j];

                        if (loopBlock != null)
                        {
                            tempBlockHandler = _blockDataSO.GetBlockHandlerByMastery(loopBlock.mastery);

                            if(tempBlockHandler != null)
                            {
                                tempBlockHandler = Instantiate(tempBlockHandler, tempStackHandler.transform, true);
                                tempBlockHandler.Block = loopBlock;
                                SetBlockPositionAndRotation(tempStackHandler.transform.position, j, tempBlockHandler);
                                
                                tempStackHandler.AddBlock(tempBlockHandler);
                            }
                        }
                    }

                    tempStackHandler.InitializeBlocks();
                }
            }

            _stackManager.LateInitialize();
        }

        public void GenerateStack(StackHandler stackHandler)
        {
            Stack loopStack = null;
            Block loopBlock = null;

            BlockHandler tempBlockHandler = null;

            for (int i = 0; i < _stackList.Count; i++)
            {
                loopStack = _stackList[i];

                if (loopStack != null && loopStack.Grade == stackHandler.Stack.Grade)
                {
                    for (int j = 0; j < loopStack.BlockList.Count; j++)
                    {
                        loopBlock = loopStack.BlockList[j];

                        if (loopBlock != null)
                        {
                            tempBlockHandler = _blockDataSO.GetBlockHandlerByMastery(loopBlock.mastery);

                            if (tempBlockHandler != null)
                            {
                                tempBlockHandler = Instantiate(tempBlockHandler, stackHandler.transform, true);
                                tempBlockHandler.Block = loopBlock;
                                SetBlockPositionAndRotation(stackHandler.transform.position, j, tempBlockHandler);

                                stackHandler.AddBlock(tempBlockHandler);
                            }
                        }
                    }

                    stackHandler.InitializeBlocks();
                }
            }
        }

        #endregion Functions
    }
}