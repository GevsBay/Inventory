using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler {

	public Image icon;
	public Button removeButton;
	public Button itemButton;
	public Text amountText;
	public Item item;
	public GameObject gameobj;
 
	public List<GameObject> StackableGameObjects = new List<GameObject> ();

	public int amount;

	public InteractableObject intobject;
  
	public int slotID; 
 
   
	private Vector2 offset;
	Inventory inventory;

	Tooltip tooltip;

	void Start(){
		inventory = Inventory.instance; 
		if(inventory.checkIfGameObjectIsinInventory(gameobj))
		itemButton.targetGraphic = inventory.slots [slotID].GetComponent<Image> ();
		 
		InventoryUI.instance.OnInventoryShownCallback += HideRemoveButton ;
		tooltip = inventory.GetComponent<Tooltip> ();
	}

	void HideRemoveButton (){ 
		if(this.removeButton != null)
			this.removeButton.interactable = false;
	}
 
	public void AddItem(Item nitem, GameObject newGO)
	{ 
		item = nitem;
		gameobj = newGO; 
		intobject = gameobj.GetComponent<InteractableObject> ();
		icon.sprite = nitem.icon;
		icon.enabled = true;  
	}
    
	public void OnRemoveButton()
	{
		inventory.Remove (item, gameobj); 
	}

	public void SetItemtodefaultState(GameObject gameobjt){

		gameobjt.GetComponent<OnPickUpHandler> ().OnPickedUp = false;
		gameobjt.GetComponent<OnPickUpHandler> ().CheckOnPickup();
		gameobjt.transform.parent = null;
		GameObject invHolder = GameObject.FindGameObjectWithTag ("InventoryHolder");
		gameobjt.transform.position = invHolder.transform.position;
		gameobjt.SetActive (true);
	}

	public void UseItem() 
	{
		if (inventory.checkIfGameObjectIsinInventory(gameobj)) {
			   
				if (StackableGameObjects.Count != 0) {
					int index = amount; 
					index--;
					intobject = StackableGameObjects [index].GetComponent<InteractableObject> ();
					intobject.Use ();   
				} else {
					intobject = gameobj.GetComponent<InteractableObject> ();
					intobject.Use (); 
				}
			  
			HideRemoveButton ();
		}
	     
	}
    

	public void OnBeginDrag(PointerEventData ped){
		
		this.transform.SetParent (this.transform.parent.parent);
		removeButton.interactable = false;
		GetComponent<CanvasGroup> ().blocksRaycasts = false; 

	}

	public void OnDrag(PointerEventData ped){
		
		this.transform.position = ped.position - offset; 

	}

	public void OnEndDrag(PointerEventData ped){ 
		if (inventory.checkIfGameObjectIsinInventory (gameobj)) {
			this.transform.SetParent (inventory.slots [slotID].transform); 
			this.transform.position = inventory.slots [slotID].transform.position;  
			itemButton.targetGraphic = inventory.slots [slotID].GetComponent<Image> ();
		}
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}

	public void OnPointerDown(PointerEventData ped){
 
		offset = ped.position - new Vector2 (this.transform.position.x, this.transform.position.y);  
		this.transform.position = ped.position - offset; 
	}
 

	public void OnPointerEnter(PointerEventData ped){ 
		if(inventory.checkIfGameObjectIsinInventory(gameobj))
		removeButton.interactable = true;
		
		 
			if (StackableGameObjects.Count != 0) {
				int index = amount;
				index--;
				intobject = StackableGameObjects [index].GetComponent<InteractableObject> ();
            intobject.ShowTooltip("<color=#0473f0>" + item.name + "</color>\n\n" + item.description);
			} else { 
				intobject = gameobj.GetComponent<InteractableObject> ();
				intobject.ShowTooltip ("<color=#0473f0>" + item.name + "</color>\n\n" + item.description);
			}
		}
     

	public void OnPointerExit(PointerEventData ped){
		if(inventory.checkIfGameObjectIsinInventory(gameobj))
		removeButton.interactable = false;
		
		tooltip.DeActivate ();

	}
 














}
