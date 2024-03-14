using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Obiekt, za którym kamera ma pod¹¿aæ
    public float followSpeed = 5f; // Prêdkoœæ pod¹¿ania kamery za obiektem
    public float rotationSpeed = 5f; // Prêdkoœæ obrotu kamery

    private Vector3 offset; // Przechowuje pocz¹tkowy wektor odleg³oœci miêdzy kamer¹ a obiektem
    private float originalRotationX; // Pierwotna rotacja kamery w osi X

    void Start()
    {
        // Obliczanie pocz¹tkowego wektora odleg³oœci
        offset = transform.position - target.position;

        // Zachowanie pierwotnej rotacji kamery w osi X
        originalRotationX = transform.eulerAngles.x;
    }

    void LateUpdate()
    {
        // Obliczanie docelowej pozycji kamery
        Vector3 targetPosition = target.position + offset;

        // Interpolacja liniowa miêdzy aktualn¹ a docelow¹ pozycj¹ kamery
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Obliczanie wektora ruchu postaci
        Vector3 movementDirection = target.position - transform.position;

        // Obracanie kamery w kierunku wektora ruchu postaci
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Ustawienie pierwotnej rotacji kamery w osi X
            transform.rotation = Quaternion.Euler(originalRotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
    }
}
