# DocPlanner

Doctor's agenda availability system to make appointments management easy.

## Prerequisites

- **.NET 8 SDK** is required to build and run this project.
  
  You can download the .NET 8 SDK from the official website:  
  [Download .NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

- **IDE (Optional but Recommended):**
  - [Visual Studio 2022+](https://visualstudio.microsoft.com/) with .NET support or
  - [Visual Studio Code](https://code.visualstudio.com/) with the C# extension

## Getting Started

### 1. Clone the repository

First, clone the repository to your local machine using the following command:

```bash
git clone https://github.com/pelaez87/DocPlanner.git
cd DocPlanner
```

### 2. Build the project
To build the project, navigate to the project root directory and run the following command:
```
dotnet build
```

### 3. Running the API
Once the project is built, you can run it with the following command:
```
dotnet run
```

By default, the API will be hosted on:

http://localhost:5233 (HTTP)
https://localhost:7139 (HTTPS)
You can access the API using a browser, Postman, or cURL.

If you are using Visual Studio, press F5 or click on the Start button to run the project.

### 4. Testing the API
To run unit tests for the project, execute the following command:

```dotnet test```
This will run all the tests in the solution and provide a summary of the results.

### 5. Building for Production
To create a release build, use the following command:
```
dotnet publish --configuration Release --output ./publish
```
This will generate a production-ready build in the ./publish directory, ready for deployment.