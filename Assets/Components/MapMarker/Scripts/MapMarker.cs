using System;
using UnityEngine;

namespace MapSpace
{
    public class MapMarker : MonoBehaviour
    {
        public Transform WorldTargetTransform;
        
        private float _worldWidth;
        private float _worldHeight;

        private RectTransform _markerRectTransform;
        
        private Vector2 _scaleValue => new(Map.Instance.MapSize.x / _worldWidth, Map.Instance.MapSize.y / _worldHeight);

        private void Start()
        {
            _worldWidth = 3000f;
            _worldHeight = 3000f;

            _markerRectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            UpdateMarkerPosition();
        }

        private void UpdateMarkerPosition()
        {
            Vector2 newPosition = new Vector2(
                WorldTargetTransform.position.x * _scaleValue.x,
                WorldTargetTransform.position.z * _scaleValue.y
            );

            _markerRectTransform.anchoredPosition = newPosition;
        }
    }
}
