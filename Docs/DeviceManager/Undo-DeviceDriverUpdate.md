---
document type: cmdlet
external help file: DeviceManager.dll-Help.xml
HelpUri: ''
Locale: da-DK
Module Name: DeviceManager
ms.date: 04-02-2026
PlatyPS schema version: 2024-05-01
title: Undo-DeviceDriverUpdate
---

# Undo-DeviceDriverUpdate

## SYNOPSIS

Uninstalls the current installed driver for a device, and installs a previously installed driver from backup.

## SYNTAX

### __AllParameterSets

```
Undo-DeviceDriverUpdate [-Device] <Device> [-ShowUI] [-WhatIf] [-Confirm] [<CommonParameters>]
```

## ALIASES

This cmdlet has the following aliases,
  Rollback-DeviceDriver

## DESCRIPTION

This command can be used to trigger the driver rollback feature in Windows.  
When a new driver is installed, the previous driver is kept for backup and can be rolled back to if needed.  
Windows only keeps 1 driver for backup.  
If the driver that is being rolled back is not in use on other devices, and it's not an inbox/system driver then it will also be removed from the driver store.


## EXAMPLES

### Example 1
Get-Device -Class Display | where Name -Like "*RTX 5070" | Undo-DeviceDriverUpdate
Rolls back to the previously installed driver for any RTX 5070 GPUs in the system.

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

Specifies a device to rollback the driver update for.

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
For this command it will show a confirmation box, and if needed, a reboot prompt in the UI.

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