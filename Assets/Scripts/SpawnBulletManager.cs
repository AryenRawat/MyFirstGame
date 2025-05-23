using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnBulletManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject gameOverUI;
    public GameObject gameOverUI2;
    private float left=-10f;
    private float right=10f;
    private bool atRight=true;
    public Transform player;
    public bool gameOver=false;
    private float activeTime=4.0f;
    private float minActiveTime=1.0f;
    private float difficultyInterval=5.0f;
    public float bulletSpeed=5.0f;
    private float speedIncrement=1.0f;
    private float maxBulletSpeed=15.0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        StartCoroutine(SpawnBullets());
        InvokeRepeating("IncreaseDifficulty",difficultyInterval,difficultyInterval);
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(turn==false) bulletController.direction=Vector2.left;
        // else bulletController.direction=Vector2.right;
       // SpawnBullets();
       
    }

    IEnumerator SpawnBullets(){
        while(!gameOver){
            Vector2 pos = new Vector2(atRight?right:left,player.position.y);
            Vector2 dir = atRight?Vector2.left:Vector2.right; 
            GameObject bullet = Instantiate(bulletPrefab, pos,Quaternion.identity);
            BulletController bulletController=bullet.GetComponent<BulletController>();
            bulletController.direction=dir;
            bulletController.bulletManager = this;
            atRight=!atRight;
            yield return new WaitForSeconds(activeTime);
        }
    }


    void IncreaseDifficulty(){
        if(activeTime>minActiveTime){
            activeTime-=speedIncrement;
            Debug.Log("Decreased Spawn time for bullets");
        }
        if(bulletSpeed<maxBulletSpeed){
            bulletSpeed+=speedIncrement;
        }
    }
}
