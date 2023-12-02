$ProgressPreference = "SilentlyContinue"
iwr -Uri https://download.visualstudio.microsoft.com/download/pr/93961dfb-d1e0-49c8-9230-abcba1ebab5a/811ed1eb63d7652325727720edda26a8/dotnet-sdk-8.0.100-win-x64.exe `
    -OutFile dotnet.exe
start dotnet.exe -ArgumentList "/quiet", "/norestart" -Wait