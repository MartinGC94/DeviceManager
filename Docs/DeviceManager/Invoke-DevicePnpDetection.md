---
document type: cmdlet
external help file: DeviceManager.dll-Help.xml
HelpUri: ''
Locale: da-DK
Module Name: DeviceManager
ms.date: 04-02-2026
PlatyPS schema version: 2024-05-01
title: Invoke-DevicePnpDetection
---

# Invoke-DevicePnpDetection

## SYNOPSIS

Scans for new devices, similar to what Device Manager does when clicking the "Scan for hardware changes" button.

## SYNTAX

### __AllParameterSets

```
Invoke-DevicePnpDetection [-DevicePath <string>] [-Async] [<CommonParameters>]
```

## ALIASES

This cmdlet has the following aliases,
  None

## DESCRIPTION

Scans for new devices, similar to what Device Manager does when clicking the "Scan for hardware changes" button.  
By default it scans from the root, but it can also be done for devices under a specific tree (for example a USB hub).

## EXAMPLES

### Example 1
Invoke-DevicePnpDetection
Scans for new PNP devices

### Example 2
Invoke-DevicePnpDetection -DevicePath 'USB\ROOT_HUB30\5&A4D35AA&0&0'
Scans for new devices under a USB hub.

## PARAMETERS

### -Async

Specifies that the command should return as soon as the scan has started, rather than waiting for the scan to finish.

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

### -DevicePath

Specifies an optional path to a device where the scan should start.  
Can be used to reduce the scan time if you know a parent device where a currently undetected device is located.

```yaml
Type: System.String
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: true
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

Nothing.