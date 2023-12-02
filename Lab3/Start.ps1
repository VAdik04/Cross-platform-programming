cd Logics
dotnet pack -o ..\Repository
rm bin, obj -Recurse -Force
cd ..\App
dotnet build