using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereUpdater : MonoBehaviour
{
    private Camera cam;
    private Renderer renderer;
    void Start()
    {
        cam = Camera.main;
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        transform.position = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 5));

        if (Input.GetMouseButton(0))
            renderer.enabled = true;
        else
        {
            renderer.enabled = false;
        }

    }
}
