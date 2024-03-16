using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using static BLINK.AnimationDemo;

public class AnimationFunctions : MonoBehaviour
{
    public IEnumerator MobGoTo(string walkingAnimation, string idleAnimation, GameObject gameObject, float speed, float animationTime, string moveDirection)
    {
        // Pobranie komponentu Animator
        Animator animator = gameObject.GetComponent<Animator>();

        animator.SetBool(idleAnimation, false);
        animator.SetBool(walkingAnimation, true);

        float elapsedTime = 0f; // Zmienna do �ledzenia czasu

        // P�tla poruszania si� obiektu z okre�lon� pr�dko�ci�
        while (true)
        {
            Vector3 movement = Vector3.zero;
            Quaternion startRotation = gameObject.transform.rotation;
            Quaternion targetRotation = Quaternion.identity;

            // Ustawienie wektora przemieszczenia i rotacji na podstawie kierunku ruchu
            switch (moveDirection)
            {
                case "N":
                    targetRotation = Quaternion.Euler(0f, 0f, 0f); // Obr�t na p�noc (rotation Y = 180)
                    break;
                case "S":
                    targetRotation = Quaternion.Euler(0f, 180f, 0f); // Obr�t na po�udnie
                    break;
                case "W":
                    targetRotation = Quaternion.Euler(0f, 270f, 0f); // Obr�t na zach�d
                    break;
                case "E":
                    targetRotation = Quaternion.Euler(0f, 90f, 0f); // Obr�t na wsch�d
                    break;

                case "NE":
                    targetRotation = Quaternion.Euler(0f, 45f, 0f); // Obr�t na p�nocny wsch�d
                    break;
                case "NW":
                    targetRotation = Quaternion.Euler(0f, 305f, 0f); // Obr�t na p�nocny zach�d
                    break;
                case "SE":
                    targetRotation = Quaternion.Euler(0f, 135f, 0f); // Obr�t na po�udniowy wsch�d
                    break;
                case "SW":
                    targetRotation = Quaternion.Euler(0f, 225f, 0f); // Obr�t na po�udniowy zach�d
                    break;
                // Dodaj inne kierunki ruchu, je�li s� potrzebne
                default:
                    targetRotation = Quaternion.identity; // Brak obrotu
                    break;
            }
            movement = Vector3.forward * speed * Time.deltaTime;
            // Przesuni�cie obiektu
            gameObject.transform.Translate(movement);

            // P�ynne interpolowanie mi�dzy aktualn� a docelow� rotacj�
            float rotationSpeed = 5f; // Szybko�� obracania si� obiektu
            gameObject.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Zwi�kszenie czasu, kt�ry min�� od rozpocz�cia p�tli
            elapsedTime += Time.deltaTime;

            // Sprawdzenie warunku zatrzymania p�tli (np. po up�ywie okre�lonego czasu)
            if (elapsedTime >= animationTime) // Zatrzymaj po okre�lonym czasie
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

    public IEnumerator AnimalRestState(string restAnimation, string idleAnimation, GameObject gameObject, float time)
    {
        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetBool(idleAnimation, false);
        animator.SetBool(restAnimation, true);
        yield return new WaitForSeconds(time);
        animator.SetBool(restAnimation, false);
        animator.SetBool(idleAnimation, true);
    }

    public void AnimalEventAnimation(string eventAnimation, GameObject gameObject)
    {
        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetBool(eventAnimation, true);
    }

  
}
