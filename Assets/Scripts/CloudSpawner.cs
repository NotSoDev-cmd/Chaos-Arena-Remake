using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloud;
    private float timing = 0.2f;
    public Transform up;
    public Transform down;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn() {
        GameObject go = Instantiate(cloud, new Vector3(up.position.x, Random.Range(down.position.y, up.position.y), 0f), Quaternion.identity);
        go.GetComponent<Rigidbody2D>().AddForce(Random.Range(3, 10) * Vector2.right, ForceMode2D.Impulse);
        yield return new WaitForSeconds(timing);
        //timing -= 0.01f;
        StartCoroutine(Spawn());
    }
}
