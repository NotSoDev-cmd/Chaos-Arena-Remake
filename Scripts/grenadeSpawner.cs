using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeSpawner : MonoBehaviour
{

    public GameObject[] grenades;
    public float spawn_timing;

    public Transform ru;
    public Transform ld;
    private Vector3 left_down;
    private Vector3 right_up;

    public float min_timing = 2f;
    // Start is called before the first frame update
    void OnEnable()
    {
        left_down = ld.position;
        right_up = ru.position;
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn() {
        if (FindObjectsOfType<Drone>().Length < 200){
        Instantiate(grenades[Random.Range(0, grenades.Length)], new Vector3(Random.Range(left_down.x, right_up.x), Random.Range(left_down.y, right_up.y), 0f), Quaternion.identity);
        yield return new WaitForSeconds(spawn_timing);
        spawn_timing -= 0.05f;
        spawn_timing = Mathf.Max(min_timing, spawn_timing);
        }
        StartCoroutine(Spawn());
    }
}
