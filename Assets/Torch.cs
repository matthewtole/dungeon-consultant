using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Torch : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    private static readonly int Lit = Animator.StringToHash("Lit");

    void Start()
    {
        animator.SetBool(Lit, true);
    }

}
