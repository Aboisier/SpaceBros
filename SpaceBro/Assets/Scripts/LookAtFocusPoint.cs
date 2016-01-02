using UnityEngine;
using System.Collections;

public class LookAtFocusPoint : MonoBehaviour
{
    public Vector3 Offset = Vector3.zero;
    MoveCharacter mc;
    Character MainCharacter;
    CharacterAI AI;

    // Use this for initialization
    void Start()
    {
        MainCharacter = FindObjectOfType<Character>();
        mc = transform.parent.parent.GetComponent<MoveCharacter>();
        AI = transform.parent.parent.GetComponent<CharacterAI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(AI.FocusPoint) - Camera.main.WorldToScreenPoint(transform.position);
        Vector3 rot = new Vector3(0, 0, Mathf.Atan2(pos.x, pos.y) * Mathf.Rad2Deg);

        // If the mc script is set, then this parts copies the orientation of the linked script.
        Direction dir = Direction.RIGHT;

       

        if (mc != null && (int)mc.LookDirection == -1)
            dir = mc.LookDirection;

        Vector3 dif = new Vector3(0, 0, transform.parent.parent.rotation.eulerAngles.z - MainCharacter.Rotation.z);
        Debug.Log(dif);

        // That's weird but I got bored and it works.
        transform.rotation = Quaternion.Lerp(transform.rotation,  Quaternion.Euler(-(int)dir * rot + Offset + transform.parent.parent.rotation.eulerAngles - (int)dir * dif), 0.1f);

    } 
}
