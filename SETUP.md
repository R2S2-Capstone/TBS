# Setup Instructions
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

# Return to main directory
cd ../../../

# Build backend

# Open up Visual Studio .SLN file
# Under TBS.API, copy azure-secrets-template.json to azure-secrets.json and replace data with actual data
```