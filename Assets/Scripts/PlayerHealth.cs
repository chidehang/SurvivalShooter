using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int startHP = 100;
    public int curHP;
    public AudioClip deadAudio;
    public Slider HPSlider;
    public Image damageImage;
    public float flashSpeed = 10f;
    public Color flashColor = new Color(1, 0, 0, 0.2f); //玩家受到攻击屏幕闪烁颜色

    private bool isDead;    //是否死亡
    private bool damaged;   //是否受到伤害

    private PlayerMovement playerMovement;
    private PlayerShooting playerShooting;
    private Animator anim;
    private AudioSource playerAudio;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        playerAudio = GetComponent<AudioSource>();

        curHP = startHP;

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(damaged)
        {
            damageImage.color = flashColor;
            damaged = false;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed);
        }
	}

    //受到伤害
    public void TakeDamaged(int amount)
    {
        damaged = true;

        curHP -= amount;
        HPSlider.value = curHP;

        playerAudio.Play();

        if(curHP <= 0 && !isDead)
        {
            Death();
        }
    }

    //死亡
    public void Death()
    {
        isDead = true;
        anim.SetTrigger("Die"); //播放死亡动画
        playerAudio.clip = deadAudio;
        playerAudio.Play();

        playerMovement.enabled = false; //死亡后禁止控制
        playerShooting.enabled = false;
    }

    public void RestartLevel()
    {
        //重新加载当前已经加载的场景
        Application.LoadLevel(Application.loadedLevel);
    }
}
