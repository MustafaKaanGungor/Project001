using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHP = 100;
    private int hp;



    public int _Hp
    {
        get => hp;
        private set
        {
            var isDamage = value < hp;
            hp = Mathf.Clamp(value, 0, maxHP);
            if(isDamage)
            {
                Damaged?.Invoke(hp);
            }
            else
            {
                Healed?.Invoke(hp);
            }

            if(hp <= 0)
            {
                Died?.Invoke(hp);
            }
        }
    }


    public UnityEvent<int> Healed;
    
    public UnityEvent<int> Damaged;
    public UnityEvent<int> Died;

    private void Awake() 
    {
        hp = maxHP;    
    }

    public void Damage(int amount)
    {
        _Hp -= amount;
    }

    public void Repair(int amount)
    {
        _Hp += amount;
    }

    public void Healfull()
    {
        _Hp = maxHP; 
    }

    public void Kill()
    {
        _Hp = 0;
    }

    public void Adjust(int value)
    {
        _Hp = value;
    }
}
