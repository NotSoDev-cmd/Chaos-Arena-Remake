using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{

    public float speed;

    public GameObject effect;

    private float scale;

    public Transform[] spawns;

    public Transform Shrink;
    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        Shrink.localScale = new Vector3(scale, scale, scale);
        if (scale <= 0f) {
            Instantiate(effect, transform.position, transform.rotation);
            FindObjectOfType<AudioManager>().Play("objective");

            for (int i = 0; i < 1; i++) {
                FindObjectOfType<Player>().ApplyDamage(-2f);
                Instantiate(gameObject, spawns[Random.Range(0, spawns.Length)].position, Quaternion.identity);
            }

            FindObjectOfType<Player>().Capture();

            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.name == "Body") {
            scale -= speed;
        }
    }
}
