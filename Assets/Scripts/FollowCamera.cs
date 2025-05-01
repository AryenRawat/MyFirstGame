using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0,3.8f,-10);
    public float speed = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(transform.position.x, -4.7814f + offset.y, -10);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos= new Vector3(transform.position.x,player.position.y+offset.y,transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos,speed*Time.deltaTime);
        // Debug.Log("Camera Position: " + transform.position);
        // Debug.Log("Player Position: " + player.position);
        
    }
}
