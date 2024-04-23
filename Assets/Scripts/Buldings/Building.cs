using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IProfitable
{
    public abstract IEnumerator CalculateCityParameters();
}
public class Building : MonoBehaviour
{
    private int level;
    public int id; 
    public int cost;
    public string buildingName;
}
