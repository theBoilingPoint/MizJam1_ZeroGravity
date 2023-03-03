# Miz Jam 1: Zero Gravity
Hi, this is __Xinran Tao__. Thank you very much for downloading this project! This projected was submitted on 05/09/2020.

## Check it out!
The game is published on [itch.io](https://theboilingpoint.itch.io/zero-gravity). You can play it with a browser on your PC. I've also written a blog post about the development process. You can check it out [here](https://www.xinrantao.com/project/zero-gravity).

## The Structure of the Scripts
```
├─Player
│  ├─PlayerAction.cs
│  ├─PlayerController.cs
│  ├─PlayerSensor.cs
│  └─PlayerState.cs
└─System
    ├─Audio
    │  └─AudioManager.cs
    ├─Effect
    │  ├─CameraEffect
    │  │  └─CameraShake.cs
    │  └─MaterialEffect
    │      ├─BlinkEffect.cs
    │      └─DissolveEffect.cs
    ├─Map
    │  ├─BallController.cs
    │  ├─DeathController.cs
    │  ├─HealthIncreaseController.cs
    │  ├─MapGeneration.cs
    │  └─ObstacleController.cs
    └─UI
        ├─HealthBar.cs
        ├─HighestScoreController.cs
        ├─MainMenu.cs
        ├─ManaBar.cs
        ├─MouseCursor.cs
        └─ScoreController.cs
```

## How to Build the Project
Please make sure you use Unity [2019.4.9f1](https://unity.com/releases/editor/whats-new/2019.4.9) release to import the package. 
