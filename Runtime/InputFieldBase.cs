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

        public bool inputFieldIsActive => m_keyborad.inputFieldBase == this;

        public TLabVKeyborad keyborad
        {
            get => m_keyborad;
            set => m_keyborad = value;
        }

        #region KEY_EVENT

        public virtual void OnEnterPressed()
        {
            AfterOnEnterPressed();
        }

        public virtual void AfterOnEnterPressed() => m_onEnterPressed.Invoke();

        public virtual void OnTabPressed()
        {
            AddKey("    ");

            AfterOnTabPressed();
        }
        public virtual void AfterOnTabPressed() => m_onTabPressed.Invoke();

        public virtual void OnShiftPressed() => AfterOnShiftPressed();
        public virtual void AfterOnShiftPressed() => m_onShiftPressed.Invoke();

        public virtual void OnSpacePressed()
        {
            AddKey(" ");

            AfterOnSpacePressed();
        }
        public virtual void AfterOnSpacePressed() => m_onSpacePressed.Invoke();

        public virtual void OnBackSpacePressed() => AfterOnBackSpacePressed();
        public virtual void AfterOnBackSpacePressed() => m_onBackSpacePressed.Invoke();

        public virtual void OnSymbolPressed() => AfterOnSymbolPressed();
        public virtual void AfterOnSymbolPressed() => m_onSymbolPressed.Invoke();

        public virtual void OnKeyPressed(string input)
        {
            AddKey(input);

            AfterOnKeyPressed(input);
        }
        public virtual void AfterOnKeyPressed(string input) => m_onKeyPressed.Invoke(input);

        public virtual void AddKey(string input) { }

        #endregion KEY_EVENT

        #region FOUCUS_EVENET

        protected virtual void SwitchInputField(bool active)
        {
            if (m_keyborad.inputFieldBase != this)
            {
                if (active)
                {
                    var prev = m_keyborad.inputFieldBase;

                    m_keyborad.SwitchInputField(this);

                    if (prev != null)
                        prev.AfterOnFocus(false);
                }
            }
            else
            {
                if (!active)
                    m_keyborad.SwitchInputField(null);
            }
        }

        protected virtual void AfterOnFocus(bool active) => m_onFocus.Invoke(active);

        public virtual void OnFocus(bool active)
        {
            if (active == inputFieldIsActive)
                return;

            SwitchInputField(active);

            if (m_keyborad.mobile)
                m_keyborad.SetVisibility(active);

            m_onFocus.Invoke(active);
        }

        public virtual void OnFocus() => OnFocus(true);

        public virtual void OnSwitchFocus() => OnFocus(!inputFieldIsActive);

        #endregion FOUCUS_EVENT

        protected virtual void Start()
        {
            if (m_activeOnAwake)
            {
                SwitchInputField(true);

                if (m_keyborad.mobile)
                    m_keyborad.SetVisibility(true);
            }
        }

        protected virtual void Update() { }
    }
}