using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public float attackTime = 0.5f; //攻击间隔
    public int attackDamage = 10; //攻击伤害值

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;

    bool playerInRange; //是否处于攻击范围
    float timer = 0;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();       
    }

    // Use this for initialization
    void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //玩家进入攻击范围
        if(other.tag.Equals("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //玩家离开攻击范围
        if (other.tag.Equals("Player"))
            playerInRange = false;
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        if(playerInRange && timer>=attackTime && enemyHealth.curHP>0)
        {
            Attack();
        }

        if(playerHealth.curHP <= 0)
        {
            //玩家死亡，停止移动
            anim.SetTrigger("PlayerDead");
        }
	}

    void Attack()
    {
        timer = 0;
        if(playerHealth.curHP > 0)
        {
            playerHealth.TakeDamaged(attackDamage);
        }
    }
}
