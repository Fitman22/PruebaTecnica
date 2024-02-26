using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemBuyHelmet : MonoBehaviour
{
    [SerializeField] public Helmet helmet;
    [SerializeField] TextMeshProUGUI priceTx,ShieldTx;
    [SerializeField] Image sprite;
    bool onBuy;
    private void Start()
    {
        sprite.sprite = helmet.helmet;
        priceTx.text = "$" + helmet.price;
        ShieldTx.text = "" + helmet.shield;
        GetComponent<Button>().onClick.AddListener(Select);  
    }
    public void Sell()
    {
        onBuy = false;
        priceTx.text = "$" + helmet.price;
    }
    public void Select()
    {
        if (!onBuy && ShoopController.instance.CanPrice(helmet.price))
        {
            ShoopController.instance.SelectHelmet(helmet,this, true);
            priceTx.text = "Sold";
            onBuy = true;
        }
        else if(onBuy)
        {
            ShoopController.instance.SelectHelmet(helmet,this);
        }
     
    }
}
