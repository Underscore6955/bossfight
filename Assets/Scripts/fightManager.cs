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
    [SerializeField] Transform phase1pos;
    [SerializeField] Transform phase2pos;
    [SerializeField] Transform phase3pos;
    public int phase = 0;
    public void lose()
    {

    }
    public void win()
    {

    }
    public void Start()
    {
        DontDestroyOnLoad(gameObject);
        nextPhase();
        AudioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        
    }
    public void nextPhase()
    {
        phase++;
        if (phase == 1) { changePhase(phase1music, "phase1",phase1pos,phase1sprite); }
        if (phase == 2) { changePhase(phase2music, "phase2", phase2pos, phase2sprite); }
        if (phase == 3) { changePhase(phase3music, "phase3", phase3pos, phase3sprite); }
    }
    void changePhase(AudioClip clip, string sceneName, Transform bossPos, Sprite bossSprite)
    {
        SceneManager.LoadScene(sceneName);
        AudioSource.clip = clip;
       transform.position = bossPos.position;
        GetComponent<SpriteRenderer>().sprite = bossSprite;
    }
}
