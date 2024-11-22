using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IHoldable<KitchenObject>
{
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private GameObject selectedVisual;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public bool HasObjectOnTop => _holdingObject != null;

    private KitchenObject _holdingObject;

    [SerializeField] private Material[] counterSkins;
    // Start is called before the first frame update
    public void Interact(Player player)
    {
        DebugUtility.LogWithColor("Interact Pressed for Counter: "+ name, Color.green);
        selectedVisual.SetActive(true);

        if (!HasObjectOnTop && !player.HoldingObject())
        {
            DebugUtility.LogWithColor("Added Object on top: "+ kitchenObjectSO.name, Color.green);
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, HoldPoint());
            kitchenObjectTransform.localPosition = Vector3.zero;

            kitchenObjectTransform.GetComponent<KitchenObject>().Initialize(this);
        }
        else
        {
            if (HoldingObject())
            {
                DebugUtility.LogWithColor("Holding Object "+ HoldingObject().name);
                HoldingObject().ChangeOwner(player);
            }
            else if (player.HoldingObject())
            {
                DebugUtility.LogWithColor("Player Holding Object "+ player.HoldingObject().name);
                player.HoldingObject().ChangeOwner(this);
            }
        }

    }
    public void InteractOut()
    {
        DebugUtility.LogWithColor("Interact Released for Counter: "+ name, Color.green);
        selectedVisual.SetActive(false);
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        _holdingObject = kitchenObject;
    }

    public void ClearKitchenObject()
    {
        _holdingObject = null;
    }
    
    
    public KitchenObject HoldingObject()
    {
        return _holdingObject;
    }

    public void HoldObject(KitchenObject toHold)
    {
        if (_holdingObject != null)
        {
            Debug.LogWarning("Already holding: " + _holdingObject.name);
        }
        _holdingObject = toHold;
    }

    public void DropObject()
    {
        _holdingObject = null;
    }

    public Transform HoldPoint()
    {
        return counterTopPoint;
    }
    public string Name()
    {
        return name;
    }
}
