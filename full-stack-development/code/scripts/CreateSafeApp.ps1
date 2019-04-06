## need dotnet sdk 2.2, node.js, yarn, 

dotnet tool install -g fake-cli

dotnet new -i SAFE.Template

dotnet new SAFE

fake build --target run