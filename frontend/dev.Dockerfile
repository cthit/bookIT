FROM node:13.14.0
WORKDIR /usr/src/app
COPY . .
CMD ["sh", "startup.sh"]
