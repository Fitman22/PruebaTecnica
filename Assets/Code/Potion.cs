using UnityEngine;

[CreateAssetMenu(fileName = "NewPotion", menuName = "Heal", order = 3)]
public class Potion : ScriptableObject
{
    public int heal;
    public int maxHealth;
    public int price;
    public Sprite potion;
}
