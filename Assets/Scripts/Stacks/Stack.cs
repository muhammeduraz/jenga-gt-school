using System;
using System.Collections.Generic;
using Assets.Scripts.Stacks.Blocks;

namespace Assets.Scripts.Stacks
{
    [Serializable]
    public class Stack
    {
        #region Variables

        private string _grade;
        private List<Block> _blockList;

        #endregion Variables

        #region Properties

        public List<Block> BlockList { get => _blockList; }
        public string Grade { get => _grade; set => _grade = value; }

        #endregion Properties

        #region Functions

        public Stack(string grade, List<Block> blockList)
        {
            _grade = grade;
            _blockList = blockList;
        }

        #endregion Functions
    }
}