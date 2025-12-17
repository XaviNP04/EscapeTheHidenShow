using UnityEngine;

public class GateAccessOutlineOff : MonoBehaviour
{

    private Outline outline;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }
}
