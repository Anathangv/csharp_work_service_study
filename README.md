# WorkerStydy

This project is a .NET 8 Worker Service that runs background tasks, periodically making HTTP requests and saving the results to text files.

## Features

- Periodically (every 5 seconds) sends an HTTP GET request to `https://jsonplaceholder.typicode.com/users/1/todos`.
- Saves the response content to a uniquely named `.txt` file.
- The save directory depends on the operating system:
  - **Windows:** `C:\temp`
  - **Linux/Docker:** `/tmp`

## How to Run

### Locally (Windows)

1. Build and run the project using Visual Studio.
2. The files will be saved in `C:\temp`.

### In Docker (Linux Container)

1. Build the Docker image:
2. To save files directly to your Windows machine, it is necessary to map a volume

## Requirements

- .NET 8 SDK
- Docker (optional, for containerized execution)
