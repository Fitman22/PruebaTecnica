using UnityEngine;

[CreateAssetMenu(fileName = "NewGun", menuName = "Stats", order = 1)]
public class Gun : ScriptableObject
{
    public int damage;
    public int price;
    public Sprite weapon;
     public string animation;
}
