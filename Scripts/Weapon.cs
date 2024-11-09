using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    private float bullet_Force;
    public Transform FirePoint;
    public float offset;

    private bool can_shoot = true;
    private float shot_delay;
    private float damage;
    private bool shoots_grenades;
    public bool isPlayer = false;
    public bool iSLMG;
    public WeaponSRC src;

    public Camera cam;

    public bool change_sprite = true;
    // Start is called before the first frame update
    void Start()
    {
        damage = src.damage;
        shot_delay = src.shooting_speed;
        bullet_Force = src.bullet_force;
        shoots_grenades = src.shoots_grenades;
        if (change_sprite) {
            GetComponent<SpriteRenderer>().sprite = src.sprite;
        }
    }

    void OnEnable() {
        can_shoot = true;
        //iSLMG = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayer) {
            return;
        }
        Vector2 dir = transform.position - cam.ScreenToWorldPoint(Input.mousePosition);
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
            if (!shoots_grenades) {
               bul.GetComponent<Bullet>().damage = damage;
            }
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
        if ("LMG" == gameObject.name) {
            Debug.Log(can_shoot);
        }
        if (can_shoot || iSLMG) {
            if (iSLMG) {
                iSLMG = false;
            }
            GameObject bul = Instantiate(bullet, FirePoint.position, Quaternion.identity);
            if (!shoots_grenades){
            bul.GetComponent<Bullet>().damage = damage;
            }
            bul.GetComponent<Rigidbody2D>().AddForce(FirePoint.right * bullet_Force * coef, ForceMode2D.Impulse);
            StartCoroutine(Recharge());
            if (!isPlayer && !shoots_grenades) {
                bul.GetComponent<Bullet>().hit_enemies = false;
            }
        }
    }

    public void SetWeapon(WeaponSRC _src) {
        src = _src;
        Start();
    }

    public void CanShoot() {
        can_shoot = true;
    }
}
