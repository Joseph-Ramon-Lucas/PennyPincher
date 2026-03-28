# Internal API Configuration Notes 
Before starting, make sure to set up the DB docker container located in `/Scripts/pennypincher-docker`

## Building and running your application

### Creating secrets for container connection
#### DB connection string 
- Ensure a `dbConnectionString.secret.conf` file is created:
    - `touch dbConnectionString.secret.conf`
- Open the `dbConnectionString.secret.conf` file and enter your password for the psql db in the following format:
    - `hostname:port:database:username:password`
    - For example: `db:5432:penny_pincher_db:postgres:YOUR_PASSWORD_HERE`
- Add this file to docker secrets: 
    - `docker create secret db-conn-str ./dbConnectionString.secret.conf`

#### dev TLS certificate HTTPS on the API
1. Create a local directory to store the cert (using BASH or similar shell)
    - `mkdir -p "$HOME/.aspnet/https"`
2. Create TLS dev-cert
    - create a password for the cert
    - `dotnet dev-certs https -ep "$HOME/.aspnet/https/pennypincher-api.pfx" -p "YOUR_PASSWORD_HERE"`
3. Add your password to an .env file:
    - Ensure a `.env` file is created:
        - `touch .env`
    - Open the `.env` file and enter your password for the TLS dev-cert in the following format:
        - `API_CERT_PASSWORD=YOUR_PASSWORD_HERE`


## Build and run Dev API   
- When you're ready, start your application by running: `docker compose watch`.

Your application will be available at http://localhost:7181/.

### Deploying your application to the cloud

First, build your image, e.g.: `docker build -t myapp .`.
If your cloud uses a different CPU architecture than your development
machine (e.g., you are on a Mac M1 and your cloud provider is amd64),
you'll want to build the image for that platform, e.g.:
`docker build --platform=linux/amd64 -t myapp .`.

Then, push it to your registry, e.g. `docker push myregistry.com/myapp`.

Consult Docker's [getting started](https://docs.docker.com/go/get-started-sharing/)
docs for more detail on building and pushing.

### References
* [Docker's .NET guide](https://docs.docker.com/language/dotnet/)
* The [dotnet-docker](https://github.com/dotnet/dotnet-docker/tree/main/samples)
  repository has many relevant samples and docs.