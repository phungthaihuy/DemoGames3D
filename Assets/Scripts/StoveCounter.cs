using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingKitchenObjectSO[] fryingKitchenObjectSOArray;
    [SerializeField] private BurnedKitchenObjectSO[] burnedKitchenObjectSOArray;

    public enum State { Idle, Frying, Fried, Burned };
    private State state;

    private FryingKitchenObjectSO fryingKitchenObjectSO;
    private float fryingTimer;
    private BurnedKitchenObjectSO burnedKitchenObjectSO;
    private float burningTimer;

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Frying:
                fryingTimer += Time.deltaTime;

                if (fryingTimer > fryingKitchenObjectSO.maxFryingProgress)
                {
                    KitchenObjectSO outputKitchenObjectSO = GetOutputFryingKitchenObjectSO(GetKitchenObject().GetKitchenObjectSO());

                    GetKitchenObject().DestroyKitchenObject();

                    Transform kitchenObjectTransform = Instantiate(outputKitchenObjectSO.prefab);// spawn kitchenObjectSO output
                    kitchenObjectTransform.localPosition = Vector3.zero;
                    kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this); //setParent cho kitchenObjectSO output 

                    state = State.Fried;
                    burningTimer = 0f;
                    burnedKitchenObjectSO = GetBurningKitchenObjectSO(this.GetKitchenObject().GetKitchenObjectSO());
                }
                
                break;
            case State.Fried:
                burningTimer += Time.deltaTime;

                if (burningTimer > burnedKitchenObjectSO.maxBurnedProgress)
                {
                    KitchenObjectSO outputKitchenObjectSO = GetOutputBurningKitchenObjectSO(GetKitchenObject().GetKitchenObjectSO());

                    GetKitchenObject().DestroyKitchenObject();

                    Transform kitchenObjectTransform = Instantiate(outputKitchenObjectSO.prefab);// spawn kitchenObjectSO output
                    kitchenObjectTransform.localPosition = Vector3.zero;
                    kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this); //setParent cho kitchenObjectSO output 

                    state = State.Burned;
                }
                break;
            case State.Burned:
                break;
            default:
                break;
        }

    }

    public override void Interact(Playerr playerr)
    {
        
        if (!HasKitchenObject())
        {
            //Counter khong co kitchenObject
            if (playerr.HasKitchenObject())
            {
                if (HasKitchenObjectSOCanFried(playerr.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //player dang giu kitchenObject co the nuong dc
                    playerr.GetKitchenObject().SetKitchenObjectParent(this);

                    fryingKitchenObjectSO = GetFryingKitchenObjectSO(GetKitchenObject().GetKitchenObjectSO());
                    burnedKitchenObjectSO = GetBurningKitchenObjectSO(this.GetKitchenObject().GetKitchenObjectSO());

                    state = State.Frying;
                    fryingTimer = 0f;                
                }
                else
                {
                    Debug.Log("This kitchenObject can't fry!");
                }
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
    }

    private bool HasKitchenObjectSOCanFried(KitchenObjectSO kitchenObjectSO)
    {
        FryingKitchenObjectSO fryingKitchenObjectSO = GetFryingKitchenObjectSO(kitchenObjectSO);
        return fryingKitchenObjectSO != null;
    }

    private KitchenObjectSO GetOutputFryingKitchenObjectSO(KitchenObjectSO kitchenObjectSO) // lay cuttingKitchenObjectSO.output
    {
        FryingKitchenObjectSO fryingKitchenObjectSO = GetFryingKitchenObjectSO(kitchenObjectSO);
        if (fryingKitchenObjectSO != null)
        {
            return fryingKitchenObjectSO.output;
        }
        return null;
    }

    private KitchenObjectSO GetOutputBurningKitchenObjectSO(KitchenObjectSO kitchenObjectSO) // lay cuttingKitchenObjectSO.output
    {
        BurnedKitchenObjectSO burningKitchenObjectSO = GetBurningKitchenObjectSO(kitchenObjectSO);
        if (burningKitchenObjectSO != null)
        {
            return burningKitchenObjectSO.output;
        }
        return null;
    }

    private FryingKitchenObjectSO GetFryingKitchenObjectSO(KitchenObjectSO kitchenObjectSO) // lay cuttingKitchenObjectSO can slices
    {
        foreach (FryingKitchenObjectSO fryingKitchenObjectSO in fryingKitchenObjectSOArray)
        {
            if (fryingKitchenObjectSO.input == kitchenObjectSO)
            {
                return fryingKitchenObjectSO;
            }
        }
        return null;
    }

    private BurnedKitchenObjectSO GetBurningKitchenObjectSO(KitchenObjectSO kitchenObjectSO) // lay cuttingKitchenObjectSO can slices
    {
        foreach (BurnedKitchenObjectSO burningKitchenObjectSO in burnedKitchenObjectSOArray)
        {
            if (burningKitchenObjectSO.input == kitchenObjectSO)
            {
                return burningKitchenObjectSO;
            }
        }
        return null;
    }
}
