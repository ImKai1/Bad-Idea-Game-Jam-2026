using UnityEngine;

public class ENTITYTESTING : MonoBehaviour
{
    void Update()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(r, out hit))
        {
            foreach (AdventurerEntity a in Resources.FindObjectsOfTypeAll<AdventurerEntity>())
            {
                a.WalkToTarget(hit.point);
            }
        }
    }
}
