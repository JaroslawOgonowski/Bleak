using System.Collections;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] private GameObject aim;
    [SerializeField] private GameObject secondaryAim;  // Secondary aim for smoothing
    [SerializeField] private float maxDistance = 1000f;
    [SerializeField] private float minDistance = 2f;   // Minimum distance from player on the z-axis
    [SerializeField] private float maxAngle = 30f;
    [SerializeField] private float transitionDuration = 4f;
    [SerializeField, Range(0, 100)] private float lookAtHeightPercentage = 50f;

    private Vector3 startingAimPosition;
    private Coroutine moveCoroutine;
    public static LookAt instance;
    private bool isReturningToStart = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        startingAimPosition = aim.transform.localPosition;
        secondaryAim.transform.position = startingAimPosition;
    }

    public void StartMove(GameObject target)
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(SmoothFollowTarget(target));
    }

    private IEnumerator SmoothFollowTarget(GameObject target)
    {
        Vector3 directionToTarget = target.transform.position - transform.position;
        float distanceToTarget = directionToTarget.magnitude;
        float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

        // Check if the target is within the allowed distance and angle range
        if (distanceToTarget <= maxDistance && angleToTarget <= maxAngle)
        {
            Bounds targetBounds = target.GetComponent<Renderer>()?.bounds ?? default;
            Vector3 targetPosition = targetBounds.center;
            float heightOffset = Mathf.Lerp(targetBounds.min.y, targetBounds.max.y, lookAtHeightPercentage / 100f);
            targetPosition.y = heightOffset;

            // Calculate the position behind the target based on minDistance
            Vector3 directionFromPlayerToTarget = (targetPosition - transform.position).normalized;
            Vector3 minDistancePosition = targetPosition - directionFromPlayerToTarget * minDistance; 

            while (true)
            {
                // Smoothly move the secondary aim towards the minDistancePosition
                secondaryAim.transform.position = Vector3.Lerp(secondaryAim.transform.position, minDistancePosition, Time.deltaTime * transitionDuration);             

                // Main aim follows the secondary aim
                aim.transform.position = Vector3.Lerp(aim.transform.position, secondaryAim.transform.position, Time.deltaTime * transitionDuration);

                yield return null;
            }
        }
        else
        {
            ReturnToStart();
        }
    }




    public void ReturnToStart()
    {
        if (!isReturningToStart)
        {
            if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);            
            isReturningToStart = true;
            StartCoroutine(ReturnToStartingPosition());
        }
    }

    private IEnumerator ReturnToStartingPosition()
    {
        Vector3 startPosition = aim.transform.localPosition;

        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / transitionDuration);

            secondaryAim.transform.localPosition = Vector3.Lerp(secondaryAim.transform.localPosition, startingAimPosition, t);

            aim.transform.localPosition = Vector3.Lerp(startPosition, startingAimPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        aim.transform.localPosition = startingAimPosition;
        isReturningToStart = false;
    }
}
