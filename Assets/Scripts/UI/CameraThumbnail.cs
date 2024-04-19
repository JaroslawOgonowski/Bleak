using UnityEngine;
using UnityEngine.UI;

public class CameraThumbnail : MonoBehaviour
{
    public Camera secondCamera;
    public RawImage rawImage;

    public RenderTexture renderTexture;

    void Start()
    {
        // Utw�rz now� tekstur� docelow� o rozmiarze obrazu z kamery
        renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
        // Ustaw tekstur� docelow� dla drugiej kamery
        secondCamera.targetTexture = renderTexture;
    }
}
