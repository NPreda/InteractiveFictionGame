using UnityEngine;


namespace Custom.UI
{
    [ExecuteInEditMode()]
    public class CustomUI : MonoBehaviour
    {
        protected bool _isDirty;

        protected virtual void OnSkinUI()
        {
            _isDirty = false;
        }

        public virtual void Awake()
        {

        }

        public virtual void Update()
        {
            if(!_isDirty) return;
            OnSkinUI();
        }

        private void OnValidate()
        {
            _isDirty = true;
        }

        public virtual void DestroyHost()
        {
            Destroy(this.gameObject);
        }
    }
}

