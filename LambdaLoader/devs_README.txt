Thanks for taking the time to read this!

* This program is functional, but far from finished with the scrope I have in mind
* Map cycle generation as far as I know does not have bugs associated with it. All my issues are with the extra features the program offers.
* The first window you see when running the program is the Game Profile selector.
        - I want this program to support players who may have different Half-Life setups that they switch between and the Game Profile selector would make this easy on them. All settings would be saved to a separate profile folder
        - This feature is somewhat implemented, but not finished
* There seem to be some odd issues when launching a listen server. I have had a friend host servers launched from this program and when I would join matches the map files would sometimes be saved to valve_downloads/overviews instead of the normal /valve_downloads/maps and I can find no documentation on what this folder is and why it would be generated.




Game Profile Selector, very rough outline of how I would like it implemented

1. Lambda Launcher is run by the user
2. It scans \data\ for .llgp files (Lambda Launcher Game Profile) and populates a list in the initial window.
2a. The .llgp file is a text file that uses the name to determine the Game Profile name, the first line of contents would dictate which image is used for the window to show the user. This way profiles can stand out from one another visually aside from differing names
3. Whichever Game Profile is selected, a folder in \data\ has the same name as the file and it contains .arg, .cfg, and the file that stores favorited maps.