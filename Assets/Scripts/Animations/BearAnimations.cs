using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Collections;
public class BearAnimations : MonoBehaviour
{
    [SerializeField] private GameObject Bear;
    [SerializeField] private int walkPerSecond = 3;
    [SerializeField] private int runPerSecond = 8;
    [SerializeField] private string idleAnimation = "Idle";
    [SerializeField] private string walkingAnimation = "WalkForward";
    [SerializeField] private string runningAnimation = "Run Forward";
    private void Start()
    {
        Animator animator = Bear.GetComponent<Animator>();
        StartCoroutine(MobGoE(walkingAnimation, runningAnimation, idleAnimation, Bear, walkPerSecond, runPerSecond, true));



        //animator.SetBool("Attack1", true);
    }

    private IEnumerator AnimalRestState(string animation, GameObject gameObject)
    {
        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetBool(animation, true);

        return null;
    }

    private IEnumerator MobGoE(string walkingAnimation, string runningAnimation, string idleAnimation, GameObject gameObject, float walkPerSecond, float runPerSecond, bool run)
    {
        Animator animator = gameObject.GetComponent<Animator>();

        animator.SetBool(idleAnimation, false);

        if (run)
        {
            animator.SetBool(runningAnimation, true);   
        }
        else
        {
            animator.SetBool(walkingAnimation, true);
        }
           
        while (true)
        {
            if(run)
            {
                Vector3 movement = Vector3.forward * walkPerSecond * Time.deltaTime;
                gameObject.transform.Translate(movement);
            }
            else
            {
                Vector3 movement = Vector3.forward * runPerSecond * Time.deltaTime;
                gameObject.transform.Translate(movement);
            }

            yield return null;
        }
    }
}
