using UnityEngine;

[CreateAssetMenu(fileName = "NewHelmet", menuName = "Shield", order = 2)]
public class Helmet : ScriptableObject
{
    public int shield;
    public int price;
    public Sprite helmet;
}
