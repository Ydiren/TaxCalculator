function launchBrowser() {
  
  echo "Waiting for API to start"
  until curl --head --silent --fail https://localhost:5001 1> /dev/null 2>&1; do
      sleep 1
  done
  
  open "https://localhost:5001"
}

dotnet publish -c release --no-self-contained ./TaxCalculator.sln

if  docker ps | grep -q tax-calculator-db;
  then
    echo "Database already running..."
  else
    echo "Pulling MySQL docker image"
    docker run --name tax-calculator-db --rm -p 3306:3306 -e MYSQL_RANDOM_ROOT_PASSWORD=true -e MYSQL_DATABASE=tax-calculation -e MYSQL_USER=testuser -e MYSQL_PASSWORD=test1234 -d mysql:latest
fi

dotnet ef database update -p ./TaxCalculator.Infrastructure/TaxCalculator.Infrastructure.csproj -s ./TaxCalculator.Api/TaxCalculator.Api.csproj 

cd ./TaxCalculator.Api/bin/Release/net6.0/publish

launchBrowser &
dotnet ./TaxCalculator.Api.dll
