using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    public float lifespan = 10f;
    public float maturityspan = 5f;
    GridCell[,] Grid;
    GridCell currentGridCell;
    public float moveInterval;
    public bool isAbleToReproduce = false;
    public Manager manager;

    public bool antiMale;
    public bool antiFemale;
    public bool antiChildren;

    public void spawn(){
        Grid = Manager.Grid;
        manager = GameObject.Find("Grid").GetComponent<Manager>();
        moveInterval = Manager.moveInterval;
        assignPosition();
    }

    public void assignPosition(){

        foreach(GridCell cell in Grid){
            if (cell.position == this.transform.position)
                currentGridCell = cell;
        }

        if (currentGridCell.occupied == true){
            currentGridCell.resident.Die();
            currentGridCell.occupied = true;
            currentGridCell.isVirus = true;
            Debug.Log("GridCell set!");
            StartCoroutine(lifetime());
            StartCoroutine(move());
        } else {
            currentGridCell.occupied = true;
            currentGridCell.isVirus = true;
            Debug.Log("GridCell set!");
            StartCoroutine(lifetime());
            StartCoroutine(move());
        } 
    }

    public void OccupyCell(GridCell newCell){
        currentGridCell.occupied = false;
        currentGridCell.resident = null;
        currentGridCell.isVirus = false;
        currentGridCell = newCell;
        transform.position = currentGridCell.position;
        currentGridCell.occupied = true;
        currentGridCell.resident = null;
        currentGridCell.isVirus = true;

        if (antiFemale){
            GridCell rightCell = getRightCell();
            if (rightCell.occupied == true && rightCell.resident != null && rightCell.resident.female && rightCell.resident.isAbleToReproduce){
                rightCell.resident.Die();
            } 

            GridCell leftCell = getLeftCell();
            if (leftCell.occupied == true && leftCell.resident != null && leftCell.resident.female && leftCell.resident.isAbleToReproduce){
                leftCell.resident.Die();
            }

            GridCell upperCell = getUpperCell();
            if (upperCell.occupied == true && upperCell.resident != null && upperCell.resident.female && upperCell.resident.isAbleToReproduce){
                upperCell.resident.Die();
            }

            GridCell lowerCell = getLowerCell();
            if (lowerCell.occupied == true && lowerCell.resident != null && lowerCell.resident.female && lowerCell.resident.isAbleToReproduce){
                lowerCell.resident.Die();
            }
        }

        else if (antiMale){
            GridCell rightCell = getRightCell();
            if (rightCell.occupied == true && rightCell.resident != null && !rightCell.resident.female && rightCell.resident.isAbleToReproduce){
                rightCell.resident.Die();
            } 

            GridCell leftCell = getLeftCell();
            if (leftCell.occupied == true && leftCell.resident != null && !leftCell.resident.female && leftCell.resident.isAbleToReproduce){
                leftCell.resident.Die();
            }

            GridCell upperCell = getUpperCell();
            if (upperCell.occupied == true && upperCell.resident != null && !upperCell.resident.female && upperCell.resident.isAbleToReproduce){
                upperCell.resident.Die();
            }

            GridCell lowerCell = getLowerCell();
            if (lowerCell.occupied == true && lowerCell.resident != null && !lowerCell.resident.female && lowerCell.resident.isAbleToReproduce){
                lowerCell.resident.Die();
            }
        }

        else if (antiChildren){
            GridCell rightCell = getRightCell();
            if (rightCell.occupied == true && rightCell.resident != null && !rightCell.resident.isAbleToReproduce){
                rightCell.resident.Die();
            } 

            GridCell leftCell = getLeftCell();
            if (leftCell.occupied == true && leftCell.resident != null && !leftCell.resident.isAbleToReproduce){
                leftCell.resident.Die();
            }

            GridCell upperCell = getUpperCell();
            if (upperCell.occupied == true && upperCell.resident != null && !upperCell.resident.isAbleToReproduce){
                upperCell.resident.Die();
            }

            GridCell lowerCell = getLowerCell();
            if (lowerCell.occupied == true && lowerCell.resident != null && !lowerCell.resident.isAbleToReproduce){
                lowerCell.resident.Die();
            }
        }

    }

    public IEnumerator lifetime(){
        yield return new WaitForSeconds(10f);
        currentGridCell.occupied = false;
        currentGridCell.isVirus = false;
        this.gameObject.SetActive(false);
    }

    public IEnumerator move(){
        yield return new WaitForSeconds(0.3f);
        int dice = Random.Range(0,4);
        switch(dice){
            case 0:
                moveRight();
                break;
            case 1:
                moveLeft();
                break;
            case 2:
                moveUp();
                break;
            case 3:
                moveDown();
                break;        
        }

        StartCoroutine(move());
    }

    public void moveRight(){
        GridCell rightCell = getRightCell();
        if (rightCell.occupied == false){
            OccupyCell(rightCell);            
        } else {
            OccupyCell(currentGridCell);
        }
    }

    public void moveLeft(){
        GridCell leftCell = getLeftCell();
        if (leftCell.occupied == false){
            OccupyCell(leftCell);
        } else {
            OccupyCell(currentGridCell);
        }
    }

    public void moveUp(){
        GridCell upperCell = getUpperCell();
        if (upperCell.occupied == false){
            OccupyCell(upperCell);
        } else {
            OccupyCell(currentGridCell);
        }
    }

    public void moveDown(){
        GridCell lowerCell = getLowerCell();
        if (lowerCell.occupied == false){
            OccupyCell(lowerCell);
        } else {
            OccupyCell(currentGridCell);
        }
    }

    public GridCell getRightCell(){
        GridCell result;
        foreach (GridCell cell in Grid){
            if (cell.position.x == currentGridCell.position.x + 1 && cell.position.y == currentGridCell.position.y){
                result = cell;
                return result;
            }
        }

        return currentGridCell;
    }

    public GridCell getLeftCell(){
        GridCell result;
        foreach (GridCell cell in Grid){
            if (cell.position.x == currentGridCell.position.x - 1 && cell.position.y == currentGridCell.position.y){
                result = cell;
                return result;
            }
        }

        return currentGridCell;
    }

    public GridCell getUpperCell(){
        GridCell result;
        foreach (GridCell cell in Grid){
            if (cell.position.x == currentGridCell.position.x && cell.position.y + 1 == currentGridCell.position.y){
                result = cell;
                return result;
            }
        }

        return currentGridCell;
    }

    public GridCell getLowerCell(){
        GridCell result;
        foreach (GridCell cell in Grid){
            if (cell.position.x == currentGridCell.position.x && cell.position.y - 1 == currentGridCell.position.y){
                result = cell;
                return result;
            }
        }

        return currentGridCell;
    }

}
