using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell
{
    public Vector3 position;
    public bool occupied = false;
    public Human resident;
    public bool isVirus;

    public GridCell(Vector3 position){
        this.position = position;
    }

}
