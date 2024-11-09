using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    private Transform player;
    public GameObject effect;
    public LayerMask player_and_ground;
    public float dist;

    public float damage;
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
      //  Debug.Log(CanSeePlayer());
        if (CanSeePlayer() && Vector3.Distance(transform.position, player.transform.position) <= dist) {
//            Debug.Log(damage);
            player.gameObject.GetComponent<Player>().ApplyDamage(damage);
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
