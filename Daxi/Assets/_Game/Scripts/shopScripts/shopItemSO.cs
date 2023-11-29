using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "shopMenu", menuName = "scriptable object/ new shop item", order = 1)]
public class shopItemSO : ScriptableObject
{
    public string title;
    public int basecost;
    public string selectOrBuy;
}
