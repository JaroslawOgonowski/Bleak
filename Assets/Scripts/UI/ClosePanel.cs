using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePanel : MonoBehaviour
{
[SerializeField] private GameObject panel;
[SerializeField] private Button closeButton;

    private void Start()
    {
        closeButton.onClick.AddListener(() => panel.SetActive(false));
    }
}
