using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meele : MonoBehaviour
{

    private bool can_atack;
    private float damage;
    private bool atacking;
    private bool hit;
    public GameObject hit_eff;
    public float atack_duration;
    private float knockback;

    public MeeleSRC src;

    public SpriteRenderer sp;
    // Start is called before the first frame update
    void Start()
    {
        knockback = src.knockback;
        damage = src.damage;
        sp.sprite = src.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Atack() {
        StartCoroutine(CAtack());
    }

    IEnumerator CAtack() {
        if (!atacking) {
            FindObjectOfType<AudioManager>().Play("meele");
            hit = false;
            atacking = true;
            yield return new WaitForSeconds(atack_duration);
            atacking = false;
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        //Debug.Log(atacking && !hit);
        if (other.gameObject.GetComponent<Drone>() && atacking && !hit) {
            other.gameObject.GetComponent<Drone>().ApplyDamage(damage);
            hit = true;
            //FindObjectOfType<AudioManager>().Play("hit");
            Instantiate(hit_eff, other.transform.position, Quaternion.identity);
            other.gameObject.GetComponent<Rigidbody2D>().AddForce((transform.position - other.transform.position) * knockback, ForceMode2D.Impulse);
     
        }
        if (other.gameObject.GetComponent<Bullet>() && atacking)  {
            Instantiate(hit_eff, other.transform.position, Quaternion.identity);
            FindObjectOfType<AudioManager>().Play("hit");
            Destroy(other.gameObject);
        }
    }

    public void SetMeele(MeeleSRC _src) {
        src = _src;
        Start();
    } 
}
