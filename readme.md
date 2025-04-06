 #DevTools  
## PTA Plugin Loader | PTA Bundle Toolkit

A small project aiming to improve some of the constraints and hassles of developing MODS for MelonLoader.

---

### Goals

- Incorporate the Unity Editor during the development process of a mod.
- Bring a wide range of content into a game using Asset Bundles.
- Automatically incorporate custom classes into the game's DLL.


### What would be great

- Having more control over plugins initialization.
- Cut down a lot of "IDLE time" by enabling plugin hot reloads.
- Storing user's preferences using the Melon Loader API.
- Automatically create needed directories.
---

## Getting Started

### Before we start
>[!CAUTION] ⚠️
> - **This set of tools is a proof of concept. Don’t expect long-term maintenance.**  
> - **These tools edit some of the original game's DLL files.** This can result in your account **getting banned**, your **computer's security being at risk**, or cyber-monkeys knocking on your walls.  
> - **I'm not responsible for any violations you may commit using these tools regarding any game/software EULAs.**

---

### Setting up the environment

- Set up a fresh install of **MelonLoader** in your game's folder. Here's the [official guide](https://melonwiki.xyz/#/?id=requirements) to install MelonLoader.  
- Run your game once to initialize the MelonLoader environment.  
- Download and install the latest version of **[Visual Studio Community](https://visualstudio.microsoft.com/vs/community/)** and set it up for C# and Unity development. Here's the [official guide](https://learn.microsoft.com/en-us/visualstudio/gamedev/unity/get-started/getting-started-with-visual-studio-tools-for-unity).  
- Download and install the **Unity Editor** corresponding to the version used to build the game. You can find the correct Unity version in `..(GameFolder)/MelonLoader/latest.log`. Here's the [Unity version archive](https://unity.com/releases/editor/archive).  
- Download and unpack the **[latest release](https://github.com/PTac-h/PTABundleToolkit/releases)** of this project anywhere you want.

---

### Building the Loader mod

- Open the Visual Studio project located at `../PTAPluginLoader/PTAPluginLoader.sln`.  
- Select the version of the .NET runtime corresponding to your game. You’ll find it in `..(GameFolder)/MelonLoader/latest.log`.

> ⚠️ **WARNING**  
> You WILL have a lot of errors regarding missing assemblies. This is normal.  
> We'll import the required assemblies next.

- Right-click on `References` in your project explorer and select `Add Reference...`.  
- In the `Reference Manager` window, click the `Browse` button in the bottom-right corner.  
- Import the necessary assemblies using the following list:

    | Assembly Location  |
    | ------------------ |
    | `..(GameFolder)/MelonLoader/net35/MelonLoader.dll` |
    | `..(GameFolder)/(GameName)_Data/Managed/Assembly-CSharp.dll` |
    | `..(GameFolder)/(GameName)_Data/Managed/UnityEngine.dll` |
    | `..(GameFolder)/(GameName)_Data/Managed/UnityEngine.AssetBundleModule.dll` |
    | `..(GameFolder)/(GameName)_Data/Managed/UnityEngine.CoreModule.dll` |
    | `..(GameFolder)/(GameName)_Data/Managed/UnityEngine.JSONSerializeModule.dll` |

- Once imported, the errors should disappear.  
- Try building the mod by clicking on `Build > Build Solution` or pressing `CTRL + SHIFT + B`.  
- If your build is successful, move the file `../PTAPluginLoader/bin/Debug/PTAPluginLoader.dll` to `..(GameFolder)/Mods/PTAPluginLoader.dll`.  
- Run the game. A terminal window should pop up and display the MelonLogger output. If you see this message, you’re good to go:

    ```
    [12:38:19.259] ------------------------------
    [12:38:19.259] PTAc-h Plugin Loader v0.3
    [12:38:19.259] by PTac-h
    [12:38:19.260] Assembly: PTAPluginLoader.dll
    [12:38:19.260] ------------------------------
    [12:38:19.261] ------------------------------
    [12:38:19.261] 1 Mod loaded.
    ```

---

### Importing Unity tools

- Create a new Unity project **matching your game's [Render Pipeline](https://docs.unity3d.com/Manual/render-pipelines.html)** and name it however you like.  
- Once loaded, right-click in the project explorer and select `Import Package > Custom Package...`.  
- Import `../PTABundleToolkit/PTAAssetBundleToolkit.unitypackage`.  
- Let Unity compile the scripts.  
- The tool should now be available at `Menu > Assets > PTA Bundle Toolkit`.

You're now ready to create custom plugins inside Unity.

---

### Plugin Example

- In your project, right-click in the Scene Explorer and select `UI > Panel`.

![Capture d'écran 2025-04-06 132141](https://github.com/user-attachments/assets/dbba8baf-dfcb-4ea9-a9ed-57a3670a651b)

- Here, I created a simple panel with the label "Hello World", then made it a prefab by dragging it from the Scene Viewer into the Project Explorer.

> ⚠️ **WARNING**  
> To create the label, I had to import TextMesh Pro.  
> The game **MUST** include TextMesh Pro as well, or you’ll run into issues.  
> This applies to **ANY** Editor dependencies/plugins.

![Capture d'écran 2025-04-06 132612](https://github.com/user-attachments/assets/804c1044-dd16-4a76-9042-5e8f75147e85)

- After renaming the prefab to `TestPlugin`, click the box next to `Asset Bundles` in the Inspector, hold it, then release and select `New...`.  
- Name your plugin (in my case, `MyPlugin`), then press Enter.

![Capture d'écran 2025-04-06 132653](https://github.com/user-attachments/assets/bc1e1f1a-4914-4ffb-a947-b704da0fa807)

- Your plugin is now ready to export.  
- Open the Bundle Toolkit at `Menu > Assets > PTA Bundle Toolkit`.  
- If not already set, choose the plugin you want to export.  
- Click `Build`, and wait for the packing to finish.

![Capture d'écran 2025-04-06 132914](https://github.com/user-attachments/assets/91058353-48e4-491c-bc7d-e5a38ef7f1a4)

- You should now see 5 files in `Assets > PTA Bundle Toolkit > Generated Asset Bundle`. If not, right-click the folder and select `Refresh`.  
  - Note: Two extra files are auto-generated by Unity’s build pipeline. You can ignore them.

    | /Assets/PTA Bundle Toolkit/Generated Asset Bundle/ |
    | --------------------------------------------------- |
    | `Generated Asset Bundle` ← ignore |  
    | `Generated Asset Bundle.manifest` ← ignore |  
    | `myplugin` |  
    | `myplugin.json` |  
    | `myplugin.manifest` |

---

### And that's it!

- To use your freshly baked bundle, copy the files `myplugin`, `myplugin.json`, and `myplugin.manifest` to the folder `..(GameFolder)/PTA-Plugins`.  
- Run the game.  
- Enjoy 🎉

![Capture d'écran 2025-04-06 144916](https://github.com/user-attachments/assets/7b01c32b-a983-4d51-8982-be9daf6b00f2)

![Capture d'écran 2025-04-06 145303](https://github.com/user-attachments/assets/974eabfb-6afb-4683-8c61-ee3249693778)

![Capture d'écran 2025-04-06 145312](https://github.com/user-attachments/assets/f3ae2f89-b35d-45b4-90ff-8bf7d4a16d28)
