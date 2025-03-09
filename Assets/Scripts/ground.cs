using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground : MonoBehaviour
{
    GameObject player;
    private void Start()
    {
        player = GameObject.Find("Player"); 
    }
    void Update()
    {
        if (player.transform.position.y-player.GetComponent<characterControl>().curCollider.size.y < transform.position.y) { GetComponent<BoxCollider2D>().enabled = false; } else GetComponent<BoxCollider2D>().enabled = true;
    }
}
