# DeviceManager
This PowerShell module can be used to manage devices and drivers on Windows, just like the system utility with the same name does.  
It is built on the Device and driver APIs found here: https://learn.microsoft.com/en-us/windows/win32/api/_devinst/ (Mainly setupapi).

# Getting started
Install the module from the PowerShell gallery: `Install-Module DeviceManager`  
Then check the available commands in the module: `Get-Command -Module DeviceManager`  
Here's a few example of what can be done with this module:
## List the drivers that are in use for all PCI network adapters
```powershell

Get-Device -DeviceClass Net -EnumeratorId PCI | Get-DeviceDriver
```
## Scan a folder for drivers and install the best one for the hardware, but only if it's better than what is currently installed
```powershell
Get-Device |
    Where Name -EQ "Realtek PCIe 5GbE Family Controller" |
    Get-DeviceDriver -Path C:\realtek_pcielan_w11\ -Recurse -IncludeAvailableDrivers |
    Sort |
    Select -First 1 |
    Where IsInstalledDriver -ne $true |
    Install-DeviceDriver
```
Breaking down the previous pipeline:
1. We find the device we want to update.
2. We scan for compatible drivers in a particular folder, plus drivers already installed in the driverstore due to the `-IncludeAvailableDrivers` parameter.
3. We sort all the found drivers using the default sorter which ranks drivers the same way Windows would (Lowest rank, then most recent date, then highest version).
4. We select the first driver.
5. We exclude the driver if it's already in use, otherwise we pass it along to `Install-DeviceDriver` for installation.

## Find a specific driver in the driverstore and force install it on an incompatible device
```powershell
$Driver = Get-DeviceDriver -DeviceClass Net | Where Description -EQ 'Intel(R) Ethernet Connection I218-LM'
$Device = Get-Device -EnumeratorId PCI | Where Name -EQ "Ethernet Controller"
Install-DeviceDriver -Driver $Driver -Device $Device
```
In this case Windows has not installed a driver in the driverstore on the device because the .inf file has marked it as incompatible but we know the driver can be force installed anyway.
We find the driver from the global class driver list, find the device, and then install it on the device with `Install-DeviceDriver`.

## Disable all USB network adapters
```powershell
Get-Device -DeviceClass Net -EnumeratorId USB | Disable-Device
```