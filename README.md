# Sheep_Rescue_M
In this repository, you will find a mini game called Sheep Rescue, developed after Eric Van de Kerckhove's Introduction to Unity Scripting tutorial, given for Practice 2. This project is an adaptation of the original tutorial, where new adaptations been added to enhance the gameplay experience.

## Description
In this mini game, the player controls a `Hay machine`, and has to save sheeps from falling, feeding them with hay. In addition to the original features from the second part of the tutorial, I have added the following changes:

## Version 1
- Fixed error where sheeps dropped where counted twice.
- Activated the Audio Source on the Music GameObject in the `Game scene` so that the music cplays during the game.

## Version 2
- Increaed spawnrate of sheeps over time has been implemented.
- Implemented Qol elements: when returning from `Game scene` the color of the `Hay machine` does not return to blue, keeps the current color.
- Added `PowerUps` to increase survivability after some time has passed and difficulty has increased.
- Added ScreenShake when sheeps drop.
