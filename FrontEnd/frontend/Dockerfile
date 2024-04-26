# Use Node.js as base image for building the React app
FROM node:14 AS build-env
WORKDIR /app

# Copy package.json and install dependencies
COPY package.json ./
RUN npm install

# Copy the rest of the app and build
COPY . ./
RUN npm run build

# Production build using NGINX
FROM nginx:alpine

# Copy custom NGINX configuration
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Copy built React app into NGINX HTML directory
COPY --from=build-env /app/build /usr/share/nginx/html

# Expose port 80 (default HTTP port)
EXPOSE 80

# Command to start NGINX in the foreground
CMD ["nginx", "-g", "daemon off;"]
