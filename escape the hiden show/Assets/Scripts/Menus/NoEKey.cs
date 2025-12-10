using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NoEKey : MonoBehaviour
{

    public Image eKeyImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eKeyImage.enabled = false;
    }
}
