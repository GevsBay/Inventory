using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiDatabase : MonoBehaviour {

    #region Singeltone
    public static UiDatabase instance;


    void Awake () {
        if (instance != null)
            return;

        instance = this;
    }

    #endregion

    #region ExamineSystemManagerStuff
    public Text examinesystemDescription;
    public GameObject examinePanel; 

     
    #endregion

    #region CommunicationManagerStuff
    public CanvasGroup CommunicationmanagerCanvas;
    public Text hintText;

    #endregion

    #region Tooltip Stuff
    public GameObject tooltip;

    #endregion

    #region InventoryStuff
    public GameObject inventoryUiPanel;
    public Transform itemsParent; 
    public GameObject SlotPanel; 

    #endregion

    #region RaycastManagerStuff
    public Image Crosshair;
    public Text itemName;

    #endregion
}
