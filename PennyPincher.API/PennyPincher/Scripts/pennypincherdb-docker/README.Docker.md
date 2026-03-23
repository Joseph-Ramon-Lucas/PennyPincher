# Internal Configuration Notes

## Setting up a Docker DB Container with Compose (Recommended)
- Ensure a dbpassword file is created `touch dbpassword.txt`
- enter your password for the psql db
- `docker compose up`

## Setting up a Docker DB Container Manually

`docker run --name pennypincherdb -e POSTGRES_PASSWORD=password -e POSTGRES_USER=postgres -e POSTGRES_DB=penny_pincher_db -p 5432:5432 -v pennypincher-postgres-data:/var/lib/postgresql -d postgres:18`

### Command explanation
- `--name` = Container Name
- `--e` = Environment Variable
- `POSTGRES_PASSWORD` = Password for psql in container
- `POSTGRES_USER` = User for psql in container
- `POSTGRES_DB` = Database name for psql in container
- `-p` = Connection Port range
- `-v` = Create a Docker volume
- `pennypincher-postgres-data` = The name of the Docker volume 
- `/var/lib/postgresql` = Location of which files inside the container will be copied into the Docker volume
- `-d` = Run container in detached mode so it doesn't block the terminal for commandline access
- `postgres:18` = Run this container with the postgres Docker image version 18

## How to stop the container
In the terminal running the container, perform the following:
1. Ctrl + C
2. `docker compose down`

## Access PostgreSQL within a client
`docker exec -it pennypincherdb psql -U postgres`
This command logs into the Docker container and runs the psql command as the postgres user from there.  


## Inspect Docker Volume
`docker volume inspect pennypincher-postgres-data`