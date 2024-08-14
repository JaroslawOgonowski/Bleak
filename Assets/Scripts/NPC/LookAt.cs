using System.Collections;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] private GameObject aim;           // Obiekt, który patrzy   
    [SerializeField] private float maxDistance = 1000f;  // Maksymalny dystans, przy którym obiekt mo¿e patrzeæ na cel
    [SerializeField] private float maxAngle = 30f;     // Maksymalny k¹t, przy którym obiekt mo¿e patrzeæ na cel
    [SerializeField] private float transitionDuration = 4f; // Czas trwania animacji przeniesienia
    [SerializeField, Range(0, 100)] private float lookAtHeightPercentage = 50f; // Kontrola wysokoœci punktu patrzenia (0 - 100)

    private Vector3 startingAimPosition;
    private Quaternion startingAimRotation;
    private Coroutine moveCoroutine;
    public bool isAimoNstartingPos = true;
    public static LookAt instance;

    private void Awake()
    {
        instance = this;    
    }
    private void Start()
    {
        startingAimPosition = aim.transform.localPosition;
        startingAimRotation = aim.transform.localRotation;
        Debug.Log("Starting Aim Position: " + startingAimPosition);
        Debug.Log("Starting Aim Rotation: " + startingAimRotation);
    }

    public void StartMove(GameObject target)
    {
        Vector3 directionToTarget = target.transform.position - aim.transform.position;
        float distanceToTarget = directionToTarget.magnitude;
        float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

        if (distanceToTarget <= maxDistance && angleToTarget <= maxAngle)
        {
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine); // Przerwanie bie¿¹cej animacji
            }

            moveCoroutine = StartCoroutine(MoveAimToTarget(target.transform));
        }
        else
        {
            ReturnToStart();
        }
    }

    private IEnumerator MoveAimToTarget(Transform targetTransform)
    {
        if (targetTransform == null)
        {
            Debug.LogError("Target Transform is null!");
            yield break;
        }

        Vector3 startPosition = aim.transform.position;
        Quaternion startRotation = aim.transform.rotation;

        Bounds targetBounds = (Bounds)(targetTransform.GetComponent<Renderer>()?.bounds);
        if (targetBounds == null)
        {
            Debug.LogError("Target Bounds is null!");
            yield break;
        }

        Vector3 targetPosition = targetBounds.center;
        float heightOffset = Mathf.Lerp(targetBounds.min.y, targetBounds.max.y, lookAtHeightPercentage / 100f);
        targetPosition.y = heightOffset;

        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - aim.transform.position);

        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            t = Mathf.SmoothStep(0f, 1f, t);

            aim.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            aim.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        aim.transform.position = targetPosition;
        aim.transform.rotation = targetRotation;
        moveCoroutine = null;
    }


    bool rts = false;
    public void ReturnToStart()
    {
        if (!rts)
        {
            StartCoroutine(ReturnToStartingPosition());
        }       
    }

    private IEnumerator ReturnToStartingPosition()
    {
        rts = true;
        Vector3 startPosition = aim.transform.localPosition; // Upewnij siê, ¿e u¿ywasz `localPosition`
        Quaternion startRotation = aim.transform.localRotation;

        if (float.IsNaN(startPosition.x) || float.IsNaN(startPosition.y) || float.IsNaN(startPosition.z))
        {
            Debug.LogError("Start position contains NaN values!");
            yield break;
        }

        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            t = t * t * (3f - 2f * t); // U¿ycie funkcji "ease-in-out" dla bardziej p³ynnego efektu

            aim.transform.localPosition = Vector3.Lerp(startPosition, startingAimPosition, t);
            aim.transform.localRotation = Quaternion.Slerp(startRotation, startingAimRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        aim.transform.localPosition = startingAimPosition;
        aim.transform.localRotation = startingAimRotation;
        moveCoroutine = null;
        rts = false;
    }



}
