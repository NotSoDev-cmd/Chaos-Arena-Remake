using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashbang : MonoBehaviour
{

    private Transform player;
    public GameObject effect;
    public LayerMask player_and_ground;
    public float dist;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        Destroy(gameObject, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy() {
        if (CanSeePlayer() && Vector3.Distance(transform.position, player.transform.position) <= dist) {
            GameObject.Find("Flashed").GetComponent<Animator>().SetTrigger("Flash");
            FindObjectOfType<AudioManager>().Play("flashed");
        }
        FindObjectOfType<AudioManager>().Play("explosion");
        Instantiate(effect, transform.position, transform.rotation);
    }

    bool CanSeePlayer() {
        Vector2 dir = transform.position - player.position;
       // Debug.DrawRay(transform.position, dir);
        RaycastHit2D rh = Physics2D.Raycast(transform.position, -dir, Mathf.Infinity, player_and_ground);
        //Debug.Log(rh.transform.name);
        return rh.transform.gameObject.name == "Player";
    }
}
