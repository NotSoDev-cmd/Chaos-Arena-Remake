using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private bool atack_ready = true;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other) {
        if (atack_ready) {
            if (other.gameObject.name == "Body") {
               // Debug.Log("abobus");
                other.transform.parent.gameObject.GetComponent<Player>().ApplyDamage(damage);
                StartCoroutine(Recharge());
            }
            if (other.gameObject.GetComponent<Drone>()) {
                other.transform.parent.gameObject.GetComponent<Player>().ApplyDamage(damage / 10);
            }
        }
    }

    IEnumerator Recharge() {
        atack_ready = false;
        yield return new WaitForSeconds(0.3f);
        atack_ready = true;
    }
}
