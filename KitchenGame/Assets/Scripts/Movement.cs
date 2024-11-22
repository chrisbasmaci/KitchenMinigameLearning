using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
#pragma warning restore CS8524 // Restores the warning


public class Movement
{
    public static readonly float DefaultMoveSpeed = 7f;
    public static readonly InputLayout[] inputLayoutsArr = (InputLayout[])Enum.GetValues(typeof(Movement.InputLayout));

    public enum InputLayout //If you update InputLayout, also update the stuff below to handle it right!
    {
        Forward,
        Left,
        Right,
        Back,
        Jump
    }
    
    public static readonly Dictionary<InputLayout, KeyCode> DefaultKeyMapping = CreateDefaultKeyMapping();

    private static Dictionary<InputLayout, KeyCode> CreateDefaultKeyMapping()
    {
        var mapping = new Dictionary<InputLayout, KeyCode>();

        foreach (var key in inputLayoutsArr)
        {
            mapping[key] = GetKeyCode(key);
        }

        return mapping;

        static KeyCode GetKeyCode(InputLayout key) => key switch
        {
            InputLayout.Forward => KeyCode.W,
            InputLayout.Left    => KeyCode.A,
            InputLayout.Right   => KeyCode.D,
            InputLayout.Back    => KeyCode.S,
            InputLayout.Jump    => KeyCode.Space,
            _ => LogAndReturnDefault(key)
        };
        static KeyCode LogAndReturnDefault(InputLayout key)
        {
            Debug.LogWarning("Unhandled enum value: " + key);
            return KeyCode.None;
        }

    }

    public struct InputLayoutCover
    {
        private Action _forward;
        private Action _left;
        private Action _right;
        private Action _back; 
        private Action _jump;
        private Action[] _actions;

        public InputLayoutCover(Action forward, Action left, Action right, Action back, Action jump)
        {
            var paramCount = MethodBase.GetCurrentMethod().GetParameters().Length;
            if (paramCount != inputLayoutsArr.Length)
            {
                Debug.LogWarning("Unhandled enum values in ActionMapping");
            }
            _forward = forward;
            _left = left;
            _right = right;
            _back = back;
            _jump = jump;
            _actions = new[] { _forward, _left, _right, _back, _jump };

            if (_actions.Length != inputLayoutsArr.Length)
            {
                Debug.LogWarning("_actions need the variable added");
            }
        }

        public Action Forward => _forward;
        public Action Left => _left;
        public Action Right => _right;
        public Action Back => _back;
        public Action Jump => _jump;
        
        public Action[] ActionsArr => _actions;
    }

    public static Dictionary<InputLayout, Action> GetActionMapping
        (InputLayoutCover inputLayoutCover)
    {
        var dict = new Dictionary<InputLayout, Action>();
        for (int i = 0; i < inputLayoutCover.ActionsArr.Length; i++)
        {
            dict.Add(inputLayoutsArr[i], inputLayoutCover.ActionsArr[i]);
        }
        return dict;
    }
    



}

 
