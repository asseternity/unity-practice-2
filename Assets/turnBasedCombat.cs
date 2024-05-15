using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class turnBasedCombat : MonoBehaviour
{
    public GameObject player;
    public float playerMovementSpeed;
    public List<GameObject> enemies;
    public string whoseTurnIsIt = "player";

    void Start()
    {
        // so, let's start coding the battle system
        // first comes the logic. the flow of the code
        // I'll connect it to the gameObjects later
        // <-- interjection: once I do this, and the choices / dialogue options, I'll start working on an isometric game! -->
        // <-- other things, like XP and char creation I'll do later :) -->
        // back to the topic at hand. battle system

        // first - track whose turn it is

        // second - start from the simplest, and make things do one thing only!
        // so, one method should be the player moving on a click
        // store the player's location before the click
        // then in update check how many meters the player has walked
        // once it equals to playerMovementSpeed, stop the player

        // third - make a method to use the enemies array
        // the method loops through the objects in the enemies array
        // calling a move function in each of them

        // fourth - the movement function in the enemy objects should just be the same as player, except the destination
        // IS the player. again, track their distanceWalked and stop them once it exceeds it

        // fifth - then it's the player's turn again. switch the whoseTurnIsIt to player
        // and playerMove only works when whoseTurnIsIt = player.

        // add combat later
    }

    void PlayersTurn()
    {
        // 1) get the pointMoveAnimation to track how much distance it covered for each click:
        // https://www.youtube.com/watch?v=BFT8Fa4ZsMk&t=1s&ab_channel=LlamAcademy
    }
}
