using UnityEngine;
using UnityEngine.UI;

public class TLabVKeyboradSample : MonoBehaviour
{
    [SerializeField] Text isThisMobile;

#if !UNITY_EDITOR && UNITY_WEBGL
    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern bool IsMobile();
#endif

    void Start()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        isThisMobile.text = IsMobile() ? "Mobile" : "PC";
#endif
    }
}
