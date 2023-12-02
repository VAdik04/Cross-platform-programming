iwr -Uri https://github.com/loic-sharma/BaGet/releases/download/v0.4.0-preview2/BaGet.zip `
    -OutFile BaGet.zip
Expand-Archive -Path BaGet.zip -DestinationPath BaGet
rm BaGet.zip
cd Baget
start dotnet -ArgumentList Baget.dll
cd ..\App
dotnet pack -o .
dotnet nuget push VBahrii.1.0.0.nupkg -s http://localhost:5000/v3/index.json
rm VBahrii.1.0.0.nupkg, bin, obj -Recurse -Force
cd ..\Labs
rm bin, obj -Recurse -Force
cd ..
vagrant up mac
vagrant halt mac
vagrant up linux
vagrant halt linux
vagrant up windows
vagrant halt windows