using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

	new public string name = "NewItem"; 
	[TextArea]
	public string description = "new description";
	public Sprite icon = null;
	public bool isStackable = false;

    //leave this at zero if object is not appearing to big if it is then adjust it in game view and apply it to the object
    public Vector3 location;

    public float weight;

	public  virtual void Use () 
	{ 
		
	}



 





}
