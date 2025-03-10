using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class seekerScript : MonoBehaviour
{
    [SerializeField] float speed;
    GameObject player;
    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        Vector3 desiredPosition = transform.position + direction.normalized * Time.fixedDeltaTime * speed;
        GetComponent<Rigidbody2D>().MovePosition(desiredPosition);

        Vector3 targetDir = player.transform.position - gameObject.transform.position;
        float angle = Vector3.Angle(targetDir, Vector3.right);
        if (targetDir.y <= 0) { angle = 360 - angle; }
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
