using UnityEngine;
public class PlayerMovement:MonoBehaviour
{
int playerHealth = 100;
void Start(){
    Debug.Log("THE PLAYER SCRIPT IS RUNNING!");
    int damage = 30 ; 
    playerHealth = playerHealth - damage;
    Debug.Log("Health after damage is "+playerHealth);

    int healing = 10;
    playerHealth = playerHealth +healing;
    Debug.Log("Health after healing is "+ playerHealth);


    int totalPlayers = 5;
    int babysitters = 4;
    int monsters = totalPlayers - babysitters;
    Debug.Log("Number of Monsters is "+monsters );
}
 void Update(){



 }   


}