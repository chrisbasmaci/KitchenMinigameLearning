using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public ClearCounter ClearCounter { get; private set; }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            // ClearCounter.ClearKitchenObject();
            
            ChangeOwner(GameController.Instance.testCounter);
            // GameController.Instance.testCounter.SetKitchenObject(this);
        }

    }

    public void Initialize(ClearCounter clearCounter)
    {
        SetClearCounter(clearCounter);
    }

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetClearCounter(ClearCounter newCounter)
    {
        
        if (ClearCounter != null) 
        {
            Debug.LogError("This object Already has an owner");
            return;
        }
        ClearCounter = newCounter;

        
        ClearCounter.SetKitchenObject(this);
        
        transform.parent = ClearCounter.CounterTopPoint.transform;
        transform.localPosition = Vector3.zero;
    }

    public void ChangeOwner(ClearCounter newOwner)
    {
        if (newOwner.HasObjectOnTop)
        {
            Debug.LogError("This Counter to Move Already has an object on top : " + newOwner);
            return;
        }
        ClearCounter.ClearKitchenObject();
        ClearCounter = null;
        SetClearCounter(newOwner);
    }


}
