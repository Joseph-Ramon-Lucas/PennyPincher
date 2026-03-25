# Internal DB Configuration Notes
Before starting, make sure to disable existing psql services that may be running on the host machine:
- On Windows: stop the PostgreSQL service in Services.msc.
- On Linux/macOS: sudo service postgresql stop or brew services stop postgresql depending on how you installed it.
- You want no Postgres listening on 5432 except what Docker will start.


## Setting up a Docker DB Container with Compose (Recommended)
- Ensure a dbpassword file is created `touch dbpassword.conf`
- Open the dbpassword.conf file and enter your password for the psql db in the following format:
    - hostname:port:database:username:password`
    - For example: `db:5432:penny_pincher_db:postgres:yourpassword` 
- Add this file to docker secrets `docker create secret dbpassword ./dbpassword.conf`
- `docker compose up --build`

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
- `docker compose down`
- Optional: `docker compose down -v`
    - To delete the volume

## Access PostgreSQL within a client
- `docker exec -it pennypincherdb psql -U postgres`
    - This command logs into the Docker container and runs the psql command as the postgres user from there.  
- `\c penny_pincher_db`
    - To connect to the Penny Pincher Database


## Inspect Docker Volume
`docker volume inspect pennypincher-postgres-data`