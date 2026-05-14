# GDIM33 Vertical Slice
## Milestone 1 Devlog

The storeBgGraph is used in my game to reveal and hide the hidden background elements. When the player's sanity drops to 10/50, they are one wrong decision away from the game over. When the player's HP is at 10 or less, they will begin to hallucinate. In order to achieve this, I have set up two additional gameObjects in the background that are initially set to false. The storeBgGraph uses OnUpdate to check every frame whether or not the condition for the hallucinations is true or false through an If node. The If node compares the value of the player's current sanity with a float of the value 10. I set this part up by first creating a scene variable called "player" which is of type GameObject and its value is the Player gameObject in the hierarchy. In the graph, I get a reference to this scene variable using the GetVariable Scene node. This node is connected to the StorePlayer Get CurrentHP node which is a C# node. The StorePlayer script is the script which contains data on the player's sanity value and CurrentHP is the float value of the player's current HP. The float value of CurrentHP is compared with the float value 10 using a Less Or Equal node, and the boolean result is then fed to the If node. If the value is true, the If node causes the StoreBgChanger RevealHidden node to run. The RevealHidden method causes the storeStitches and storeEYes gameObjects to be SetActive(true). Thus, the hidden parts of the background become visible. If the value is false, the If node causes the StoreBgChanger HideHidden node to run. This causes the storeStitches and storeEyes gameObjects to be SetActive(false), which will then hide these background sprites. This means that if the player makes the correct decision and regains some sanity after being at 10 sanity, the hallucinations will stop.

One state machine I am using is in the Boss script. The Boss script is used to control boss enemy's behavior during the 2D platformer mode. The script includes a public enum called BossDistanceState which includes three states: Close, Mid, Far. I created an Update function which sets the bossDistance state to Close when the distance between the player and boss gameObjects are less than 5f. bossDistance is set to Mid when the distance between the two is greater than or equal to 5 and less than 10. And if the distance is greater than 10, the state is set to Far. The distanceState is used to determine which actions the boss is able to take. The Boss script contains a method called RestartAttackPattern. This method causes the boss to change color depending on what state it is in. Currently, the content inside this method is a placeholder to test how the state machine will work. Right now, depending on which state the boss is in, the color of the boss gameObject's SpriteRenderer will change. When the boss is Close, the color becomes orange. When it is Mid, the color becomes blue. When it is Far, the color becomes yellow. This is to represent the different action that the boss will perform depending on the distance it is from the player. When the state is set to Far, the boss will also begin chasing after the player. When the state is set to Close, the boss will stop chasing. 

The bossDistance state machine works with the boss moveset system to build the boss enemy behavior. The bossDistance state machine is used to choose which action from the boss moveset system will be performed by the boss. 

[Breakdown](https://docs.google.com/presentation/d/1V5xXVmgtFbwtfhezYsHWtT8_yKYKTivaR_gJYe6Lf6Y/edit?usp=sharing)

The breakdown has beeen updated with a new slide which describes how the Boss state machine works. 

## Milestone 2 Devlog

ANSWER THIS BEFORE CODING: Follow the same steps from the W5 Activity to write a quick summary of your complicating gameplay feature (that you are building for this Milestone) and a task break-down of the steps you need to take to build this feature.

Number 2-3 big steps.
Under each big step, write 2-6 more detailed steps towards completing that task. 
(This is NOT the architecture break-down with bubbles- it's the task steps from the W5 activity/slides.)
Do NOT write about the same feature that you wrote about in class. If you feel like you wrote about your complicating feature in class, pick another feature- I'm sure you have more to build even after the W5 class :P.

#### 1. Creating the Boss enemy for the 2D platformer mode.

##### Step A: Creating and setting up the Boss.

Part 1: Draw the Boss sprites and import the sprite sheet to Unity. The sheet will include sprites for the boss' idle, movement, and attack animations.

Part 2: Slice the sprites and adjust the pivot points for each sprite so that the animations will play correctly.

Part 3: Set up each animation for the Boss GameObject.

##### Step B: Creating the Boss script.

Part 1: Create references to all the prefabs that the boss will use. These prefabs represent the projectiles and effects that the boss will instantiate during its attacks.

Part 2: Create a state machine that will determine the state of the boss based on how far it is from the player. There will be three states: Close, Midrange, Far. Depending on which state the boss is in, it will have a different selection of actions it can take.

Part 3: Create methods for each unique attack/action that the boss can take. Each method will be responsible for performing one action that the boss will take when it runs. For example, the meleeDash() method will cause the boss to dash to the player when it is called. 

##### Step C: Implementing everything to complete the Boss.

Part 1: The attack/action methods created in the previous step will now be implemented into the Animation. The methods will be called at specific points in the boss' animations. For example, the meleeDash() method will be called when the boss' sprite first changes to the dashing sprite. By implementing the methods into the Animation, the boss will be able to perform a variety of actions at correct points in its animation. 

Part 2: Implement debug logs which will show which action the boss will take. There will be debug logs for when the boss performs an action. This will help me check if the boss is performing the correct actions or if certain actions aren't running properly.


#### 2.
The break-down was helpful in planning how to begin building the Boss because it allowed me to better understand which steps to work on first. This helped me plan my schedule better. I think I can improve my break-down by defining the number of actions the boss will have. While working on the boss, I ended up creating signficantly more movesets for it than I orginally anticipated. I realize that this can result in scope creeps if left unchecked. Thus, I will be defining specifically what the boss should be able to do in the break-down for future projects.

Explain how you bridged visual scripting and code in your game. Are you calling a custom event from a Graph from a C# method, or vice versa, and what purpose does this serve in your architecture? Make sure to name the C# script(s) involved, and attach a screenshot of the relevant Graph.

#### 3. 
I bridged visual scripting and code by calling C# events from a Graph.

#### 4. 
The Unity system that I used for this milestone is the Animator. The Animator is used on most prefabs that belong to the boss' attacks, the player GameObject, and boss GameObject. Please check out the Animator attached to the boss GameObject in the Battle scene. 

## Milestone 3 Devlog
Milestone 3 Devlog goes here.
## Milestone 4 Devlog
Milestone 4 Devlog goes here.
## Final Devlog
Final Devlog goes here.
## Open-source assets
- Cite any external assets used here!
