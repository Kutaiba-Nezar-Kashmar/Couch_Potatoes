# Development Environment - Web App 

## Requirements

### Languages and runtimes
- NodeJS 18 (LTS) 
- Typescript

### NVM
It is recommended to use Node Version Manager to manage your NodeJS. 
NVM can be downloaded from here: https://github.com/coreybutler/nvm-windows/releases/tag/1.1.10


To install Node open a terminal and run: 
```bash
$ nvm install 18.15.0
```

To use a specific Node version, install the version and then run: 
```bash
$ nvm use 18.15.0
```

## Install dependencies
Before doing any development install the dependnecies through npm

1. Navigate to <em><PROJECT_ROOT>/src/Web/couch-potatoes-web-app</em>
2. Run ```bash npm install```


## Start Web App (Development mode)
You can start the Web App by navigating to <em><PROJECT_ROOT>/src/Web/couch-potatoes-web-app</em> and run: 
```bash
$ npm run start
```
It hosts the Web App locally on port 3000 by default. It has hot-reload by default, so every changes you make to the code will be reflected in the Web App after each save.
