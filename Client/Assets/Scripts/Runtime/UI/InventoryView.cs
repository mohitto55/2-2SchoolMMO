using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class InventoryView : UIView
{
    [SerializeField] GameObject slotGrid;
    private List<InventorySlot> slots = new List<InventorySlot>();
    protected override void Awake()
    {
        base.Awake();
        if (slotGrid != null)
        {
            slots = slotGrid.GetComponentsInChildren<InventorySlot>().ToList();
        }
    }
    protected override void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}
