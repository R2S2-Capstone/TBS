# Build Instructions
```bash
# Build front-end
cd TBS.Website
npm run build
# Now upload files to front-end server to via FTP

# Build backend
dotnet publish -o build/{buildVersion}
# Now upload files to backend server via FTP
# Delete 'Last' folder
# Rename 'Current' folder to 'Last'
# Rename '{buildVersion}' to 'Current'
sudo systemctl restart tbs-api
# Check the service status
sudo systemctl status tbs-api
```