# pull official base image
FROM node:latest

WORKDIR /usr/src/app

COPY package*.json ./

# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

# install app dependencies
COPY package*.json ./
RUN npm install --silent
RUN npm install react-scripts@3.4.1 -g --silent

# add app
COPY . .

# start app
EXPOSE 3000
CMD ["npm", "start"]