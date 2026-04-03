[ValidateNotNull()]
$SolutionFile = Get-ChildItem -Path $PSScriptRoot -Filter *.slnx -File | Select-Object -First 1
MSBuild.exe $SolutionFile.FullName