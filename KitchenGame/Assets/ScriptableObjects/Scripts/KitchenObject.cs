using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public ClearCounter owningClearCounter { get; private set; }
    public Player owningPlayer { get; private set; }




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
        if (owningClearCounter != null) 
        {
            Debug.LogError("This object Already has an owner");
            return;
        }
        owningClearCounter = newCounter;

        
        owningClearCounter.SetKitchenObject(this);
        
        transform.parent = owningClearCounter.CounterTopPoint.transform;
        transform.localPosition = Vector3.zero;
    }

    public void ChangeOwner<T>(T newOwner)
    {
        if (owningClearCounter)
        {
            owningClearCounter.ClearKitchenObject();
            owningClearCounter = null;
        }else if (owningPlayer)
        {
            owningPlayer.DropObject();
            owningPlayer = null;
        }


        if (newOwner is ClearCounter clearCounter)
        {
            ChangeOwnerClearCounter(clearCounter);
        }else if (newOwner is Player player)
        {
            ChangeOwnerPlayer(player);
        }
        else
        {
            Debug.LogError("Cant Move to this object type");
        }
    }

    private void ChangeOwnerClearCounter(ClearCounter counterToMove)
    {

        if (counterToMove.HasObjectOnTop)
        {
            Debug.LogError("This Counter to Move Already has an object on top : " + counterToMove.name);
            return;
        }
        
        SetClearCounter(counterToMove);

    }

    private void ChangeOwnerPlayer(Player playerToHold)
    {
        if (owningPlayer)
        {
            DebugUtility.LogWithColor("Changing from player: " + owningPlayer.name + "to" + playerToHold.name);
        }
        if (playerToHold.HoldingObject)
        {
            Debug.LogError("This Player already holds the object: " + playerToHold.HoldingObject.name);
        }
        SetPlayer(playerToHold);
    }

    public void SetPlayer(Player player)
    {

        
        owningPlayer = player;
        owningPlayer.HoldObject(this);
        
        transform.parent = player.HoldPoint.transform;
        transform.localPosition = Vector3.zero;
    }



}
