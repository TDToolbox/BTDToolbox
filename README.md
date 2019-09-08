# BTD Toolbox
a fully featured .jet editor

![Toolbox logo](https://media.discordapp.net/attachments/231001909442379776/620116877515161611/ReadmeBanner.png)

## Downloading
1. Get the latest release [here](https://github.com/TDToolbox/BTDToolbox/releases)
2. Extract the archive into an empty folder
3. Run ``BTDToolbox.exe``

## Usage
In this section youll learn how to use BTD Toolbox
### Creating/Opening a project
To create a project, click ``File -> New -> Project from .jet``
Or alternatively, if you already have a project, ``File -> Open -> Existing project``

### Creating a mod
Now you should see a new "JetViewer" window. In here is where youll find all of the .json files for the game.
You can use ``CTRL+F`` to search for folders in the JetViewer's treeview.
Expand the project tree until you reach a file you need.
Double click on any .json file in the file view to open the json editor.
The json editor will check to make sure your json is valid.
if your json is valid, youll see this: ![Valid Json](https://media.discordapp.net/attachments/231001909442379776/620355121703813120/unknown.png)
otherwise, youll see this: ![Invalid Json](https://media.discordapp.net/attachments/231001909442379776/620355210640097280/unknown.png)

### Testing a mod
Awesome! You've changed the files and want to see the result, sweet!
To test, ensure you only have **ONE** JetViewer window open. This window's project will be compiled for the testing.
Now, simply click ``Launch`` on the main window's menu bar. ![Launch Button](https://media.discordapp.net/attachments/231001909442379776/620354734972338186/unknown.png)
Your mod will be compiled into a .jet and the game will be launched with your mod.
If you dont have a backup of your .jet, the program will save one in the ``Backups`` folder regardless if its a legitemate .jet or not.

### Exporting a mod
Want to share your mod? Soon there will be a patch launcher for multi-mod support and easy mod installation, but until then,
Click on "Save .jet" (CTRL+S) in the JetViewer window and then pick a spot to export the .jet
Give out your modded .jet to whomever and have them place it into their game's files.

## Debugging & Future features
There are a bunch of unfinished features inside, so expect bugs when working with those.
Here is a list of known not finished features as of 08/09/2019 (dd/mm/yyyy)
``File -> Open Recent``
``File -> Settings``
``Edit -> *``
``View -> Jet Explorer``
``Debug -> *`` (Anything under here is used for testing & development of the program, you shouldn't need to touch these)

## Help
Need help? You can ask for help [here](https://discord.gg/Yr2tYte), all of the developers for the program can be found here.