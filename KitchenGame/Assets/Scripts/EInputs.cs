using System;
using System.Collections.Generic;
using UnityEngine;

public enum InputTag
{
    Walking,
    Action
}
public class EInputs<T>
{
    public static readonly EInputs<Vector2> Up = new EInputs<Vector2>(KeyCode.W, InputTag.Walking);
    public static readonly EInputs<Vector2> Left = new EInputs<Vector2>(KeyCode.A, InputTag.Walking);
    public static readonly EInputs<Vector2> Down = new EInputs<Vector2>(KeyCode.S, InputTag.Walking);
    public static readonly EInputs<Vector2> Right = new EInputs<Vector2>(KeyCode.D, InputTag.Walking);
    public static readonly EInputs<bool> Interact = new EInputs<bool>(KeyCode.E, InputTag.Action);

    private KeyCode _key;
    private Func<T> _action;
    private Func<T> _releaseAction;
    private InputTag _tag;
    private bool _isActive;
    public bool IsActive => _isActive;

    private EInputs(KeyCode key, InputTag tag, Func<T> releaseAction = null, bool isActive = true)
    {
        _releaseAction = releaseAction;
        Debug.Log($"Constructor called for key: {key}, tag: {tag}");
        _tag = tag;
        _isActive = isActive;
        _key = key;
    }

    private static void AddIfNotExists<T>(List<EInputs<T>> list, EInputs<T> newItem)
    {
        foreach (var item in list)
        {
            if (item.Key == newItem.Key)
            {
                return; // Skip adding if the key already exists
            }
        }
        list.Add(newItem);
    }



    public void SetAction(Func<T> action)
    {
        Debug.Log("Set Action: " + action.Method.Name);
        _action = action;
    }
    public void SetReleaseAction(Func<T> action)
    {
        Debug.Log("Set Action: " + action.Method.Name);
        _releaseAction = action;
    }
    public Func<T> GetAction()
    {
        if (_action == null)
        {
            
            Debug.LogWarning("NO ACTION for key: " + _key);
        }
        return _action;
    }
    public bool ListenAction()
    {
        if (_action == null)
        {
            Debug.LogWarning("NO ACTION for key: " + _key);
            return false;
        }
        
        if (Input.GetKey(_key))
        {
            _action.Invoke();
        }else if (Input.GetKeyUp(_key))
        {
            _releaseAction?.Invoke();
        }
    
        return true;
    }

    public KeyCode Key => _key;

    public void ChangeKey(KeyCode newKey)
    {
        _key = newKey;
    }
}

