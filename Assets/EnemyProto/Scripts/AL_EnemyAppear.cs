using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AL_EnemyAppear : MonoBehaviour
{
    SkinnedMeshRenderer Mren;

    List<MeshRenderer> list;

    public SkinnedMeshRenderer MRen
    {
        get { return Mren; }
        set { Mren = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        Mren = GetComponent<SkinnedMeshRenderer>();
        list = new List<MeshRenderer>();
        MeshRenderer[] rens = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < rens.Length; i++)
        {
            if (rens[i] != Mren)
            {
                list.Add(rens[i]);
            }
        }
    }


    void Update()
    {
        Enemy pEnemy = this.transform.parent.gameObject.GetComponent<Enemy>();
        if (pEnemy.state != Enemy.State.death)
            Mren.enabled = !list[0].enabled || !list[1].enabled;
        else
            Mren.enabled = false;
    }
}
