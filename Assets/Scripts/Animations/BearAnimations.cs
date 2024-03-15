using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Collections;
public class BearAnimations : MonoBehaviour
{
    [SerializeField] private GameObject Bear;
    [SerializeField] private int walkPerSecond = 20;
    private void Start()
    {
        Animator animator = Bear.GetComponent<Animator>();
        StartCoroutine(MobGoN("WalkForward", Bear, walkPerSecond));



        //animator.SetBool("Attack1", true);
    }

    private IEnumerator AnimalRestState(string animation, GameObject gameObject)
    {
        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetBool(animation, true);

        return null;
    }

    private IEnumerator MobGoN(string walkingAnimation, GameObject gameObject, float walkPerSecond)
    {
        // Pobranie komponentu Animator
        Animator animator = gameObject.GetComponent<Animator>();
        StopCurrentAnimation(animator);

        // Ustawienie parametru animatora na true, aby odtworzy� animacj� chodzenia
        animator.SetBool(walkingAnimation, true);

        // P�tla poruszania si� obiektu z okre�lon� pr�dko�ci�
        while (true)
        {
            // Obliczenie wektora przemieszczenia z uwzgl�dnieniem pr�dko�ci na sekund� i delta time
            Vector3 movement = Vector3.forward * walkPerSecond * Time.deltaTime;

            // Przesuni�cie obiektu
            gameObject.transform.Translate(movement);

            // Oczekiwanie na kolejn� klatk�
            yield return null;
        }
    }


    private string currentAnimationName;

    // Zapami�taj bie��c� animacj�
    private void RememberCurrentAnimationName(Animator animator)
    {
        foreach (var clipInfo in animator.GetCurrentAnimatorClipInfo(0))
        {
            currentAnimationName = clipInfo.clip.name;
        }
    }

    // Zatrzymaj bie��c� animacj�
    private void StopCurrentAnimation(Animator animator)
    {
        RememberCurrentAnimationName(animator);
        animator.SetBool(currentAnimationName, false);
    }

    // Wzn�w ostatni� zapami�tan� animacj�
    private void ResumeLastAnimation(Animator animator)
    {
        animator.Play(currentAnimationName, 0, 0);
    }
}
