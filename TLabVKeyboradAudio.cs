using UnityEngine;

public class TLabVKeyboradAudio : MonoBehaviour
{
    [Header("RockButtonClick")]
    [SerializeField] AudioSource rockButtonAudio;
    [Header("KeyClick")]
    [SerializeField] AudioSource keyAudio;

    public static TLabVKeyboradAudio instance;

    public void RockButtonAudio()
    {
        rockButtonAudio.Play();
    }

    public void KeyAudio()
    {
        keyAudio.Play();
    }

    void Start()
    {
        instance = this;
    }
}
