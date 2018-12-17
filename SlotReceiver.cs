using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.EventSystems;

public class SlotReceiver : MonoBehaviour, IDropHandler {

	private Inventory inventroy;
	public int id;

	void Start() {
		inventroy = Inventory.instance;
	}
	 
	public void OnDrop(PointerEventData ped){
		InventorySlot slot = ped.pointerDrag.GetComponent<InventorySlot> (); 
		if (inventroy.checkIfGameObjectIsinInventory (slot.gameobj)) {

			if (this.transform.childCount == 0) {
				inventroy.gameobjects [slot.slotID] = null;
				inventroy.gameobjects [id] = slot.gameobj;
				inventroy.items [slot.slotID] = null;
				inventroy.items [id] = slot.item;
				slot.slotID = id; 
			} else {
				Transform item = this.transform.GetChild (0);
				InventorySlot itemsslot = item.GetComponent<InventorySlot> ();
				itemsslot.slotID = slot.slotID;
				item.transform.SetParent (inventroy.slots [slot.slotID].transform);
				item.transform.position = inventroy.slots [slot.slotID].transform.transform.position;

		
				slot.transform.SetParent (this.transform);
				slot.transform.position = this.transform.position;

				inventroy.gameobjects [slot.slotID] = itemsslot.gameobj;
				inventroy.gameobjects [id] = slot.gameobj;
				inventroy.items [slot.slotID] = itemsslot.item;
				inventroy.items [id] = slot.item;
				slot.slotID = id;
			}
		} else {
			if (this.transform.childCount == 0) { 
				inventroy.gameobjects [id] = slot.gameobj; 
				inventroy.items [id] = slot.item; 
				slot.slotID = id;  
			}  

			inventroy.UpdateCurrentWeightofInventory ();
			inventroy.CurrentAmountofobjectsInInvetory++;
		}
	}



































}
