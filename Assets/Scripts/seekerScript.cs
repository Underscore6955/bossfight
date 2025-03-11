using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seekerScript : MonoBehaviour
{
    [SerializeField] float speed;
    GameObject player;
    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>(); 
        Vector3 direction = (player.transform.position - transform.position).normalized; 
        Vector2 desiredPosition = (Vector2)transform.position + (Vector2)direction * speed * Time.fixedDeltaTime; 
        rb.MovePosition(desiredPosition);

        Vector3 targetDir = player.transform.position - transform.position; 
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg; 
        transform.rotation = Quaternion.Euler(0, 0, angle-90); 
    }
}
