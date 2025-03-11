using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fightManager : MonoBehaviour
{
    AudioSource AudioSource;
    [SerializeField] AudioClip phase1music;
    [SerializeField] AudioClip phase2music;
    [SerializeField] AudioClip phase3music;
    [SerializeField] Sprite phase1sprite;
    [SerializeField] Sprite phase2sprite;
    [SerializeField] Sprite phase3sprite;
    [SerializeField] Vector2 phase1pos;
    [SerializeField] Vector2 phase2pos;
    [SerializeField] Vector2 phase3pos;
    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject lossMenu;
    public int phase = 0;
    bool started;
    public GameObject healthUI;
    bool finished;
    public void lose()
    {
        if (finished) return;
        finished = true;
        gameObject.GetComponent<bossAttacks>().enabled = false;
        GameObject.Find("Player").GetComponent<characterControl>().enabled = false;
        GameObject.Find("attackBox").GetComponent<playerAttack>().enabled = false;
        phase = 0;
        Instantiate(lossMenu, Vector2.zero, Quaternion.identity);
    }
    public void win()
    {
        if (finished) return;
        finished = true;
        gameObject.GetComponent<bossAttacks>().enabled = false;
        GameObject.Find("Player").GetComponent<characterControl>().enabled = false;
        GameObject.Find("attackBox").GetComponent<playerAttack>().enabled = false;
        phase = 0;
        Instantiate(winMenu, Vector2.zero, Quaternion.identity) ;
    }
    void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
        if (!started) { startBattle(); }
    }
    public void startBattle()
    {
        finished = false;
        gameObject.GetComponent<bossAttacks>().enabled = true;
        GameObject.Find("Player").GetComponent<characterControl>().enabled = true;
        GameObject.Find("attackBox").GetComponent<playerAttack>().enabled = true;
        gameObject.GetComponent<health>().Health = gameObject.GetComponent<health>().startHealth;
        GameObject.Find("Player").GetComponent<health>().Health = GameObject.Find("Player").GetComponent<health>().startHealth;
        started = true;
        AudioSource = GetComponent<AudioSource>();
        nextPhase();
        GameObject.Find("Player").transform.position = new Vector2(7.31f, -2f);
    }
    void Update()
    {
        healthUI = GameObject.Find("healthUI");
        healthUI.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.6f - GameObject.Find("Player").GetComponent<health>().Health / GameObject.Find("Player").GetComponent<health>().startHealth * 0.6f);
    }
    public void nextPhase()
    {
        phase++;
        if (phase == 1) { changePhase(phase1music, "phase1",phase1pos,phase1sprite); }
        if (phase == 2) { changePhase(phase2music, "phase2", phase2pos, phase2sprite); }
        if (phase == 3) { changePhase(phase3music, "phase3", phase3pos, phase3sprite); }
        if (phase == 4) { win(); }
    }
    void changePhase(AudioClip clip, string sceneName, Vector2 bossPos, Sprite bossSprite)
    {
        SceneManager.LoadScene(sceneName);
        AudioSource.clip = clip;
        AudioSource.Play();
        transform.position = bossPos;
        GetComponent<SpriteRenderer>().sprite = bossSprite;
    }
}
