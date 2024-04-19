using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobOnClick : MonoBehaviour
{
    [SerializeField] private GameObject mobInfoPanelManager;
    [SerializeField] private MobInfoPanelManager mobInfoPanelManagerScript;
    
    void Start()
    {
        mobInfoPanelManagerScript = mobInfoPanelManager.GetComponent<MobInfoPanelManager>();
        GetComponent<Button>().onClick.AddListener(() => mobInfoPanelManagerScript.OpenMobPanel());
    }

}
