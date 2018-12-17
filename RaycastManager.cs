using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RaycastManager : MonoBehaviour 
{
    //Read This
    //This script is fully prepeared for you to add your own stuff into it because you will need my script to get the system working, just add your code to the script l already filtered out my stuff
   

	public static RaycastManager instance;

	[Header("Raycast Settings")]
	[SerializeField] private float rayLength = 10;
	[SerializeField] private LayerMask newLayerMask;

    #region References
    [Header("Refrences")]
	[SerializeField] private Image crossHair;
	[SerializeField] private Text itemNameText; 
    #endregion

    //Current object under ray
    public Interactable focus;  
	
	float Timer;
	float delay = 0.1f;

	void Awake(){
        #region Instanceing
        if (instance != null)
            return;

        instance = this;
        #endregion

    }

    void Start()
    {

        #region References 
        crossHair = UiDatabase.instance.Crosshair;
        itemNameText = UiDatabase.instance.itemName;
        #endregion


    }

    void Update () 
	{
        #region RayStuff
        if (EventSystem.current.IsPointerOverGameObject () || ExamineSystemManager.instance.isbeingExamined || Inventory.instance.isShown) {

			crossHair.enabled = false;
			itemNameText.text = null; 
			return; 
		}  

		if (Timer < delay)
			Timer += Time.deltaTime;
		
		RaycastHit hit;
	   	Vector3 fwd = transform.TransformDirection (Vector3.forward);

		if (Physics.Raycast (transform.position, fwd, out hit, rayLength, newLayerMask.value)) {
			 
			Interactable interactable = hit.collider.GetComponent<Interactable> ();
            if (interactable != null)
            {
                itemNameText.text = interactable.item.name + " [E]";
                crosshairActive();
            }
			if (Timer < delay) {
				return;
			}
					else {
				if (Input.GetKeyDown (KeyCode.E) && Time.time > 0.1f) {  
					if (interactable != null) { 
						Setfocus (interactable, interactable.item, interactable.gameObject); 
						Timer = 0.0f;
					}
				}
			}
			} else {

			crosshairNormal ();
			itemNameText.text = null; 
		}
        #endregion


    }

    #region DonotMessWithTHis
    void Setfocus (Interactable newfocus, Item item, GameObject go)
	{  
		if (newfocus != focus) {
			if(focus != null)
			focus.OnDeFocused();
			
			focus = newfocus;
		}
         
            ExamineSystemManager.instance.allowClick = false;
            ExamineSystemManager.instance.Examine(item, go);
         

        newfocus.OnFocused (transform.parent);
 
	}

	public void Removefocus ()
	{
		if(focus != null)
		focus.OnDeFocused();
		
		focus = null;
	}

	void crosshairActive() 
	{
		if (crossHair.enabled == false)
			crossHair.enabled = true;
		crossHair.color = Color.red;

	}
	void crosshairNormal() 
	{
		if(crossHair != null){
		if (crossHair.enabled == false)
			crossHair.enabled = true;
		crossHair.color = Color.white;
	}
        #endregion

    }





































































}
