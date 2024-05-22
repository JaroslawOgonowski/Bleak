using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResTextManager : MonoBehaviour
{
    public static ResTextManager instance;
    [SerializeField] private GameObject textPrefab;
    [SerializeField] private Transform canvasTransform;

    public void Awake()
    {
        instance = this;
    }
    public void ShowText(string text)
    {
        Vector3 startPosition = GetBottomScreenPosition();
        GameObject textInstance = Instantiate(textPrefab, canvasTransform);
        textInstance.GetComponent<ResTextAnimation>().Initialize(text, startPosition);
    }

    private Vector3 GetBottomScreenPosition()
    {
        // Pobierz rozmiar canvasu
        RectTransform canvasRectTransform = canvasTransform.GetComponent<RectTransform>();

        // Oblicz pozycjê startow¹ na dole ekranu
        float yPos = -canvasRectTransform.rect.height / 2 + 20; // 20 jednostek nad doln¹ krawêdzi¹, mo¿na dostosowaæ
        return new Vector3(0, yPos, 0); // x = 0, y = yPos, z = 0
    }
}
