using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidSpawnerIdUpdater : MonoBehaviour
{
    private Material mat;
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    public void UpdateID(int forcedId = -1)
    {
        Color c = mat.color;
        
        if (forcedId == -1)
            c.b += 0.1f;
        else
            c.b = forcedId;
        
        c.b = c.b % 1.0f;
        
        mat.color = c;
    }
}
