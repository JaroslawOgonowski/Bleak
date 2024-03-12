using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Analytics;
public class version : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI versionText;
    // Start is called before the first frame update
    void Start()
    {
        versionText.text ="version: " + Application.version;
    }
}
