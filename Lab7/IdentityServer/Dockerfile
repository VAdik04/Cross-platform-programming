FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy
COPY IdentityServer IdentityServer
WORKDIR IdentityServer
RUN dotnet publish -c Release -o Release
WORKDIR Release
ENTRYPOINT ["dotnet", "IdentityServer.dll", "--urls", "http://0.0.0.0:5001"]