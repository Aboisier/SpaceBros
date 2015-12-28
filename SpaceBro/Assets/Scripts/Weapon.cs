using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{

    public float FireRate = 0; // 0 equals single burst
    public LayerMask WhatToHit;
    Transform FirePoint;
    public float Damage = 10;

    // Use this for initialization
    void Start()
    {
        FirePoint = transform.FindChild("FirePoint");
        if (FirePoint == null)
            Debug.LogError("No FirePoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector2 mouse = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                    Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePoint = new Vector2(FirePoint.position.x, FirePoint.position.y);

        Debug.DrawLine(firePoint, mouse + (mouse - firePoint) * 100, Color.green, 0.02f);

        RaycastHit2D hit = Physics2D.Raycast(firePoint, mouse - firePoint, 100);

        if (hit.collider != null)
        {
            Debug.DrawLine(firePoint, hit.point, Color.red, 0.02f);
        }
    }
}
