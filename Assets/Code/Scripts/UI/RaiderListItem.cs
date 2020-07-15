using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaiderListItem : MonoBehaviour
{
    [SerializeField]
    protected TextMeshProUGUI textName;

    public RaiderListModal modal;

    protected Raider raider;

    public void SetRaider(Raider raider) {
        this.raider = raider;
        textName.SetText(raider.characterName);
    }

    public void OnView() {
        modal.OnView(raider);
    }
}
