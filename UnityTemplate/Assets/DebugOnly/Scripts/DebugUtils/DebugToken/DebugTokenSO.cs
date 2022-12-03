using UnityEngine;

[CreateAssetMenu(fileName = "DebugTokenSO", menuName = "Scriptable Object/DebugTokenSO")]
public class DebugTokenSO : ScriptableObject
{
    [Header("Data")]
    public TokenModel debugToken;

    [Header("Debug Option")]
    public bool isDebugMode = false;
}
