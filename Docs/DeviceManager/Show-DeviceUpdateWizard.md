---
document type: cmdlet
external help file: DeviceManager.dll-Help.xml
HelpUri: ''
Locale: da-DK
Module Name: DeviceManager
ms.date: 04-02-2026
PlatyPS schema version: 2024-05-01
title: Show-DeviceUpdateWizard
---

# Show-DeviceUpdateWizard

## SYNOPSIS

Shows the device update wizard, allowing you to interactively select a driver to use for a specific device.

## SYNTAX

### __AllParameterSets

```
Show-DeviceUpdateWizard -Device <Device> [<CommonParameters>]
```

## ALIASES

This cmdlet has the following aliases,
  None

## DESCRIPTION

This command will trigger the same driver update wizard seen in Device Manager when selecting Update Driver.  
This can be used to interactively browse for a driver to install, even on Windows Server Core installations.  
The selected driver will be installed by the wizard. If it fails for whatever reason, the command will write an error.

## EXAMPLES

### Example 1
Get-Device | where Name -Like Ethernet* | Show-DeviceUpdateWizard
Shows the device update wizard for matching devices.

## PARAMETERS

### -Device

{{ Fill Device Description }}

```yaml
Type: MartinGC94.DeviceManager.API.Device
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### CommonParameters

This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable,
-InformationAction, -InformationVariable, -OutBuffer, -OutVariable, -PipelineVariable,
-ProgressAction, -Verbose, -WarningAction, and -WarningVariable. For more information, see
[about_CommonParameters](https://go.microsoft.com/fwlink/?LinkID=113216).

## OUTPUTS

Nothing.