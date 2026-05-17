# GDQuest_DeVera

## GD Quest 1

For Quest 1 we were assigned to create a level fulfilling certain requirements.

For my level specifically, please make sure that the Parent Object "ENDGAME" is not active before starting the game. This is part of my level design. Thank you.

<img width="421" height="225" alt="image" src="https://github.com/user-attachments/assets/adb9eacb-de54-4e80-8470-e904f10089d7" />

If set as inactive this should not be visible before starting the game:
<img width="829" height="393" alt="image" src="https://github.com/user-attachments/assets/47b9347e-653b-49b6-a63e-e4b3ed9c8477" />
<img width="1113" height="852" alt="image" src="https://github.com/user-attachments/assets/241a95c4-b2e8-4872-845f-3214b0f52929" />

How it's supposed to look:
<img width="1915" height="935" alt="image" src="https://github.com/user-attachments/assets/62d741c5-2832-496e-8400-14addd090a3e" />

## GD Quest 2

For this quest we were assigned to add particles and sound effects to our animated game. The effects I have implemented are listed below:

The Running animation now has a "Footsteps" Sound effect as well as a "Dirt Trail" Particle effect emitting from the character's feet whenever she runs.

The Jumping animation also has a fun sound effect.

Added new enemy "Mousey" to the game. Mousey has the footstep sound effect as well. If you touch Mousey you get set back to the last checkpoint. If you jump on Mousey, Mousey will die, not only emitting a sound effect, but also a particle effect. I have intentionally left the Mousey death sound cut off and loud as I found it humorous.

I also added a particle effect to the treasure chest when opened.

Background Music has been added. This will play through the level and will constantly loop.

## GD Quest 3
For this Quest we were tasked to implement a UI and HUD and further expand our game.

The Player now has a Health Bar that depletes incremently (continuous is possible but not implemented here) when touching hazards and enemies. If the player reaches 0 HP, they will respawn at the latest checkpoint and will lose a life, indicated by 3 hearts on the top left corner of the UI. If the player dies on their last life, the Game Over Canvas will fade in. The UI also includes a Coin Counter that counts how many coins the player has collected. There is also a Language Settings Option that changes the Sign Dialogues.

<img width="1175" height="639" alt="image" src="https://github.com/user-attachments/assets/e02d4022-66c2-4163-8e93-0fcfc40aa970" />

The Game Over Canvas displays a "Game Over!" text alongside 2 Buttons, "Try Again" restarts the scene (all collectibles and enemies will be reinstated) and "Exit Game" exits the instance (during an actual build).

<img width="1181" height="645" alt="image" src="https://github.com/user-attachments/assets/d3854bdc-aafb-459f-862e-1725ee0ac747" />

The DeathBox now sets HP to 0 and instantly kills you. I understand the task was to have it automatically receive the Game Over Screen, but I decided to only have that happen when it occurs on the last life as that has a better game feel.

###**SPOILERS**
This Section will show the location of the Jewel, so if you want to experience the maze blind don't read this yet!

Implemented Win Condition with the Jewel at the end of the Maze. Once the Jewel is collected, the Victory Canvas will fade in. This will have the same buttons but a different background from the Game Over Canvas.

Victory Canvas:
<img width="1176" height="636" alt="image" src="https://github.com/user-attachments/assets/cd1fd10d-c6cb-436d-be2e-bc028584a267" />


Location of Jewel:
<img width="1205" height="648" alt="image" src="https://github.com/user-attachments/assets/8c03d413-b1d4-478b-95c9-184cef61c95b" />

<img width="480" height="488" alt="image" src="https://github.com/user-attachments/assets/65d06be4-162e-424a-aa93-5ffb43498d22" />

4 Signs have been implemented with localized text (ENG and DE):
Sign 1:
ENG:
<img width="1181" height="644" alt="image" src="https://github.com/user-attachments/assets/c93ecfa3-2b9b-46e4-b98f-c06ed92ad606" />
DE:
<img width="1181" height="641" alt="image" src="https://github.com/user-attachments/assets/610bcfab-ee8c-46e6-b29c-b7a3d7e2a7de" />

Sign 2:
ENG:
<img width="1184" height="640" alt="image" src="https://github.com/user-attachments/assets/b21312c3-74d7-4f20-bdd8-24f07fda9d5d" />
DE:
<img width="1176" height="638" alt="image" src="https://github.com/user-attachments/assets/b3561a91-dc9a-466f-9308-b026d35df5cb" />

Sign 3:
ENG:
<img width="1179" height="645" alt="image" src="https://github.com/user-attachments/assets/b5e283d0-9b74-47fd-8e0b-71622b449f4e" />
DE:
<img width="1179" height="637" alt="image" src="https://github.com/user-attachments/assets/7ce1de72-38de-434c-be37-edc2fa212251" />

Sign 4:
ENG:
<img width="1179" height="640" alt="image" src="https://github.com/user-attachments/assets/93290883-b57f-40d3-9556-020f597772a8" />
DE:
<img width="1180" height="639" alt="image" src="https://github.com/user-attachments/assets/443863f4-ae05-4572-9c4a-6a281a81c7af" />

Saw Traps have been implemented through out the entire Maze along with more Mouseys!
<img width="1180" height="636" alt="image" src="https://github.com/user-attachments/assets/242ac32f-0814-4e87-8930-5ae7ff0bcfe5" />
<img width="1173" height="640" alt="image" src="https://github.com/user-attachments/assets/b6719be6-7234-43a4-b49d-328f4c84017c" />
<img width="1179" height="636" alt="image" src="https://github.com/user-attachments/assets/bf956033-2cce-4e8b-bfb4-cd0a4f65e8c1" />

Losing all your HP and lives causes a Game Over!
<img width="1183" height="633" alt="image" src="https://github.com/user-attachments/assets/8f8bb859-8018-4a9e-8ae7-f45cba616c89" />








