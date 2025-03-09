using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pulseAttack : MonoBehaviour
{
    [SerializeField] GameObject pulseObj;
    private void OnEnable()
    {
        Instantiate(pulseObj, GameObject.Find("boss").transform.position, Quaternion.identity);
        GetComponent<MonoBehaviour>().enabled = false;
    }
}
