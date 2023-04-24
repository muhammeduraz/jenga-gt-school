using UnityEngine;
using System.Text;
using Newtonsoft.Json;
using System.Collections;
using Assets.Scripts.Stacks;
using UnityEngine.Networking;
using System.Collections.Generic;
using Assets.Scripts.Stacks.Blocks;
using Stack = Assets.Scripts.Stacks.Stack;

namespace Assets.Scripts.API
{
    public class APIHelper
    {
        #region Variables

        private const string URL = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";

        public List<Stack> stackList;

        #endregion Variables

        #region Functions

        public IEnumerator GetStackList()
        {
            using UnityWebRequest webRequest = UnityWebRequest.Get(URL);
            yield return webRequest.SendWebRequest();

            string rawJson = Encoding.Default.GetString(webRequest.downloadHandler.data);

            List<Block> blockList = JsonConvert.DeserializeObject<List<Block>>(rawJson);
            stackList = new List<Stack>();

            Block loopBlock = null;
            Stack loopStack = null;

            for (int i = 0; i < blockList.Count; i++)
            {
                loopBlock = blockList[i];

                if (loopBlock != null)
                {
                    loopStack = GetStackByGrade(loopBlock.grade, stackList);

                    if (loopStack != null)
                    {
                        loopStack.BlockList.Add(loopBlock);
                    }
                    else
                    {
                        stackList.Add(new Stack(loopBlock.grade, new List<Block>() { loopBlock }));
                    }
                }
            }
        }

        private Stack GetStackByGrade(string grade, List<Stack> stackList)
        {
            Stack loopStack = null;

            for (int i = 0; i < stackList.Count; i++)
            {
                loopStack = stackList[i];

                if (loopStack != null && loopStack.Grade.Equals(grade))
                {
                    return loopStack;
                }
            }

            return null;
        }

        #endregion Functions
    }
}