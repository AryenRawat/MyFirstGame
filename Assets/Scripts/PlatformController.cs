using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float activePlatformTimer=7.0f;
    public SpawnManager spawnManager;
    private Coroutine deleteTimer;
    void Start()
    {
        spawnManager = FindAnyObjectByType<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupOneWayPlatforms(){
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        if(boxCollider2D==null){boxCollider2D=gameObject.AddComponent<BoxCollider2D>();}
        PlatformEffector2D effector2D = GetComponent<PlatformEffector2D>();
        if (effector2D==null){
            effector2D=gameObject.AddComponent<PlatformEffector2D>();
        }
        effector2D.useOneWay=true;
        effector2D.surfaceArc=170f;

    }

    IEnumerator deletePlatform(GameObject platform){
        yield return new WaitForSeconds(activePlatformTimer);
        Destroy(platform);
        spawnManager.cntPlatforms--;
        spawnManager.platforms.Remove(platform);

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            if(deleteTimer!=null){
                StopCoroutine(deleteTimer);
            }
            deleteTimer=StartCoroutine(deletePlatform(gameObject));
        }
    }
}
