using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolPlane : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private BuildingHandler buildingHandler;
    [SerializeField] private Vector3 auxPosition;
    void Update()
    {
        UpdatePlanePosition();
    }

    private void UpdatePlanePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit) && buildingHandler.SelectedBuilding != null)
        {
            transform.position = grid.CalculateGridPosition(hit.point);
        }
        else if(buildingHandler.SelectedBuilding == null)
        {
            transform.position = auxPosition;
        }
    }
}
