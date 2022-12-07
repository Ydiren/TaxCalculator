
function IsMySqlDockerContainerAlreadyRunning(){
    $matches = (docker ps 2>&1 | Select-String -Pattern "tax-calculator-db")
    $result = $matches.Matches.Count -gt 0;
    return $result;
}

function IsMySQLInitialised(){
    $matches = (docker logs tax-calculator-db 2>&1 | Select-String -Pattern "MySQL init process done")
    $result = $matches.Matches.Count -gt 0;
    return $result;
}

Write-Host "Prompting user to trust development ASP.NET SSL certificate"
dotnet dev-certs https --trust

dotnet publish -c release --no-self-contained ./TaxCalculator.sln

if (IsMySqlDockerContainerAlreadyRunning) {
    Write-Host "Database already running...";
} 
else
{
    Write-Host "Pulling MySQL docker image";
    docker run --name tax-calculator-db --rm -p 3306:3306 -e MYSQL_RANDOM_ROOT_PASSWORD=true -e MYSQL_DATABASE=tax-calculation -e MYSQL_USER=testuser -e MYSQL_PASSWORD=test1234 -d mysql:latest

    Write-Output "Waiting for database to start"
    $step = 0;
    while (-not (IsMySQLInitialised))
    {
        Start-Sleep -Milliseconds 1000;
        $step = $step + 1;
        Write-Progress -Activity "Waiting For database to initialise" -PercentComplete $step;
    }
}

dotnet ef database update -p ./TaxCalculator.Infrastructure/TaxCalculator.Infrastructure.csproj -s ./TaxCalculator.Api/TaxCalculator.Api.csproj

Set-Location ./TaxCalculator.Api/bin/Release/net6.0/publish

Start-ThreadJob -ScriptBlock {
    function IsWebServerAvailable(){
        try
        {
            $response = Invoke-WebRequest -Uri https://localhost:5001
            return $response.StatusCode -eq 200;
        }
        catch{
            return $false;
        }
    }
    Write-Host "Waiting for API to start"
    while (-not (IsWebServerAvailable))
    {
        Write-Host "Waiting for API to start..."
        Start-Sleep -Milliseconds 100
    }

    Start-Process "https://localhost:5001"
};

dotnet ./TaxCalculator.Api.dll

