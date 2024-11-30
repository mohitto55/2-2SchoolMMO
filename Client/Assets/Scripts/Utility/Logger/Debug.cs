using Unity.VisualScripting;

public static class Debug
{
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Log(object message) =>
           UnityEngine.Debug.Log(message);

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogWarning(object message) =>
       UnityEngine.Debug.LogWarning(message);

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogError(object message) =>
        UnityEngine.Debug.LogError(message);

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void DrawRay(UnityEngine.Vector3 start, UnityEngine.Vector3 end) =>
        UnityEngine.Debug.DrawRay(start, end);

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void DrawLine(UnityEngine.Vector3 start, UnityEngine.Vector3 end) =>
        UnityEngine.Debug.DrawLine(start, end);

}