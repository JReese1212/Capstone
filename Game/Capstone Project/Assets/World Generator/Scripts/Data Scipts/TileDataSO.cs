using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TileDataSO/Tile Lists")]
public class TileDataSO : ScriptableObject
{
    [Header("Residential")]
    [SerializeField] public List<GameObject> W0;
    [SerializeField] public List<GameObject> FILL0;
    [SerializeField] public List<GameObject> RE0;
    [SerializeField] public List<GameObject> RI0;
    [SerializeField] public List<GameObject> RW0;
    [SerializeField] public List<GameObject> RL0;
    [SerializeField] public List<GameObject> RT0;
    [SerializeField] public List<GameObject> RX0;
    [Header("Commercial")]
    [SerializeField] public List<GameObject> W1;
    [SerializeField] public List<GameObject> FILL1;
    [SerializeField] public List<GameObject> RE1;
    [SerializeField] public List<GameObject> RI1;
    [SerializeField] public List<GameObject> RW1;
    [SerializeField] public List<GameObject> RL1;
    [SerializeField] public List<GameObject> RT1;
    [SerializeField] public List<GameObject> RX1;
    [Header("Downtown")]
    [SerializeField] public List<GameObject> W2;
    [SerializeField] public List<GameObject> FILL2;
    [SerializeField] public List<GameObject> RE2;
    [SerializeField] public List<GameObject> RI2;
    [SerializeField] public List<GameObject> RW2;
    [SerializeField] public List<GameObject> RL2;
    [SerializeField] public List<GameObject> RT2;
    [SerializeField] public List<GameObject> RX2;
}
