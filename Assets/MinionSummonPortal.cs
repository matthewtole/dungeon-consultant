using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Characters;
using Code.Scripts.Minions;
using UnityEngine;

public class MinionSummonPortal : MonoBehaviour
{
    [SerializeField] protected GameObject minionPrefab;
    private NameGenerator _nameGenerator;

    private void Start()
    {
        _nameGenerator = new NameGenerator();
    }

    public void OnClick()
    {
        GameObject minion = Instantiate(minionPrefab, transform.position + Vector3.left, Quaternion.identity);
        minion.GetComponent<Minion>().Name = _nameGenerator.GenerateName(); 
        //GetComponentInChildren<ParticleSystem>().Play();
    }
}
