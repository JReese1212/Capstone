using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BuildingDataSO/Building Settings")]
public class BuildingDataSO : ScriptableObject
{
    public List<int> Heights;
    public List<Material> Materials;
}
