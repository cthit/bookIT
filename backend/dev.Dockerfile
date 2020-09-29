FROM node:lts-alpine

WORKDIR /usr/src/app
COPY package.json .
COPY package-lock.json .

RUN npm install

COPY . .

EXPOSE 4000

CMD ["sh", "./startup.sh" , "dev"]