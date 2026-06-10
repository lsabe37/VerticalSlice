# GDIM33 Vertical Slice
## Milestone 1 Devlog

The storeBgGraph is used in my game to reveal and hide the hidden background elements. When the player's sanity drops to 10/50, they are one wrong decision away from the game over. When the player's HP is at 10 or less, they will begin to hallucinate. In order to achieve this, I have set up two additional gameObjects in the background that are initially set to false. The storeBgGraph uses OnUpdate to check every frame whether or not the condition for the hallucinations is true or false through an If node. The If node compares the value of the player's current sanity with a float of the value 10. I set this part up by first creating a scene variable called "player" which is of type GameObject and its value is the Player gameObject in the hierarchy. In the graph, I get a reference to this scene variable using the GetVariable Scene node. This node is connected to the StorePlayer Get CurrentHP node which is a C# node. The StorePlayer script is the script which contains data on the player's sanity value and CurrentHP is the float value of the player's current HP. The float value of CurrentHP is compared with the float value 10 using a Less Or Equal node, and the boolean result is then fed to the If node. If the value is true, the If node causes the StoreBgChanger RevealHidden node to run. The RevealHidden method causes the storeStitches and storeEYes gameObjects to be SetActive(true). Thus, the hidden parts of the background become visible. If the value is false, the If node causes the StoreBgChanger HideHidden node to run. This causes the storeStitches and storeEyes gameObjects to be SetActive(false), which will then hide these background sprites. This means that if the player makes the correct decision and regains some sanity after being at 10 sanity, the hallucinations will stop.

One state machine I am using is in the Boss script. The Boss script is used to control boss enemy's behavior during the 2D platformer mode. The script includes a public enum called BossDistanceState which includes three states: Close, Mid, Far. I created an Update function which sets the bossDistance state to Close when the distance between the player and boss gameObjects are less than 5f. bossDistance is set to Mid when the distance between the two is greater than or equal to 5 and less than 10. And if the distance is greater than 10, the state is set to Far. The distanceState is used to determine which actions the boss is able to take. The Boss script contains a method called RestartAttackPattern. This method causes the boss to change color depending on what state it is in. Currently, the content inside this method is a placeholder to test how the state machine will work. Right now, depending on which state the boss is in, the color of the boss gameObject's SpriteRenderer will change. When the boss is Close, the color becomes orange. When it is Mid, the color becomes blue. When it is Far, the color becomes yellow. This is to represent the different action that the boss will perform depending on the distance it is from the player. When the state is set to Far, the boss will also begin chasing after the player. When the state is set to Close, the boss will stop chasing. 

The bossDistance state machine works with the boss moveset system to build the boss enemy behavior. The bossDistance state machine is used to choose which action from the boss moveset system will be performed by the boss. 

