using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TLab.VKeyborad
{
    public class SimpleInputField : InputFieldBase
    {
        [Header("Text (TMPro)")]
        [SerializeField] protected TextMeshProUGUI m_inputText;
        [SerializeField] protected TextMeshProUGUI m_placeholder;

        [Header("Button")]
        [SerializeField] protected Button m_focusButton;

        [System.NonSerialized] public string text = "";

        #region KEY_EVENT

        public override void OnBackSpacePressed()
        {
            if (text != "")
            {
                text = text.Remove(text.Length - 1);
                Display();
            }

            base.OnBackSpacePressed();
        }

        public override void OnSpacePressed()
        {
            AddKey(" ");

            base.OnSpacePressed();
        }

        public override void OnTabPressed()
        {
            AddKey("    ");

            base.OnTabPressed();
        }

        public override void OnKeyPressed(string input)
        {
            AddKey(input);

            base.OnKeyPressed(input);
        }

        #endregion KEY_EVENT

        #region FOUCUS_EVENET

        public override void OnFocus(bool active)
        {
            if (active == inputFieldIsActive)
                return;

            SwitchInputField(active);

            m_focusButton.enabled = !active;

            if (m_keyborad.mobile)
                m_keyborad.SetVisibility(active);

            m_onFocus.Invoke(active);
        }

        protected override void OnInputFieldSwitched(bool active)
        {
            m_focusButton.enabled = !active;
            base.OnInputFieldSwitched(active);
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
            m_inputText.text = text;

            SwitchPlaseholder();
        }

        public override void AddKey(string key)
        {
            text += key;
            Display();
        }

        public void SetPlaceHolder(string text)
        {
            this.text = "";
            m_placeholder.text = text;
            Display();
        }

        protected override void Start()
        {
            base.Start();

            text = m_inputText.text;

            SwitchPlaseholder();
        }
    }
}
