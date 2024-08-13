using System.Collections;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] private GameObject aim;          // Obiekt, który patrzy   
    [SerializeField] private float maxDistance = 10f; // Maksymalny dystans, przy którym obiekt mo¿e patrzeæ na cel
    [SerializeField] private float maxAngle = 30f;    // Maksymalny k¹t, przy którym obiekt mo¿e patrzeæ na cel
    [SerializeField] private float transitionDuration = 1f; // Czas trwania animacji przeniesienia
    private Vector3 startingAimPosition;
    private Coroutine moveCoroutine;

    private void Start()
    {
        startingAimPosition = aim.transform.localPosition;
    }

    // Metoda uruchamiana z zewn¹trz, która inicjuje ruch
    public void StartMove(GameObject target)
    {
        Vector3 directionToTarget = target.transform.position - aim.transform.position;
        float distanceToTarget = directionToTarget.magnitude;
        float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

        if (distanceToTarget <= maxDistance && angleToTarget <= maxAngle)
        {
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(MoveAimToTarget(target.transform));
        }
        else
        {
            // Resetuje pozycjê do pocz¹tkowej, jeœli target jest poza zasiêgiem
            aim.transform.localPosition = startingAimPosition;
        }
    }

    private IEnumerator MoveAimToTarget(Transform t)
    {
        Vector3 startPosition = aim.transform.position;
        Vector3 targetPosition = t.position;

        // P³ynne przejœcie pozycji przez transitionDuration sekund
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            aim.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Upewnij siê, ¿e pozycja koñcowa jest dok³adnie na target
        aim.transform.position = targetPosition;
    }
}
