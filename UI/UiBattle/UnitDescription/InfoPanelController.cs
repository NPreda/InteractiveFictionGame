using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoPanelController : MonoBehaviour
{
    [SerializeField]private TMP_Text unitName;
    [SerializeField]private TMP_Text unitDescription;

    [SerializeField]private PlanPanel planPanel;
    [SerializeField]private StatusPanel statusPanel;

    public void Load(IUiUnit unit)
    {
        this.unitName.text = unit.unitName;
        this.unitDescription.text = unit.unitDescription;

        planPanel.Load(unit.combatActions);
        statusPanel.Load(unit.statusEffects);
    }

    public void ShowPanel() => this.gameObject.SetActive(true);
    public void HidePanel() => this.gameObject.SetActive(false);

}
