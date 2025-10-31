using UnityEngine;
using TMPro;

public class SpriteInfo : MonoBehaviour
{
    [SerializeField] GameObject infoPanel;
    [SerializeField] TMP_Text textInfo;
    [SerializeField] bool isPlayer;

    private void OnMouseEnter()
    {
        if (!transform.parent.GetComponent<Fight>().fightGo) return;
        infoPanel.SetActive(true);
    }

    private void OnDisable()
    {
        infoPanel.SetActive(false);
    }

    private void Update()
    {
        textInfo.text = isPlayer ? transform.parent.GetComponent<App>().player.getTextInfo() :
            transform.parent.GetComponent<App>().enemy.getTextInfo();
        if (!transform.parent.GetComponent<Fight>().fightGo) infoPanel.SetActive(false);
    }

    private void OnMouseExit()
    {
        infoPanel.SetActive(false);
    }
}
