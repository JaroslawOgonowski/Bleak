using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPOIManager : MonoBehaviour
{
    public List<GameObject> poiNPCs;
    private Dictionary<GameObject, Vector3> poiNPCLocations;
    public static NPCPOIManager Instance;

    private void Awake()
    {
        Instance = this;

        // Inicjalizuj list� i s�ownik
        poiNPCs = new List<GameObject>();
        poiNPCLocations = new Dictionary<GameObject, Vector3>();

        // Znajd� wszystkie obiekty z tagiem "POINPC" i dodaj je do listy
        GameObject[] foundObjects = GameObject.FindGameObjectsWithTag("POINPC");
        foreach (GameObject obj in foundObjects)
        {
            poiNPCs.Add(obj);
            poiNPCLocations[obj] = obj.transform.position;
        }

        // Wydrukuj list� obiekt�w i ich pozycje w konsoli
        foreach (GameObject obj in poiNPCs)
        {
            Debug.Log("Obiekt: " + obj.name + " Pozycja: " + poiNPCLocations[obj]);
        }
    }
}
