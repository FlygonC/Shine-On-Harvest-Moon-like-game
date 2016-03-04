using UnityEngine;
using System.Collections;

[System.Serializable]
[CreateAssetMenu(fileName = "New Plant", menuName = "Plant", order = 2)]
public class Plant : ScriptableObject
{
    public int growTime;// Days per stage of growth
    public int stages;// Number of Stages of growth
    public float fragility;// Health lost in harmfull conditions
    public Mesh[] stagesMeshs;// Visual Meshes for stages
    public bool dieOnHarvest;// If dies When Harvested
    public int harvestRevertStage;// Stage to revert to on harvest
    public Produce yield;// Item you get from Harvesting
}