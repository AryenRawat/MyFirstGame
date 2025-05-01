using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed=5.0f;
    
    public Vector2 direction;
    private Rigidbody2D bulletRb;
    public SpawnBulletManager bulletManager;
    public GameObject Player;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletRb = GetComponent<Rigidbody2D> ();
        bulletManager = FindAnyObjectByType<SpawnBulletManager>();
        Player = GameObject.FindWithTag("Player");
        
      
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(direction.x==0){Destroy(gameObject);}
        if( transform.position.x < -10.3f){
            Debug.Log("Out Of Scene of left");
            Destroy(gameObject);
        }else if(transform.position.x>10.3f){
            Debug.Log("out of scene of right");
            Destroy(gameObject);
        }else bulletRb.linearVelocity=direction*bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
       // Debug.Log("Hit something");
        if(other.gameObject.CompareTag("Player")){
            Debug.Log("Player Died !!!!");
            bulletManager.gameOver = true;
            bulletManager.gameOverUI.SetActive(true);
            bulletManager.gameOverUI2.SetActive(true);
            Time.timeScale = 0;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
}
