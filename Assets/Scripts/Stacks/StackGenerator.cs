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
                            tempBlockHandler = _blockDataSO.GetBlockHandlerByMastery(loopBlock.mastery);

                            if(tempBlockHandler != null)
                            {
                                tempBlockHandler = Instantiate(tempBlockHandler, tempStackHandler.transform, true);
                                tempBlockHandler.Block = loopBlock;
                                tempBlockHandler.name = $"Block_{loopBlock.id}";

                                //Vector3 rot = ((j / 3) % 2) * 90f * Vector3.up;
                                //tempBlockHandler.transform.rotation = Quaternion.Euler(rot);

                                //Vector3 pos = Vector3.right * (5 * (i + 1));
                                //pos += tempBlockHandler.BoxCollider.bounds.size.x * (j % 3) * tempBlockHandler.transform.right;
                                //pos += tempBlockHandler.BoxCollider.bounds.size.y * (j / 3) * tempBlockHandler.transform.up;

                                //if (rot.y > 89f)
                                //    pos +=  Vector3.right * tempBlockHandler.BoxCollider.bounds.size.x / 2f;

                                Quaternion rot = Quaternion.Euler(Vector3.up * 90f * ((j / 3) % 2));
                                tempBlockHandler.transform.rotation = rot;

                                Vector3 pos = Vector3.right * 10 * i;
                                pos += tempBlockHandler.transform.right * Mathf.Pow(-1, (j % 3)) * (((j % 3)) == 2 ? 1 : ((j % 3) % 2)) * 0.05f + tempBlockHandler.transform.right * Mathf.Pow(-1, (j % 3)) * (((j % 3)) == 2 ? 1 : ((j % 3) % 2)) * tempBlockHandler.BoxCollider.bounds.size.x;
                                pos += Vector3.up * (j / 3) * tempBlockHandler.BoxCollider.bounds.size.y;

                                tempBlockHandler.transform.position = pos;
                            }
                        }
                    }
                }
            }
        }

        #endregion Functions
    }
}