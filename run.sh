
docker run --name tax-calculator-db --rm -p 3306:3306 -e MYSQL_RANDOM_ROOT_PASSWORD=true -e MYSQL_DATABASE=tax-calculation -e MYSQL_USER=testuser -e MYSQL_PASSWORD=test1234 -d mysql:latest

dotnet ef migrations add InitialCreate -c TaxDbContext -p ./TaxCalculator.Infrastructure/TaxCalculator.Infrastructure.csproj -s ../TaxCalculator.Api/TaxCalculator.Api.csproj

dotnet ef database update -p TaxCalculator.Infrastructure.csproj -s ../TaxCalculator.Api/TaxCalculator.Api.csproj 