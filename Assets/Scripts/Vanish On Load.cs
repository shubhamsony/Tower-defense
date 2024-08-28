using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishOnLoad : MonoBehaviour
{
    MeshRenderer m;
    // Start is called before the first frame update
    void Start()
    {
        m = GetComponent<MeshRenderer>();
        m.enabled = false;
    }
}
