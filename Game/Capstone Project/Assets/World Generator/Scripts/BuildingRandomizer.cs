using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingRandomizer : MonoBehaviour
{
    [SerializeField]
    private BuildingDataSO BuildingData;

    [SerializeField] private MeshRenderer MeshRend;

    // Start is called before the first frame update
    void Start()
    {
        MeshRend = GetComponent<MeshRenderer>();
        //Random.InitState(MasterSeed);
        SetHeight();
        SetMaterial();
    }

    void SetHeight() 
    {
        int select = BuildingData.Heights[Random.Range(0, BuildingData.Heights.Count)];

        Vector3 Pos = this.transform.position;
        Pos.y = select;
        Pos.y /= 2;
        this.transform.position = Pos;

        Vector3 Scale = this.transform.localScale;
        Scale.y = select;
        this.transform.localScale = Scale;
    }

    void SetMaterial()
    {
        MeshRend.material = BuildingData.Materials[Random.Range(0, BuildingData.Materials.Count)];
    }
}

