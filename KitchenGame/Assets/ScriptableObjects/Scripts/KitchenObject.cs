using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public IHoldable<KitchenObject> owner { get; private set; }




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
        SetOwner(clearCounter);
    }

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
    

    public void SetOwner(IHoldable<KitchenObject> newOwner)
    {
        if (owner != null)
        {
            Debug.LogError("Object Already has an owner");
        }
        owner = newOwner;
        owner.HoldObject(this);
        DebugUtility.LogWithColor("New Owner is: " + owner.Name(), Color.red);
    }
    public void ChangeOwner(IHoldable<KitchenObject> newOwner)
    {
        if (newOwner.HasObjectOnTop)
        {
            Debug.LogError("New owner:"+newOwner.Name()+" already has an object on top");
            return;
        }
        //if we have an old owner we have to be on top of it, just for checks
        if (owner != null && owner.HoldingObject() != this)
        {
            DebugUtility.LogWithColor("Owner: " + owner.Name());
            Debug.LogError("Owner already has an object on top");
            return;
        }


        DebugUtility.LogWithColor("About to drop");
        if (owner != null)
        {
            DebugUtility.LogWithColor(owner.Name() + "Dropped " + this.name);
            owner.DropObject();
            owner = null;
        }
        newOwner.HoldObject(this);

        transform.parent = newOwner.HoldPoint().transform;
        transform.localPosition = Vector3.zero;
        owner = newOwner;
        DebugUtility.LogWithColor("New Owner is: " + owner.Name(), Color.red);

    }
    
}
