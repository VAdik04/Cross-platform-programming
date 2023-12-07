FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy
COPY App App
COPY Labs Labs
WORKDIR App
RUN dotnet publish -c Release -o Release
WORKDIR Release
ENTRYPOINT ["dotnet", "App.dll", "--urls", "http://0.0.0.0:5002"]