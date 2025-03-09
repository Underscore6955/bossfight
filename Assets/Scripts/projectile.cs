using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class projectile : MonoBehaviour
{
    [SerializeField] bool players;
    public float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "boss" || collision.tag == "Attackable" || collision.tag == "Player") { if (!players && collision.name == "Player") { Hit(collision.gameObject); } else if (players && collision.name != "Player") { Hit(collision.gameObject); } }
    }
    private void Hit(GameObject hit)
    {
        hit.GetComponent<health>().Damage(damage);
        Destroy(gameObject);
    }
    private void Update()
    {
        if (Mathf.Abs(transform.position.x) > 13 || Mathf.Abs(transform.position.y) > 8) { Destroy(gameObject); }
    }
}
