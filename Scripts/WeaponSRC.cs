using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponsRC", menuName = "SmthEpikIDK/Weapon", order = 0)]
public class WeaponSRC : ScriptableObject {
    public float damage;
    public float bullet_force;
    public float shooting_speed;

    public bool shoots_grenades;
    public Sprite sprite;
}
