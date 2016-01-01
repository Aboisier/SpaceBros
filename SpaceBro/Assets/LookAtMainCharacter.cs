using UnityEngine;
using System.Collections;

public class LookAtMainCharacter : MonoBehaviour
{
    public Vector3 Offset = Vector3.zero;
    MoveCharacter mc;
    Character MainCharacter;

    // Use this for initialization
    void Start()
    {
        MainCharacter = FindObjectOfType<Character>();
        mc = transform.parent.GetComponent<MoveCharacter>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 pos = Camera.main.WorldToScreenPoint(MainCharacter.Position) - Camera.main.WorldToScreenPoint(transform.position);
        Vector3 rot = new Vector3(0, 0, Mathf.Atan2(pos.x, pos.y) * Mathf.Rad2Deg);

        // If the mc script is set, then this parts copies the orientation of the linked script.
        Direction dir = Direction.RIGHT;
        if (mc != null)
            dir = mc.LookDirection;

        transform.rotation = Quaternion.Euler(-(int)dir * rot + Offset + transform.parent.parent.rotation.eulerAngles);
    }
}
