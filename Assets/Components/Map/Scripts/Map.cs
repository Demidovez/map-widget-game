using System;
using UnityEngine;

namespace MapSpace
{
    public class Map : MonoBehaviour
    {
        public static Map Instance;
        
        private RectTransform _rectTransform;
        private Vector2 _initialSizeDelta;
        private Camera _camera;
        
        public Vector2 MapSize => _rectTransform.sizeDelta;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _initialSizeDelta = _rectTransform.sizeDelta;
            _camera = Camera.main;
        }
        
        private void Update()
        {
            float scrollDirection = Input.GetAxis("Mouse ScrollWheel");

            if (scrollDirection != 0)
            {
                Vector3 mouseWorldPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
                Vector2 localPoint = _rectTransform.InverseTransformPoint(mouseWorldPoint);
                
                if (!_rectTransform.rect.Contains(localPoint))
                {
                    return;
                }
                
                UpdatePivot(localPoint);
                ScaleMap(scrollDirection);
            }
        }

        private void UpdatePivot(Vector2 localPoint)
        {
            Vector2 newPivot = new Vector2(
                localPoint.x / _rectTransform.rect.width + _rectTransform.pivot.x,
                localPoint.y / _rectTransform.rect.height + _rectTransform.pivot.y
            );
            
            Vector2 deltaPivot = _rectTransform.pivot - newPivot;
            float deltaX = deltaPivot.x * _rectTransform.sizeDelta.x;
            float deltaY = deltaPivot.y * _rectTransform.sizeDelta.y;
            Vector3 deltaPosition = new Vector3(deltaX, deltaY);
            
            _rectTransform.pivot = newPivot;
            _rectTransform.localPosition -= deltaPosition;
        }

        private void ScaleMap(float scrollDirection)
        {
            float sign = Mathf.Sign(scrollDirection);
            
            float x = _rectTransform.sizeDelta.x * (1 + sign * 0.1f);
            float y = _rectTransform.sizeDelta.y * (1 + sign * 0.1f);

            _rectTransform.sizeDelta = new Vector2(
                Mathf.Clamp(x, _initialSizeDelta.x, 20000f), 
                Mathf.Clamp(y, _initialSizeDelta.y, 20000f)
            );

            if (_rectTransform.sizeDelta == _initialSizeDelta)
            {
                _rectTransform.pivot = new Vector2(0.5f, 0.5f);
                _rectTransform.localPosition = new Vector3(0,0,0);
            }
        }
    }  
}

