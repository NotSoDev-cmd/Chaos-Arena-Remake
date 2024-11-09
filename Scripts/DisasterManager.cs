using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;
using TMPro;

public class DisasterManager : MonoBehaviour
{
    public Weapon player_weapon;
    public Meele player_meele;
    public MeeleSRC[] meeles;
    public WeaponSRC[] weapons;
    public float dis_time = 15f;

    public int disasters_count = 2;
    private int last_disaster = -1;

    public GameObject[] fire;

    public GameObject hespawn;

    public Texture2D cursor;
    
    

    public GameObject[] LMGS;

    private void Awake() {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }


    void Start() {
        StartCoroutine(Disaster());
    }

    void Update() {

        Debug.Log(FindObjectsOfType<Drone>().Length);
        if (last_disaster == 1) {
            foreach(GameObject lmg in LMGS) {
                lmg.SetActive(true);
            }
        }
    }

    IEnumerator Disaster() {
        yield return new WaitForSeconds(dis_time);
        MeeleSRC meele = meeles[Random.Range(0, meeles.Length)];
        WeaponSRC weapon = weapons[Random.Range(0, weapons.Length)];
        //Debug.Log(meele);
        ///Debug.Log(weapon);
        player_meele.SetMeele(meele);
        player_weapon.SetWeapon(weapon);
        

        if (last_disaster != -1) {
            if (last_disaster == 0) {
                PostProcessProfile profile = FindObjectOfType<PostProcessVolume>().profile;
                Vignette vigntette;
                profile.TryGetSettings(out vigntette);
                vigntette.intensity.value = 0.159f;
            } else if (last_disaster == 1) {
                foreach(GameObject lmg in LMGS) {
                    lmg.GetComponent<LMG>().Disable();
                }
            } else if (last_disaster == 2) {
                foreach(GameObject molik in fire) {
                    molik.SetActive(false);
                }
            } else if (last_disaster == 3) {
                PostProcessProfile profile = FindObjectOfType<PostProcessVolume>().profile;
                LensDistortion vigntette;
                profile.TryGetSettings(out vigntette);
                vigntette.intensity.value = 30;
            } else if (last_disaster == 4) {
                hespawn.SetActive(false);
            } else if (last_disaster == 5) {
                //FindObjectOfType<TextMeshProUGUI>().text = "Disaster: Anti-Gravity";
                GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale = 7.5f;
                float scale = Mathf.Abs(GameObject.Find("Player").transform.localScale.x);
                GameObject.Find("Player").transform.localScale = new Vector3(scale, scale, scale);
            } else if (last_disaster == 6) {
                PostProcessProfile profile = FindObjectOfType<PostProcessVolume>().profile;
                Bloom vigntette;
                profile.TryGetSettings(out vigntette);
                vigntette.intensity.value = 0f;

            //FindObjectOfType<TextMeshProUGUI>().text = "Disaster: Discord light theme";
            }
        } 
        
        int disaster = Random.Range(0, disasters_count);
        while (disaster == last_disaster) {
            disaster = Random.Range(0, disasters_count);
        }
        last_disaster = disaster;
        if (disaster == 0) {
            BloodBath();
        } else if (disaster == 1) {
            LMG_MADNESS();
        } else if (disaster == 2) {
            Fire();
        } else if (disaster == 3) {
            Drunk();
        } else if (disaster == 4) {
            FindObjectOfType<TextMeshProUGUI>().text = "Disaster: Its raining";
            hespawn.SetActive(true);
        } else if (disaster == 5) {
            FindObjectOfType<TextMeshProUGUI>().text = "Disaster: Anti-Gravity";
            GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale = -7.5f;
            float scale = Mathf.Abs(GameObject.Find("Player").transform.localScale.x);
            GameObject.Find("Player").transform.localScale = new Vector3(scale, -scale, scale);
        } else if (disaster == 6) {
            PostProcessProfile profile = FindObjectOfType<PostProcessVolume>().profile;
            Bloom vigntette;
            profile.TryGetSettings(out vigntette);
            vigntette.intensity.value = 60f;
            Debug.Log(vigntette.intensity.value);

            FindObjectOfType<TextMeshProUGUI>().text = "Disaster: Discord light theme";
        } else if (disaster == 7) {
            FindObjectOfType<TextMeshProUGUI>().text = "Disaster: Sneaky enemie";
        }

        FindObjectOfType<AudioManager>().Play("disaster");

        StartCoroutine(Disaster());
    }

    public int LastDisaster() {
        return last_disaster;
    }

    void BloodBath() {
        PostProcessProfile profile = FindObjectOfType<PostProcessVolume>().profile;
        Vignette vigntette;
        profile.TryGetSettings(out vigntette);
        vigntette.intensity.value = 1f;

        FindObjectOfType<TextMeshProUGUI>().text = "Disaster: Blood Bath";
    }

    void LMG_MADNESS() {
        FindObjectOfType<TextMeshProUGUI>().text = "Disaster: LMG MADNESS!!!!";
        foreach (GameObject lmg in LMGS) {
            lmg.SetActive(true);
        }
    }

    void Fire() {
        FindObjectOfType<TextMeshProUGUI>().text = "Disaster: Hell F1re";
        foreach(GameObject molik in fire) {
            molik.SetActive(true);
        }
    }

    void Drunk() {
        PostProcessProfile profile = FindObjectOfType<PostProcessVolume>().profile;
        LensDistortion vigntette;
        profile.TryGetSettings(out vigntette);
        vigntette.intensity.value = -80;
        FindObjectOfType<TextMeshProUGUI>().text = "Disaster: You are drunk.";
        
    }
}
