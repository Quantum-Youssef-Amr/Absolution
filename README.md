# Absolution

Absolution is a gacha tower-defense Unity game where you protect an Era by deploying and promoting Heroes. The core twist: to summon new Heroes you must sacrifice your existing ones. This forces players to make meaningful, strategic decisions about short-term power vs long-term collection and progression.

## About
Absolution is a Unity project implementing a hybrid of tower defense and gacha-collection mechanics. You defend a central Era (a persistent base/monument) from waves of enemies by placing Heroes that act as towers. Heroes can be promoted and evolved, but obtaining new Heroes requires sacrificing (permanently consuming) one or more of your current Heroes. This creates high-stakes choices and emergent strategies.

## Core gameplay
- Place Heroes on the battlefield to defend the Era from incoming waves.
- Each Hero has unique abilities, range, and role (damage, support, control).
- Waves become progressively harder and may include special enemy types and bosses.
- Between waves you can promote Heroes, rearrange deployment, and perform gacha summons — at the cost of sacrificing Heroes you already own.

## Features
- Tower-defense style levels with enemy waves and boss encounters.
- Diverse roster of Heroes with distinct skills and synergies.
- Promotion system that strengthens Heroes (levels, star ranks, or evolution).
- Risk/reward gacha summoning that requires sacrificing owned Heroes to obtain new ones.
- Persistent Era health and meta-progression across runs/levels.
- Unity project structure ready to open in the Unity Editor.

## Requirements
- Unity Editor (see `ProjectSettings/ProjectVersion.txt` for exact version if present).
- Git (to clone the repository).
- Optional: Unity Hub for version and module management.

## Getting started
1. Clone the repository:
   ```
   git clone https://github.com/Quantum-Youssef-Amr/Absolution.git
   cd Absolution
   ```
2. Open the project in Unity:
   - Use Unity Hub > Add/Open and point to the cloned folder, or open the folder directly from Unity.
   - If a different Unity version is required, Unity Hub will prompt you to install it.
3. Wait for Unity to import assets and compile scripts.

## Controls
- Editor / PC (default):
  - Left-click: select/place a Hero or interact with UI.
  - Right-click: cancel placement / deselect.
- In-game UI will show additional buttons for Promotion, Sacrifice, and Summo

## Hero system & progression
- Heroes have stats: HP, attack, range, attack speed, and a unique skill.
- Promotion increases base stats and may unlock secondary effects.
- Evolution (higher-tier promotion) requires resources and/or sacrificed units depending on design.
- Team composition and synergy matter: some Heroes buff allies, others debuff enemies or control crowds.

## Gacha & sacrifice mechanic
- Summoning new Heroes uses a gacha-like system but instead of paying only currency, you must sacrifice one or more existing Heroes as the cost.
- Sacrificed Heroes are permanently consumed; plan sacrifices carefully.
- The system encourages tactical decisions:
  - Sacrifice low-value duplicates to chase a higher-tier hero.
  - Sacrifice a powerful unit to obtain a hero that fits needed synergy, accepting short-term loss for long-term gain.
- The summon results can be randomized with rarity tiers (Common, Rare, Epic, Legendary). Implementers can tune drop rates and sacrifice requirements.

## Tips
- Balance your roster: retain at least one reliable core unit while experimenting with sacrifices.
- Promote Heroes you expect to keep before sacrificing lower-tier duplicates.
- Adapt your sacrifices to the current level's enemy types and boss mechanics.

#
## Issues & troubleshooting
- If Unity refuses to open the project or scripts fail to compile:
  - Confirm the Unity Editor version.
  - Check the Unity Console for errors and fix script issues.
- If the Editor crashes, examine local Editor logs (not the committed binary crash dumps) and consider removing those blobs from source control.

## Credits
- Project maintained by the repository owner.
- Built with Unity — see [Unity](https://unity.com) for engine docs and downloads.

## License
No License — All rights reserved.

This repository does not include a license file. That means:
- All rights are reserved by the repository owner.
- You do not have permission to reuse, modify, distribute, or copy the project's contents unless you obtain explicit permission from the owner.
