using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class exitGame : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    void Start()
    {
        exitButton.onClick.AddListener(() => { Application.Quit();});
    }

}
