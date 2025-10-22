# Backend Deployment Guide

## Docker Setup

This repository includes a Dockerfile for containerizing the .NET 8.0 backend application for deployment on Render or other container platforms.

### Files

- **Dockerfile**: Multi-stage build configuration for the .NET 8.0 application
- **.dockerignore**: Excludes unnecessary files from the Docker build context

### Building the Docker Image Locally

```bash
docker build -t consoleapp3-backend .
```

### Running the Container Locally

```bash
docker run --rm consoleapp3-backend
```

## Deploying to Render

### Prerequisites
- A [Render](https://render.com) account
- This repository pushed to GitHub

### Deployment Steps

1. **Create a New Web Service on Render**
   - Go to your Render dashboard
   - Click "New +" and select "Web Service"
   - Connect your GitHub repository

2. **Configure the Service**
   - **Name**: Choose a name for your service
   - **Environment**: Docker
   - **Branch**: Select your deployment branch (e.g., `main`)
   - **Root Directory**: Leave empty (unless your Dockerfile is in a subdirectory)

3. **Instance Type**
   - Select an appropriate instance type based on your needs
   - The application uses minimal resources (approximately 32MB RAM)

4. **Deploy**
   - Click "Create Web Service"
   - Render will automatically build and deploy your Docker container

### Environment Variables (Optional)

If you need to add environment variables:
- Go to the "Environment" tab in your Render service
- Add any required environment variables

### Dockerfile Details

The Dockerfile uses a multi-stage build:
- **Stage 1 (build)**: Uses `mcr.microsoft.com/dotnet/sdk:8.0` to restore dependencies and build the application
- **Stage 2 (runtime)**: Uses `mcr.microsoft.com/dotnet/runtime:8.0` for a smaller runtime image

This approach minimizes the final image size and improves security by excluding build tools from the production image.

## Application Details

This is a .NET 8.0 console application that simulates a multi-threaded service processing system with:
- Sequence allocation across services
- Thread-safe cache implementation
- Memory usage monitoring

The application runs to completion and exits, processing all allocated sequences.
