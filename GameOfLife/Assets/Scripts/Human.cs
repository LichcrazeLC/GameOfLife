using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public float lifespan = 5f;
    public float maturityspan = 5f;
    GridCell[,] Grid;
    GridCell currentGridCell;
    public float moveInterval;
    public bool female;
    public bool isAbleToReproduce = false;
    public Manager manager;

    public void spawn(){
        Grid = Manager.Grid;
        manager = GameObject.Find("Grid").GetComponent<Manager>();
        moveInterval = Manager.moveInterval;
        assignPosition();
    }

    public void assignPosition(){
        currentGridCell = Grid[Random.Range(0, Grid.GetLength(0)), Random.Range(0, Grid.GetLength(1))];
        if (currentGridCell.occupied == false){
            transform.position = currentGridCell.position;
            currentGridCell.occupied = true;
            currentGridCell.resident = this;
            StartCoroutine(maturitytime());
            StartCoroutine(move());
        } else {
            assignPosition();
        }
    }

    public void OccupyCell(GridCell newCell){
        currentGridCell.occupied = false;
        currentGridCell.resident = null;
        currentGridCell = newCell;
        transform.position = currentGridCell.position;
        currentGridCell.occupied = true;
        currentGridCell.resident = this;

        GridCell rightCell = getRightCell();
        if (rightCell.occupied == true && rightCell.isVirus == false && rightCell.resident.female != this.female && this.isAbleToReproduce == true){
            manager.spawnChild();
            return;
        } 

        GridCell leftCell = getLeftCell();
        if (leftCell.occupied == true && leftCell.isVirus == false && leftCell.resident.female != this.female && this.isAbleToReproduce == true){
            manager.spawnChild();
            return;
        }

        GridCell upperCell = getUpperCell();
        if (upperCell.occupied == true && upperCell.isVirus == false && upperCell.resident.female != this.female && this.isAbleToReproduce == true){
            manager.spawnChild();
            return;
        }

        GridCell lowerCell = getLowerCell();
        if (lowerCell.occupied == true && lowerCell.isVirus == false && lowerCell.resident.female != this.female && this.isAbleToReproduce == true){
            manager.spawnChild();
            return;
        }

    }

    public IEnumerator lifetime(){
        yield return new WaitForSeconds(5f);
        Die();
    }

    public void Die(){
        
        currentGridCell.occupied = false;
        this.gameObject.SetActive(false);
        if (this.female == true && this.isAbleToReproduce)
            manager.AddFemale(-1);
        else if (this.female == false && this.isAbleToReproduce)
            manager.AddMale(-1);
        else if (!this.isAbleToReproduce){
            manager.AddChildren(-1);
        }

    }

    public IEnumerator maturitytime(){
        yield return new WaitForSeconds(5f);
        if (this.female == true){
            this.gameObject.GetComponent<SpriteRenderer>().sprite = manager.female_sprite;
            manager.AddFemale(1);
        }
        else {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = manager.male_sprite;
            manager.AddMale(1);
        }

        manager.AddChildren(-1);
        this.isAbleToReproduce = true;

        StartCoroutine(lifetime());
    }
    
    public IEnumerator move(){
        yield return new WaitForSeconds(0.9f);
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
        }
    }

    public void moveLeft(){
        GridCell leftCell = getLeftCell();
        if (leftCell.occupied == false){
            OccupyCell(leftCell);
        }
    }

    public void moveUp(){
        GridCell upperCell = getUpperCell();
        if (upperCell.occupied == false){
            OccupyCell(upperCell);
        }
    }

    public void moveDown(){
        GridCell lowerCell = getLowerCell();
        if (lowerCell.occupied == false){
            OccupyCell(lowerCell);
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
