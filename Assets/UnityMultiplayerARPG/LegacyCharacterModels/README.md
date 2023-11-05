# UnityMultiplayerARPG_LegacyCharacterModels
An old character model components which will be excluded from the CORE project, because it won't be developed to add new features but still being updated follow some CORE code changes.

There are following components:
- `AnimationCharacterModel`, Use legacy `Animation` component to play animations
- `AnimatorCharacterModel`, Use `Animator` component to play animations, it's using [AnimatorOverrideController](https://docs.unity3d.com/ScriptReference/AnimatorOverrideController.html) to setup animations based on equipped weapon and using skills, it's going to create animator override controller to do it at runtime.
- `AnimatorCharacterModel2D`, Use `Animator` component to play animations, it's using [AnimatorOverrideController](https://docs.unity3d.com/ScriptReference/AnimatorOverrideController.html) to setup animations based on equipped weapon and using skills, it's going to create animator override controller to do it at runtime.