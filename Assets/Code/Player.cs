using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] int MaxHealth,DistanceFight;
    [SerializeField] float speed;
    [SerializeField] public Gun gun;
    [SerializeField] public Helmet helmet;
    [SerializeField] public Potion potion;
    [SerializeField] GameObject points;
    [SerializeField] SpriteRenderer weaponSprite,helmetSprite,potionSprite;
    
    int Health;
    bool onFight;
    public bool onAttack;
    Transform target;
    public UnityEvent turn,Getdamage,Died = new UnityEvent();
    private void Start()
    {
        Health = MaxHealth;
        ShoopController.instance.UpdateHealthTx(Health, MaxHealth);
    }
    public void setEquipment(Gun weapon = null, Helmet helmet = null, Potion potion = null,bool remove=false)
    {
        if (weapon)
        { gun = weapon;
            weaponSprite.sprite = gun.weapon;}
        if (helmet)
        {
            this.helmet = helmet;
            helmetSprite.sprite = helmet.helmet;
        }
        if (remove) { this.helmet = null; helmetSprite.sprite = null; }
        if (potion)
        {
            this.potion = potion;
            potionSprite.sprite = potion.potion;
            int dif = potion.maxHealth - MaxHealth;
            Health += dif;
            MaxHealth = potion.maxHealth;
            ShoopController.instance.UpdateHealthTx(Health, MaxHealth);
            Getdamage.Invoke();
        }

    }
    public float getDiference()
    {
        return (float)Health / (float)MaxHealth;
    }
    private void Update()
    {
        if (target && !onFight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, target.position) <= DistanceFight)
            {      
                onFight = true;
            }
        }
    }
    public void StartHeal()
    {
        if (!onAttack) return;
        GetComponent<Animator>().Play("Drink");
        onAttack = false;
    }
    public void Heal()
    {
        Health += potion.heal;
        if (Health > MaxHealth) Health = MaxHealth;
        ShoopController.instance.UpdateHealthTx(Health, MaxHealth);
        Getdamage.Invoke();
        turn.Invoke();
    }
    public void GetDamage(int damage)
    {
       if(helmet)damage -= helmet.shield;
        if (damage <= 0) damage = 0;
        Health -= damage;
        Getdamage.Invoke();
        GameObject p = Instantiate(points, transform.position,transform.rotation);
        p.GetComponentInChildren<TextMeshProUGUI>().text = "-" + damage;
        Destroy(p, 2);
        ShoopController.instance.UpdateHealthTx(Health, MaxHealth);
        if (Health <= 0)
        {
            died();
        }
    }
    void died()
    {
        GetComponent<Animator>().Play("Died");
        Died.Invoke();
    }
    public void FinishFight()
    {
        onFight = false;
        target = null;
    }
    public void AssignBattle(Transform nextEnemy)
    {
        target = nextEnemy;
    }
    public void Attack()
    {
        if (!onAttack) return;
        GetComponent<Animator>().Play(gun.animation);
        onAttack = false;
    }

    public void FinishAttack()
    {
        target.gameObject.GetComponent<Enemy>().GetDamage(gun.damage);
        turn.Invoke();
    }
}
