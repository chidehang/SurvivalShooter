using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 6f;

    private Vector3 movement;
    private Rigidbody playerBody;
    private Animator anim;

    private int floorMask;
    private float camRayLength = 100f;

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");

        playerBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Move(h, v);
        Turning();
        Animating(h, v);
    }

    //移动
    private void Move(float h, float v)
    {
        movement.Set(h, 0, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerBody.MovePosition(movement + transform.position);
    }

    //跟随鼠标面向
    private void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition); //获取一条射线
        //获取射线投射碰撞信息
        RaycastHit floorHit;
        if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 target = floorHit.point - transform.position;
            target.y = 0;
            Quaternion newRotation = Quaternion.LookRotation(target);
            playerBody.MoveRotation(newRotation);
        }
    }

    //动画
    private void Animating(float h, float v)
    {
        bool isWalking = h != 0 || v != 0;
        anim.SetBool("isWalking", isWalking);
    }
}
