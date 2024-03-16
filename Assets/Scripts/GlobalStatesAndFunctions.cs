using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStatesAndFunctions : MonoBehaviour
{
    public float destroyYPosition = -100f;
    public void CheckPosition(GameObject gameObject)
    {
        // SprawdŸ pozycjê Y obiektu
        if (gameObject.GetComponent<Transform>().position.y < destroyYPosition)
        {
            // Zniszcz obiekt
            Destroy(gameObject);
        }
    }
}
