
# File System Discord Rich Presence Client

## About

FSDiscordRPC is a cool program that **allows to update Discord's Rich Presence using a file**.
This file **can be updated real-time** by the **user or other applications** to change the user's activity on Discord.

I'd like to point out that this is my first C# Console App so even if it's a simple concept I may screw up some stuff here and there.

## Why use this?

**This was made with Game Mods in mind**:
For security reasons some mod loaders don't include networking in their scripting languages,
so if a mod wants to add Rich Presence to a game it needs to have an external dll loaded by the game.
The dll interacts with Discord and this may cause the game to crash or other unwanted behaviour.
With the help of this Application the mod just needs to be able to write to a single json
(Other formats may be supported in the future) file (which can also be a config).

## Projects that use this

 - [Cyberpunk2077RPC](https://github.com/Marco4413/Cyberpunk2077RPC)
