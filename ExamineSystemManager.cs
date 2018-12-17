using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class ExamineSystemManager : MonoBehaviour {
	
	public static ExamineSystemManager instance; 

	[HideInInspector]
	public GameObject examinablegameObject;

    public PostProcessingProfile ppb;

	[HideInInspector]
	public bool isbeingExamined = false;

	[HideInInspector]
	public bool canExamine = false;

	Vector3 originalposition;
	Quaternion originalRotation;
	Vector3 OriginalScale;

	Item itm;
	public Examiner examiner;
	public Text description;
	[HideInInspector]
	public bool allowClick = true; 
	public GameObject ExaminePanel; 

	void Awake () {
        #region Instancing
        if (instance != null)
            return;

        instance = this;
#endregion

    }
    private void Start()
    {
        
        ExaminePanel = UiDatabase.instance.examinePanel;
        description = UiDatabase.instance.examinesystemDescription;
        examiner = Examiner.instance;
    }

    void Update () {
		if (isbeingExamined) {
  
			if (allowClick) {
				if (Input.GetMouseButtonDown (0)) {
					canExamine = true;
				} else if (Input.GetMouseButtonDown (1)) {
					StopExamining ();
				}
			
			}
		}
	}

	public void Examine(Item item, GameObject gameobj){ 
		examinablegameObject = gameobj;
		itm = item;  
		this.transform.parent.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
		examinablegameObject.GetComponent<Rigidbody> ().isKinematic = true;
		examinablegameObject.GetComponent<Collider> ().enabled = false;
		originalposition = examinablegameObject.transform.position;
		originalRotation = examinablegameObject.transform.rotation;
		OriginalScale = examinablegameObject.transform.localScale; 
		examinablegameObject.transform.SetParent (examiner.transform);
		examinablegameObject.transform.localScale /= 10;
		examinablegameObject.transform.localPosition = item.location; 
		examiner.go = examinablegameObject;  
		 
			description.text = "<b>" + itm.name + "</b>\n\n" + itm.description;
		
		ExaminePanel.SetActive(true);
        ppb.vignette.enabled = true;
        ppb.depthOfField.enabled = true;
		isbeingExamined = true;
		allowClick = true; 
		 
		} 

	public void StopExamining(){
		canExamine = false;
		isbeingExamined = false;  
		itm = null; 
		examiner.go = null; 
		examinablegameObject.GetComponent<Rigidbody> ().isKinematic = false; 
		examinablegameObject.GetComponent<Collider> ().enabled = true; 
		examinablegameObject.transform.parent = null;
		examinablegameObject.transform.position = originalposition;
		examinablegameObject.transform.rotation = originalRotation; 
		examinablegameObject.transform.localScale = OriginalScale;
		this.transform.parent.GetComponent<Rigidbody> ().constraints =  RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
		RaycastManager.instance.Removefocus (); 
		description.text = null;
		ExaminePanel.SetActive (false);
        ppb.vignette.enabled = false;
        ppb.depthOfField.enabled = false; 
	}

	public bool isbeingEXAMINED(Item item){ 
			if (itm == item)
				return true;
			else
				return false;
		} 
}
