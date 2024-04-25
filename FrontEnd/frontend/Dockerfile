# Use Node.js image as base
FROM node:14 AS build-env
WORKDIR /app

# Copy package.json and install dependencies
COPY package.json ./
RUN npm install

# Copy the rest of the app and build
COPY . ./
RUN npm run build

# Production build
FROM nginx:alpine
COPY --from=build-env /app/build /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
