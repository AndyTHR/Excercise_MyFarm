using System.Collections;
using System.Collections.Generic;
using PolyAndCode.UI;
using UnityEngine;
using UnityEngine.UI;

public class CellItemData : MonoBehaviour, ICell
{
    [Header("UI")]
    public Text namelabel;
    public Text desLabel;

    private InvenItems _contactInfo;
    private int _cellIndex;

    public void ConfigureCell (InvenItems invenItems, int cellIndex)
    {
        _cellIndex = cellIndex;
        _contactInfo = invenItems;
        namelabel.text= invenItems.name;
        desLabel.text = invenItems.description;
    }    


}
