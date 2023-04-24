using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Stacks.Blocks;

namespace Assets.Scripts.Stacks.UI
{
    public enum SortType
    {
        Domain,
        Cluster,
        StandardID
    }

    public class SortStackButton : MonoBehaviour, IDisposable
    {
        #region Variables

        [SerializeField] private SortType _sortType;

        [SerializeField] private Button _button;

        [SerializeField] private StackManager _stackManager;

        #endregion Variables

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
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(OnButtonClicked);
        }

        public void Dispose()
        {
            _button.onClick.RemoveAllListeners();
            _button = null;
        }

        private void OnButtonClicked()
        {
            Comparison<Block> comparison = (Block a, Block b) =>
            {
                if (_sortType == SortType.Domain)
                    return a.domain.CompareTo(b.domain);
                else if (_sortType == SortType.Cluster)
                    return a.cluster.CompareTo(b.cluster);
                else
                    return a.standardId.CompareTo(b.standardId);
            };

            _stackManager.CurrentStackHandler.SortStack(comparison);
        }

        #endregion Functions
    }
}