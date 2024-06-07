using Mario.Entities.Base;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mario.Entities.Input
{
    public class InputView : View<IInputModelObserver, IInputCommand>, IBeginDragHandler, IDragHandler, IEndDragHandler,
        IPointerDownHandler, IPointerUpHandler
    {
        private const float Divider = 2.0f;
        private const float MagnitudeLimit = 1.0f;
        
        [SerializeField] private RectTransform backgroundRectTransform;
        [SerializeField] private RectTransform knobRectTransform;
        [SerializeField] private float offset;

        private Vector2 pointPosition = Vector2.zero;

        private Rect BackgroundRect => backgroundRectTransform.rect;
        private Rect KnobRect => knobRectTransform.rect;
        
        protected override void OnInitialize() {}
        protected override void OnDispose() {}

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData) {}

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            OnDragInternal(eventData);
        }
        
        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            OnEndDragInternal();
        }
        
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            OnDragInternal(eventData);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            OnEndDragInternal();
        }
        
        private void OnDragInternal(PointerEventData eventData)
        {
            Vector3 backPosition = backgroundRectTransform.position;
            pointPosition.x = (eventData.position.x - backPosition.x) /
                              ((BackgroundRect.size.x - KnobRect.size.x) / Divider);
            pointPosition.y = (eventData.position.y - backPosition.y) /
                              ((BackgroundRect.size.y - KnobRect.size.y) / Divider);
            
            pointPosition = (pointPosition.magnitude > MagnitudeLimit) ? pointPosition.normalized : pointPosition;

            Vector3 knobPosition = knobRectTransform.transform.position;
            knobPosition.x = (pointPosition.x * ((BackgroundRect.size.x - KnobRect.size.x) / Divider) * offset) + backPosition.x;
            knobPosition.y = (pointPosition.y * ((BackgroundRect.size.y - KnobRect.size.y) / Divider) * offset) + backPosition.y;
            knobRectTransform.transform.position = knobPosition;
            
            Command.UpdateDirection(pointPosition);
        }
        
        private void OnEndDragInternal()
        {
            pointPosition.x = 0f;
            pointPosition.y = 0f;
            knobRectTransform.transform.position = backgroundRectTransform.position;
            Command.UpdateDirection(pointPosition);
        }
    }
}