using UnityEngine;
using TMPro;

public class TLabKey : MonoBehaviour
{
    [SerializeField] string key;
    [SerializeField] string shiftsKey;
    [SerializeField] TextMeshProUGUI keyText;
    private TLabInputField inputField;
    private bool isShiftOn = false;

    public void SetTLabInputField(TLabInputField inputField)
    {
        this.inputField = inputField;
        // Debug.Log(gameObject.name + ": TLabInputField registed");
    }

    public void Press()
    {
        TLabVKeyboradAudio.instance.KeyAudio();
        inputField.AddKey(this.keyText.text);
    }

    public void ShiftPressed()
    {
        isShiftOn = !isShiftOn;
        keyText.text = isShiftOn == true ? shiftsKey : key;
    }
}
