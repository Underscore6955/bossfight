using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burstFire : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletSpeed;
    private void OnEnable()
    {
        for (int i = 0; i < 4; i++) 
        { 
            GameObject curBullet = Instantiate(bullet, GameObject.Find("boss").transform.position, Quaternion.Euler(0, 0, -(i * 90+45)));
            curBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sin((i*90+45)*Mathf.Deg2Rad),Mathf.Cos((i*90+45) * Mathf.Deg2Rad)); 
        }
        GetComponent<MonoBehaviour>().enabled = false;
    }
}
