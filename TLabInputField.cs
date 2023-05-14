using System;
using UnityEngine;
using TMPro;

public class TLabInputField : MonoBehaviour
{
    [Header("KeyBOX")]
    [SerializeField] GameObject keyBOX;
    [SerializeField] GameObject romajiBOX;
    [SerializeField] GameObject symbolBOX;
    [SerializeField] GameObject operatorBOX;

    [Header("Text(TMPro)")]
    [SerializeField] TextMeshProUGUI inputText;
    [SerializeField] TextMeshProUGUI placeholder;

    [Header("Image")]
    [SerializeField] GameObject openImage;
    [SerializeField] GameObject rockImage;

    [Header("Button")]
    [SerializeField] GameObject inputFieldButton;

    [Header("HideObject")]
    [SerializeField] GameObject[] hideObjects;

    [Header("IsThisMobile")]
    [SerializeField] bool isMobile = false;

    [System.NonSerialized] public string text = "";

    private Action keyUpdate;
    private TLabKey[] keys;
    private TLabKeyOperator[] keyOperators;
    private bool inputFieldFocused = true;
    private float backSpaceKeyTime = 0.0f;

    private void SwitchKeyborad(bool isActive)
    {
        keyBOX.SetActive(isActive && isMobile);
        inputFieldFocused = isActive;
        openImage.SetActive(isActive);
        rockImage.SetActive(!isActive);
        inputFieldButton.SetActive(!isActive);

        if (isMobile == true)
            foreach (GameObject hideObject in hideObjects) hideObject.SetActive(!isActive);
    }

    public void CloseInputFieldButtonClicked()
    {
        SwitchKeyborad(false);
        TLabVKeyboradAudio.instance.RockButtonAudio();
    }

    public void InputFieldButtonClicked()
    {
        SwitchKeyborad(true);
        TLabVKeyboradAudio.instance.RockButtonAudio();
    }

    public void SetPlaceHolder(string text)
    {
        this.text = "";
        placeholder.text = text;
        Display();
    }

    private void OnBackSpacePressed()
    {
        if (text != "")
        {
            text = text.Remove(text.Length - 1);
            Display();
        }
    }

    private void OnEnterPressed()
    {
        //
    }

    private void OnShiftPressed()
    {
        foreach (TLabKey key in keys) key.ShiftPressed();
    }

    private void OnSpacePressed()
    {
        AddKey(" ");
    }

    private void OnSymbolPressed()
    {
        if (romajiBOX.activeSelf == true)
        {
            romajiBOX.SetActive(false);
            symbolBOX.SetActive(true);
        }
        else
        {
            romajiBOX.SetActive(true);
            symbolBOX.SetActive(false);
        }
    }

    private void OnTabPressed()
    {
        AddKey("    ");
    }

    public void OperatorEvent(KeybordOperator keybordOperator)
    {
        TLabVKeyboradAudio.instance.KeyAudio();
        switch (keybordOperator)
        {
            case KeybordOperator.BackSpace:
                OnBackSpacePressed();
                break;
            case KeybordOperator.Enter:
                OnEnterPressed();
                break;
            case KeybordOperator.Shift:
                OnShiftPressed();
                break;
            case KeybordOperator.Space:
                OnSpacePressed();
                break;
            case KeybordOperator.Symbol:
                OnSymbolPressed();
                break;
            case KeybordOperator.Tab:
                OnTabPressed();
                break;
        }
    }

    public void Display()
    {
        inputFieldFocused = true;
        inputText.text = text;
        if (inputText.text == "")
            placeholder.color = new Color(0.196f, 0.196f, 0.196f, 0.5f);
        else
            placeholder.color = new Color(0.196f, 0.196f, 0.196f, 0.0f);
    }

    public void AddKey(string key)
    {
        text += key;
        Display();
    }

    private void UpdateKeyboradInMobile()
    {
        //
    }

    private void UpdateKeyboradInPC()
    {
        backSpaceKeyTime += Time.deltaTime;

        if (inputFieldFocused && Input.anyKey == true)
        {
            TLabVKeyboradAudio.instance.KeyAudio();
            string inputString = Input.inputString;
            if (Input.GetKeyDown(KeyCode.Return)) OnEnterPressed();
            else if (Input.GetKeyDown(KeyCode.Tab)) OnTabPressed();
            else if (Input.GetKeyDown(KeyCode.Space)) OnSpacePressed();
            else if (Input.GetKey(KeyCode.Backspace) && backSpaceKeyTime > 0.1f)
            {
                OnBackSpacePressed();
                backSpaceKeyTime = 0.0f;
            }
            else if (inputString != "" && inputString != "") AddKey(inputString);
            else if (Input.GetMouseButtonDown(1)) AddKey(GUIUtility.systemCopyBuffer);
        }
    }

#if !UNITY_EDITOR && UNITY_WEBGL
    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern bool IsMobile();
#endif

    private bool CheckIfMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        isMobile = IsMobile();
#endif

#if UNITY_ANDROID
        isMobile = true;
#endif

        return isMobile;
    }

    private void Start()
    {
        if (CheckIfMobile() == true)
        {
            bool romajiBOXActive = romajiBOX.activeSelf;
            bool symbolBOXActive = symbolBOX.activeSelf;
            romajiBOX.SetActive(true);
            symbolBOX.SetActive(true);
            keys = keyBOX.GetComponentsInChildren<TLabKey>();
            foreach (TLabKey key in keys) key.SetTLabInputField(this);
            romajiBOX.SetActive(romajiBOXActive);
            symbolBOX.SetActive(symbolBOXActive);

            bool operatorBOXActive = operatorBOX.activeSelf;
            operatorBOX.SetActive(true);
            keyOperators = keyBOX.GetComponentsInChildren<TLabKeyOperator>();
            foreach (TLabKeyOperator keyOperator in keyOperators) keyOperator.SetTLabInputField(this);
            operatorBOX.SetActive(operatorBOXActive);

            keyUpdate = UpdateKeyboradInMobile;
        }
        else
        {
            keyUpdate = UpdateKeyboradInPC;
        }

        SwitchKeyborad(false);
    }

    private void Update()
    {
        keyUpdate();
    }
}

public enum KeybordOperator
{
    BackSpace,
    Enter,
    Shift,
    Symbol,
    Tab,
    Space
}
