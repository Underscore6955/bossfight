using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sideFire : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletSpeed;
    public Camera mainCamera;
    float x, y, z = 0;
    private void OnEnable()
    {
        mainCamera = Camera.main;

        int edge = Random.Range(0, 4); 
        float[] positions = { 1.5f / 5f, 2.5f / 5f, 3.5f / 5f };

        Vector3 screenMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 screenMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        Quaternion rotation = Quaternion.identity;
        Vector2 velocity = Vector2.zero;

        foreach (float pos in positions)
        {
            switch (edge)
            {
                case 0: 
                    x = Mathf.Lerp(screenMin.x, screenMax.x, pos);
                    y = screenMax.y;
                    rotation = Quaternion.Euler(0, 0, 180);
                    velocity = Vector2.down * bulletSpeed;
                    break;
                case 1: 
                    x = Mathf.Lerp(screenMin.x, screenMax.x, pos);
                    y = screenMin.y;
                    rotation = Quaternion.Euler(0, 0, 0);
                    velocity = Vector2.up * bulletSpeed;
                    break;
                case 2: 
                    x = screenMin.x;
                    y = Mathf.Lerp(screenMin.y, screenMax.y, pos);
                    rotation = Quaternion.Euler(0, 0, -90);
                    velocity = Vector2.right * bulletSpeed;
                    break;
                case 3: 
                    x = screenMax.x;
                    y = Mathf.Lerp(screenMin.y, screenMax.y, pos);
                    rotation = Quaternion.Euler(0, 0, 90);
                    velocity = Vector2.left * bulletSpeed;
                    break;
            }

            Vector3 spawnPosition = new Vector3(x, y, z);
            GameObject curBullet = Instantiate(bullet, spawnPosition, rotation);
            curBullet.GetComponent<Rigidbody2D>().velocity = velocity;
            Debug.Log(curBullet.transform.position);
        }
        GetComponent<MonoBehaviour>().enabled = false;
    }
}
