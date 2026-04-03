---
document type: cmdlet
external help file: DeviceManager.dll-Help.xml
HelpUri: ''
Locale: da-DK
Module Name: DeviceManager
ms.date: 04-03-2026
PlatyPS schema version: 2024-05-01
title: Get-DeviceDriver
---

# Get-DeviceDriver

## SYNOPSIS

Retrieves information about devices drivers installed on the PC and from .inf files.

## SYNTAX

### Default (Default)

```
Get-DeviceDriver [-Device <Device>] [-DeviceClass <string>] [-IncludeAvailableDrivers]
 [-AllClassDrivers] [<CommonParameters>]
```

### FromPath

```
Get-DeviceDriver -Path <string[]> [-Device <Device>] [-DeviceClass <string>] [-Recurse]
 [-IncludeAvailableDrivers] [-AllClassDrivers] [<CommonParameters>]
```

### FromLiteralPath

```
Get-DeviceDriver -LiteralPath <string[]> [-Device <Device>] [-DeviceClass <string>] [-Recurse]
 [-IncludeAvailableDrivers] [-AllClassDrivers] [<CommonParameters>]
```

### FromDeviceClass

```
Get-DeviceDriver -DeviceClass <string> [<CommonParameters>]
```

## ALIASES

This cmdlet has the following aliases,
  None


## DESCRIPTION

This command finds drivers on the machine and it has 4 distinct modes in which it can be used:
1: It can be used to find the driver currently active for a device.
2: It can list all the compatible drivers in the driverstore for a particular device.
3: It can list drivers from the global class driver list for a user specified device class.
4: It can list drivers from a folder/.inf file that are compatible with a user specified device.

## EXAMPLES

### Example 1

Get-Device -DeviceClass Display | select -First 1 | Get-DeviceDriver
Lists the currently active driver for a display adapter.

### Example 2

Get-Device -DeviceClass Display | select -First 1 | Get-DeviceDriver -IncludeAvailableDrivers
Lists the drivers in the driver store which are compatible with a display adapter.

### Example 3

Get-DeviceDriver -DeviceClass Display
Lists all the displaydrivers installed in the driverstore.

### Example 4

Get-Device | Where Name -EQ "Realtek PCIe 5GbE Family Controller" | Get-DeviceDriver -Path C:\realtek_pcielan_w11\ -Recurse
Finds drivers in the specified folder that are compatible with the specified device.

## PARAMETERS

### -AllClassDrivers

Specifies whether or not drivers who are of the same device class as the specified device should be included.
This can be used to find drivers that don't explicitly list the device as being compatible, but might be compatible anyway.

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: Default
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: FromPath
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: FromLiteralPath
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

The devices to find drivers for.

```yaml
Type: MartinGC94.DeviceManager.API.Device
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: Default
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: FromPath
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: FromLiteralPath
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -DeviceClass

Specifies which kind of drivers to find from the driverstore.

```yaml
Type: System.String
DefaultValue: ''
SupportsWildcards: false
Aliases:
- Class
ParameterSets:
- Name: Default
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: FromPath
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: FromLiteralPath
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -IncludeAvailableDrivers

Specifies whether or not drivers from the driverstore should be included in the results.

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: Default
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: FromPath
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: FromLiteralPath
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

Specifies the path(s) to find drivers from.
This does not expand wildcards.

```yaml
Type: System.String[]
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: FromLiteralPath
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Path

Specifies the path(s) to find drivers from.
This will expand wildcard characters like * ? and [].

```yaml
Type: System.String[]
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: FromPath
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Recurse

Specifies whether or not to search recursively in the specified path(s) to find drivers.

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: FromPath
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: FromLiteralPath
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

## INPUTS

### MartinGC94.DeviceManager.API.Device

The device to find drivers for. Use Get-Device to find one.

## OUTPUTS

### MartinGC94.DeviceManager.API.DeviceDriver

A driver object bound to a specific device, or a general purpose global driver object.