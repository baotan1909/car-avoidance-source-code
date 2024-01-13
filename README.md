<h1 align="center">Car Avoidance</h1>
<h3 align="center" margin="0">COS20007 – Object Oriented Programming</h3>
<h5 align="center">An Upgrade from Custom Program – Swinburne Vietnam (Sep 2023)</h5>
<p align="center"><img src="https://github.com/baotan1909/car-avoidance-source-code/assets/125344198/353598f0-392b-4088-bbfa-9b6fb824fc34" alt="Car Avoidance gameplay screen" width="50%" height="50%"></p>
<h2>Overview</h2>
In this project, I attempt to make a complete game for the first time. It is a simple car game, which is developed from my school project! :D
<br>You can click <a href="https://github.com/baotan1909/Car-Avoidance">here</a> to see the first version of it in Ruby programming language (it was really a mess).
<h2>Project Files Description</h2>
<ul>
  <li>GameObject: the parent class of all the game objects (abstract class).</li>
  <li>Player, Line, Obstacle, Coin: the implementation for the specific game objects.</li>
  <li>LineManager, ObstacleManager, CoinManager: the code responsible for managing generation and movement of game objects automatically (except Player).</li>
  <li>GameScreen: the parent class of all the game screens (abstract class).</li>
  <li>IngameScreen: the parent class of Pause and GameOver screen.</li>
  <li>GameOver, Instruction, Pause, Settings, Shop, Start: the implementation for the specific game screen.</li>
  <li>ISkill: the interface of skills.</li>
  <li>Skill: the common implementation for the skills (duration, cooldown, draw).</li>
  <li>/configuration/GameConfiguration: The file contains game’s properties (Speed and Collision).</li>
  <li>/skill/SkillArgument: The argument for skills (i.e. Speed for Time stop and Slowdown).</li>
  <li>S_Invicible, S_Slowdown, S_Timestop: the specific implementation for each skill’s ability.</li>
  <li>SkillManager: the logic to use, save and load the skills (this one exists because my architecture was bad).</li>
  <li>ScoreHandler: manages the scoring system of the game.</li>
  <li>AudioHandler: manages the audio-related functionality of the game.</li>
  <li>Game: Where the game loop and loading resource reside.</li>
</ul>
<em>All the files are .cs.</em>
<h2>How to play?</h2>
<ul>
  <li>Use arrow keys for move</li>
  <li>Avoid obstacles</li>
  <li>Collect coins</li>
  <li>Buy skills (press 1, 2, or 3 to use the according skills).</li>
</ul>
<em>There is also an instruction in-game.</em>
<h2>Dependencies</h2>
SplashKit: Click <a href="https://splashkit.io/installation/">here</a> to see the guide for installing.
<h2>Basic Build Instruction</h2>
<ol>
  <li>Clone the repository.</li>
  <li>Put the directories in <em>/resource</em> to bin/Debug/YOUR-BUILD-FOLDER.</li>
  <li>Use Visual Studio to build and run the game.</li>
</ol>
<em>If you just want to play the game, feel free to check out the link attached in the About to download the exe file.</em>
<h2>Credits</h2>
<ol>
  <li>Art: <a href="https://tmd-studios.itch.io/cars">Cars</a> by TMD Studios. As asked, also check out <a href="https://tmdstudios.wordpress.com/">their website</a></li>
  <li>Music: <a href="https://www.youtube.com/watch?v=AfEEXuWObD8">Tchaikovsky - The Nutcracker Suite 'March' REMIX</a> by TPRMX</li>
</ol>
