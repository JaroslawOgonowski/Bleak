using UnityEngine;
using UnityEngine.UI;

public class CameraThumbnail : MonoBehaviour
{
    public Camera secondCamera;
    public RawImage rawImage;

    public RenderTexture renderTexture;

    void Start()
    {
        // Utwórz now¹ teksturê docelow¹ o rozmiarze obrazu z kamery
        renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
        // Ustaw teksturê docelow¹ dla drugiej kamery
        secondCamera.targetTexture = renderTexture;
    }
}
