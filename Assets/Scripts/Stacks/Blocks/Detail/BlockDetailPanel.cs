using TMPro;
using System;
using UnityEngine;
using DG.Tweening;
using Assets.Scripts.Stacks;
using Assets.Scripts.Stacks.Blocks;

namespace Assets.Scripts.Blocks.Detail
{
    public class BlockDetailPanel : MonoBehaviour, IDisposable
    {
        #region Variables

        private Block _block;
        private Tween _scaleTween;

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

            transform.localScale = Vector3.zero;
            gameObject.SetActive(true);

            _scaleTween?.Kill();
            _scaleTween = transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack);
        }

        public void Disappear()
        {
            _scaleTween?.Kill();
            _scaleTween = transform.DOScale(0f, 0.25f).SetEase(Ease.InBack);

            _scaleTween.OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
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