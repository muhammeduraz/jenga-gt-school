using UnityEngine;
using Assets.Scripts.API;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Stacks
{
    public class StackGenerator : MonoBehaviour
    {
        #region Variables

        private APIHelper _apiHelper;

        [SerializeField] private BlockDataSO _blockDataSO;
        [SerializeField] private StackHandler _stackPrefab;

        #endregion Variables

        #region Properties



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
            _apiHelper = new APIHelper();

            StartCoroutine(GenerateStacks());
        }

        private void Dispose()
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
            Quaternion rot = Quaternion.Euler(Vector3.up * 90f * ((j / 3) % 2));
            blockHandler.transform.rotation = rot;

            Vector3 pos = center;
            pos += blockHandler.transform.right * Mathf.Pow(-1, (j % 3)) * (((j % 3)) == 2 ? 1 : ((j % 3) % 2)) * 0.05f + blockHandler.transform.right * Mathf.Pow(-1, (j % 3)) * (((j % 3)) == 2 ? 1 : ((j % 3) % 2)) * blockHandler.BoxCollider.bounds.size.x;
            pos += Vector3.up * (j / 3) * blockHandler.BoxCollider.bounds.size.y;

            blockHandler.transform.position = pos;
        }

        private IEnumerator GenerateStacks()
        {
            yield return _apiHelper.GetStackList();
            List<Stack> stackList = _apiHelper.stackList;

            Stack loopStack = null;
            Block loopBlock = null;

            StackHandler tempStackHandler = null;
            BlockHandler tempBlockHandler = null;

            for (int i = 0; i < stackList.Count; i++)
            {
                loopStack = stackList[i];

                if (loopStack != null)
                {
                    tempStackHandler = Instantiate(_stackPrefab, transform, true);
                    SetStackPosition(Vector3.right, i, 5f, tempStackHandler);
                    tempStackHandler.Stack = loopStack;
                    tempStackHandler.Initialize();

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
        }

        #endregion Functions
    }
}