using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    public int shootDamage = 20; //攻击力
    public float shootTime = 0.15f; //射击间隔
    public float shootRange = 100;  //射程

    float timer;
    Ray shootRay;   //子弹射线
    RaycastHit shootHit;    //射线命中端点信息
    int shootableMask;      //可射击层
    ParticleSystem gunParticles;    //开枪粒子效果
    LineRenderer gunLine;
    Light gunLight;
    float effectsDisplayTime = 0.2f;//开枪粒子效果持续时间的百分比
    AudioSource gunAudio;

    private void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
        gunAudio = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if(Input.GetButton("Fire1") && timer>=shootTime && Time.timeScale!=0)
        {
            Shoot();
        }

        if(timer >= shootTime*effectsDisplayTime)
        {
            ToggleEffects(false);
        }
	}

    //开关开枪特效
    void ToggleEffects(bool enable)
    {
        gunLine.enabled = enable;
        gunLight.enabled = enable;
    }

    void Shoot()
    {
        timer = 0;
        ToggleEffects(true);

        gunAudio.Play();

        gunParticles.Stop();
        gunParticles.Play();

        //设置开枪火花效果线段的端点
        gunLine.SetPosition(0, transform.position);
        //构建射线
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        //发射射线
        if(Physics.Raycast(shootRay, out shootHit, shootRange, shootableMask))
        {
            //命中
            gunLine.SetPosition(1, shootHit.point);
            EnemyHealth health = shootHit.collider.GetComponent<EnemyHealth>();
            if(health)
            {
                health.TakeDamaged(shootDamage, shootHit.point);
            }
        }
        else
        {
            gunLine.SetPosition(1, transform.position + shootRay.direction * shootRange);
        }

    }
}
