using UnityEngine;

namespace Interface
{
    [ExecuteAlways]
    public class SizeFitterBackdrop : MonoBehaviour
    {
        [SerializeField] private int _paddingHorizontal;
        [SerializeField] private int _paddingVertical;
        [SerializeField] private RectTransform _rectTransformToCopy;

        private RectTransform _rectTransform;

        private float _copyHeight;
        private float _copyWidth;

        private void Awake()
        {
            _rectTransform = transform as RectTransform;
        }

        void Update()
        {
            _copyWidth = _rectTransformToCopy.rect.width;
            _copyHeight = _rectTransformToCopy.rect.height;
            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _copyWidth + (_copyWidth > 0 ? _paddingHorizontal : 0));
            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,  _copyHeight + (_copyHeight > 0 ? _paddingVertical : 0));
        
        }
    }
}
