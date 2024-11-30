using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace TLab.VKeyborad
{
    [AddComponentMenu("TLab/VKeyborad/InputField (TLab)")]
    public class InputField : BaseInputField
    {
        [Header("Event")]
        [SerializeField] protected UnityEvent<string> m_onValueChanged;

        [Header("Text (TMPro)")]
        [SerializeField] protected TextMeshProUGUI m_inputText;
        [SerializeField] protected TextMeshProUGUI m_placeholder;

        [Header("Button")]
        [SerializeField] protected Button m_focusButton;

        private string m_text = "";

        public string text
        {
            get => m_text;
            set
            {
                if (m_text != value)
                {
                    m_text = value;

                    Display();
                }
            }
        }

        #region KEY_EVENT

        protected override void HandlingOnBackSpaceKey()
        {
            if (m_text != "")
            {
                m_text = m_text.Remove(text.Length - 1);
                Display();
            }
        }

        #endregion KEY_EVENT

        #region FOUCUS_EVENET

        protected override void AfterOnFocus(bool active)
        {
            m_focusButton.enabled = !active;

            base.AfterOnFocus(active);
        }

        #endregion FOUCUS_EVENET

        protected void SwitchPlaseholder()
        {
            var color = m_placeholder.color;

            if (m_inputText.text == "")
            {
                color.a = 0.5f;
                m_placeholder.color = color;
            }
            else
            {
                color.a = 0f;
                m_placeholder.color = color;
            }
        }

        public void Display()
        {
            m_inputText.text = m_text;

            SwitchPlaseholder();

            m_onValueChanged.Invoke(m_text);
        }

        public override void AddKey(string key)
        {
            m_text += key;
            Display();
        }

        public void SetPlaceHolder(string text)
        {
            m_text = "";
            m_placeholder.text = text;
            Display();
        }

        protected override void Start()
        {
            base.Start();

            m_text = m_inputText.text;

            SwitchPlaseholder();
        }
    }
}
