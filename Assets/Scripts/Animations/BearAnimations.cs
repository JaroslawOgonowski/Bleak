using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BearAnimations : MonoBehaviour
{
    [SerializeField] private GameObject Bear;

    private void Start()
    {
        Animator animator = Bear.GetComponent<Animator>();
        animator.SetBool("Attack1", true);
    }
}
