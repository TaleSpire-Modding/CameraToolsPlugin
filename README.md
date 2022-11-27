# Camera Tools Plugin
Camera tools Plugin allows you to tilt the camera at more extreme angles from looking straight down to straight up.
Configuration is also provided to allow the user to alter their tilt range for the camera.

## Install

Go to the releases folder and download the latest and extract to the contents of your TaleSpire game folder.

## Usage
1. You can modify the config to adjust freedom of tilt for your camera
2. Rendering a map in Ortho Graphics:
- Whilst in a map first press "P". An input box will be asking for a Vector4 in the following format `x1,y1,x2,y2` e.g. `0,0,10,10` This will configure the view being rendered.
- Now in OrtoMode by pressing f6 and going into orto mode, you can press "U" to render the map. This will take longer bigger the 2 points are.
- You will find your screenshot in your steamapps e.g. `C:\Program Files (x86)\Steam\steamapps\common\TaleSpire\TaleSpire_Data\Photos`. This is with the rest of your normally aquired screenshots.
- The maps are currently rendered at 75 pixels per tile and due to BR rendering pipeline, it has a FOV of 2 meaning some artifacting will occur at the edge. Best way to avoid this is to render more of the map and crop.
 
Planned update of the experimental ortho feature will occur for better experience and configurability.

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
- 3.2.1: Fixed util file for Cyberpunk Update
- 3.2.0: disabled the ortho renderer shortcut, can be re-enabled in config. Now supports shortcut instead of keycode (may need to delete config file if duplicate entries are showing up).
- 3.1.2: Fixed dependency issue
- 3.1.1: Move render to Update and add config to keybinds
- 3.1.0: Experimental Mass render for map in "ortho" mode
- 3.0.1: Documentation
- 3.0.0: Complete re-write by HolloFox due to breaking in Jan 2022
- 2.3.0: Deployed to ThunderStore