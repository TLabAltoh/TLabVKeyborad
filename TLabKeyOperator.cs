using UnityEngine;

public class TLabKeyOperator : MonoBehaviour
{
    [SerializeField] KeybordOperator keybordOperator;

    private TLabInputField inputField;

    public void SetTLabInputField(TLabInputField inputField)
    {
        this.inputField = inputField;
        // Debug.Log(gameObject.name + ": TLabInputField registed");
    }

    public void Press()
    {
        TLabVKeyboradAudio.instance.KeyAudio();
        inputField.OperatorEvent(keybordOperator);
    }
}
