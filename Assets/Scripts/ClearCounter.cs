using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Playerr playerr)
    {
        if (!HasKitchenObject())
        {
            //Counter khong co kitchenObject
            if (playerr.HasKitchenObject())
            {
                //player dang giu kitchenObject
                playerr.GetKitchenObject().SetKitchenObjectParent(this);
                Debug.Log(GetKitchenObject().GetKitchenObjectParent());
            }
            else
            {
                    //player khong giu kitchenObject
            }
        }
        else
        {
            //Counter co kitchenObject
            if (!playerr.HasKitchenObject())
            {
                //player khong co kitchenObject
                GetKitchenObject().SetKitchenObjectParent(playerr);
            }
        }
        //if (kitchenObject == null)
        //{
        //    Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, kitchenObjectHoldPoint);
        //    kitchenObjectTransform.localPosition = Vector3.zero;

        //    kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>(); //dinh nghia kitchenObject, setParent cho kitchenObject, chi dc spawn tren Counter 1 lan.
        //    kitchenObject.SetKitchenObjectParent(this);
        //}
    }
    
    
}  
