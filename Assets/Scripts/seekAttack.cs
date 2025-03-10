using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seekAttack : MonoBehaviour
{
    [SerializeField] GameObject seekerObj;
    public Camera mainCamera;
    private void OnEnable()
    {
        mainCamera = Camera.main;
        Instantiate(seekerObj, new Vector3 (mainCamera.ViewportToWorldPoint(new Vector3(Random.Range(0,2), Random.Range(0, 2), 0)).x, mainCamera.ViewportToWorldPoint(new Vector3(Random.Range(0, 2), Random.Range(0, 2), 0)).y,0),Quaternion.identity);
        Instantiate(seekerObj, new Vector3(mainCamera.ViewportToWorldPoint(new Vector3(Random.Range(0, 2), Random.Range(0, 2), 0)).x, mainCamera.ViewportToWorldPoint(new Vector3(Random.Range(0, 2), Random.Range(0, 2), 0)).y,0), Quaternion.identity);
        GetComponent<MonoBehaviour>().enabled = false;
    }
}
