using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public static Inventory instance;

	public bool isShown = false;
	public bool WasPickedUp = false;
 

	public GameObject InventoryUI;

    #region Instancing
      
    void Awake(){
		if (instance != null)
			return;
		
		instance = this;
	}  
    #endregion

    public List<Item> items = new List<Item>(); 
	public List<GameObject> gameobjects  = new List<GameObject>(); 

	public delegate void OnItemChanged ();
	public OnItemChanged onItemChangedCallback;

	public int space = 20;
	public int CurrentAmountofobjectsInInvetory = 0;
 
	public float currentWeightinGrams;
	public float MaxWeightinGrams;

	[Header("Inventroy's Slot's Settings")]

	#region slots
	public Transform itemsParent;  
	public GameObject Slot;
	public GameObject SlotPanel;
	public GameObject InventorySlotgo; 

	public bool invloadLevel1 = false;
	public bool invloadLevel2 = false;
	public bool invloadLevel3 = false;

	public bool invloadLevelNormal = true;

	public bool OnLevelChanged = false;

	public List<GameObject> slots = new List<GameObject>();
	 
	public Tooltip tooltip;

	void Start() {

        InventoryUI = UiDatabase.instance.inventoryUiPanel;
        SlotPanel = UiDatabase.instance.SlotPanel;
        itemsParent = UiDatabase.instance.itemsParent;  

        for (int i = 0; i < space; i++) {
			items.Add (null);
			gameobjects.Add (null);
			slots.Add(Instantiate(Slot));
			slots [i].GetComponent<SlotReceiver> ().id = i;
			slots [i].transform.SetParent (SlotPanel.transform);
		}

		tooltip = GetComponent<Tooltip> ();


	}

 
	public float Weight(){
		float roundedCurrentWeight = Mathf.Round (currentWeightinGrams / 10);
		float currentWeightinKilos = Mathf.Round (roundedCurrentWeight) / 100f;
		return currentWeightinKilos; 
	}


	#endregion

	public void Add (Item itemtoAdd, GameObject gameobjtoAdd){

		if (currentWeightinGrams >= MaxWeightinGrams) {
			WasPickedUp = false;
			return;
		}

		if (itemtoAdd.isStackable && checkIfItemIsinInventory (itemtoAdd)) {
		   
			for (int i = 0; i < items.Count; i++) {
				if (items [i] == itemtoAdd) {
					InventorySlot inventoryslotscript = slots [i].transform.GetChild (0).GetComponent<InventorySlot> ();
					inventoryslotscript.StackableGameObjects.Add (gameobjtoAdd);
					inventoryslotscript.amount++;
					inventoryslotscript.amountText.text = inventoryslotscript.amount.ToString ();
				}
			}
			UpdateCurrentWeightofInventory ();
			return;

		} 

		  
			if (CurrentAmountofobjectsInInvetory >= space) {
				slots.Add (Instantiate (Slot));
				slots [CurrentAmountofobjectsInInvetory].GetComponent<SlotReceiver> ().id = CurrentAmountofobjectsInInvetory;
				slots [CurrentAmountofobjectsInInvetory].transform.SetParent (SlotPanel.transform);
				items.Add (null);
				gameobjects.Add (null);
				Debug.Log ("Instantiating an additional slot");
			} 

			for (int i = 0; i < items.Count; i++) {

				if (slots [i].GetComponentInChildren<InventorySlot> () == null) {

					items [i] = itemtoAdd;
					gameobjects [i] = gameobjtoAdd; 
					GameObject invslot = Instantiate (InventorySlotgo); 
					InventorySlot slotscript = invslot.GetComponent<InventorySlot> (); 
					slotscript.AddItem (itemtoAdd, gameobjtoAdd); 
					invslot.transform.SetParent (slots [i].transform); 
					slotscript.slotID = slots[i].GetComponent<SlotReceiver> ().id; 
					invslot.transform.localPosition = Vector2.zero;
					CurrentAmountofobjectsInInvetory ++;
					break;

				}

			} 
		
			tooltip.DeActivate ();
		 
		WasPickedUp = true; 
		UpdateCurrentWeightofInventory (); 

			if(onItemChangedCallback != null)
			onItemChangedCallback.Invoke ();
	
	} 

	  
	public void UpdateCurrentWeightofInventory(){
		
		float weight = 0;

		for (int i = 0; i < items.Count; i++) {
			
			if (items [i] != null) {
				
				  if (items [i].isStackable) {
					InventorySlot slot = slots [i].GetComponentInChildren<InventorySlot> ();
                    weight += slot.item.weight;
				}  
                   
				 
			}
		}
		currentWeightinGrams = weight;
		CheckLoadLevel ();
	}

	public bool checkIfItemIsinInventory(Item item){
		for (int i = 0; i < items.Count; i++) {
			if (items [i] == item)   
				return true;
		}
		return false;
	}

	public bool checkIfGameObjectIsinInventory(GameObject gameobj){
		for (int i = 0; i < gameobjects.Count; i++) {
			if (gameobjects [i] == gameobj)   
				return true; 
		}
		return false;
	}
     

	public void Remove(Item itemtoremove, GameObject gombgd)
	{ 
		if (itemtoremove.isStackable && checkIfItemIsinInventory (itemtoremove)) {
			for (int i = 0; i < items.Count; i++) {
				if (items [i] == itemtoremove) {  
					InventorySlot inventoryslotscript = slots [i].transform.GetChild (0).GetComponent<InventorySlot> ();
					if (inventoryslotscript.amount != 0) {
						inventoryslotscript.amount--; 
						inventoryslotscript.SetItemtodefaultState (inventoryslotscript.StackableGameObjects [inventoryslotscript.amount]);
						inventoryslotscript.StackableGameObjects.RemoveAt (inventoryslotscript.amount);
						if (inventoryslotscript.amount != 0)
							inventoryslotscript.amountText.text = inventoryslotscript.amount.ToString ();
						else
							inventoryslotscript.amountText.text = null;
					} else { 
						items [inventoryslotscript.slotID] = null;
						gameobjects [inventoryslotscript.slotID] = null;
						CurrentAmountofobjectsInInvetory--;
						inventoryslotscript.SetItemtodefaultState (gombgd);
						if(inventoryslotscript.amount == 0)
						tooltip.DeActivate ();
						Destroy (inventoryslotscript.gameObject);
					}
					UpdateCurrentWeightofInventory ();
					return;
				} 
			}
		}
         
 
			foreach (GameObject sltt in slots) {
				InventorySlot slt = sltt.transform.GetComponentInChildren<InventorySlot> ();
				if (slt != null) {
					if (slt.gameobj == gombgd) {
						items [slt.slotID] = null;
						gameobjects [slt.slotID] = null;
						CurrentAmountofobjectsInInvetory--;
						slt.SetItemtodefaultState (gombgd);
						tooltip.DeActivate (); 
						Destroy (slt.gameObject);
						break;
					}
				}
			} 

		if (CurrentAmountofobjectsInInvetory >= space) {
			if (slots.Count > space) {
				for (int i = 0; i < slots.Count; i++) {
					if (i >= space) {
						if (items [i] == null) { 
							Destroy (slots [i].gameObject);
							slots.RemoveAt (i);
							items.RemoveAt (i);
							gameobjects.RemoveAt (i);
						}
					}
				}
				for (int i = 0; i < slots.Count; i++) {
					slots [i].GetComponent<SlotReceiver> ().id = i;  
				}
				foreach (GameObject sltt in slots) {
					InventorySlot slt = sltt.transform.GetComponentInChildren<InventorySlot> ();
					if (slt != null) {
						slt.slotID = slt.transform.GetComponentInParent<SlotReceiver> ().id;
						}
					}
				}

			}

		UpdateCurrentWeightofInventory ();
		 
 
		if(onItemChangedCallback != null)
			onItemChangedCallback.Invoke ();

	}
 

	public	void CheckLoadLevel(){
		if (currentWeightinGrams >= 30000 && !invloadLevel1 && !invloadLevel2 && !invloadLevel3) {
			invloadLevel1 = true;
			invloadLevelNormal = false;
			OnLevelChanged = true;
		} if (currentWeightinGrams >= 40000 && !invloadLevel2 && !invloadLevel3) {
			invloadLevel1 = false;
			invloadLevel2 = true;
			invloadLevelNormal = false;
			OnLevelChanged = true;
		} if (currentWeightinGrams >= 50000 && !invloadLevel3) {
			invloadLevel2 = false;
			invloadLevel3 = true;
			invloadLevelNormal = false;
			OnLevelChanged = true;
		}
 
		if (currentWeightinGrams < 50000 && invloadLevel3) {
			invloadLevel3 = false;
			invloadLevel2 = true;
			OnLevelChanged = true;
		} if (currentWeightinGrams < 40000 && invloadLevel2 || invloadLevel3) {
			invloadLevel2 = false;
			invloadLevel1 = true;
			OnLevelChanged = true;
		} if (currentWeightinGrams < 30000 && invloadLevel1 || invloadLevel2 || invloadLevel3) {
			invloadLevel1 = false;
			OnLevelChanged = true;
		}

		if(currentWeightinGrams < 30000){ 
			invloadLevelNormal = true;
		}
	}

	public void HideInventory()
	{
		InventoryUI.SetActive (false);  
		isShown = false;
		CursorManager.HideCursorLook ();
	}

	public void ShowInventory()
	{
		InventoryUI.SetActive (true);
		isShown = true;
		CursorManager.ShowCursorLook ();
	}
















































}
