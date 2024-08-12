using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq; // Import for LINQ

public class NPCRoutine : MonoBehaviour
{
    private NavMeshAgent nav;
    private List<GameObject> poiNPCs;
    private GameObject destinationPOI;
    private GameObject currentPOI;
    private Animator animator;

    public float proximityThreshold = 2.0f; // Odleg�o��, przy kt�rej uznajemy punkt za osi�gni�ty
    private GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        poiNPCs = NPCPOIManager.Instance.poiNPCs;
        nav = GetComponent<NavMeshAgent>();
        nav.avoidancePriority = Random.Range(0, 100);
        nav.speed = Random.Range(2, 5);
        // Check if poiNPCs is not null and has elements
        if (poiNPCs != null && poiNPCs.Count > 0)
        {
            // Start from the closest POI
            destinationPOI = CalculateClosestPOI();
            if (destinationPOI != null)
            {
                nav.SetDestination(destinationPOI.transform.position);
                animator.SetBool("move", true);
            }
            else
            {
                Debug.LogError("CalculateClosestPOI() returned null.");
            }
        }
        else
        {
            Debug.LogError("poiNPCs is null or empty. Ensure NPCPOIManager is initialized correctly.");
        }
    }

    void Update()
    {
        // Ensure destinationPOI is not null before using it
        if (destinationPOI == null)
        {
            Debug.LogWarning("destinationPOI is null in Update(). Skipping update logic.");
            return;
        }

        // Check if the NPC is within proximity of the destination
        float distanceToDestination = Vector3.Distance(transform.position, destinationPOI.transform.position);
        if (distanceToDestination <= proximityThreshold)
        {


            //Debug.Log(destinationPOI.GetComponent<POIusage>().usage);
            currentPOI = destinationPOI;

            // Calculate new destination
            destinationPOI = CalculateNextPOI();
            if (destinationPOI != null)
            {
                nav.SetDestination(destinationPOI.transform.position);
            }
            else
            {
                Debug.LogWarning("CalculateNextPOI() returned null.");
            }

        }
    }

    private GameObject CalculateClosestPOI()
    {
        // Find the closest POI to the NPC's current position
        return poiNPCs.OrderBy(poi => Vector3.Distance(transform.position, poi.transform.position)).FirstOrDefault();
    }

    private GameObject CalculateNextPOI()
    {
        // Ensure there are at least two POIs available excluding the current one
        if (poiNPCs.Count <= 1)
        {
            Debug.LogWarning("Not enough POIs available to calculate the next destination.");
            return currentPOI; // Return current if no other POIs are available
        }

        // Find the closest POIs, excluding the current one
        var closestPOIs = poiNPCs
            .Where(poi => poi != currentPOI)
            .OrderBy(poi => Vector3.Distance(transform.position, poi.transform.position))
            .ToList();

        // If less than three POIs are available, adjust the random range accordingly
        if (closestPOIs.Count == 0)
        {
            Debug.LogWarning("No available POIs after filtering current. Returning current POI.");
            return currentPOI;
        }

        // If we have less than three, just return a random one from available
        int randomIndex = Random.Range(1, 3);

        return closestPOIs[randomIndex];
    }
    
    bool courtineInProgress = false;

    public void StopAndTalk()
    {
        if (!courtineInProgress)
        {
            StartCoroutine(StopAndTalkProcess());
        }
    }

    private IEnumerator StopAndTalkProcess()
    {
        courtineInProgress= true;
        // Zatrzymanie poruszania si�
        nav.Stop();
        animator.SetBool("move", false);

        // Obliczanie kierunku do gracza
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0; // Opcjonalnie zablokuj obr�t w osi Y, je�li to potrzebne

        // Docelowa rotacja w stron� gracza
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Dop�ki obr�t nie jest kompletny, wykonuj obr�t
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            // Obr�t w stron� gracza
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * nav.speed);
        }

        // Rozpocz�cie animacji rozmowy po zako�czeniu obrotu
        animator.SetTrigger("talk");
        courtineInProgress = false;

        return null;
    }
}
