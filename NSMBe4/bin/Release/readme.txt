Treeki's New Super Mario Bros editor
Version 4.7 - released 29th June 2009
http://jul.rustedlogic.net/thread.php?id=244&page=15
----------
Thanks for downloading my new editor.
This editor will allow you to modify levels and the file system in New Super Mario Bros.
It supports editing objects, enemies/sprites and entrances, with more to come soon.

----------
Changelog:

v4.0 - 13th April 2009:
- Editor released.
v4.1 - 13th April 2009:
- Now supports different region ROMs.
v4.2 - 19th April 2009:
- Fixed a bug with different region ROMs.
- Sprites are now listed and choosable. Some of them may be inaccurate; please inform me if so. Many thanks to Pirahnaplant for the list!
v4.3 - 26th April 2009:
- The biggest update yet!
- Entrance/exit editing is now supported.
- Views are viewable (sorry for the bad pun) but can't be edited yet.
- Multi-language support is added; Spanish is available.
- Level settings changeable, including tilesets and backgrounds.
- Right-click dragging to move the level field around added.
v4.4 - 13th May 2009:
- New option allowing levels which wrap horizontally and loop.
- Alternate version included which works on Mono, for Linux and Mac OS.
v4.5 - 23rd May 2009:
- Data finder added to the Settings tab. This tool makes it easier for advanced ROM hackers to find out more data about NSMB.
- Sprite sets are now choosable, so you can choose which sprites work in a level. Want to put Bowser in 1-1? Go for it, but don't complain when the game crashes halfway through the battle ;)
- Mono graphics support fixed.
v4.6 - 14th June 2009:
- All levels should now work without crashing.
- Level files can now be edited in a hex editor without a separate program.
- Added options to the Options menu to delete all sprites and objects.
v4.7 - 29th June 2009:
- LZ compression is fixed.
- Object definitions updated.
- Dragging an object with the Shift key held will now allow you to resize it.
- Clicking and/or dragging an object with the Ctrl key held will create an exact clone of it.

----------
FAQ:

Q: How do I edit levels?
A: Open a ROM, pick a level and move/change stuff. If you can't figure that out you shouldn't be ROM hacking in the first place.

Q: Sprite data? What's this?
A: Some enemies/sprites have extra settings which are set in here - Koopa colours, for example.
   This page has a list of the various pieces of sprite data which we've figured out so far: http://treeki.shacknet.nu/nsmbsprites.html

Q: Why are some sprites red and not blue?
A: Due to the DS's RAM limits, not all sprites can be used in all levels at once. The specific sets which are usable can be chosen under Sprite Sets in Level Configuration.
   The ones which are red are not available in the current sprite set.

Q: I changed the sprite sets, and now the level crashes on the "World 1-1" screen, why is this?
A: Some combinations can cause the game to crash from our experience - especially using the bosses.
   Try disabling sets (changing them to 0) until you find a combination that works.

Q: How do I change the tileset or background in a level?
A: Go to the Level Configuration dialog and choose what you want.

Q: How do I change entrances/exits?
A: Click on the Entrances button in the toolbar, then feel free to edit them! Note that the format can be somewhat confusing, so you may want to look at other levels or pre-existing entrances to see how it works.

Q: I made or moved a pipe, why won't it work?
A: Create an entrance for it.

Q: I made the level bigger, why won't it work?
A: Views aren't supported yet, because I don't know enough of the format. Therefore, for now you'll have to stick to the default level sizes. However, to help you, you can see the size of each view.

Q: I can't run it! halp!
A: Check to make sure you have the .NET Framework 2.0 installed. (If you're on Vista, you should already have it.) If not, download this: http://www.microsoft.com/downloads/details.aspx?displaylang=en&FamilyID=0856eacb-4362-4b0d-8edd-aab15c5e04f5

Q: I don't have Windows, can I use it?
A: As from version 4.4, it works on Mono: http://www.mono-project.com/Main_Page

Q: Where do I get a NSMB ROM?
A: http://www.google.com


----------
Credits:

Treeki- Main coding and design
Blackhole89- Object parsing/rendering code
Piranhaplant- Sprite list
Master01 - Spanish translation for the sprite list
Treeki, Piranhaplant, Madman200- Sprite data list
Icons- http://www.pinvoke.com