# TaxCalculator

A simple service that calculates the monthly and annual tax for the given salary.

## Running the service
To start the service, just open a terminal and run the appropriate script<br/>
Powershell - `./run.ps1`<br/>
MacOS zsh - `chmod +x ./run.sh | ./run.sh`<br/>
The script assumes that a few prerequisites are in place
- Docker is installed
- dotnet-ef tool is installed globally
- NodeJS v >=14.0 is installed

### Script Details
The script performs a number of tasks
- Builds the service solution and publishes to the output directory
- Checks if the database container is already running
-- If not, pulls the latest MySQL docker image and creates the required database
- Applies the database migrations (which currently only seed the TaxBands table)
- Starts the service
- Waits for the service API endpoint to be available and then launches the SPA in the default browser
