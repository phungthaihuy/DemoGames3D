using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField] private Transform kitchenObjectHoldPoint;

    private KitchenObject kitchenObject;

    public virtual void Interact(Playerr playerr) { }

    public virtual void InteractAlternate(Playerr playerr) { }
    public Transform GetKitchenObjectFollower()
    {
        return kitchenObjectHoldPoint;
    }

    public void ClearKitchenObject() 
    {
        kitchenObject = null;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
