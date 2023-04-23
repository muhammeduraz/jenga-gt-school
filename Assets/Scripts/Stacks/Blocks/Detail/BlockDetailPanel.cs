using TMPro;
using System;
using UnityEngine;
using Assets.Scripts.Stacks;
using Assets.Scripts.Stacks.Blocks;

namespace Assets.Scripts.Blocks.Detail
{
    public class BlockDetailPanel : MonoBehaviour, IDisposable
    {
        #region Variables

        private Block _block;

        [SerializeField] private TextMeshProUGUI _gradeLevelText;
        [SerializeField] private TextMeshProUGUI _clusterText;
        [SerializeField] private TextMeshProUGUI _standardIDText;

        #endregion Variables

        #region Unity Functions

        private void OnDestroy()
        {
            Dispose();
        }

        #endregion Unity Functions

        #region Functions

        public void Appear(Block block, Vector3 position, Quaternion rotation)
        {
            _block = block;

            transform.position = position;
            transform.rotation = rotation;

            _gradeLevelText.text = _block.grade + ": " + _block.domain;
            _clusterText.text = _block.cluster;
            _standardIDText.text = _block.standardId + ": " + _block.standardDescription;

            gameObject.SetActive(true);
        }

        public void Disappear()
        {
            gameObject.SetActive(false);
        }

        public void Dispose()
        {
            _block = null;

            _gradeLevelText = null;
            _clusterText = null;
            _standardIDText = null;
        }

        #endregion Functions
    }
}