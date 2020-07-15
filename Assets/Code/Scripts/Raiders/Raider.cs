using DungeonManager;
using UnityEngine;

public class Raider : MonoBehaviour {
    [SerializeField]
    protected ObjectList raiderList;

    public string characterName;
    
    private void Awake() {
        raiderList.Add(gameObject);
    }

    private void OnDestroy() {
        raiderList.Remove(gameObject);
    }
}
