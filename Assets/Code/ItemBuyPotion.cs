using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemBuyPotion : MonoBehaviour
{
    [SerializeField] public Potion potion;
    [SerializeField] TextMeshProUGUI priceTx,HealTx,MaxHealthTx;
    [SerializeField] Image sprite;
    bool onBuy;
    private void Start()
    {
        sprite.sprite = potion.potion;
        priceTx.text = "$" + potion.price;
        HealTx.text = "" + potion.heal;
        MaxHealthTx.text = "" + potion.maxHealth;
        GetComponent<Button>().onClick.AddListener(Select);
        if (potion.price == 0) { setDefault(); }
    }
    public void Sell()
    {
        onBuy = false;
        priceTx.text = "$" + potion.price;
    }
    public void setDefault()
    {
            ShoopController.instance.SelectPotion(potion, this, true,potion);
            priceTx.text = "Default";
            onBuy = true;
    }
    public void Select()
    {
        if (!onBuy && ShoopController.instance.CanPrice(potion.price))
        {
            ShoopController.instance.SelectPotion(potion,this,true);
            priceTx.text = "Sold";
            onBuy = true;
        }
        else if(onBuy)
        {
            ShoopController.instance.SelectPotion(potion,this);
        }
     
    }
}
