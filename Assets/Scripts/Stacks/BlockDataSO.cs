using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Stacks
{
    [CreateAssetMenu(fileName = "BlockDataSO", menuName = "ScriptableObjects/BlockDataSO", order = 1)]
    public class BlockDataSO : ScriptableObject
    {
        #region Variables

        public List<BlockData> blockDataList;

        #endregion Variables

        #region Functions

        public BlockHandler GetBlockHandlerByMastery(int mastery)
        {
            BlockData blockData = null;

            for (int i = 0; i < blockDataList.Count; i++)
            {
                blockData = blockDataList[i];

                if (blockData != null && blockData.mastery == mastery)
                {
                    return blockData.blockHandler;
                }
            }

            return null;
        }

        #endregion Functions
    }
}