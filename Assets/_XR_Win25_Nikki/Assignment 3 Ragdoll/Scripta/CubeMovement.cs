using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public float distance = 5f;
    public float speed = 2f;
    private Vector3 startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        float zPosition = startPosition.z + Mathf.PingPong(Time.time * speed, distance);
        transform.position = new Vector3(transform.position.x, transform.position.y, zPosition);
    }
}
