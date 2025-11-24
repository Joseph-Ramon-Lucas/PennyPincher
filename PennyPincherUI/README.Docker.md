### Building and running your application

## Startup process without Docker
1. cd into PennyPincherUI directory
    - `cd PennyPincherUI`
2. Build and Serve files locally
    - `npm run start`
3. Access the local site at the default port
    - http://localhost:4200/

# How to run the front end within a local Docker development container
1. cd into PennyPincherUI directory
    - `cd PennyPincherUI`
2. Start up Docker Desktop
3. Build and Run the Docker image
    - `docker compose up --build`
    - Current dev docker container will allow for hot reloading when making edits to local files


First, build your image, e.g.: `docker build -t PennyPincher .`.
If your cloud uses a different CPU architecture than your development
machine (e.g., you are on a Mac M1 and your cloud provider is amd64),
you'll want to build the image for that platform, e.g.:
`docker build --platform=linux/amd64 -t PennyPincher .`.

Then, push it to your registry, e.g. `docker push myregistry.com/PennyPincher`.

Consult Docker's [getting started](https://docs.docker.com/go/get-started-sharing/)
docs for more detail on building and pushing.

### References
* [Docker's Node.js guide](https://docs.docker.com/language/nodejs/)