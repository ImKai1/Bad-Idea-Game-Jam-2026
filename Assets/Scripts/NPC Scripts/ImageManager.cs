using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class ImageManager : MonoBehaviour
{
    [Header("Put in the renderer of the plane")]
    public MeshRenderer rend; //use rend.material to change the image

    [Header("Images (Start Front turning right)")]
    public List<Material> Images; //Front-Right-...

    private Transform player;
    private float imageAngles;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (Images.Count > 0)
        {
            imageAngles = 360f / Images.Count;
        }
    }

    void Update()
    {
        if (player == null) return;

        Vector3 dirToPlayer = player.position - transform.position;
        dirToPlayer.y = 0f;

        Vector3 forward = transform.forward;
        forward.y = 0f;

        float angle = Vector3.SignedAngle(forward, dirToPlayer, Vector3.up);

        float shiftedAngle = angle + (imageAngles / 2f);

        if (shiftedAngle < 0f)
        {
            shiftedAngle += 360f;
        }

        int index = Mathf.FloorToInt(shiftedAngle / imageAngles);

        index = index % Images.Count;

        rend.material = Images[index];
    }
}
