using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {

    Inventory inventory;

    public virtual void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Inventory>();
    }

    public virtual void ShowTooltip (string description){
        inventory.tooltip.ConstructItemDescription(description);
    }

	public virtual void Use (){
        Debug.Log("USED");
    }

	public virtual void Unequip (){
	}
}
 
