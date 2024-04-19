using UnityEngine;
using UnityEngine.EventSystems;

public class MobOnClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject mobInfoPanelManager;
    private MobInfoPanelManager mobInfoPanelManagerScript;
    void Start()
    {
        mobInfoPanelManager = GameObject.Find("MobInfoPanelManager");
        mobInfoPanelManagerScript = mobInfoPanelManager.GetComponent<MobInfoPanelManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject myGameObject = this.gameObject;
        Debug.Log("Click detected!");
        mobInfoPanelManagerScript.OpenMobPanel(myGameObject);
    }
}
