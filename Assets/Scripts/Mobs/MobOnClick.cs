using UnityEngine;
using UnityEngine.EventSystems;

public class MobOnClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject mobInfoPanelManager;
    private MobInfoPanelManager mobInfoPanelManagerScript;

    void Start()
    {
        mobInfoPanelManagerScript = mobInfoPanelManager.GetComponent<MobInfoPanelManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click detected!");
        mobInfoPanelManagerScript.OpenMobPanel();
    }
}
