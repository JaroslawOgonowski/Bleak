using System.Collections;
using UnityEngine;
using TMPro;

public class ResTextAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sourceText;

    public void Initialize(string text, Vector3 startPosition)
    {
        sourceText = GetComponent<TextMeshProUGUI>();
        sourceText.text = text;
        transform.localPosition = startPosition;
        gameObject.SetActive(true);
        StartCoroutine(ResTextAnim());
    }

    private IEnumerator ResTextAnim()
    {
        // Ustawienia pocz¹tkowe
        sourceText.fontSize = 1;
        Vector3 startPos = transform.localPosition;
        Vector3 endPos = startPos + new Vector3(0, 70, 0);

        // Animacja powiêkszania czcionki
        float duration = 0.5f;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            sourceText.fontSize = Mathf.Lerp(1, 20, t);
            transform.localPosition = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
        sourceText.fontSize = 20;
        transform.localPosition = endPos;

        // Czekaj 1 sekundê
        yield return new WaitForSeconds(1.5f);

        // Animacja zmniejszania czcionki
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            sourceText.fontSize = Mathf.Lerp(20, 1, t);
            yield return null;
        }
        sourceText.fontSize = 1;

        // Dezaktywuj obiekt
        gameObject.SetActive(false);

        // Opcjonalnie: Zniszcz obiekt, jeœli nie bêdzie ponownie u¿ywany
        Destroy(gameObject);
    }
}
