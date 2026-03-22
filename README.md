# PresidentialGameEngine

This is a project that attempts to replicate the rules of the board game _1960: The Making of the President_.  

### About the game
1960 is a historical board game simulating the US presidential election between John F. Kennedy and Richard M. Nixon.  For more information about the game, you can visit [the game's page on BoardGameGeek](https://boardgamegeek.com/boardgame/27708/) or watch [the rules tutorial on the Watch It Played YouTube channel](https://www.youtube.com/watch?v=iIOvpOte-ik).

### About the repo
This repo is an attempt to create the rules of this moderately complicated board game in C#.  This mostly serves as an exercise in coding, design, and some practice with test-driven development.  This is not playable game on its own, and at time of writing it is not feature complete.

### How to use this repo
This is a basic class library and console application and should run in your average full-featured .NET IDE (Visual Studio, JetBrains Rider, etc.).

#### Project Descriptions

* PresidentialGameEngine.ClassLibrary
    * The core functional classes, built mostly on generic enums and classes
	
* PresidentialGameEngine.ClassLibrary.Tests
    * The unit tests covering those classes

* PresidentialGameEngine.ConsoleRunner
    * A console application providing a sandbox and simple access to conduct manual testing
	
* NineteenSixty
    * A project to consume the classes above and implement them with non-generic uses.
 
* NineteenSixty.Tests
    * The unit tests covering the 1960 specific concepts.

* NineteenSixtyApplication
	* A console application offering some semblance of structure and normal play to try and expose any design issues

### License Information
All game materials are © GMT Games, LLC.  Used with permission.

My work is © John Hibbert and licensed under the terms of the MIT License.
