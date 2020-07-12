using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaiderInfoPanel : MonoBehaviour
{
    [SerializeField]
    public Raider raider;
    
    [SerializeField]
    protected TextMeshProUGUI textName;
    [SerializeField]
    protected Camera followCameraPrefab;

    protected Camera followCamera;

    void Start() {
        textName.text = raider.characterName;
        followCamera = Instantiate(followCameraPrefab, raider.gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClose() {
        Destroy(followCamera);
    }
}
