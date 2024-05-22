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
        Vector3 endPos = startPos + new Vector3(0, 100, 0);

        // Animacja powiêkszania czcionki
        float duration = 1f;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            sourceText.fontSize = Mathf.Lerp(1, 30, t);
            transform.localPosition = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
        sourceText.fontSize = 30;
        transform.localPosition = endPos;

        // Czekaj 1 sekundê
        yield return new WaitForSeconds(1f);

        // Animacja zmniejszania czcionki
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            sourceText.fontSize = Mathf.Lerp(30, 1, t);
            yield return null;
        }
        sourceText.fontSize = 1;

        // Dezaktywuj obiekt
        gameObject.SetActive(false);

        // Opcjonalnie: Zniszcz obiekt, jeœli nie bêdzie ponownie u¿ywany
        Destroy(gameObject);
    }
}
