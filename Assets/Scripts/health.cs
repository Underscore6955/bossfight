using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    [SerializeField] float Health;
    void Start()
    {
        
    }

    void Update()
    {
        if (Health <= 0) Death();
    }
    public void Damage(float damage)
    {
        Health -= damage;
    }
    void Death()
    {
        if (gameObject.name == "Player") Debug.Log("died");
    }
}
