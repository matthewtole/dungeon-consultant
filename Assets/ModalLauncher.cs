using System.Collections;
using System.Collections.Generic;
using Code.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

public class ModalLauncher : MonoBehaviour
{
    [SerializeField] protected GameObject modalPrefab;
    [SerializeField] protected Canvas canvas;

    public void Launch()
    {
        Instantiate(modalPrefab, canvas.transform);
    }
}
