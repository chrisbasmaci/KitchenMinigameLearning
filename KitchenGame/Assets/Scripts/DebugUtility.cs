
using System;
using System.Collections.Generic;
using UnityEngine;

public class DebugUtility
{
    public enum DebugType
    {
        Level1,
        Level2,
        Level3,
        Level4,
        Level5
    }

    public static readonly Dictionary<DebugType, (string Identifier, Color TextColor)> DebugDictionary =
        new Dictionary<DebugType, (string Identifier, Color TextColor)>
        {
            { DebugType.Level1, ("-", Color.white) },               
            { DebugType.Level2, ("--", new Color(0.5f, 0.75f, 1f)) }, 
            { DebugType.Level3, ("***", Color.yellow) },            
            { DebugType.Level4, ("####", new Color(1f, 0.65f, 0f)) }, 
            { DebugType.Level5, ("!!!!!", Color.red) }             
        };
    
    public static void LogWithColor(string message, Color? color = null)
    {
        Color logColor = color ?? Color.green; 
        string hexColor = ColorUtility.ToHtmlStringRGBA(logColor); 
        Debug.Log($"<color=#{hexColor}>{message}</color>");
    }
    public static void LogStart(UnityEngine.Object unityObject)
    {
        (string Identifier, Color TextColor) currentLevel = DebugDictionary[DebugType.Level1];
        String msg = currentLevel.Identifier + unityObject.name + "'s Script Is Running" + currentLevel.Identifier;
        LogWithColor(msg, currentLevel.TextColor);
    }
    
    public static void LogEnumButton(Enum input)
    {
        (string Identifier, Color TextColor) currentLevel = DebugDictionary[DebugType.Level2];
        string msg = $"{currentLevel.Identifier} {input} Pressed {currentLevel.Identifier}";
        LogWithColor(msg, currentLevel.TextColor);
    }

    public static void LogSoftAssert(String msg)
    {
        (string Identifier, Color TextColor) currentLevel = DebugDictionary[DebugType.Level3];
        msg = currentLevel.Identifier + msg + currentLevel.Identifier;
        LogWithColor(msg, currentLevel.TextColor);
    }

    
    
}
