using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    protected GameObject raiderInfoPanel;
    [SerializeField]
    protected Canvas canvas;

    protected GameObject panel = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (ray) {
                Debug.Log(ray.collider.name);
                Raider raider = ray.collider.GetComponent<Raider>();
                if (raider) {
                    Debug.Log(raider.characterName);
                    if (panel != null) { Destroy(panel); }
                    panel = Instantiate(raiderInfoPanel, canvas.transform);
                    panel.GetComponent<RaiderInfoPanel>().raider = raider;
                }
            }
        }
    }
}
