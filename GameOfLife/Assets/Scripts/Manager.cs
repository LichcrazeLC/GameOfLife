using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Manager : MonoBehaviour
{
    
    public static GridCell[,] Grid;
    public GameObject male;
    public GameObject female;
    public float spawnInterval;
    public static float moveInterval = 0.3f;
    public Sprite female_sprite;
    public Sprite male_sprite;

    public int maleNr;
    public Text maleNrText;

    public int femaleNr;
    public Text femaleNrText;

    public int childrenNr;
    public Text childrenNrText;

    public GameObject maleVirus;
    public GameObject femaleVirus;
    public GameObject childrenVirus;

    public int SelectedVirus = 1;

    public int MaleVirusAmount;

    public int FemaleVirusAmount;

    public int ChildrenVirusAmount;

    public Text MaleVirusAmountText;

    public Text FemaleVirusAmountText;

    public Text ChildrenVirusAmountText;


    void Start()
    {
        Grid = new GridCell[17,9];

        for (int i = 0; i < 17; i++)
            for (int j = 0; j < 9; j++)
                Grid[i,j] = new GridCell(new Vector3(i - 8, j - 4, -1));

        for (int i = 0; i < 30; i++){
            spawnChild();
        }

        AddMaleVirus(0);
        AddChildrenVirus(0);
        AddFemaleVirus(0);

        StartCoroutine(getRandomVirus());

    }

    public void AddMaleVirus(int amount){
        MaleVirusAmount += amount;
        MaleVirusAmountText.text = MaleVirusAmount.ToString();
    }

    public void AddFemaleVirus(int amount){
        FemaleVirusAmount += amount;
        FemaleVirusAmountText.text = FemaleVirusAmount.ToString();
    }

    public void AddChildrenVirus(int amount){
        ChildrenVirusAmount += amount;
        ChildrenVirusAmountText.text = ChildrenVirusAmount.ToString();
    }

    public void spawnChild(){

        int coinflip = Random.Range(0, 2);

        GameObject newHuman;

        if (coinflip == 0){
            newHuman = Instantiate(male);
            newHuman.GetComponent<Human>().female = false;
        }
        else {
            newHuman = Instantiate(female); 
            newHuman.GetComponent<Human>().female = true;
        }    

        AddChildren(1);
        newHuman.GetComponent<Human>().spawn();
    }

    public void spawnMaleVirus(Vector3 pos){

        GameObject newMaleVirus;
        newMaleVirus = Instantiate(maleVirus, pos, Quaternion.identity);
        newMaleVirus.GetComponent<Virus>().antiMale = true;
        newMaleVirus.GetComponent<Virus>().spawn();
    }

     public void spawnFemaleVirus(Vector3 pos){

        GameObject newMaleVirus;
        newMaleVirus = Instantiate(femaleVirus, pos, Quaternion.identity);
        newMaleVirus.GetComponent<Virus>().antiFemale = true;
        newMaleVirus.GetComponent<Virus>().spawn();
    }

     public void spawnChildrenVirus(Vector3 pos){

        GameObject newMaleVirus;
        newMaleVirus = Instantiate(childrenVirus, pos, Quaternion.identity);
        newMaleVirus.GetComponent<Virus>().antiChildren = true;
        newMaleVirus.GetComponent<Virus>().spawn();
    }

    public void AddMale(int count){
        maleNr += count;
        maleNrText.text = maleNr.ToString();
    }

    public void AddFemale(int count){
        femaleNr += count;
        femaleNrText.text = femaleNr.ToString();
    }

    public void AddChildren(int count){
        childrenNr += count;
        childrenNrText.text = childrenNr.ToString();
    }

    public Vector3 snap(Vector3 pos) {
        float x = pos.x;
        float y = pos.y;
        float z = pos.z;
        x = Mathf.FloorToInt(x);
        y = Mathf.FloorToInt(y);
        z = -1;
        return new Vector3(x, y, z);
    }

    public IEnumerator getRandomVirus(){
        int dice = Random.Range(1,4);
        switch (dice){
            case 1:
                AddChildrenVirus(1);
                break;
            case 2:
                AddFemaleVirus(1);
                break;
            case 3:
                AddMaleVirus(1);
                break;
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(getRandomVirus());
    }

    void Update()
    {
         if (Input.GetMouseButtonDown(0))
         {  
             Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
             switch (SelectedVirus){
                 case 1:
                    if (MaleVirusAmount > 0){
                        spawnMaleVirus(snap(point));
                        AddMaleVirus(-1);
                    }
                    break; 
                 case 2:
                    if (FemaleVirusAmount > 0){
                        spawnFemaleVirus(snap(point));
                        AddFemaleVirus(-1);
                    }
                    break;
                case 3:
                    if (ChildrenVirusAmount > 0){
                        spawnChildrenVirus(snap(point));
                        AddChildrenVirus(-1);
                    }
                    break;
             }
         }

         if (Input.GetKey(KeyCode.Q)){
             SelectedVirus = 1;
         } 

         if (Input.GetKey(KeyCode.W)){
             SelectedVirus = 2;
         }

          if (Input.GetKey(KeyCode.E)){
             SelectedVirus = 3;
         }
    }

}
