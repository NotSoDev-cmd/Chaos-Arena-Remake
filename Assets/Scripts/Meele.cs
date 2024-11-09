using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meele : MonoBehaviour
{

    private bool can_atack;
    public float damage;
    private bool atacking;
    private bool hit;
    public GameObject hit_eff;

    public float knockback;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Atack() {
        StartCoroutine(CAtack());
    }

    IEnumerator CAtack() {
        hit = false;
        atacking = true;
        yield return new WaitForSeconds(0.5f);
        atacking = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log(atacking && !hit);
        if (other.gameObject.GetComponent<Drone>() && atacking && !hit) {
            other.gameObject.GetComponent<Drone>().ApplyDamage(damage);
            hit = true;
            Instantiate(hit_eff, other.transform.position, Quaternion.identity);
            other.gameObject.GetComponent<Rigidbody2D>().AddForce((transform.position - other.transform.position) * knockback, ForceMode2D.Impulse);
        }
    }
}
