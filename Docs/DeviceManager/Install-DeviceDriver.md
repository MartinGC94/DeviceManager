---
document type: cmdlet
external help file: DeviceManager.dll-Help.xml
HelpUri: ''
Locale: da-DK
Module Name: DeviceManager
ms.date: 04-02-2026
PlatyPS schema version: 2024-05-01
title: Install-DeviceDriver
---

# Install-DeviceDriver

## SYNOPSIS

Installs device drivers.

## SYNTAX

### Default (Default)

```
Install-DeviceDriver [-Driver <DeviceDriver>] [-Device <Device>] [-InstallNullDriver] [-ShowUI]
 [-WhatIf] [-Confirm] [<CommonParameters>]
```

### FromPath

```
Install-DeviceDriver -Path <string[]> [-StageForNextBoot] [-ForceInstall] [-ShowUI] [-WhatIf]
 [-Confirm] [<CommonParameters>]
```

### WithHardwareIdAndPath

```
Install-DeviceDriver -Path <string[]> -HardwareId <string> [-ForceInstall] [-ShowUI] [-WhatIf]
 [-Confirm] [<CommonParameters>]
```

### FromLiteralPath

```
Install-DeviceDriver -LiteralPath <string[]> [-StageForNextBoot] [-ForceInstall] [-ShowUI] [-WhatIf]
 [-Confirm] [<CommonParameters>]
```

### WithHardwareIdAndLiteralPath

```
Install-DeviceDriver -LiteralPath <string[]> -HardwareId <string> [-ForceInstall] [-ShowUI]
 [-WhatIf] [-Confirm] [<CommonParameters>]
```

## ALIASES

This cmdlet has the following aliases,
  None

## DESCRIPTION

This command is used to install drivers on devices in the computer.  
It has 6 distinct modes in which it can be used:  
1: Install the specified driver on the specified device.  
2: Install a "Null" driver on a specified device.  
3: Stage a folder of .infs to be installed on next boot.  
4: Install a new driver on all devices matching a specific hardware ID.  
5: Install a new driver on all compatible devices.  
6: Install the best possible driver from the driver store on a specified device.

## EXAMPLES

### Example 1
Get-Device -DeviceClass Display | Get-DeviceDriver -IncludeAvailableDrivers | Where-Object DriverVersion -EQ 1.2.3.4 | Install-DeviceDriver
Finds a driver in the driverstore with a specific version and installs it on the specified device.

### Example 2
$Device = Get-Device -DeviceClass Display | select -First 1
Install-DeviceDriver -Device $Device -InstallNullDriver
Installs a null driver on the specified device.

### Example 3
Install-DeviceDriver -StageForNextBoot -Path C:\SP8Drivers
Adds the the drivers found in the specified folder to the driverstore, but prevents them from being available until the next boot.

### Example 4
Install-DeviceDriver -HardwareId 'PCI\VEN_10EC&DEV_8126&CC_0200' -Path C:\NicDrivers\Driver.inf
Installs the specified nic driver on devices with a matching hardware ID.

### Example 5
Install-DeviceDriver -Path C:\NicDrivers\Driver.inf
Installs the specified nic driver on all compatible devices.

### Example 6
$Device = Get-Device -DeviceClass Display | select -First 1
Install-DeviceDriver -Device $Device
Installs the best available driver from the driver store on the specified device.

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

The device to install the driver to.  
This parameter is optional and can be left out if the driver is already associated to a device.

```yaml
Type: MartinGC94.DeviceManager.API.Device
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
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Driver

The driver to install.  
This parameter is optional. If this parameter is not specified Windows will find the best driver to install from the driverstore.  
Alternatively, if the InstallNullDriver parameter has been specified then a null driver will be installed instead.

```yaml
Type: MartinGC94.DeviceManager.API.DeviceDriver
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
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ForceInstall

Forces a driver to be installed, even if a better driver has already been installed on the devices.

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
- Name: WithHardwareIdAndPath
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: WithHardwareIdAndLiteralPath
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -HardwareId

Specifies a hardwareId for devices where the specified driver should be installed.

```yaml
Type: System.String
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: WithHardwareIdAndPath
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
- Name: WithHardwareIdAndLiteralPath
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -InstallNullDriver

If this parameter is specified, Windows will try to install a null driver on the device so it runs in raw mode.  
If the device doesn't support raw mode, then the device will be left in an unconfigured state.

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
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -LiteralPath

Specifies the path(s) to find drivers from.  
The paths should resolve to .inf driver installation files, unless the StageForNextBoot parameter is set
in which case the path should resolve to a directory.
This does not expand wildcards.

```yaml
Type: System.String[]
DefaultValue: ''
SupportsWildcards: false
Aliases:
- FullName
ParameterSets:
- Name: FromLiteralPath
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
- Name: WithHardwareIdAndLiteralPath
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

Specifies the path(s) to find drivers from.  
The paths should resolve to .inf driver installation files, unless the StageForNextBoot parameter is set
in which case the path should resolve to a directory.
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
- Name: WithHardwareIdAndPath
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
If a device installer has post setup wizards they will be displayed.  
If this flag is set and the command is trying to find the best driver for a device and it fails, then the "Found New Hardware" wizard will show.

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

### -StageForNextBoot

Indicates that the drivers should be added to the driverstore and installed on next boot.  
If this flag is set then Path/LiteralPath should point to a directory, rather than .inf files.

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