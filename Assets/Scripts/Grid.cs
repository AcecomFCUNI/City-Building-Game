using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private Building[,] buildings= new Building[100,100];
    public void AddBuilding(Building building, Vector3 gridPosition)
    {
        Building buildingToAdd = Instantiate(building, gridPosition, Quaternion.identity);
        buildings[(int)gridPosition.x, (int)gridPosition.z] = buildingToAdd;
    }
    public Building CheckForBuildingAtPosition(Vector3 position)
    {
        return buildings[(int)position.x, (int)position.z];
    }
    public void RemoveBuilding(Vector3 position)
    {
        Destroy(buildings[(int)position.x, (int)position.z].gameObject);
        buildings[(int)position.x, (int)position.z] = null;
    }
    public Vector3 CalculateGridPosition(Vector3 position)
    {
        return new Vector3(Mathf.Round(position.x), 0.5f, Mathf.Round(position.z));
    }
}
