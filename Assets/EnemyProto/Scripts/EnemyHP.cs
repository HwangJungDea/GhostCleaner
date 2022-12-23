using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ü�°� ü�� UI�� �����ϰ�ʹ�.
// �¾ �� ü���� �ִ�ü������ �ϰ�ʹ�.
public class EnemyHP : MonoBehaviour
{
    public int maxHP = 5;
    public float hp;
    public float tf;

    public static EnemyHP instance;
    private void Awake()
    {
        instance = this;
    }


    public float HP
    {
        get { return hp; }
        set
        {
            hp = value;
        }
    }

    public float TF
    {
        get { return tf; }
        set
        {
            tf = value;
        }
    }
    void Start()
    {
        // �¾ �� ü���� �ִ�ü������ �ϰ�ʹ�.
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
