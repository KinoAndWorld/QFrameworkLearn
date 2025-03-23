using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace script
{
    // 拖拽功能示例
    public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Vector3 offsetPosition;
        private RectTransform rectTransform;
        
        public Vector2 targetPos = new Vector2(0, 0);
        
        private bool isFinished = false;
    
        // [SerializeField] private Vector2 curCenterPos = new Vector2(0, 0); 
        
        void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }
    
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (isFinished)
            {
                return;
            }
            offsetPosition = rectTransform.position - Input.mousePosition;
        }
    
        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.position = Input.mousePosition + offsetPosition;
        }
    
        public void OnEndDrag(PointerEventData eventData)
        {
            // 检测是否靠近正确位置并吸附
            CheckForSnapping();
        }
    
        void CheckForSnapping()
        {
            // 实现吸附逻辑
            var deltaX = Mathf.Abs(rectTransform.anchoredPosition.x - targetPos.x);
            var deltaY = Mathf.Abs(rectTransform.anchoredPosition.y - targetPos.y);
            if (deltaX <=15.0f && deltaY <= 15.0f)
            {
                rectTransform.anchoredPosition = targetPos;
                // 锁定，表示已经完成
                isFinished = true;
            }
        }

        private void OnDrawGizmosSelected()
        {
            
        }
    }
}