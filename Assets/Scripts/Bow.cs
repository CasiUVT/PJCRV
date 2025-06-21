using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    public Transform player;
    public GameObject arrowPrefab;
    public Transform shootPoint;
    public float rotationOffset; // Offset pentru a alinia săgeata corect
    public float shootForce = 10f;
    private float shootCooldown = 3f;
    private float shootTimer = 0f;
    private Animator animator; // Referință la componenta Animator

    private void Start()
    {
        animator = GetComponent<Animator>(); // Obține componenta Animator atașată acestui GameObject
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + rotationOffset);

        shootTimer += Time.deltaTime;
        if (shootTimer >= shootCooldown)
        {
            ShootArrow(direction.normalized);
            shootTimer = 0f;
        }
    }

    void ShootArrow(Vector3 direction)
    {

        if (animator != null)
            animator.SetTrigger("Shoot"); // Pornește animația

        if (arrowPrefab != null && shootPoint != null)
        {
            GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            arrow.transform.rotation = Quaternion.Euler(0, 0, angle + rotationOffset);

            Arrow arrowScript = arrow.GetComponent<Arrow>();
            if (arrowScript != null)
                arrowScript.SetDirection(direction, shootForce);
        }
        else
        {
            Debug.LogWarning("arrowPrefab sau shootPoint nu este setat!");
        }
    }
}
