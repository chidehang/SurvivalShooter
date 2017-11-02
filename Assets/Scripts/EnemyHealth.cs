using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int startHP = 100;
    public int curHP;
    public float sinkSpeed = 2.5f;
    public AudioClip deadClip;

    private Animator anim;
    private CapsuleCollider capsuleCollider;
    private NavMeshAgent nav;
    private ParticleSystem hitParticle;
    private ParticleSystem deadParticle;
    private AudioSource enemyAudio;

    private bool isDead;
    private bool isSinking; //是否下沉

    private void Awake()
    {
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        nav = GetComponent<NavMeshAgent>();
        ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
        enemyAudio = GetComponent<AudioSource>();

        hitParticle = particles[0];
        deadParticle = particles[1];
        curHP = startHP;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
	}

    public void TakeDamaged(int amount, Vector3 hitPoint)
    {
        if (isDead)
            return;

        curHP -= amount;
        enemyAudio.Play();

        if(hitParticle)
        {
            hitParticle.transform.position = hitPoint;
            hitParticle.Play();
        }
        

        if(curHP <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        isDead = true;

        capsuleCollider.isTrigger = true; //变成触发器，不会产生物理碰撞阻挡

        anim.SetTrigger("Dead");

        enemyAudio.clip = deadClip;
        enemyAudio.Play();
    }

    //死亡下沉
    public void StartSinking()
    {
        nav.enabled = false;

        if(deadParticle)
            deadParticle.Play();

        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;

        Destroy(gameObject, 2);
    }
}
