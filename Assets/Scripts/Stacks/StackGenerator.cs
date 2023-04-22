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

        [SerializeField] private BlockHandler _blockPrefab;
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
                    tempStackHandler.Stack = loopStack;
                    tempStackHandler.name = loopStack.Grade;

                    for (int j = 0; j < loopStack.BlockList.Count; j++)
                    {
                        loopBlock = loopStack.BlockList[j];

                        if (loopBlock != null)
                        {
                            tempBlockHandler = Instantiate(_blockPrefab, tempStackHandler.transform, true);
                            tempBlockHandler.Block = loopBlock;
                            tempBlockHandler.name = $"Block_{loopBlock.id}";
                            tempBlockHandler.transform.position = 2f * i * Vector3.right + Vector3.up * j *  tempBlockHandler.BoxCollider.bounds.size.y;
                        }
                    }
                }
            }
        }

        #endregion Functions
    }
}