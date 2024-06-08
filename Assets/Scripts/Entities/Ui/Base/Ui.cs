using UnityEngine;
using Zenject;

namespace Mario.Entities.Ui.Base
{
    public abstract class Ui: MonoBehaviour, IInitializable, ILateDisposable
    {
        [SerializeField] protected Canvas canvas;

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent, false);
            OnSetParent(parent);
        }

        public void Show()
        {
            SetCanvasState(true);
        }

        public void Close()
        {
            SetCanvasState(false);
        }

        private void SetCanvasState(bool isActive) => canvas.enabled = isActive;
        
        public virtual void Initialize() {}
        public virtual void LateDispose() {}
        protected virtual void OnClose() {}
        protected virtual void OnShow() {}
        protected virtual void OnSetParent(Transform parent) {}
    }
}