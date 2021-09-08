#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
# Set base image
FROM node:latest AS builder

# Set working directory
WORKDIR /app

# Copy package.json and install packages
COPY ./package*.json ./app
RUN npm install

# Copy other project files and build
COPY . ./
RUN npm run build

RUN ls -al
EXPOSE 3000
CMD ["npm","start"]