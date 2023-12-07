FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy
COPY Api Api
WORKDIR Api
RUN dotnet publish -c Release -o Release
WORKDIR Release
ENTRYPOINT ["dotnet", "Api.dll", "--urls", "http://0.0.0.0:5003"]