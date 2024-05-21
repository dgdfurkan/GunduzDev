using System;
using System.Net.Mime;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GD.Controllers
{
    [RequireComponent(typeof(EventTrigger), typeof(Image))]
    public class GameRightPanelController : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        #region Self Variables

        #region Public  Variables

        //

        #endregion

        #region Serialized Variables
        
        [SerializeField] private GameObject expandArrow;
        
        [Space(10)]
        [Header("Color Settings")]
        [SerializeField] private Color normalColor = new Color(1, 1, 1, 1); // Hedef renk
        [SerializeField] private Color targetColor = new Color(1, 1, 1, 1); // Hedef renk

        [Space(10)]
        [Header("Animation Settings")]
        [SerializeField] private float scaler = 6;
        [SerializeField] private float animationDuration = 0.3f;
        [SerializeField] private Ease openEaseType;
        [SerializeField] private Ease closeEaseType;
        
        #endregion

        #region Private Variables

        private RectTransform _rectTransform;
        private float _originalWidth;
        private bool _isClicked = false;
        private Tween _tween;

        #endregion

        #endregion

        private void OnEnable()
        {
            expandArrow.SetActive(false);
            GetComponent<Image>().color = normalColor;
            _rectTransform = GetComponent<RectTransform>();
            _originalWidth = _rectTransform.sizeDelta.x;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _tween?.Kill();
            if (_isClicked) return;
            
            expandArrow.SetActive(true);
            
            GetComponent<Image>().DOColor(targetColor, animationDuration);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _isClicked = true;
            
            expandArrow.SetActive(false);
            
            DOTween.To(() => _rectTransform.sizeDelta, x => _rectTransform.sizeDelta = x,
                    new Vector2(_originalWidth * scaler, _rectTransform.sizeDelta.y), animationDuration)
                .SetEase(openEaseType);
            
            // TODO: After clicking the button, Items should be shown in the right panel
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isClicked)
            {
                expandArrow.SetActive(false);
                GetComponent<Image>().DOColor(normalColor, animationDuration);
                return;
            }
            
            _tween = DOVirtual.DelayedCall(2f, () =>
            {
                expandArrow.SetActive(false);
            
                DOTween.To(() => _rectTransform.sizeDelta, x => _rectTransform.sizeDelta = x,
                        new Vector2(_originalWidth, _rectTransform.sizeDelta.y), animationDuration)
                    .SetEase(closeEaseType);
            
                GetComponent<Image>().DOColor(normalColor, animationDuration);
            }).OnComplete(()=> {_isClicked = false;});
        }

        private void OnDisable()
        {
            _tween?.Kill();
            expandArrow.SetActive(false);
            GetComponent<Image>().color = normalColor;
            _rectTransform.sizeDelta = new Vector2(_originalWidth, _rectTransform.sizeDelta.y);
        }
    }
}