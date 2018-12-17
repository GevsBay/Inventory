using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Examiner : MonoBehaviour
{
    #region Instancing
    public static Examiner instance;
     
    private void Awake()
    {

        if (instance != null)
            return;

        instance = this;
    }
#endregion



	public GameObject go;
	public float speed = 1f;
	void FixedUpdate() {
		if (go != null) {
			float h = speed * Input.GetAxis ("Mouse X");
			float v = speed * Input.GetAxis ("Mouse Y");
			go.transform.Rotate (v, h, 0);
		}
	}
}
