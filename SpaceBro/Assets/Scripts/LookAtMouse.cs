using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour {
    public Vector3 Offset = Vector3.zero;
    public MoveCharacter mc;
    // Use this for initialization
    void Start() {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 pos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        Vector3 rot = new Vector3(0, 0, Mathf.Atan2(pos.x, pos.y) * Mathf.Rad2Deg);

        // If the mc script is set, then this parts copies the orientation of the linked script.
        Direction dir = Direction.RIGHT;
        if (mc != null)
            dir = mc.LookDirection;

        transform.rotation = Quaternion.Euler(- (int)dir * rot + Offset + transform.parent.parent.rotation.eulerAngles);
	}
}
