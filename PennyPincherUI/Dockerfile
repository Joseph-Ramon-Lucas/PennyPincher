# syntax=docker/dockerfile:1

#Development container for hot reloads

ARG NODE_VERSION=24.9.0

FROM node:${NODE_VERSION}-alpine
WORKDIR /app

COPY package*.json ./
RUN npm install -g @angular/cli \
    && npm ci --legacy-peer-deps

EXPOSE 4200

CMD ["npm", "start"]