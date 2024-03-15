using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFunctions : MonoBehaviour
{
    public IEnumerator MobGoE(string walkingAnimation, string idleAnimation, GameObject gameObject, float walkPerSecond, float animationTime)
    {
        // Pobranie komponentu Animator
        Animator animator = gameObject.GetComponent<Animator>();

        animator.SetBool(idleAnimation, false);
        animator.SetBool(walkingAnimation, true);

        float elapsedTime = 0f; // Zmienna do œledzenia czasu

        // Pêtla poruszania siê obiektu z okreœlon¹ prêdkoœci¹
        while (true)
        {
            // Obliczenie wektora przemieszczenia z uwzglêdnieniem prêdkoœci na sekundê i delta time
            Vector3 movement = Vector3.forward * walkPerSecond * Time.deltaTime;

            // Przesuniêcie obiektu
            gameObject.transform.Translate(movement);

            // Zwiêkszenie czasu, który min¹³ od rozpoczêcia pêtli
            elapsedTime += Time.deltaTime;

            // Sprawdzenie warunku zatrzymania pêtli (np. po up³ywie okreœlonego czasu)
            if (elapsedTime >= animationTime) // Zatrzymaj po 10 sekundach
            {
                break; // Zakoñcz pêtlê
            }

            // Oczekiwanie na kolejn¹ klatkê
            yield return null;
        }

        // Po zakoñczeniu pêtli, przywróæ obiekt do stanu idle
        animator.SetBool(walkingAnimation, false);
        animator.SetBool(idleAnimation, true);
    }

    public IEnumerator MobRunE(string runningAnimation, string idleAnimation, GameObject gameObject, float runPerSecond, float animationTime)
    {
        // Pobranie komponentu Animator
        Animator animator = gameObject.GetComponent<Animator>();

        animator.SetBool(idleAnimation, false);
        animator.SetBool(runningAnimation, true);

        float elapsedTime = 0f; // Zmienna do œledzenia czasu

        // Pêtla poruszania siê obiektu z okreœlon¹ prêdkoœci¹
        while (true)
        {
            // Obliczenie wektora przemieszczenia z uwzglêdnieniem prêdkoœci na sekundê i delta time
            Vector3 movement = Vector3.forward * runPerSecond * Time.deltaTime;

            // Przesuniêcie obiektu
            gameObject.transform.Translate(movement);

            // Zwiêkszenie czasu, który min¹³ od rozpoczêcia pêtli
            elapsedTime += Time.deltaTime;

            // Sprawdzenie warunku zatrzymania pêtli (np. po up³ywie okreœlonego czasu)
            if (elapsedTime >= animationTime) // Zatrzymaj po 10 sekundach
            {
                break; // Zakoñcz pêtlê
            }

            // Oczekiwanie na kolejn¹ klatkê
            yield return null;
        }

        // Po zakoñczeniu pêtli, przywróæ obiekt do stanu idle
        animator.SetBool(runningAnimation, false);
        animator.SetBool(idleAnimation, true);
    }
}
