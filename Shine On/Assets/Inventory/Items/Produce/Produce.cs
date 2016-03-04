using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Produce", menuName = "Produce", order = 3)]
public class Produce : ItemObject
{
    [SerializeField]
    float _BaseValue;
    public float baseValue
    {
        get
        {
            return _BaseValue;
        }
    }
}