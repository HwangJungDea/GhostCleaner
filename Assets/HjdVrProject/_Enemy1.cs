using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRN_Enemy : MonoBehaviour
{
    public static VRN_Enemy instance;
    MeshRenderer Mren;

    List<MeshRenderer> list;

    public MeshRenderer MRen
    {
        get { return Mren; }
        set { Mren = value; }
    }


    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Mren = GetComponent<MeshRenderer>();
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

    // Update is called once per frame
    void Update()
    {
        Mren.enabled = !list[0].enabled || !list[1].enabled;
    }
}
