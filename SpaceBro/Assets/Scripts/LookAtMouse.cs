using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour {
    public string rott;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 pos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.parent.position);
        Vector3 rot = new Vector3(0, 0, Mathf.Atan2(pos.x, pos.y) * Mathf.Rad2Deg);
        rott = rot.ToString();

        transform.rotation = Quaternion.Euler(rot);
	}
}
