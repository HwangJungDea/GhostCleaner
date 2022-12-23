using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 체력과 체력 UI를 관리하고싶다.
// 태어날 때 체력을 최대체력으로 하고싶다.
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
        // 태어날 때 체력을 최대체력으로 하고싶다.
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
