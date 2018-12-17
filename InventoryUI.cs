using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {
	public static InventoryUI instance;

	Inventory inventory;

	public Transform itemsParent; 
	public KeyCode invKey;
 
	public delegate void OnInventoryShown();
	public OnInventoryShown  OnInventoryShownCallback;

	public Slider InventoryWeightindicator;
	public Image fillImage; 
	public Text inventoryWeightText;
	public GameObject tooltip;

	public Gradient grad;  

	void Start () {
        if (instance != null)
            return;

        instance = this;
        inventory = Inventory.instance;   
	}
	 
	void Update () {
		if (ExamineSystemManager.instance.isbeingExamined) {
			return;
		} 


		if (Input.GetKeyDown (invKey)) {
			if (inventory.isShown) {
				inventory.HideInventory ();
				tooltip.SetActive (false);
				if (OnInventoryShownCallback != null)
					OnInventoryShownCallback.Invoke ();
				 
			} else {
				inventory.ShowInventory ();  
				if (OnInventoryShownCallback != null)
					OnInventoryShownCallback.Invoke ();
		 
			} 
		}

		if (inventory.isShown) {
			inventoryWeightText.text = inventory.Weight().ToString() + " / 40 Kg";
			InventoryWeightindicator.value = inventory.currentWeightinGrams;
			float value = inventory.Weight () / 50f;
			fillImage.color = grad.Evaluate (value);
	}
 
	 























}
}
