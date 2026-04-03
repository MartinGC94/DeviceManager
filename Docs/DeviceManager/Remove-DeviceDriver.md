---
document type: cmdlet
external help file: DeviceManager.dll-Help.xml
HelpUri: ''
Locale: da-DK
Module Name: DeviceManager
ms.date: 04-03-2026
PlatyPS schema version: 2024-05-01
title: Remove-DeviceDriver
---

# Remove-DeviceDriver

## SYNOPSIS

Removes the specified driver from any device where it is installed and deletes it from the driver store.

## SYNTAX

### Driver

```
Remove-DeviceDriver -Driver <DeviceDriver> [-KeepInf] [-ShowUI] [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

### Path

```
Remove-DeviceDriver -Path <string[]> [-KeepInf] [-ShowUI] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### LiteralPath

```
Remove-DeviceDriver -LiteralPath <string[]> [-KeepInf] [-ShowUI] [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

## ALIASES

This cmdlet has the following aliases,
  None


## DESCRIPTION

This command removes the specified driver from any device where it is installed and deletes it from the driver store.
If another compatible driver package exists in the driver store, then it will be installed on the affected devices, otherwise they will be left with no driver installed.
The KeepInf parameter can be specified to prevent deletion from the driver store.

## EXAMPLES

### Example 1

Get-Device -DeviceClass Display | Get-DeviceDriver | Remove-DeviceDriver
Removes the display driver for all installed GPUs.

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

### -Driver

A driver object that represents the driver to remove.
Note that even though a driver object is device specific, the command will remove the driver package from any device that uses the same driver.

```yaml
Type: MartinGC94.DeviceManager.API.DeviceDriver
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: Driver
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -KeepInf

Specifies that the driver package should be uninstalled, but not removed from the driver store.
Note that some drivers may end up getting removed from the driver store regardless if this flag is set or not.
Ultimately it is up to the internal API to determine whether or not this flag works or not.

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

### -LiteralPath

Specifies the path(s) to the drivers that should be removed.
The paths should resolve to .inf driver installation files typically located in C:\Windows\INF\oemXX.inf
System/inbox drivers cannot be removed by this command, though it will still uninstall them from devices that use them.
This does not expand wildcards.

```yaml
Type: System.String[]
DefaultValue: ''
SupportsWildcards: false
Aliases:
- FullName
ParameterSets:
- Name: LiteralPath
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Path

Specifies the path(s) to the drivers that should be removed.
The paths should resolve to .inf driver installation files typically located in C:\Windows\INF\oemXX.inf
System/inbox drivers cannot be removed by this command, though it will still uninstall them from devices that use them.
This will expand wildcard characters like * ? and [].

```yaml
Type: System.String[]
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: Path
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
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

Nothing