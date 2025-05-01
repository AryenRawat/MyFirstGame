using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public Transform cameraTransform;
    public float scrollSpeed=0.1f;
    private Material material;
    private float starY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        starY = cameraTransform.position.y;
        
    }

    // Update is called once per frame
    void Update()
    {
        float cameraDelta = cameraTransform.position.y-starY;
        float offset = cameraDelta*scrollSpeed*1.0f;
        material.mainTextureOffset=new Vector2(0, offset);
        
    }
}
