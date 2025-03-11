using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class retryButton : MonoBehaviour
{
    private void Update()
    {
        if (!Input.GetMouseButtonDown(0) || Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)).gameObject != gameObject) return;
        GameObject.Find("boss").GetComponent<fightManager>().startBattle();
        Destroy(transform.parent.gameObject);
    }
}
