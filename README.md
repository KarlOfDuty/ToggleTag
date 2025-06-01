# ToggleTag [![Build Status](https://jenkins.karlofduty.com/job/NWAPI/job/ToggleTag/job/master/badge/icon)](https://jenkins.karlofduty.com/blue/organizations/jenkins/NWAPI%2FToggleTag/activity) [![Release](https://img.shields.io/github/release/KarlofDuty/ToggleTag.svg)](https://github.com/KarlOfDuty/ToggleTag/releases) [![Downloads](https://img.shields.io/github/downloads/KarlOfDuty/ToggleTag/total.svg)](https://github.com/KarlOfDuty/ToggleTag/releases) [![Discord Server](https://img.shields.io/discord/430468637183442945.svg?label=discord)](https://discord.gg/C5qMvkj)


## No longer maintained, I stopped using it myself and I don't know if anyone else really used it.

An SCP:SL NWAPI plugin which lets you persistently toggle your staff tag using the default `hidetag` and `showtag` commands. You have to use the RA console, not the local one.

## Installation

### Using package manager

Use `p install KarlOfDuty/ToggleTag` in your server console and restart it.

### Manual
Place the plugin and dependencies directory in your server's plugin directory.

The plugin has no config, it starts automatically.

## Commands

`console_hidetag <userid>`

`console_showtag <userid>`

These are not meant to be used directly, they just exist for SCPDiscord integration. You can instead just use the default `hidetag` and `showtag` commands.
