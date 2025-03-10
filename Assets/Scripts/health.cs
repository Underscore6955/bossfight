using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    public float startHealth;
    public float Health;
    float hurtTime = 0.4f;
    float curHurtTime;
    [SerializeField] List<SpriteRenderer> renderers = new List<SpriteRenderer>();
    void Start()
    {
        if (renderers.Count == 0) { renderers.Add(GetComponent<SpriteRenderer>()); } 
        Health = startHealth;
    }

    void Update()
    {
        if (Health <= 0) Death();
        hitAnimation();
    }
    public void Damage(float damage)
    {
        curHurtTime = hurtTime;
        Health -= damage;
        if (gameObject.name == "boss" && GetComponent<health>().Health < (float)(3-GetComponent<fightManager>().phase) / 3 * GetComponent<health>().startHealth) { GetComponent<fightManager>().nextPhase(); }
    }
    void Death()
    {
        if (tag == "Player") { GameObject.Find("boss").GetComponent<fightManager>().lose(); }
        if (tag == "Attackable") { if (GameObject.Find("attackBox").GetComponent<playerAttack>().swordList.Contains(gameObject)) { GameObject.Find("attackBox").GetComponent<playerAttack>().swordList.Remove(gameObject); } Destroy(gameObject); }
    }
    private void hitAnimation()
    {
        curHurtTime-= Time.deltaTime;
        if (curHurtTime > 0) { foreach (var curRenderer in renderers) { curRenderer.color = new Color(255f / 255f, 150f /255f, 150f / 255f); } } else { foreach (var curRenderer in renderers) { curRenderer.color = Color.white; } }
    }
}
