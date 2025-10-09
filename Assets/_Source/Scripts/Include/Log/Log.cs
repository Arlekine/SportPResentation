using System.Diagnostics;
using Debug = UnityEngine.Debug;

public static class Log
{
    public static void Info(string text)
    {
        Debug.Log(Prepare("INFO", text));
    }

    public static void Warning(string text)
    {
        Debug.LogWarning(Prepare("WARNING", text));
    }

    public static void Error(string text)
    {
        Debug.LogError(Prepare("ERROR", text));
    }

    static string Prepare(string type, string text)
    {
        var method2 = new StackTrace().GetFrame(2).GetMethod();
        var className2 = method2.ReflectedType.Name;

        var className = className2;

        if (className.StartsWith("<>")) // Если вызов из колбэка
        {
            // We need to go deeper...
            var method3 = new StackTrace().GetFrame(3).GetMethod();
            var className3 = method3.ReflectedType.Name;
            className = className3;
        }

        var time = System.DateTime.Now.ToString("HH:mm:ss.ms");
        
        return $"[{time}] {type} [{className}] {text}";
    }
}
