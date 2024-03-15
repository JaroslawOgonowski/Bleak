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

        float elapsedTime = 0f; // Zmienna do �ledzenia czasu

        // P�tla poruszania si� obiektu z okre�lon� pr�dko�ci�
        while (true)
        {
            // Obliczenie wektora przemieszczenia z uwzgl�dnieniem pr�dko�ci na sekund� i delta time
            Vector3 movement = Vector3.forward * walkPerSecond * Time.deltaTime;

            // Przesuni�cie obiektu
            gameObject.transform.Translate(movement);

            // Zwi�kszenie czasu, kt�ry min�� od rozpocz�cia p�tli
            elapsedTime += Time.deltaTime;

            // Sprawdzenie warunku zatrzymania p�tli (np. po up�ywie okre�lonego czasu)
            if (elapsedTime >= animationTime) // Zatrzymaj po 10 sekundach
            {
                break; // Zako�cz p�tl�
            }

            // Oczekiwanie na kolejn� klatk�
            yield return null;
        }

        // Po zako�czeniu p�tli, przywr�� obiekt do stanu idle
        animator.SetBool(walkingAnimation, false);
        animator.SetBool(idleAnimation, true);
    }

    public IEnumerator MobRunE(string runningAnimation, string idleAnimation, GameObject gameObject, float runPerSecond, float animationTime)
    {
        // Pobranie komponentu Animator
        Animator animator = gameObject.GetComponent<Animator>();

        animator.SetBool(idleAnimation, false);
        animator.SetBool(runningAnimation, true);

        float elapsedTime = 0f; // Zmienna do �ledzenia czasu

        // P�tla poruszania si� obiektu z okre�lon� pr�dko�ci�
        while (true)
        {
            // Obliczenie wektora przemieszczenia z uwzgl�dnieniem pr�dko�ci na sekund� i delta time
            Vector3 movement = Vector3.forward * runPerSecond * Time.deltaTime;

            // Przesuni�cie obiektu
            gameObject.transform.Translate(movement);

            // Zwi�kszenie czasu, kt�ry min�� od rozpocz�cia p�tli
            elapsedTime += Time.deltaTime;

            // Sprawdzenie warunku zatrzymania p�tli (np. po up�ywie okre�lonego czasu)
            if (elapsedTime >= animationTime) // Zatrzymaj po 10 sekundach
            {
                break; // Zako�cz p�tl�
            }

            // Oczekiwanie na kolejn� klatk�
            yield return null;
        }

        // Po zako�czeniu p�tli, przywr�� obiekt do stanu idle
        animator.SetBool(runningAnimation, false);
        animator.SetBool(idleAnimation, true);
    }
}
