using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpening : MonoBehaviour
{
    private Transform chestCover;

    void Start()
    {
        // Znajduje pokryw� skrzyni jako dziecko tego obiektu
        chestCover = GetComponent<Transform>();
        StartCoroutine(ChestOpener());
    }

    public IEnumerator ChestOpener()
    {
        // Pocz�tkowa rotacja
        Quaternion startRotation = chestCover.localRotation;
        // Ko�cowa rotacja
        Quaternion endRotation = Quaternion.Euler(-100, 0, 0);

        float duration = 2.0f; // Czas trwania animacji w sekundach
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            // Oblicza procent czasu, kt�ry min��
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            // Interpoluje rotacj�
            chestCover.localRotation = Quaternion.Lerp(startRotation, endRotation, t);

            // Czeka do nast�pnej klatki
            yield return null;
        }

        // Upewnia si�, �e ko�cowa rotacja jest dok�adnie ustawiona
        chestCover.localRotation = endRotation;
    }

    public IEnumerator ChestCloser()
    {
        // Pocz�tkowa rotacja
        Quaternion startRotation = chestCover.localRotation;
        // Ko�cowa rotacja
        Quaternion endRotation = Quaternion.Euler(0, 0, 0);

        float duration = 2.0f; // Czas trwania animacji w sekundach
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            // Oblicza procent czasu, kt�ry min��
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            // Interpoluje rotacj�
            chestCover.localRotation = Quaternion.Lerp(startRotation, endRotation, t);

            // Czeka do nast�pnej klatki
            yield return null;
        }

        // Upewnia si�, �e ko�cowa rotacja jest dok�adnie ustawiona
        chestCover.localRotation = endRotation;
    }
}

