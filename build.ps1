$root = "../../"
$outputFolder = "artifacts"
$outputContents = Join-Path $outputFolder "*.*"
$libraryFolder =  Join-Path $outputFolder "OISYS.Web"
$wwwrootFolder = Join-Path $libraryFolder "wwwroot"

Write-Output "Cleaning the artifacts folder..."
Remove-Item $outputContents -Recurse

Write-Output "Restoring nuget packages..."
dotnet restore

Write-Output "Building the application..."
$outputPath = Join-Path $root $libraryFolder
dotnet build -f netcoreapp1.1 -o $outputPath

Write-Output "Publishing the application..."
dotnet publish -f netcoreapp1.1 -o $outputPath

Write-Output "Copying the docker folder to artifacts..."
Copy-Item "docker/Dockerfile" $outputFolder