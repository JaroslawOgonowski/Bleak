using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Obiekt, za kt�rym kamera ma pod��a�
    public float followSpeed = 5f; // Pr�dko�� pod��ania kamery za obiektem
    public float rotationSpeed = 5f; // Pr�dko�� obrotu kamery

    private Vector3 offset; // Przechowuje pocz�tkowy wektor odleg�o�ci mi�dzy kamer� a obiektem

    void Start()
    {
        // Obliczanie pocz�tkowego wektora odleg�o�ci
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        // Obliczanie docelowej pozycji kamery
        Vector3 targetPosition = target.position + offset;

        // Interpolacja liniowa mi�dzy aktualn� a docelow� pozycj� kamery
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Obliczanie kierunku, w kt�rym porusza si� posta�
        Vector3 movementDirection = target.position - transform.position;

        // Obracanie kamery w kierunku ruchu postaci
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
