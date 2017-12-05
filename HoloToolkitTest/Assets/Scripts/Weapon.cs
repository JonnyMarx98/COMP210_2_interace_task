using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Pick Ups/Weapons")]
public class Weapon : ScriptableObject
{
    [Tooltip("The name of the weapon in-game")]
    public string name;
    [Tooltip("Is this weapon the default? (Can only be checked on ONE weapon)")]
    public bool defaultWeapon;
    [Tooltip("Damage per bullet")]
    public float damage;
    [Tooltip("Maximum ammo when the gun is picked up")]
    public int maxAmmo;
    [Tooltip("Amount of time you must wait between each shot")]
    public float shotCooldown;
    [Tooltip("Force applied to Rigidbody hit by bullet")]
    public float hitImpactForce;
    [Tooltip("How fast the bullet travels")]
    public float bulletVelocity;
    [Tooltip("The bullet prefab which will be fired by the gun")]
    public GameObject bulletPrefab;
    [Tooltip("Chance for the weapon to spawn, higher this number; the higher the chance of spawning")]
    [Range(0, 1)]
    public float spawnChance;
    [Tooltip("The fire rate of the gun (Spread is for shotguns)")]
    public FireRate fireRate;
    [Tooltip("How long the player can carry the gun for")]
    public float timeActive;

    public enum FireRate
    {
        SemiAutomatic,
        Spread,
        Automatic
    };
}

