using UnityEngine;
using UnityEngine.Events;

namespace TLab.VKeyborad
{
    public class InputFieldBase : MonoBehaviour
    {
        [Header("Keyborad")]
        [SerializeField] protected TLabVKeyborad m_keyborad;

        [Header("Option")]
        [SerializeField] protected bool m_activeOnAwake = false;

        [Header("Event")]
        [SerializeField] protected UnityEvent<bool> m_onFocus;
        [SerializeField] protected UnityEvent m_onEnterPressed;
        [SerializeField] protected UnityEvent m_onTabPressed;
        [SerializeField] protected UnityEvent m_onShiftPressed;
        [SerializeField] protected UnityEvent m_onSpacePressed;
        [SerializeField] protected UnityEvent m_onBackSpacePressed;
        [SerializeField] protected UnityEvent m_onSymbolPressed;
        [SerializeField] protected UnityEvent<string> m_onKeyPressed;

        public bool inputFieldIsActive
        {
            get
            {
                if (m_keyborad == null)
                    return false;

                return m_keyborad.inputFieldBase == this;
            }
        }

        #region KEY_EVENT

        public virtual void OnEnterPressed() => m_onEnterPressed.Invoke();

        public virtual void OnTabPressed() => m_onTabPressed.Invoke();

        public virtual void OnShiftPressed() => m_onShiftPressed.Invoke();

        public virtual void OnSpacePressed() => m_onSpacePressed.Invoke();

        public virtual void OnBackSpacePressed() => m_onBackSpacePressed.Invoke();

        public virtual void OnSymbolPressed() => m_onSymbolPressed.Invoke();

        public virtual void OnKeyPressed(string input) => m_onKeyPressed.Invoke(input);

        #endregion KEY_EVENT

        #region FOUCUS_EVENET

        protected virtual void SwitchInputField(bool active) => m_keyborad?.SwitchInputField(active ? this : null);

        public virtual void OnFocus(bool active)
        {
            SwitchInputField(active);

            m_onFocus.Invoke(active);
        }

        public virtual void OnFocus()
        {
            SwitchInputField(!inputFieldIsActive);

            m_onFocus.Invoke(true);
        }

        #endregion FOUCUS_EVENT

        protected virtual void Start()
        {
            if (m_activeOnAwake && (m_keyborad != null))
            {
                m_keyborad.SetVisibility(true);
                m_keyborad.SwitchInputField(this);
            }
        }

        protected virtual void Update() { }
    }
}