using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAttacks : MonoBehaviour
{
    List<GameObject> attacks = new List<GameObject>();
    float curCooldown;
    [SerializeField] int cooldown;
    public void updateAttacks()
    {
        attacks.Clear();
        foreach (GameObject curObj in GameObject.FindGameObjectsWithTag("bossAttack"))
        {
            attacks.Add(curObj);
        }
    }
    private void Update()
    {
        updateAttacks();
        curCooldown -= Time.deltaTime;
        if (curCooldown <= 0)
        {
            attacks[Random.Range(0, attacks.Count)].GetComponent<MonoBehaviour>().enabled = true;
            curCooldown = cooldown;
        }
    }
}
