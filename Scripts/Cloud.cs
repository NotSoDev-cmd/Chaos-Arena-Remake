using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        Destroy(gameObject, 120);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
