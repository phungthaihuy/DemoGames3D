using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interact(Playerr playerr)
    {
        if (playerr.HasKitchenObject())
        {
            playerr.GetKitchenObject().DestroyKitchenObject();
        }
        else
        {
            Debug.Log("didn't have any kitchen object");
        }
    }
}
