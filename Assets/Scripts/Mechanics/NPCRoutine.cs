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
    public GameObject currentPOI;

    public float proximityThreshold = 2.0f; // Odleg³oœæ, przy której uznajemy punkt za osi¹gniêty

    void Start()
    {
        poiNPCs = NPCPOIManager.Instance.poiNPCs;
        nav = GetComponent<NavMeshAgent>();

        // Check if poiNPCs is not null and has elements
        if (poiNPCs != null && poiNPCs.Count > 0)
        {
            // Start from the closest POI
            destinationPOI = CalculateClosestPOI();
            if (destinationPOI != null)
            {
                nav.SetDestination(destinationPOI.transform.position);
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
            // Update current POI to the destination
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
        int randomIndex = Random.Range(0, closestPOIs.Count);

        return closestPOIs[randomIndex];
    }
}
