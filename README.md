# ToggleTag
An SCP:SL ServerMod plugin which lets you persistently toggle your staff tag using the default `hidetag` and `showtag` commands. You have to use the RA console, not the local one.

Requires you to enable the text based admin commands or use scpdiscord.

Now also persistently toggles overwatch.

Has a single config entry, `toggletag_global` which decides whether the plugin files are placed in the global or local config directory.

## Installation

Extract the included zip and place the contents in `sm_plugins`.

## Commands

`console_hidetag <steamid>`

`console_showtag <steamid>`

These are not meant to be used directly, they just exist for SCPDiscord integration. You can instead just use the default `hidetag` and `showtag` commands.

## Permissions

`toggletag.savetag` - Allows players to have their tag status saved, given by default.

`toggletag.saveoverwatch` - Allows players to have their overwatch status saved, given by default.
