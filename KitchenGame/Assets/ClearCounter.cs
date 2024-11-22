using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private Transform counterTopPoint;
    public Transform CounterTopPoint => counterTopPoint;

    [SerializeField] private GameObject selectedVisual;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public bool HasObjectOnTop => KitchenObject != null;

    public KitchenObject KitchenObject {get; private set;}

    [SerializeField] private Material[] counterSkins;
    // Start is called before the first frame update
    public void Interact(Player player)
    {
        DebugUtility.LogWithColor("Interact Pressed for Counter: "+ name, Color.green);
        selectedVisual.SetActive(true);

        if (!HasObjectOnTop && !player.HoldingObject)
        {
            DebugUtility.LogWithColor("Added Object on top: "+ kitchenObjectSO.name, Color.green);
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, CounterTopPoint);
            kitchenObjectTransform.localPosition = Vector3.zero;

            KitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            KitchenObject.Initialize(this);
        }else
        {
            if (!KitchenObject)
            {
                if (player.HoldingObject)
                {
                    player.HoldingObject.ChangeOwner(this);
                }
            }
            else
            {
                KitchenObject.ChangeOwner(player);
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
        KitchenObject = kitchenObject;
    }

    public void ClearKitchenObject()
    {
        KitchenObject = null;
    }

}
