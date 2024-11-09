using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    public float bullet_Force;
    public Transform FirePoint;
    public float offset;

    private bool can_shoot = true;
    public float shot_delay;
    public float damage;

    public bool isPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayer) {
            return;
        }
        Vector2 dir = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + offset;
        angle = Normalize(angle);
        if (!(90f <= angle && angle < 270f)) {
            //angle += 180f;
            transform.parent.localScale = new Vector3(Mathf.Abs(transform.parent.localScale.x), transform.parent.localScale.y, 1f);
        } else {
            angle += 180f;
            transform.parent.localScale = new Vector3(-Mathf.Abs(transform.parent.localScale.x), transform.parent.localScale.y, 1f);
        }
        

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        float coef = 1f;
        if (transform.parent.localScale.x < 0f) {
            coef = -1f;
        }
        if (Input.GetMouseButton(0) && can_shoot) {
            GameObject bul = Instantiate(bullet, FirePoint.position, Quaternion.identity);
            bul.GetComponent<Bullet>().damage = damage;
            bul.GetComponent<Rigidbody2D>().AddForce(FirePoint.right * bullet_Force * coef, ForceMode2D.Impulse);
            StartCoroutine(Recharge());
        }
    }

    float Normalize(float angle) {
        if (angle < 0f) {
            angle += 360f;
        }
        if (angle > 360f) {
            angle %= 360f;
        }
        return angle;
    }

    IEnumerator Recharge() {
        can_shoot = false;
        yield return new WaitForSeconds(shot_delay);
        can_shoot = true;
    }

    public void Shoot() {
        float coef = 1;
        if (can_shoot) {
            GameObject bul = Instantiate(bullet, FirePoint.position, Quaternion.identity);
            bul.GetComponent<Bullet>().damage = damage;
            bul.GetComponent<Rigidbody2D>().AddForce(FirePoint.right * bullet_Force * coef, ForceMode2D.Impulse);
            StartCoroutine(Recharge());
            if (!isPlayer) {
                bul.GetComponent<Bullet>().hit_enemies = false;
            }
        }
    }
}
