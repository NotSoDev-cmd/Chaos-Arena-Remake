using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    private Transform player;
    public float speed;
    public float max_health;
    private float health;
    private Rigidbody2D rb;

    public float knock_out_time;
    public Color ded_color;

    public LayerMask player_and_ground;

    public GameObject[] particles;

    private Vector3 last_known_pos;

    public GameObject hit_eff;

    public float damage;
    public bool isRanged;
    public Weapon gun;
    // Start is called before the first frame update
    void Start()
    {
        health = max_health;
        player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        Vector2 dir = new Vector2(Random.Range(-1f, -1f), Random.Range(-1f, 1f));
        RaycastHit2D rh = Physics2D.Raycast(transform.position, -dir, Mathf.Infinity, player_and_ground);
        last_known_pos = rh.point;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSeePlayer()) {
            last_known_pos = player.position;
            if (isRanged) {
                gun.Shoot();
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, last_known_pos, speed);
    }

    public void ApplyDamage(float val) {
        health -= val;
        if (health <= 0f) {
            rb.gravityScale = 1;
//            Debug.Log((int)ded);
            GetComponent<SpriteRenderer>().color = ded_color;
            gameObject.layer = 9;
            foreach (GameObject go in particles) {
                Destroy(go);
            }
            rb.freezeRotation = false;
            rb.angularVelocity = Random.Range(-20f, 20f);
            Destroy(gameObject, 20);
            Destroy(this);
        }
    }
    

    bool CanSeePlayer() {
        Vector2 dir = transform.position - player.position;
       // Debug.DrawRay(transform.position, dir);
        RaycastHit2D rh = Physics2D.Raycast(transform.position, -dir, Mathf.Infinity, player_and_ground);
        //Debug.Log(rh.transform.name);
        return rh.transform.gameObject.name == "Player";
    }
    
    void OnCollisionEnter2D(Collision2D other) {
        
        ApplyDamage(Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.y + rb.velocity.y) / 10);
        Instantiate(hit_eff, transform.position, Quaternion.identity);
        if (other.transform.name == "Player") {
            other.gameObject.GetComponent<Player>().ApplyDamage(damage);
            ApplyDamage(10000);
        }
    } 
}
