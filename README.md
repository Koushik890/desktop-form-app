# Slidely Form App

This project consists of a Windows desktop application and a backend server for managing form submissions. The desktop application allows users to create new submissions and view existing ones, while the backend server handles the storage and retrieval of these submissions.

## Table of Contents

1. [Desktop Application](#desktop-application)
    - [Features](#features)
    - [Requirements](#requirements)
    - [Setup](#setup)
    - [Usage](#usage)
2. [Backend Server](#backend-server)
    - [Features](#features-1)
    - [Requirements](#requirements-1)
    - [Setup](#setup-1)
    - [Usage](#usage-1)
3. [Screenshots](#screenshots)
4. [Contribution](#contribution)
5. [License](#license)

## Desktop Application

### Features

- View Submissions
  - Navigate through submissions using Previous and Next buttons.
  - Edit and Delete submissions.
- Create New Submission
  - Input fields for Name, Email, Phone Number, and GitHub link.
  - A stopwatch that can be toggled on and off.
  - Submit the form data to the backend server.

### Requirements

- Windows OS
- Visual Studio

### Setup

1. Clone the repository:
    ```sh
    git clone https://github.com/Koushik890/desktop-form-app
    cd desktop-form-app/slidelyFormApp
    ```

2. Open the solution file `slidelyFormApp.sln` in Visual Studio.

3. Build the solution:
    - Select `Build > Build Solution` from the menu or press `Ctrl + Shift + B`.

### Usage

1. Run the application:
    - Press `F5` in Visual Studio or select `Debug > Start Debugging` from the menu. Otherwise if you want to run the application without debugging press `CTRL + F5`.

2. Keyboard Shortcuts:
    - `Ctrl + V` to view submissions.
    - `Ctrl + N` to create a new submission.
    - `Ctrl + P` to navigate to the previous submission.
    - `Ctrl + N` to navigate to the next submission.
    - `Ctrl + T` to toggle the stopwatch.
    - `Ctrl + S` to submit the form.
    - `Ctrl + D` to delete any submitted form.

## Backend Server

### Features

- API Endpoints
  - `/ping`: A GET request that always returns `true`.
  - `/submit`: A POST request to save a new submission.
  - `/read`: A GET request to retrieve a submission by index.
  - `/delete`: A DELETE request to delete a submission by index.
  - `/edit`: A PUT request to edit a submission by index.

### Requirements

- Node.js
- npm (Node Package Manager)

### Setup

1. Navigate to the backend directory:
    ```sh
    cd desktop-form-app/Slidely-Backend
    ```

2. Initialize the project:
    ```sh
    npm init -y
    ```

3. Install the dependencies:
    ```sh
    npm install express body-parser fs-extra
    npm install --save-dev typescript @types/node @types/express @types/body-parser @types/fs-extra
    ```

4. Initialize TypeScript configuration:
    ```sh
    npx tsc --init
    ```

5. Build the TypeScript files:
    ```sh
    npm run build
    ```

### Usage

1. Start the server:
    ```sh
    node dist/server.js
    ```

2. The server will be running on `http://localhost:3000`.

3. API Endpoints:
    - `GET /ping`: Returns `true`.
    - `POST /submit`: Submit a new form with parameters `name`, `email`, `phone`, `github_link`, and `stopwatch_time`.
    - `GET /read?index=<index>`: Retrieve the submission at the given index.
    - `DELETE /delete?index=<index>`: Delete the submission at the given index.
    - `PUT /edit?index=<index>`: Edit the submission at the given index.

## Screenshots

### Desktop App Interface

![Desktop App Image](https://github.com/Koushik890/desktop-form-app/blob/main/Assets/Frontend/Frontend_1.png)

### Backend Server Running

![Backend Server](https://github.com/Koushik890/desktop-form-app/blob/main/Assets/Backend/Backen%20server_1.png)

### Additional Screenshots and Videos

#### Desktop App

![Desktop App Interface](https://github.com/Koushik890/desktop-form-app/blob/main/Assets/Frontend/Frontend_1.png) 

Check the video demonstration:
[Desktop App Interface](https://drive.google.com/file/d/1-rBhp1yfEShHTNMR0GEbBrDTRKQxuBPW/view?usp=sharing)

#### Backend Server

![Backend Server Running](https://github.com/Koushik890/desktop-form-app/blob/main/Assets/Backend/Backen%20server_1.png) 
![Backend Server Running](https://github.com/Koushik890/desktop-form-app/blob/main/Assets/Backend/Backen%20server_2.png) 
![Backend Server Running](https://github.com/Koushik890/desktop-form-app/blob/main/Assets/Backend/Backen%20server_3.png) 

Check the video demonstration:
[Backend Server Running](https://drive.google.com/file/d/1XceCW4xfbrNrntRPRtFeWTPf6yyiLkjf/view?usp=sharing)

## Contribution

Feel free to fork this repository and contribute by submitting a pull request.