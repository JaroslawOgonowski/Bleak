using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpening : MonoBehaviour
{
    private Transform chestCover;

    void Start()
    {
        // Znajduje pokrywê skrzyni jako dziecko tego obiektu
        chestCover = GetComponent<Transform>();
        StartCoroutine(ChestOpener());
    }

    public IEnumerator ChestOpener()
    {
        // Pocz¹tkowa rotacja
        Quaternion startRotation = chestCover.localRotation;
        // Koñcowa rotacja
        Quaternion endRotation = Quaternion.Euler(-100, 0, 0);

        float duration = 2.0f; // Czas trwania animacji w sekundach
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            // Oblicza procent czasu, który min¹³
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            // Interpoluje rotacjê
            chestCover.localRotation = Quaternion.Lerp(startRotation, endRotation, t);

            // Czeka do nastêpnej klatki
            yield return null;
        }

        // Upewnia siê, ¿e koñcowa rotacja jest dok³adnie ustawiona
        chestCover.localRotation = endRotation;
    }

    public IEnumerator ChestCloser()
    {
        // Pocz¹tkowa rotacja
        Quaternion startRotation = chestCover.localRotation;
        // Koñcowa rotacja
        Quaternion endRotation = Quaternion.Euler(0, 0, 0);

        float duration = 2.0f; // Czas trwania animacji w sekundach
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            // Oblicza procent czasu, który min¹³
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            // Interpoluje rotacjê
            chestCover.localRotation = Quaternion.Lerp(startRotation, endRotation, t);

            // Czeka do nastêpnej klatki
            yield return null;
        }

        // Upewnia siê, ¿e koñcowa rotacja jest dok³adnie ustawiona
        chestCover.localRotation = endRotation;
    }
}

