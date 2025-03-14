using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using Unity.VisualScripting;

public class playerAttack : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject gunSpawn;
    public List<GameObject> swordList = new List<GameObject>();
    [SerializeField] float attackCooldownSword;
    [SerializeField] float attackCooldownGun;
    [SerializeField] float swordDmg;
    [SerializeField] float gunDmg;
    [SerializeField] float bloodMult;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletBlood;
    [SerializeField] float bulletSpeed;
    Vector2 startPos;
    float attackTime;
    [SerializeField] AnimationClip startAimVer;
    [SerializeField] AnimationClip startAimDia;
    [SerializeField] AnimationClip startAimUp;
    [SerializeField] AnimationClip swordAttack;
    bool aiming;
    int activeWeapon = 0;
    void Start()
    {
        startPos = gameObject.transform.localPosition;
    }

    void Update()
    {
        if (player.GetComponent<characterControl>().dashTime > 0 ) { return; }
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") <= 0 || activeWeapon != 1 ) { aiming = false; } else if (activeWeapon == 1) { aiming = true; }
        if (Input.GetAxisRaw("Horizontal") != 0){ transform.localPosition = new Vector2(Mathf.Abs(startPos.x) * player.GetComponent<characterControl>().dir, startPos.y); }
        attackTime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Z) && attackTime <= 0 && !player.GetComponent<characterControl>().crouching) { if (player.GetComponent<bloodOrb>().hasOrb) { meleeAttackBlood(); } else meleeAttack(); activeWeapon = 2;}
        if (Input.GetKey(KeyCode.X) && attackTime <= 0 && !player.GetComponent<characterControl>().crouching) { if (player.GetComponent<bloodOrb>().hasOrb) { gunAttackBlood(); } else gunAttack(); if (activeWeapon != 1 ||!Input.GetButton("Vertical") && !Input.GetButton("Horizontal")) { gunAnimInit(); } activeWeapon = 1;  }
        if (aiming) { player.GetComponent<characterControl>().srTop.GetComponent<Animator>().SetBool("aiming", true); } else { player.GetComponent<characterControl>().srTop.GetComponent<Animator>().SetBool("aiming", false); }
        gunAnim();
    }
    void gunAnim()
    {
        if (activeWeapon != 1) { return; }
        if (Input.GetButtonDown("Horizontal") && !Input.GetButton("Vertical") || Input.GetButton("Horizontal") && Input.GetButtonUp("Vertical")) { changeAnim(startAimVer, true); return; }
        if (Input.GetButton("Horizontal") && Input.GetButtonDown("Vertical") || Input.GetButtonDown("Horizontal") && Input.GetButton("Vertical") || Input.GetButtonUp("Horizontal") && Input.GetButton("Horizontal") && Input.GetButton("Vertical")) { changeAnim(startAimDia, true); return; }
        if (!Input.GetButton("Horizontal") && Input.GetButtonDown("Vertical") || Input.GetButton("Vertical") && Input.GetButtonUp("Horizontal")) { changeAnim(startAimUp, true); return; }
    }
    void gunAnimInit()
    {
        if (Input.GetButton("Horizontal") && Input.GetButton("Vertical")) { changeAnim(startAimDia, true); return; }
        if (Input.GetButton("Vertical")) { changeAnim(startAimUp, true); return; }
        changeAnim(startAimVer, true);
    }
    void meleeAttack()
    {
        attackTime = attackCooldownSword;
        foreach (GameObject obj in swordList)
        {
            obj.GetComponent<health>().Damage(swordDmg);
        }
        changeAnim(swordAttack, true);
    }
    void meleeAttackBlood()
    {

    }
    void gunAttack()
    {
        GameObject curBullet = Instantiate(bullet, gunSpawn.transform.position, Quaternion.Euler(0, 0, 0));
        if (player.GetComponent<characterControl>().lookingUp) { curBullet.transform.rotation *= Quaternion.Euler(new Vector3(0, 0, Input.GetAxisRaw("Horizontal")).normalized * -45); }
        else { curBullet.transform.rotation *= Quaternion.Euler(new Vector3(0, 0, player.GetComponent<characterControl>().dir).normalized * 90); }
        curBullet.GetComponent<projectile>().damage = gunDmg;
        if (player.GetComponent<characterControl>().lookingUp) { curBullet.GetComponent<Rigidbody2D>().velocity += new Vector2(0, 1); attackTime = attackCooldownGun; }
        else { curBullet.GetComponent<Rigidbody2D>().velocity += new Vector2(player.GetComponent<characterControl>().dir, 0); }
        if (Input.GetAxisRaw("Horizontal") != 0) { curBullet.GetComponent<Rigidbody2D>().velocity += new Vector2(player.GetComponent<characterControl>().dir, 0); }
        curBullet.GetComponent<Rigidbody2D>().velocity = curBullet.GetComponent<Rigidbody2D>().velocity.normalized * bulletSpeed;
        attackTime = attackCooldownGun;
    }
    void gunAttackBlood()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attackable" || collision.gameObject.tag == "boss")
        {
            swordList.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attackable" || collision.gameObject.tag == "boss")
        {
            swordList.Remove(collision.gameObject);
        }
    }
    public void changeAnim(AnimationClip curAnim, bool top)
    {
        if (top) { player.GetComponent<characterControl>().animTop.Play(curAnim.name); } else { player.GetComponent<characterControl>().animBottom.Play(curAnim.name); }
    }
}
