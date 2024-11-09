using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject effect;
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public bool hit_enemies = true;
    // Start is called before the first frame update
    void Start()
    {

        FindObjectOfType<AudioManager>().Play("shoot");
        if (!hit_enemies) {
            gameObject.layer = 13;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other) {
       // Debug.Log(other.gameObject.name);
        Instantiate(effect, transform.position, Quaternion.identity);
        if (other.gameObject.GetComponent<Drone>()) {
            other.gameObject.GetComponent<Drone>().ApplyDamage(damage);
        }
        if (other.gameObject.GetComponent<Player>()) {
            other.gameObject.GetComponent<Player>().ApplyDamage(damage);
        }
        Destroy(gameObject);
    }
}
