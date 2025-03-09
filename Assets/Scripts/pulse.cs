using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pulse : MonoBehaviour
{
    float size = 0.2f;
    Vector3 initSize;
    private void Start()
    {
        initSize = transform.localScale;
    }
    private void Update()
    {
        size += Time.deltaTime*5;
        transform.localScale = initSize*size;
        if (size > 10) { Destroy(gameObject); }
    }
}
