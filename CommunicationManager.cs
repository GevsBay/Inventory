using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommunicationManager : MonoBehaviour {

	public CanvasGroup textcanvas;
	public Text HintText;
	public bool Communicating;

    private void Start()
    {
        textcanvas = UiDatabase.instance.CommunicationmanagerCanvas;
        HintText = UiDatabase.instance.hintText;
    }

    public void HelpPlayer (string Text) {
		Communicating = true;
		HintText.text = Text;
		StartCoroutine (Hide ());
	}

	void Update(){
		if (Communicating) {
			textcanvas.alpha = Mathf.MoveTowards (textcanvas.alpha, 1, 0.25f);
		} else {
			textcanvas.alpha = Mathf.MoveTowards (textcanvas.alpha, 0, 0.25f);
		}
	}

	public void StopHelping(){
		Communicating = false;
		HintText.text = null;
	}

	IEnumerator Hide(){
		yield return new WaitForSeconds (1f);
		StopHelping ();
	}
	 
}
