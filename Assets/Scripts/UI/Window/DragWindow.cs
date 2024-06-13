using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace TRPG.UI
{
    public class DragWindow : Window, IBeginDragHandler, IDragHandler
    {
        [SerializeField] private RectTransform _section;
        private Vector2 _offset;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_section == null)
            {
                _section = transform.parent.transform as RectTransform;
            }
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_section, eventData.position, Camera.current, out Vector2 cursor);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_section, transform.position, Camera.current, out Vector2 pos);
            _offset = cursor - pos;
        }

        public void OnDrag(PointerEventData eventData)
        {
            // Convert screen point to local point
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_section, eventData.position, Camera.current, out Vector2 position);
            position -= _offset;
            
            Rect parentRect = _section.rect;
            Rect rect = ((RectTransform)transform).rect;

            // Calculate the half sizes of the child RectTransform
            float halfWidth = rect.width / 2f;
            float halfHeight = rect.height / 2f;

            // Clamp the position
            float x = Mathf.Clamp(position.x, parentRect.xMin + halfWidth, parentRect.xMax - halfWidth);
            float y = Mathf.Clamp(position.y, parentRect.yMin + halfHeight, parentRect.yMax - halfHeight);

            position = new Vector2(x, y);
            
            // Move the RectTransform
            transform.position = _section.TransformPoint(position);
        }
    }
}