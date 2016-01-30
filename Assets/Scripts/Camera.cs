using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour
{
    private Vector2 velocity;
    private float cameraY;

    public float smoothTimeX;
    public float smoothTimeY;

    public GameObject player;

    public bool bounds;

    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        cameraY = player.transform.position.y + 1.5f;

    }

    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);

        transform.position = new Vector3(posX, cameraY, transform.position.z);

        if (bounds)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x), Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
                                             Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
        }
    }
}