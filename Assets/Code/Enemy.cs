using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int MaxHealth;
    [SerializeField] public int damage;
    [SerializeField] protected int reward;
    [SerializeField] protected int heal;
    [SerializeField] protected GameObject points;
    protected bool OnDied;
    protected int Health;
    public UnityEvent died = new UnityEvent();
    public UnityEvent turn = new UnityEvent();
    public UnityEvent damagePlayer = new UnityEvent();
    public void Start()
    {
        Health = MaxHealth;

    }
    public abstract void FinishAttack();
    public abstract void GetDamage(int damage);
    public abstract void Died();
    public abstract void Turn();
    public abstract void Heal();
}
