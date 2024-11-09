using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LMG : MonoBehaviour
{

    public Weapon gun;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnabled() {
        GetComponent<Animator>().SetTrigger("Enable");
        gun.CanShoot();
    }

    public void Disable() {
        GetComponent<Animator>().SetTrigger("Disable");
        StartCoroutine(_Disable());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("kabum");;
        gun.Shoot();
    }

    IEnumerator _Disable() {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
