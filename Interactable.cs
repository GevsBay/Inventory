using UnityEngine;

public class Interactable : MonoBehaviour {

	public float radius = 3.5f; 

	bool isFocused = false;
    Transform Player;

	bool hasInteracted = false;
	public Item item; 

	public virtual void Interact()
	{  
		
	}
 
	void Update()
	{
		if (isFocused && !hasInteracted) {
            //determening the distance
			float Dist = Vector3.Distance (Player.position, transform.position);
            // checking the distance
			if (Dist <= radius) {
                           //here you check to see if player decided to add the object
                            if (ExamineSystemManager.instance.canExamine)
                            {
                                if (ExamineSystemManager.instance.isbeingEXAMINED(item))
                                {
                                    ExamineSystemManager.instance.StopExamining();
                                    Interact();
                                    hasInteracted = true;
                                }
                            } 
                       
                     
            }
		}

	}

	public void OnFocused(Transform playerTransform){ 
		isFocused = true;
		Player = playerTransform;
		hasInteracted = false;
	 
	} 

	public void OnDeFocused(){
		isFocused = false;
		Player = null;
		hasInteracted = false;
	}
 












































































}
