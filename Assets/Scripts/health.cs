using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    public float startHealth;
    public float Health;
    void Start()
    {
        Health = startHealth;
    }

    void Update()
    {
        if (Health <= 0) Death();
    }
    public void Damage(float damage)
    {
        Health -= damage;
        if (gameObject.name == "boss" && GetComponent<health>().Health < (float)(3-GetComponent<fightManager>().phase) / 3 * GetComponent<health>().startHealth) { GetComponent<fightManager>().nextPhase(); }
    }
    void Death()
    {
        if (gameObject.name == "Player") Debug.Log("died");
    }
}
