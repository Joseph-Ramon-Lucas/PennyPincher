# syntax=docker/dockerfile:1

#Development container for hot reloads
# Modified from: https://docs.docker.com/guides/angular/develop/

ARG NODE_VERSION=24.12.0

FROM node:${NODE_VERSION}-alpine

# Set environment variable to indicate development mode
ENV NODE_ENV=development

# Set the working directory inside the container
WORKDIR /app

# Copy only the dependency files first to optimize Docker caching
COPY package*.json ./

# Install dependencies using npm with caching to speed up subsequent builds
RUN --mount=type=cache,target=/root/.npm npm install -g @angular/cli \
    && npm ci --legacy-peer-deps

# Copy all application source files into the container
COPY . .

# Expose the port Angular uses for the dev server 
EXPOSE 4200

# Start the Angular dev server
CMD ["npm", "start"]