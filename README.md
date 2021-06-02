# PhasmoScreenshotSaver
A simple program that watches for Phasmophobia camera pictures in the game folder and copies them to a folder of your choosing for safe keeping and later review.

### Directions
[Install program](https://jupiter.waggz.rocks/phasmoapp/) (Note: Windows will complain about running this program, it is not signed but the source code is here for you to compile and run for yourself if you wish!)

Upon program startup it will load the previously configured directories. If not found, it will try to determine the Phasmophobia installation directory via the registry's uninstaller installation directory value, and the "Pictures" windows library folder as defaults. 
Choose your directories and hit 'Start'. The program minimizes to the system tray and will check for *.png files in the install directory that are saved, and then copy those files over to the save directory with a timestamped file name. 

### TODO

 - [ ] Add option for windows startup
 - [ ] Add options for removing old files either by age or number or both
 - [ ] Expand options to include a choice for filter to make the program more useful outside of this nice usage

This program is only tested in Windows 10. Open an issue if you find one or have further suggestions not included in the TODO.

I am not a professional developer so I am sure there are much better ways to do what I have done here, but it's simple and it works, which is what my goal was. 😉

Special thanks to [@benbhall](https://github.com/benbhall) for [his class](https://github.com/benbhall/FileSystemWatcherMemoryCache/blob/main/FileSystemWatcherMemoryCache/SimpleBlockAndDelayExample.cs) to work around the annoyances of the filesystemwatcher events. 

Icon is sourced from [here](https://icon-icons.com/icon/camera-image-photo-photography-shot/107774) and is labeled as free for commercial use.
