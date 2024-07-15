dotnet pack -p:PackageVersion=1.0.0 
dotnet nuget push src/main/bin/Release/erpc_core.1.0.0.nupkg -k <API_KEY> -s https://api.nuget.org/v3/index.json