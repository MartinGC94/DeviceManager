---
document type: cmdlet
external help file: DeviceManager.dll-Help.xml
HelpUri: ''
Locale: da-DK
Module Name: DeviceManager
ms.date: 04-03-2026
PlatyPS schema version: 2024-05-01
title: Remove-Device
---

# Remove-Device

## SYNOPSIS

Removes a device from the machine, the same way Device Manager would with the Uninstall Device option.

## SYNTAX

### __AllParameterSets

```
Remove-Device [-Device] <Device> [-ShowUI] [-WhatIf] [-Confirm] [<CommonParameters>]
```

## ALIASES

This cmdlet has the following aliases,
  None


## DESCRIPTION

Removes a device from the machine, the same way Device Manager would with the Uninstall Device option.
A device that has been removed can normally be detected again with Invoke-DevicePnpnDetection.
This can also be used to remove old devices entries for devices that have previously been connected to the PC.

## EXAMPLES

### Example 1

Get-Device -IncludeNonPresent -DeviceClass Monitor | where IsPresent -EQ $false | Remove-Device
Removes the device entries for old monitors that have previously been connected to the computer.

## PARAMETERS

### -Confirm

Prompts you for confirmation before running the cmdlet.

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: ''
SupportsWildcards: false
Aliases:
- cf
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Device

Specifies the device to remove.

```yaml
Type: MartinGC94.DeviceManager.API.Device
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ShowUI

Enables UI functionality for the underlying APIs.
This will generally be for restart prompts, which will show up in a separate window, rather than being a warning written to the PowerShell host.

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -WhatIf

Runs the command in a mode that only reports what would happen without performing the actions.

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: ''
SupportsWildcards: false
Aliases:
- wi
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
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