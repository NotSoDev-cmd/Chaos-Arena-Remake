using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    private bool regenerating;

    private bool trying_to_regen;

    private Coroutine last_regen;
    private Coroutine c_regenerating;

    private Healthbar healthbar;

    public GameObject blood;

    public MonoBehaviour[] EneableWhenDed;
    public Collider2D[] colliders;

    public GameObject DeathScreen;

    private int captured;
    private int kills;

    public TextMeshProUGUI capt;
    public TextMeshProUGUI kill;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scale = transform.localScale.x;
        anim = GetComponent<Animator>();
        meele = FindObjectOfType<Meele>();

        hp = maxhp;
        healthbar = FindObjectOfType<Healthbar>();
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
                if (rb.gravityScale <= 0f) {
                    rb.velocity -= new Vector2(0f, jump_vel);
                } else {
                    rb.velocity += new Vector2(0f, jump_vel);
                }
                StartCoroutine(RechargeJump());
            }
        }

        if (can_meele && Input.GetMouseButtonDown(1)) {
            MeeleWeapon.SetTrigger("atack");
            meele.Atack();
        }

        hp = Mathf.Min(hp, maxhp);
    }

    void LateUpdate() {
//        Debug.Log(hp / maxhp);
        healthbar.Set(hp / maxhp);
    }

    bool CanJump() {
        //Debug.DrawRay(Foot1.position, Vector2.up);

        //Debug.Log(Physics2D.Raycast(Foot1.position, Vector2.up, 0.1f, ground).collider.name);

        if (rb.gravityScale >= 0f) {
            return Physics2D.Raycast(Foot1.position, -Vector2.up, 0.1f, ground) || Physics2D.Raycast(Foot2.position, -Vector2.up, 0.1f, ground);
        } else {
            return true;
        }
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

        FindObjectOfType<AudioManager>().Play("hit");

        if (regenerating) {
            StopCoroutine(c_regenerating);
            regenerating = false;
        }

        if (trying_to_regen) {
            StopCoroutine(last_regen);
            trying_to_regen = false;
        }
        last_regen = StartCoroutine(TryRegen());
        if (hp <= 0f) {
            foreach(MonoBehaviour mn in EneableWhenDed) {
                mn.enabled = false;
                Rigidbody2D q = mn.gameObject.AddComponent<Rigidbody2D>();
                q.velocity = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
                mn.transform.parent = null;
            }

            foreach(Collider2D col in colliders) {
                col.enabled = !col.enabled;
                col.transform.parent = null;
            }

            anim.enabled = false;

            rb.freezeRotation = false;

            Instantiate(blood, transform.position, transform.rotation);

            DeathScreen.SetActive(true);

            capt.text = captured.ToString();
            kill.text = kills.ToString();

            Destroy(this);
        }
    }

    IEnumerator TryRegen() {
        trying_to_regen = true;
        yield return new WaitForSeconds(3f);
        trying_to_regen = false;
        c_regenerating = StartCoroutine(Regenerate());
    }

    IEnumerator Regenerate() {
        yield return new WaitForSeconds(1f);
        hp += 1;
        if (hp < maxhp) {
            c_regenerating = StartCoroutine(Regenerate());
        }
    }

    public void Capture() {
        captured++;
    }

    public void Kill() {
        kills++;
    }
}
