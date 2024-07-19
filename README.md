# Camera Tools Plugin
[![Nuget Published](https://github.com/TaleSpire-Modding/CameraToolsPlugin/actions/workflows/release.yml/badge.svg)](https://github.com/TaleSpire-Modding/CameraToolsPlugin/actions/workflows/release.yml)

Camera tools Plugin allows you to tilt the camera at more extreme angles from looking straight down to straight up.
Configuration is also provided to allow the user to alter their tilt range for the camera.

## Install

Go to the releases folder and download the latest and extract to the contents of your TaleSpire game folder.

## Usage
1. You can modify the config to adjust freedom of tilt for your camera

## How to Compile / Modify

Open ```CameraToolsPlugin.sln``` in Visual Studio.

Build the project.

Browse to the newly created ```bin/Debug``` or ```bin/Release``` folders and copy the ```CameraToolsPlugin.dll``` to ```Steam\steamapps\common\TaleSpire\BepInEx\plugins```

## Changelog
```
- 3.3.3: Fully remove legacy Ortho renderer code.
- 3.3.2: Seats fix
- 3.3.1: Fix UI Bug
- 3.3.0: net48 upgrade
- 3.2.1: Fixed util file for Cyberpunk Update
- 3.2.0: disabled the ortho renderer shortcut, can be re-enabled in config. Now supports shortcut instead of keycode (may need to delete config file if duplicate entries are showing up).
- 3.1.2: Fixed dependency issue
- 3.1.1: Move render to Update and add config to keybinds
- 3.1.0: Experimental Mass render for map in "ortho" mode
- 3.0.1: Documentation
- 3.0.0: Complete re-write by HolloFox due to breaking in Jan 2022
- 2.3.0: Deployed to ThunderStore
```
