using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class YoYoController : MonoBehaviour
{
    public float throwSpeed=3.0f;
    public float yoyoRange=25.0f;
    public float yoyoReturnSpeed=5.0f;
    private Rigidbody2D yoyoRb;
    public bool yoyoThrown=false;
    public Transform player;
    public bool isAttached=false;
    private Vector2 attachPoint; 
    private Vector2 throwDirection;
    private LineRenderer lineRender;
    private Rigidbody2D playerRb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        yoyoRb = GetComponent<Rigidbody2D>();
        lineRender = GetComponent<LineRenderer>();
        lineRender.positionCount=2;
        playerRb= GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(yoyoThrown){
            lineRender.SetPosition(0,player.position);
            lineRender.SetPosition(1,transform.position);
            returnYoYo();
        }
        
    }

    public void returnYoYo(){
        float distance = Vector2.Distance(player.position,transform.position);
        if(distance > yoyoRange){
            Vector2 returnDirection = (player.position - transform.position).normalized;
            yoyoRb.linearVelocity = returnDirection*yoyoReturnSpeed;
        }else if(distance<0.2f){
            yoyoRb.linearVelocity=Vector2.zero;
            yoyoRb.bodyType=RigidbodyType2D.Kinematic;
            yoyoThrown=false;
            Vector3 mousePos=(Camera.main.ScreenToWorldPoint(Input.mousePosition)-player.position).normalized;
            mousePos.z=0;
            transform.position=player.position + mousePos*1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Is it hitting");
        if(other.gameObject.CompareTag("Platform")){
            Debug.Log("YoYo hit platform");
            yoyoRb.linearVelocity=Vector2.zero;
            yoyoThrown=false;
            isAttached=true;
            playerRb.bodyType = RigidbodyType2D.Kinematic;
            attachPoint=other.contacts[0].point;
            PlayerController playerController=player.GetComponent<PlayerController>();
            if(playerController != null){
                playerController.StartClimbing(attachPoint);
            }
        }
    }

    public void ReleaseYoYo(){
        isAttached=false;
        playerRb.bodyType = RigidbodyType2D.Dynamic;
        yoyoThrown=true;
    }

    public void throwYoYo(){
            yoyoThrown = true;
            yoyoRb.bodyType=RigidbodyType2D.Dynamic;
            throwDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - player.position  ;
            yoyoRb.linearVelocity = throwDirection.normalized*throwSpeed;
    }
}
