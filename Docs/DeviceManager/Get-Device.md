---
document type: cmdlet
external help file: DeviceManager.dll-Help.xml
HelpUri: ''
Locale: da-DK
Module Name: DeviceManager
ms.date: 04-02-2026
PlatyPS schema version: 2024-05-01
title: Get-Device
---

# Get-Device

## SYNOPSIS

Retrieves devices on the computer.

## SYNTAX

### GetByEnumerator (Default)

```
Get-Device [-DeviceClass <string>] [-EnumeratorId <string>] [-IncludeNonPresent]
 [<CommonParameters>]
```

### GetByDeviceInstance

```
Get-Device -DeviceInstanceId <string> [-IncludeNonPresent] [<CommonParameters>]
```

## ALIASES

This cmdlet has the following aliases,
  None

## DESCRIPTION

Retrieves devices on the computer.  
The command can find devices by their device instance ID, Device Class and/or their enumerator (the connection type between the device and PC).  
It can also find devices that have previously been connected to the PC.

## EXAMPLES

### Example 1
Get-Device -DeviceClass Display
Find all display adapters on the computer.

### Example 2
Get-Device -DeviceInstanceId 'PCI\VEN_10DE&DEV_2F04&SUBSYS_F3261569&REV_A1\F650A1A7B92DB04800'
Find a specific device based on the device instance ID.

### Example 3
Get-Device -IncludeNonPresent -DeviceClass Net -EnumeratorId USB
Find all past and present USB network adapters

## PARAMETERS

### -DeviceClass

Specifies the kind of devices you want to find.  
Both device class GUIDs and names can be used as input.  
If an invalid name is provided, a validation error will be thrown.

```yaml
Type: System.String
DefaultValue: ''
SupportsWildcards: false
Aliases:
- Class
ParameterSets:
- Name: GetByEnumerator
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -DeviceInstanceId

Specifies the instance id of a particular device you want to find.

```yaml
Type: System.String
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: GetByDeviceInstance
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -EnumeratorId

Specifies the enumerator to find devices from.  
Windows groups devices based on how they are connected to the PC, for example USB devices use the USB enumerator  
while PCI(e) devices use the PCI enumerator.  
Both name and GUID forms are accepted as values.

```yaml
Type: System.String
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: GetByEnumerator
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -IncludeNonPresent

Specifies whether or not to include devices that are not currently present on the computer, eg. disconnected devices.

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: GetByDeviceInstance
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: GetByEnumerator
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

## OUTPUTS

### MartinGC94.DeviceManager.API.Device

A device object that can be passed along to other commands in this module.