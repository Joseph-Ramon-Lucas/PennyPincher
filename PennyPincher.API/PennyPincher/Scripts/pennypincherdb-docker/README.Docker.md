# Internal DB Configuration Notes
Before starting, make sure to disable existing psql services that may be running on the host machine:
- On Windows: stop the PostgreSQL service in Services.msc.
- On Linux/macOS: sudo service postgresql stop or brew services stop postgresql depending on how you installed it.
- You want no Postgres listening on 5432 except what Docker will start.

## 1. Set up secrets with Docker Swarm

- Enable Docker swarm if not enabled
```docker swarm init```
- Ensure a dbpassword file is created
```touch dbpassword.conf```
- Open the dbpassword.conf file and enter your password for the psql db
- Add this file to docker secrets 
```docker secret create db-password ./dbpassword.conf``
    
## 2.  Setting up a Docker DB Container with Compose (Recommended)
```docker compose up --build```

## 3. How to stop the container with Compose
```docker compose down```
- Optionally delete all data from the volume: 
```docker compose down -v```

## Setting up a Docker DB Container Manually (Deprecated)
```
docker run --name pennypincher_db \
  -e POSTGRES_PASSWORD=/run/secrets/db-password \
  -e POSTGRES_USER=postgres \
  -p 5432:5432 \
  -v pennypincher-postgres-data:/var/lib/postgresql \
  -v ../pennypincherdb.sql:/docker-entrypoint-initdb.d/pennypincherdb.sql:ro \
  -d postgres:18
  ```

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

## How to stop the container Manually
```docker stop pennypincher_db```

## Access PostgreSQL within a client
- Connect to the Docker container and run the psql command
```docker exec -it pennypincher_db psql -U postgres```
- Connect to the Penny Pincher Database
```\c penny_pincher_db```

### Exec Command explanation
- `exec` = execute a command on the running Docker container
- `-i` = Keep the terminal session interactive
- `-t` = Specifies to Linux that the type of connection is to a terminal interface (tty)


## Inspect Docker Volume
```docker volume inspect pennypincher-postgres-data```