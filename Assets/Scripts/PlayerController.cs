using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed=5.0f;
    public float playerJump=7.0f;
    public float climbSpeed=4.0f;
    private bool isClimbing=false;
    private Vector2 climbTarget;
    private Rigidbody2D playerRb;
    public bool isGrounded = true;
    private YoYoController yoYoController;
    public GameObject yoyo;
    public SpawnManager spawnManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        yoYoController = yoyo.GetComponent<YoYoController>();
        spawnManager=FindFirstObjectByType<SpawnManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
       if(isClimbing){
            transform.position = Vector2.MoveTowards(transform.position, climbTarget,climbSpeed*Time.deltaTime);
            playerRb.linearVelocity=Vector2.zero;
            if(Vector2.Distance(transform.position, climbTarget)<0.1f){
                isClimbing=false;
                playerRb.bodyType = RigidbodyType2D.Dynamic;
                yoYoController.ReleaseYoYo();
                isGrounded=true;
            } 
       }else{
        float horizontalInput = Input.GetAxis("Horizontal");
        // if(isGrounded){
        //     playerRb.linearVelocity = new Vector2(horizontalInput*playerSpeed,playerRb.linearVelocity.y);
        // }
        playerRb.linearVelocity = new Vector2(horizontalInput*playerSpeed,playerRb.linearVelocity.y);
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            playerRb.AddForce(playerJump*Vector2.up,ForceMode2D.Impulse);
            isGrounded=false;
        }

        // if(Input.GetMouseButtonDown(0) && yoYoController.yoyoThrown==false){
        //     yoYoController.throwYoYo();
        // }



        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x=Mathf.Clamp(viewPos.x, 0.05f,0.95f);
        transform.position=Camera.main.ViewportToWorldPoint(viewPos);
        }
        
    }

    public void StartClimbing(Vector2 targetPos){
        isClimbing=true;
        climbTarget=targetPos;
        playerRb.bodyType = RigidbodyType2D.Kinematic;
        playerRb.linearVelocity=Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")){
            isGrounded=true;
        }
       
        if(other.gameObject.CompareTag("Platform")){
            Debug.Log("Collision entered");
            isGrounded=true;
            FindAnyObjectByType<UIManager>().AddScore(10);
            //spawnManager.SpawnPlatforms(other.transform.position);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")){
            isGrounded=false;
        }
    }

}
