using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShoopController : MonoBehaviour
{
    [SerializeField] Image weaponI,helmetI,potionI;
    [SerializeField] TextMeshProUGUI moneyTx,DamageTx,ShieldTx,HealTx,
        sellWeapon,sellHelmet,sellPotion;
    public static ShoopController instance;
    [SerializeField] Sprite defaultSprite;
    ItemBuyPotion itemP;
    ItemBuyHelmet itemH;
    ItemBuy itemW;
    Gun defaultW;
    Potion defaultP;
    int money;
    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        instance = this;
    }
    public void UpdateHealthTx(int health,int MaxHealth)
    {
        HealTx.text = health + "/" + MaxHealth;
    }
    public void addMoney(int add)
    {
        money += add;
        moneyTx.text = ""+money;
    }
    public bool CanPrice(int cost)
    {
        if (cost <= money) return true;
        else return false;
    }
    public void SelectWeapon(Gun weapon, ItemBuy item, bool buy = false,Gun defaultW=null)
    {
        if (buy) addMoney(-weapon.price);
        if (defaultW) this.defaultW = defaultW;
        weaponI.sprite = weapon.weapon;
        player.setEquipment(weapon);
        DamageTx.text = "" + weapon.damage;
        if (weapon.price > 0) {sellWeapon.text = "$" + (weapon.price - 2); itemW = item; }
           else { sellWeapon.text = ""; }
    }
    public void SelectHelmet(Helmet helmet,ItemBuyHelmet item, bool buy = false)
    {
        if (buy) addMoney(-helmet.price);
        helmetI.sprite = helmet.helmet;
        player.setEquipment(null,helmet);
        ShieldTx.text = "" + helmet.shield;
        sellHelmet.text = "$" + (helmet.price - 2);
        itemH = item;
    }
    public void SelectPotion(Potion potion,ItemBuyPotion item, bool buy = false,Potion defaultP=null)
    {
        if (buy) addMoney(-potion.price);
        if (defaultP) this.defaultP = defaultP;
        potionI.sprite = potion.potion;
        player.setEquipment(null, null,potion);
        if (potion.price > 0) { sellPotion.text = "$" + (potion.price - 2); itemP = item; }
        else { sellPotion.text = ""; }
    }
    public void sell(int sell)
    {
        if (sell == 1&&itemW) { itemW.Sell(); addMoney(itemW.weapon.price - 2);
            weaponI.sprite = defaultW.weapon;
            player.setEquipment(defaultW);
            sellWeapon.text = "";
            itemW = null;
        }
        if (sell == 2&&itemH)
        {
            itemH.Sell(); addMoney(itemH.helmet.price - 2);
            helmetI.sprite = defaultSprite;
            player.setEquipment(null,null,null,true);
            sellHelmet.text = "";
            ShieldTx.text = "0";
            itemH = null;
        }
        if (sell == 3&&itemP)
        {
            itemP.Sell(); addMoney(itemP.potion.price - 2);
            potionI.sprite = defaultP.potion;
            player.setEquipment(null, null, defaultP);
            sellPotion.text = "";
            itemP = null;
        }
    }
}
