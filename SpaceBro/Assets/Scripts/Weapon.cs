using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{

    public float FireRate = 0; // 0 equals single burst
    public LayerMask WhatToHit;
    Transform FirePoint;
    public float Damage = 10;
    public bool isAutonomous;
    public float BetweenShots = 0.6f;
    float timer;
    LineRenderer lineRenderer;

    // Use this for initialization
    void Start()
    {
        FirePoint = transform.FindChild("FirePoint");
        if (FirePoint == null)
            Debug.LogError("No FirePoint");
        timer = 0;
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer != null)
        {
            lineRenderer.useWorldSpace = true;
            lineRenderer.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (isAutonomous && Input.GetMouseButtonDown(0))
        {
            Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    public void Shoot(Vector3 target)
    {
        if (timer > BetweenShots)
        {
            timer = 0;
            Vector2 mouse = new Vector2(target.x, target.y);
            Vector2 firePoint = new Vector2(FirePoint.position.x, FirePoint.position.y);

            RaycastHit2D hit = Physics2D.Raycast(firePoint, mouse - firePoint, 100, WhatToHit);
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, firePoint);

            if (hit.collider != null)
            {
                
                if(lineRenderer != null)
                    lineRenderer.SetPosition(1, hit.point);

                if (hit.collider.GetComponent<CharacterAI>() != null)
                    hit.collider.GetComponent<CharacterAI>().Hit(Damage);

            }
            else if(lineRenderer != null)
                lineRenderer.SetPosition(1, mouse + (mouse - firePoint) * 100);
        }
    }
}
