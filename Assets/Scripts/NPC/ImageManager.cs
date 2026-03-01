using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class ImageManager : MonoBehaviour
{
    [Header ("4 sided or 8 sided?")]
    public bool Eightsided = false;

    [Header("Put in the renderer of the plane")]
    public MeshRenderer rend; //use rend.material to change the image

    [Header("Images")]
    public Material Front;
    public Material Rear;
    public Material Left;
    public Material Right;
    [Header ("Only fill these when 8 sided")]
    public Material FLeft;
    public Material FRight;
    public Material RLeft;
    public Material RRight;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        Vector3 dirToPlayer = player.position - transform.position;
        dirToPlayer.y = 0f;

        Vector3 forward = transform.forward;
        forward.y = 0f;

        float angle = Vector3.SignedAngle(forward, dirToPlayer, Vector3.up);

        if (Eightsided)
        {
            if (angle > -22.5f && angle <= 22.5f)
                rend.material = Front;
            else if (angle > 22.5f && angle <= 67.5f)
                rend.material = FRight;
            else if (angle > 67.5f && angle <= 112.5f)
                rend.material = Right;
            else if (angle > 112.5f && angle <= 157.5f)
                rend.material = RRight;
            else if (angle < -22.5f && angle >= -67.5f)
                rend.material = FLeft;
            else if (angle < -67.5f && angle >= -112.5f)
                rend.material = Left;
            else if (angle < -112.5f && angle >= -157.5f)
                rend.material = RLeft;
            else
                rend.material = Rear;
        }
        else
        {
            if (angle > -45f && angle <= 45f)
                rend.material = Front;
            else if (angle > 45f && angle <= 135f)
                rend.material = Right;
            else if (angle < -45f && angle >= -135f)
                rend.material = Left;
            else
                rend.material = Rear;
        }
    }
}
