using UnityEngine;

public class ObstacleAvoidance : MonoBehaviour
{
    public float speed = 5.0f;
    public float detectionDistance = 2.0f;
    public float avoidanceStrength = 1.0f;
    public LayerMask obstacleMask;

    private Vector3 direction;

    void Start()
    {
        // Inicjalny kierunek ruchu
        direction = transform.forward;
    }

    void Update()
    {
        // Wykonywanie raycasta przed postaci¹
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, detectionDistance, obstacleMask))
        {
            // Obliczanie wektora unikania
            Vector3 avoidance = Vector3.Cross(hit.normal, Vector3.up) * avoidanceStrength;

            // Zmiana kierunku
            direction += avoidance;
            direction.Normalize();
        }

        // Aktualizacja pozycji
        transform.position += direction * speed * Time.deltaTime;

        // Aktualizacja rotacji w kierunku ruchu
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
