using UnityEngine;

public class ItemPickup : Interactable {


	public override void Interact()
	{
		base.Interact ();
	  
			PickUp (); 
	} 

	GameObject invholder;

	void Awake() {
		invholder = GameObject.FindGameObjectWithTag ("InventoryHolder"); 	
	}

	void moveUnderPlayer()
	{
		gameObject.transform.SetParent (invholder.transform); 
		gameObject.GetComponent<OnPickUpHandler> ().OnPickedUp = true;
		gameObject.GetComponent<OnPickUpHandler> ().CheckOnPickup();
	    gameObject.SetActive (false);
	}
 

	void PickUp()
	{ 
		Inventory.instance.Add (item, gameObject); 

		bool wasPickedUp = Inventory.instance.WasPickedUp;
		if (wasPickedUp) {
			moveUnderPlayer ();  
		}
	}














































}