[Breakdown](https://docs.google.com/presentation/d/1V5xXVmgtFbwtfhezYsHWtT8_yKYKTivaR_gJYe6Lf6Y/edit?usp=sharing)

The breakdown has beeen updated with a new slide which describes how the Boss state machine works. 

## Milestone 2 Devlog

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

#### 3. 
I bridged visual scripting and code by calling C# methods from an event inside the Graph. The gameStateGraph is a state graph that is responsible for handling the game state. It determines whether the game is running or paused during the 2D platformer mode. Inside the script state within this graph, there is an On Enter State event node which causes the Show Pause Menu method belonging to the Menu Manager C# script to run. When this method runs, the pause menu will appear on screen and the game will freeze. This script state also has an On Exit State event node which causes the Hide Pause Menu method of the Menu Manager script to run. When this method runs, the pause menu will be disabled and the game will resume. The pause script state is entered when the player presses the escape key. This is done using an On Keyboard Input event node, which is used as the transition condition to transition from the default game state to the paused game state. Another transition happens from the paused state to the default state when the player presses the escape key while in the pause state. This is achieved using an On Keyboard Input event node inside the other transition. By calling the methods from the C# script inside the Graph, I am able to pause and unpause them game and show/hide the pause menu whenever the game state changes between the paused and unpaused state.

<img width="641" height="441" alt="Screenshot 2026-05-14 204340" src="https://github.com/user-attachments/assets/7617a379-89f2-4ba9-bd41-90d6bcc43346" />

#### 4. 
The Unity system that I used for this milestone is the Animator and Animation. The Animator and Animation are used on most prefabs that belong to the boss' attacks, the player GameObject, and boss GameObject. Please check out the Animator and Animation attached to the boss GameObject in the Battle scene. 

## Milestone 3 Devlog

#### 1.
<img width="608" height="388" alt="Screenshot 2026-05-28 205832" src="https://github.com/user-attachments/assets/4fdc2a11-675f-4988-86c2-3468e11235ea" />

My shader graph is used to create a green outline effect on the donuts when the player hovers their mouse over them. The shader graph creates the outline effect by creating an offset texture of the original texture in four directions: up, down, left, right. This is done by using four tiling and offset nodes to offset the positions of the four additional sample texture 2D nodes. The alpha values of the four resulting sample texture 2D nodes are then added using add nodes. This results in the silhouette of the donuts' outline. The alpha value of this silhouette node is then subtracted by the alpha value of the original samplet texture 2D node, which creates the outline effect for each donut. The resulting outline is then multiplief by the outline color using a multiply node to make the outlines green. Finally, the node with the outline is added to the node with the original donut texture to create the donuts with the green outline. The thickness of the outline is controlled by a float called OutlineThickness. To make the outline appear when the player hovers over a donut, I then created a C# script which uses OnMouseEnter() and OnMouseExit() to change the float value of OutlineThickness. 

The DonutOutlineShader can be found in the Shader folder inside the project.

#### 2.
Based on the playtest feedback, I improved the behavior and movesets of the boss in the 2D platformer mode. One issue that was brought up was that the boss' spear summoning attack was too difficult to avoid because there was too little space in between each spear and the spears appeared too quickly. I fixed this by increasing the delay before the spears grew to their full height (which is when it does damage) and also reducing the number of spears that are summoned. Another issue that was brought up was that it was confusing at first for players to understand which health bar belonged to them and which belonged to the boss. I fixed this issue by adding the boss' name over its health bar. One more thing that was brought up during the playtests was that it was too easy to identify which donut to give to the customers. The players never felt that their sanity was in danger because of this. To address this, I increased the selection of donuts and also made the requests from some of the customers more vague, which should increase the difficulty of selecting which donut to give. The last issue that I observed was that players didn't know how to shoot the gun during the anomaly hunt mode. To address this, I added text that reads "S to Shoot" next to the reticle UI.

#### 3.
This milestone has several new features. First, the hitboxes for all of the boss' attacks have now been implemented. This means that the boss' melee attacks will also do damage to the player. Second, the there is now a game over screen for when the player reaches 0HP in the boss fight. When the player reaches 0 HP, the game's timescale will be set to 0 and the game over UI will appear. The player can click on the retry button to reattempt the fight from the beginning. Third, the 2D platformer mode can now be accessed from the anomaly hunt mode by shooting the imposter NPC (which is the second manager that shows up at the end). When the player shoots this NPC, the game will move to the 2D platformer boss fight as soon as the NPC finishes their post-shot dialogue. Fourth, there are a few new customer NPC's for the anomaly hunt mode. This increases the overall playtime for the game and increases the core gameplay loop of this mode. And lastly, there are some additional minor features that have been added to enhance the play experience. For example, when the player gets an order correct or wrong, a big text which reads "You Suck" or "Amazing" appears behind the customer sprite to help emphasize the customer's reaction. Additionally, when the player hovers over a donut, there will be a bright green outline to help players see which donut they are about to select.

## Final Devlog
Final Devlog goes here.

## Open-source assets
SFX Assets
- [Button Tap SFX](https://pixabay.com/sound-effects/film-special-effects-button-ui-sound-effect-395762/)
- [Tap SFX](https://pixabay.com/sound-effects/film-special-effects-soft-app-button-tap-sound-5-547873/)
- [Button SFX](https://pixabay.com/sound-effects/film-special-effects-button-pressed-38129/)
- [Gun Reload SFX](https://pixabay.com/sound-effects/film-special-effects-1911-reload-6248/)
-[Gun Shot SFX](https://pixabay.com/sound-effects/film-special-effects-9mm-pistol-shoot-short-reverb-7152/)
-[Wrong SFX](https://pixabay.com/sound-effects/film-special-effects-losing-horn-313723/)
-[Bell SFX](https://pixabay.com/sound-effects/film-special-effects-servicereceptionist-bell-418758/)
-[Clicking SFX](https://pixabay.com/sound-effects/film-special-effects-click-button-131479/)
-[Explosion SFX](https://pixabay.com/sound-effects/film-special-effects-debris-break-2-457507/)
-[BGM](https://pixabay.com/music/solo-piano-claire-de-lune-debussy-piano-411227/)

