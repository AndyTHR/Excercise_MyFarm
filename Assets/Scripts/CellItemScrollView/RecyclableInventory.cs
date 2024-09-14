using System.Collections;
using System.Collections.Generic;
using PolyAndCode.UI;
using UnityEngine;

public class RecyclableInventory : MonoBehaviour, IRecyclableScrollRectDataSource
{
    [SerializeField]
    RecyclableScrollRect _recyclableScrollRect;
    [SerializeField]
    private int _dataLenght;
    public bool hide = true;


    private List<InvenItems> _invenItems = new List<InvenItems>();

    public GameObject inventoyGameObject;

    private void Awake()
    {
        _recyclableScrollRect.DataSource = this;
    }

    public int GetItemCount()
    {
        {
            return _invenItems.Count;
        }
    }

    public void SetCell(ICell cell, int index)
    {
        var Item = cell as CellItemData;
        Item.ConfigureCell(_invenItems[index], index);
    }

    private void Start()
    {
        List<InvenItems> lstItem = new List<InvenItems>();
        for(int i = 0; i < 50; i++)
        {
            {
                InvenItems invenItem = new InvenItems();
                invenItem.name = "Name_" + i.ToString();
                invenItem.description = "Des_" + i.ToString();

                lstItem.Add(invenItem);
                SetLstItem(lstItem);
                _recyclableScrollRect.ReloadData();
            } 
        }
        
    }

    public void SetLstItem(List<InvenItems> lst)
    {
        _invenItems = lst;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            InvenItems invenItemDemo = new InvenItems("ca", description: "ca");
            _invenItems.Add(invenItemDemo);
            _recyclableScrollRect.ReloadData(); 
        }    

        if(Input.GetKeyDown(KeyCode.S))
        {
            //inventoyGameObject.SetActive(!inventoyGameObject.activeSelf);
            Vector3 crrPostInvent = inventoyGameObject.GetComponent<RectTransform>().anchoredPosition;
            inventoyGameObject.GetComponent<RectTransform>().anchoredPosition = crrPostInvent.y == 1000 ? Vector3.zero : new Vector3(0, 1000, 0);



        }
    }

    public void AddInventoryItem (InvenItems item)
    {
        _invenItems.Add (item);
        _recyclableScrollRect.ReloadData();
    }    
}
