# GDIM33 Vertical Slice
## Milestone 1 Devlog

The storeBgGraph is used in my game to reveal and hide the hidden background elements. When the player's sanity drops to 10/50, they are one wrong decision away from the game over. When the player's HP is at 10 or less, they will begin to hallucinate. In order to achieve this, I have set up two additional gameObjects in the background that are initially set to false. The storeBgGraph uses OnUpdate to check every frame whether or not the condition for the hallucinations is true or false through an If node. The If node compares the value of the player's current sanity with a float of the value 10. I set this part up by first creating a scene variable called "player" which is of type GameObject and its value is the Player gameObject in the hierarchy. In the graph, I get a reference to this scene variable using the GetVariable Scene node. This node is connected to the StorePlayer Get CurrentHP node which is a C# node. The StorePlayer script is the script which contains data on the player's sanity value and CurrentHP is the float value of the player's current HP. The float value of CurrentHP is compared with the float value 10 using a Less Or Equal node, and the boolean result is then fed to the If node. If the value is true, the If node causes the StoreBgChanger RevealHidden node to run. The RevealHidden method causes the storeStitches and storeEYes gameObjects to be SetActive(true). Thus, the hidden parts of the background become visible. If the value is false, the If node causes the StoreBgChanger HideHidden node to run. This causes the storeStitches and storeEyes gameObjects to be SetActive(false), which will then hide these background sprites. This means that if the player makes the correct decision and regains some sanity after being at 10 sanity, the hallucinations will stop.

One state machine I am using is in the Boss script. The Boss script is used to control boss enemy's behavior during the 2D platformer mode. The script includes a public enum called BossDistanceState which includes three states: Close, Mid, Far. I created an Update function which sets the bossDistance state to Close when the distance between the player and boss gameObjects are less than 5f. bossDistance is set to Mid when the distance between the two is greater than or equal to 5 and less than 10. And if the distance is greater than 10, the state is set to Far. The distanceState is used to determine which actions the boss is able to take. The Boss script contains a method called RestartAttackPattern. This method causes the boss to change color depending on what state it is in. Currently, the content inside this method is a placeholder to test how the state machine will work. Right now, depending on which state the boss is in, the color of the boss gameObject's SpriteRenderer will change. When the boss is Close, the color becomes orange. When it is Mid, the color becomes blue. When it is Far, the color becomes yellow. This is to represent the different action that the boss will perform depending on the distance it is from the player. When the state is set to Far, the boss will also begin chasing after the player. When the state is set to Close, the boss will stop chasing. 

The bossDistance state machine works with the boss moveset system to build the boss enemy behavior. The bossDistance state machine is used to choose which action from the boss moveset system will be performed by the boss. 

[Breakdown](https://docs.google.com/presentation/d/1V5xXVmgtFbwtfhezYsHWtT8_yKYKTivaR_gJYe6Lf6Y/edit?usp=sharing)

The breakdown has beeen updated with a new slide which describes how the Boss state machine works. 

## Milestone 2 Devlog
Milestone 2 Devlog goes here.
## Milestone 3 Devlog
Milestone 3 Devlog goes here.
## Milestone 4 Devlog
Milestone 4 Devlog goes here.
## Final Devlog
Final Devlog goes here.
## Open-source assets
- Cite any external assets used here!
