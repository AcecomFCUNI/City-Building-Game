using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Action
{
    Build,
    Remove
}
public class BuildingHandler : MonoBehaviour
{
    [SerializeField] private Building[] buildings;
    [SerializeField] private Grid grid;
    private Building selectedBuilding;
    private void Start()
    {
        Vector3 auxPos = grid.CalculateGridPosition(new Vector3(10, 0, 10));
        grid.AddBuilding(buildings[1], new Vector3(5, 0.5f, 5));
        grid.AddBuilding(buildings[2], new Vector3(3, 0.5f, 5));
        grid.AddBuilding(buildings[3], new Vector3(5, 0.5f, 3));

    }
    void Update()
    {
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift) && selectedBuilding != null)
        {
            InteractWithBoard(Action.Build);
        }
        else if(Input.GetMouseButtonDown(0) && selectedBuilding != null)
        {
            InteractWithBoard(Action.Build);
        }

        if (Input.GetMouseButtonDown(1))
        {
            InteractWithBoard(Action.Remove);
        }
    }
    private void InteractWithBoard(Action action)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            Vector3 gridPosition = grid.CalculateGridPosition(hit.point);
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                if (action == Action.Build && grid.CheckForBuildingAtPosition(gridPosition) == null)
                {
                    if (City.Instance.Cash >= selectedBuilding.cost)
                    {
                        City.Instance.DepositCash(-selectedBuilding.cost);
                        UIController.Instance.UpdateCityData();
                        City.Instance.buildingCounts[selectedBuilding.id]++;
                        grid.AddBuilding(selectedBuilding, gridPosition);
                    }
                }
                else if(action == Action.Remove && grid.CheckForBuildingAtPosition(gridPosition) != null)
                {
                    grid.RemoveBuilding(gridPosition);
                    City.Instance.DepositCash(grid.CheckForBuildingAtPosition(gridPosition).cost * 3 / 4);
                    UIController.Instance.UpdateCityData();
                }
            }

        }
    }
    public void EnableBuilder(int building)
    {
        selectedBuilding = buildings[building];
        Debug.Log("Selected building: " + selectedBuilding.buildingName);
    }
}
