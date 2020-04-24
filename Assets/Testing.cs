using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour
{
    Grid grid;
    [SerializeField] private HeatMapVisual heatMapVisual;
    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream
        grid = new Grid(20, 10, 10f, Vector3.zero);
        
    }

=======
        grid = new Grid(150, 65, 1f, new Vector3(0,0));
        heatMapVisual.SetGrid(grid);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = UtilsClass.GetMouseWorldPosition();
            int value=grid.GetValue(position);
            //grid.SetValue(position,value+5); //For Highlighting a Single Block
            //grid.AddValue(position,5,5); //For Highlighting a Ranged Diamond
            grid.GradAddValue(position,100,5,25); //For Highlighting a GradRanged Diamond
        }

        if (Input.GetMouseButtonDown(1))
        {
            //Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
        }

    }
>>>>>>> Stashed changes
}
