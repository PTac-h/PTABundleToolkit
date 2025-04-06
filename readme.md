
 #DevTools
# PTA Plugin Loader | PTA Bundle Toolkit

A small project aiming to improve some of the constraints and hassles of developping MODS for MelonLoader.


### Goals

- Incorporate the Unity Editor during the development process of a mod.
- Bring a wide range of content into a game using Assets Bundles.
- Cut down a lot of "IDLE time" by enabling plugin hot reloads.
- Automatic incorporation of custom classes into the game's dll.

# Getting Started :

### Before we start
> [!CAUTION]
> - **These sets of tools are a proof of concept. Don't expect a long term maintenance.**
> - **These sets of tools edit some of the original game's DLL files**. This can result in your account **getting banned**, your **computer security being at risks**, or cyber-monkeys knocking on your walls.
> - **I'm not responsible for any violations you may commit using these set of tools regarding any game/software EULAs.**

#
### Setting up the environment
- Setup a fresh install of **Melon Loader** to your game's folder. Here's the [official guide](https://melonwiki.xyz/#/?id=requirements) to install Melon Loader.
- Run you game a first time to initialize the Melon Loader environment.
- Download and install the latest **[Visual Studio Community](https://visualstudio.microsoft.com/vs/community/)** Version and set it up for C# and Unity devlopment. Here's the [official guide](https://learn.microsoft.com/en-us/visualstudio/gamedev/unity/get-started/getting-started-with-visual-studio-tools-for-unity)
- Download and install the **unity editor** corresponding to the version used to build the game. You can find the right unity version in `..(GameFolder)/MelonLoader/latest.log`. Here's the [Unity's versions archives](https://unity.com/releases/editor/archive).
- Download and unpack the **[latest release](https://github.com/PTac-h/PTABundleToolkit/releases)** of this project anywhere you want.

#
### Building the Loader mod
- Open up the visual studio project found at `../PTAPluginLoader/PTAPluginLoader.sln`
- Select the version of the .NET runtime corresponding to your game. You can find it at `..(GameFolder)/MelonLoader/latest.log`.
#
> [!WARNING]
> You WILL have a lot of errors regarding missing Assemblies, this is normal.
> We will import needed Assemblies right after.
#
- Make a right click on `References` inside your project explorer. And Select `Add Reference...`
- In the `Reference Manager` window, click the `Browse` button at the bottom right.
- Imports needed Assemblies according to this list : 

    | Assembly Location  |
    | ------------- |
    | `..(GameFolder)/MelonLoader/net35/MelonLoader.dll` |
    | `..(GameFolder)/(GameName)_Data/Managed/Assembly-CSharp.dll` |
    | `..(GameFolder)/(GameName)_Data/Managed/UnityEngine.dll` |
    | `..(GameFolder)/(GameName)_Data/Managed/UnityEngine.AssetBundleModule.dll` |
    | `..(GameFolder)/(GameName)_Data/Managed/UnityEngine.CoreModule.dll` |
    | `..(GameFolder)/(GameName)_Data/Managed/UnityEngine.JSONSerializeModule.dll` |

- Once imported, errors should be gone.
- Try to build the mod by clicking on `Build > Build Solution` or doing `CTRL`+`SHIFT`+`B`.
- If your build is successfull, you can move `../PTAPluginLoader/bin/Debug/PTAPluginLoader.dll` to `..(GameFolder)/Mods/PTAPluginLoader.dll`.
- Try and run the game, a terminal window should pop up and output the Melon Logger messages. If you can see this message, you can move to the next step.
    ```
    [12:38:19.259] ------------------------------
    [12:38:19.259] PTAc-h Plugin Loader v0.3
    [12:38:19.259] by PTac-h
    [12:38:19.260] Assembly: PTAPluginLoader.dll
    [12:38:19.260] ------------------------------
    [12:38:19.261] ------------------------------
    [12:38:19.261] 1 Mod loaded.
    ```

#
### Importing Unity tools

- Create a new Unity project **matching your game's [Render Pipeline](https://docs.unity3d.com/Manual/render-pipelines.html)** and name it as you want.
- Once loaded, right click in the in the project explorer and select `Import Package > Custom Package...`.
- Import `../PTABundleToolkit/PTAAssetBundleToolkit.unitypackage`
- Give Unity some time to compile the scripts.
- The new tool should now be available at `Menu > Assets > PTA Bundle Toolkit`.

- You are now Ready to create cutoms Plugins inside unity.

#
### Plugin Example 

- In your project right click in the scene explorer and select `UI > Panel`
  
![Capture d'écran 2025-04-06 132141](https://github.com/user-attachments/assets/dbba8baf-dfcb-4ea9-a9ed-57a3670a651b)

#
- Here I've created a simple panel with the label "Hello World", then made it a prefab by drag/dropping it from the scene viewer into the project explorer.
> [!WARNING]
> Note that to create the label, I had to import TextMesh Pro.
> The game **NEED** to include TextMesh Pro as well, or you'll have some issues.
> This is true for **ANY** Editor Dependencies/Plugins.
> 
![Capture d'écran 2025-04-06 132612](https://github.com/user-attachments/assets/804c1044-dd16-4a76-9042-5e8f75147e85)

#
- After renaming the prefab to `TestPlugin` click and hold the box next to `Asset Bundles` in the Inspector Window, then release click on `New...`.
- Name your Plugin, in my case `MyPlugin`, then press enter.
  
![Capture d'écran 2025-04-06 132653](https://github.com/user-attachments/assets/bc1e1f1a-4914-4ffb-a947-b704da0fa807)

#
- Your plugin is now ready to be exported. Now open the Bundle toolkit at `Menu > Assets > PTA Bundle Toolkit`
- If not already set, choose the plugin you wanna export.
- Click build, and wait for the packing to process.

![Capture d'écran 2025-04-06 132914](https://github.com/user-attachments/assets/91058353-48e4-491c-bc7d-e5a38ef7f1a4)

#
- You should now have 5 files into `Assets > PTA Bundle Toolkit > Generated Asset Bundle`. If not, right click then `Refresh`
  - Note two extra files, these are auto generated by the unity build pipeline, you can ignore them.
    
    | /Assets/PTA Bundle Toolkit/Generated Asset Bundle/ |
    | ------------- |
    | `Generated Asset Bundle` <-- ignore| 
    | `Generated Asset Bundle.manifest` <-- ignore| 
    | `myplugin` |
    | `myplugin.json` |
    | `myplugin.manifest` |
#
### And that's it !

- To use your freshly baked bundle, simply copy the files `myplugin` `myplugin.json` and `myplugin.manifest` into the folder `..(GameFolder)/PTA-Plugins`
- Run the game.
- Enjoy.

![Capture d'écran 2025-04-06 144916](https://github.com/user-attachments/assets/7b01c32b-a983-4d51-8982-be9daf6b00f2)

![Capture d'écran 2025-04-06 145303](https://github.com/user-attachments/assets/974eabfb-6afb-4683-8c61-ee3249693778)

![Capture d'écran 2025-04-06 145312](https://github.com/user-attachments/assets/f3ae2f89-b35d-45b4-90ff-8bf7d4a16d28)



