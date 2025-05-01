using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class SpawnManager : MonoBehaviour
{
    public GameObject platform;
    public Transform player;
    public float minX=-5.0f;
    public float maxX=5.0f;
    public float platformSpacing=3f;
    public float platformActive=7.0f;
    public int cntPlatforms=0;

    public float platDelay=1.5f;
    private float lastY=0;
    private float plat1=0;

   public List<GameObject>platforms = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //SpawnPlatforms();
        //platform = GameObject.FindWithTag("Platform");
        player = GameObject.FindWithTag("Player").transform;
        lastY = player.position.y;
        plat1=lastY;
        StartCoroutine(SpawnPlatformsCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        //InvokeRepeating("SpawnPlatforms",0f,2f);
        // if(cntPlatforms==0){
        //     SpawnPlatforms(player.position);
        // }
       
    }

    IEnumerator SpawnPlatformsCoroutine(){
        while(true){yield return new WaitForSeconds(platDelay);
        cntPlatforms++;
        float newY=lastY+platformSpacing;
        float newX=Random.Range(minX,maxX);
        Vector3 pos = new Vector3(newX,newY,0);
        GameObject newPlatform = Instantiate(platform, pos, Quaternion.identity);
        platforms.Add(newPlatform);
        lastY=newY;
        if(lastY>15){lastY=plat1;}
        }
    }

    

    public void SpawnPlatforms(Vector3 currPlatformPos){
        cntPlatforms++;
       // Debug.Log(currPlatformPos.y);
        float rangeY=currPlatformPos.y+platformSpacing;
        float randomX=Random.Range(minX, maxX);
        Vector3 pos=new Vector3(randomX,rangeY,0);
        GameObject newPlatform = Instantiate(platform, pos, Quaternion.identity);
        platforms.Add(newPlatform);
    }
    
//    private IEnumerator platformDelete(GameObject plat){
//        yield return new WaitForSeconds(platformActive);
//        Destroy(plat);
//        platforms.Remove(plat);
//    }

//    private void OnCollisionEnter2D(Collision2D other) {
//        if(other.gameObject.CompareTag("Player")){
//           GameObject hitPlat=gameObject;
//           StartCoroutine(platformDelete(hitPlat));
//        }
//    }

}
