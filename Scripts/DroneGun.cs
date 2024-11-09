using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneGun : MonoBehaviour
{

    private Transform player;
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = transform.position - player.position;
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + offset);
    }
}
