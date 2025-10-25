using UnityEngine;

public class GoalReached : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            transform.position += new Vector3(5.0f,0.0f, 5.0f);
        }
    }
}
