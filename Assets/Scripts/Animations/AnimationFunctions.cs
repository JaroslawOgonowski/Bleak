using System.Collections;
using UnityEngine;

public class AnimationFunctions : MonoBehaviour
{
    private bool isMoving = false;

    public IEnumerator MobGoTo(string walkingAnimation, GameObject gameObject, float speed, float animationTime, string moveDirection)
    {
        // SprawdŸ, czy obiekt nie jest ju¿ w trakcie ruchu
        if (isMoving)
        {
            yield break; // Jeœli tak, zakoñcz funkcjê
        }

        // Ustaw flagê isMoving na true, aby oznaczyæ rozpoczêcie ruchu
        isMoving = true;

        // Pobranie komponentu Animator
        Animator animator = gameObject.GetComponent<Animator>();

        animator.SetBool(walkingAnimation, true);

        float elapsedTime = 0f; // Zmienna do œledzenia czasu

        // Pêtla poruszania siê obiektu z okreœlon¹ prêdkoœci¹
        while (true)
        {
            Vector3 movement = Vector3.zero;
            Quaternion startRotation = gameObject.transform.rotation;
            Quaternion targetRotation = Quaternion.identity;

            // Ustawienie wektora przemieszczenia i rotacji na podstawie kierunku ruchu
            switch (moveDirection)
            {
                case "N":
                    targetRotation = Quaternion.Euler(0f, 0f, 0f); // Obrót na pó³noc
                    break;
                case "S":
                    targetRotation = Quaternion.Euler(0f, 180f, 0f); // Obrót na po³udnie
                    break;
                case "W":
                    targetRotation = Quaternion.Euler(0f, 270f, 0f); // Obrót na zachód
                    break;
                case "E":
                    targetRotation = Quaternion.Euler(0f, 90f, 0f); // Obrót na wschód
                    break;
                case "NE":
                    targetRotation = Quaternion.Euler(0f, 45f, 0f); // Obrót na pó³nocny wschód
                    break;
                case "NW":
                    targetRotation = Quaternion.Euler(0f, 305f, 0f); // Obrót na pó³nocny zachód
                    break;
                case "SE":
                    targetRotation = Quaternion.Euler(0f, 135f, 0f); // Obrót na po³udniowy wschód
                    break;
                case "SW":
                    targetRotation = Quaternion.Euler(0f, 225f, 0f); // Obrót na po³udniowy zachód
                    break;
                // Dodaj inne kierunki ruchu, jeœli s¹ potrzebne
                default:
                    targetRotation = Quaternion.identity; // Brak obrotu
                    break;
            }

            movement = Vector3.forward * speed * Time.deltaTime;

            // Przesuniêcie obiektu
            gameObject.transform.Translate(movement);
            // P³ynne interpolowanie miêdzy aktualn¹ a docelow¹ rotacj¹
            float rotationSpeed = 5f; // Szybkoœæ obracania siê obiektu
            gameObject.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Zwiêkszenie czasu, który min¹³ od rozpoczêcia pêtli
            elapsedTime += Time.deltaTime;

            // Sprawdzenie warunku zatrzymania pêtli (np. po up³ywie okreœlonego czasu)
            if (elapsedTime >= animationTime) // Zatrzymaj po okreœlonym czasie
            {
                break; // Zakoñcz pêtlê
            }

            // Oczekiwanie na kolejn¹ klatkê
            yield return null;
        }

        // Po zakoñczeniu pêtli, przywróæ obiekt do stanu idle
        animator.SetBool(walkingAnimation, false);
        // Ustaw flagê isMoving na false, aby oznaczyæ zakoñczenie ruchu
        isMoving = false;
    }

    public IEnumerator AnimalRestState(string animation, string idleAnimation, GameObject gameObject)
    {
        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetBool(animation, true);
        yield return new WaitForSeconds(1); // Poczekaj przez 1 sekundê
        animator.SetBool(animation, false);
    }
}
