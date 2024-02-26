using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemBuy : MonoBehaviour
{
    [SerializeField] public Gun weapon;
    [SerializeField] TextMeshProUGUI priceTx,DamageTx;
    [SerializeField] Image sprite;
    bool onBuy;
    private void Start()
    {
        sprite.sprite = weapon.weapon;
        priceTx.text = "$" + weapon.price;
        DamageTx.text = "" + weapon.damage;
        GetComponent<Button>().onClick.AddListener(Select);
        if (weapon.price == 0) { setDefault(); }
    }
    public void Sell()
    {
        onBuy = false;
        priceTx.text = "$" + weapon.price;
    }
    public void setDefault()
    {
        ShoopController.instance.SelectWeapon(weapon, this, true, weapon);
        priceTx.text = "Default";
        onBuy = true;
    }
    public void Select()
    {
        if (!onBuy && ShoopController.instance.CanPrice(weapon.price))
        {
            ShoopController.instance.SelectWeapon(weapon,this, true);
            priceTx.text = "Sold";
            onBuy = true;
        }
        else if(onBuy)
        {
            ShoopController.instance.SelectWeapon(weapon,this);
        }
     
    }
}
