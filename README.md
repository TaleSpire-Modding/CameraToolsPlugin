# Camera Tools Plugin
Camera tools Plugin allows you to tilt the camera at more extreme angles from looking straight down to straight up.
Configuration is also provided to allow the user to alter their tilt range for the camera.

## Install

Go to the releases folder and download the latest and extract to the contents of your TaleSpire game folder.

## Usage

Just install, this will automatically update TaleSpire so the devs know you are doing modding work.

## How to Compile / Modify

Open ```CameraToolsPlugin.sln``` in Visual Studio.

You will need to add references to:

```
* BepInEx.dll  (Download from the BepInEx project.)
* Bouncyrock.TaleSpire.Runtime (found in Steam\steamapps\common\TaleSpire\TaleSpire_Data\Managed)
* UnityEngine.dll
* UnityEngine.CoreModule.dll
* UnityEngine.InputLegacyModule.dll 
* UnityEngine.UI
* Unity.TextMeshPro
```

Build the project.

Browse to the newly created ```bin/Debug``` or ```bin/Release``` folders and copy the ```CameraToolsPlugin.dll``` to ```Steam\steamapps\common\TaleSpire\BepInEx\plugins```

## Changelog
- 3.0.1: Documentation
- 3.0.0: Complete re-write by HolloFox due to breaking in Jan 2022
- 2.3.0: Deployed to ThunderStore