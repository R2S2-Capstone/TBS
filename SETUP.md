# Setup Instructions

## Front-end instructions
```bash
# Setup front-end

# Clone repository
git clone git@github.com:R2S2-Capstone/TBS.git TBS
cd TBS/TBS.Website/

# Install NPM packages
npm install

# Setup firebase
cd src/firebase/
# Replace placeholder data with actual data
copy Config-template.js Config.js

# Run the project
cd ../../
npm run serve
```

## Backend instructions

1. Open up Visual Studio .SLN file
2. Under TBS.API, copy azure-secrets-template.json to azure-secrets.json and replace data with actual data
3. right click on 'TBS.API' and open the 'Properties' panel and make the following changes under the 'Debug' tab 
  a. Profile -> TBS.API
  b. Launch -> Project
  c. Launch Browser -> unchecked
  d. App URL -> https://localhost:5001;http://localhost:5000
4. Before starting the backend, change the active project from IIS Express to 'TBS.API' if it has not automatially switched
5. Run the project