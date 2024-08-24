using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    public string ItemName { get; }
    public int ItemID { get; }
    public Sprite Icon { get; }
}
