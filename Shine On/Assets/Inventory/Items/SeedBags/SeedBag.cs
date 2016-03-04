using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Seed", menuName = "SeedBag", order = 2)]
public class SeedBag : ItemObject
{
    [SerializeField]
    private Plant _Plant;
    public Plant plants
    {
        get
        {
            return _Plant;
        }
    }
}