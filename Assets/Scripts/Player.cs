using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;

    public LayerMask ground;
    public float jump_vel;
    private bool can_jump = true;
    public Transform Foot1;
    public Transform Foot2;

    private float scale;
    private Animator anim;

    public Animator MeeleWeapon;

    private bool can_meele = true;

    private Meele meele;

    public float maxhp;

    private float hp;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scale = transform.localScale.x;
        anim = GetComponent<Animator>();
        meele = FindObjectOfType<Meele>();

        hp = maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0f) {
            anim.SetBool("running", true);
            anim.SetBool("backwards", Input.GetAxis("Horizontal") / Mathf.Abs(Input.GetAxis("Horizontal")) != transform.localScale.x / Mathf.Abs(transform.localScale.x));     
        } else {
            anim.SetBool("running", false);
        }
        if (CanJump() && can_jump) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                rb.velocity += new Vector2(0f, jump_vel);
                StartCoroutine(RechargeJump());
            }
        }

        if (can_meele && Input.GetMouseButtonDown(1)) {
            MeeleWeapon.SetTrigger("atack");
            meele.Atack();
        }
    }

    bool CanJump() {
        return Physics2D.Raycast(Foot1.position, -Vector2.up, 0.1f, ground) || Physics2D.Raycast(Foot2.position, -Vector2.up, 0.1f, ground);
    }

    IEnumerator RechargeJump() {
        can_jump = false;
        yield return new WaitForSeconds(0.1f);
        can_jump = true;
    }

    IEnumerator RechargeMeele() {
        can_meele = false;
        yield return new WaitForSeconds(1f);
        can_meele = true;
    }

    public void ApplyDamage(float val) {
        hp -= val;

        if (hp <= 0f) {
            Destroy(gameObject);
        }
    }
}
