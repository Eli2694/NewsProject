# Use the official Node.js image as the base image
FROM node:16 AS build

# Set the working directory
WORKDIR /app

# Copy package.json and package-lock.json
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the application files
COPY . .

# Build the React app
RUN npm run build

# Use a lightweight Node.js image for the runtime environment
FROM node:16-alpine

# Set the working directory for the runtime image
WORKDIR /app

# Copy the built app from the build image
COPY --from=build /app/build .

# Expose the port that your app will run on
EXPOSE 80

# Define the command to run your app
CMD ["npm", "start"]
