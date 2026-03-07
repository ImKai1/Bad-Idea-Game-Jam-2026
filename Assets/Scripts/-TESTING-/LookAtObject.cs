using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    [Header ("Put the object here")]
    public GameObject lookAtObject;

    void Update()
    {
        transform.LookAt(lookAtObject.transform);
    }
}
