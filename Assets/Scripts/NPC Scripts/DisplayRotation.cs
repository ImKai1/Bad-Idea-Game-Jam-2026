using UnityEngine;

public class DisplayRotation : MonoBehaviour
{
    private Transform player;
    public float speed = 10;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Quaternion Lookat = Quaternion.LookRotation(new Vector3(player.position.x, transform.position.y, player.position.z) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Lookat, speed * Time.deltaTime);
    }
}
