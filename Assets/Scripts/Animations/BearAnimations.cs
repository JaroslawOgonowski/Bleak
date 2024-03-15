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

        // Ustawienie parametru animatora na true, aby odtworzyæ animacjê chodzenia
        animator.SetBool(walkingAnimation, true);

        // Pêtla poruszania siê obiektu z okreœlon¹ prêdkoœci¹
        while (true)
        {
            // Obliczenie wektora przemieszczenia z uwzglêdnieniem prêdkoœci na sekundê i delta time
            Vector3 movement = Vector3.forward * walkPerSecond * Time.deltaTime;

            // Przesuniêcie obiektu
            gameObject.transform.Translate(movement);

            // Oczekiwanie na kolejn¹ klatkê
            yield return null;
        }
    }


    private string currentAnimationName;

    // Zapamiêtaj bie¿¹c¹ animacjê
    private void RememberCurrentAnimationName(Animator animator)
    {
        foreach (var clipInfo in animator.GetCurrentAnimatorClipInfo(0))
        {
            currentAnimationName = clipInfo.clip.name;
        }
    }

    // Zatrzymaj bie¿¹c¹ animacjê
    private void StopCurrentAnimation(Animator animator)
    {
        RememberCurrentAnimationName(animator);
        animator.SetBool(currentAnimationName, false);
    }

    // Wznów ostatni¹ zapamiêtan¹ animacjê
    private void ResumeLastAnimation(Animator animator)
    {
        animator.Play(currentAnimationName, 0, 0);
    }
}
