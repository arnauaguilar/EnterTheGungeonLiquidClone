using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereUpdater : MonoBehaviour
{
    private Camera cam;
    private Renderer renderer;
    private Renderer areaRenderer;
    private LiquidSpawnerIdUpdater idController;
    void Start()
    {
        cam = Camera.main;
        renderer = GetComponent<Renderer>();
        areaRenderer = transform.GetChild(0).GetComponent<Renderer>();
        idController = transform.GetChild(0).GetComponent<LiquidSpawnerIdUpdater>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector3 pos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
        transform.position = new Vector3(pos.x, pos.y, 10);

        if (Input.GetMouseButtonDown(0))
        {
            idController.UpdateID();
        }
        
        if (Input.GetMouseButton(0))
        {
            renderer.enabled = true;
            areaRenderer.enabled = true;
        }
        else
        {
            renderer.enabled = false;
            areaRenderer.enabled = false;
        }

        

    }
}
