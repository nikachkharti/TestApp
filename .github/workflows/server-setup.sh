# server-setup.sh - Run this on your DigitalOcean server

#!/bin/bash

# Replace 'your-app-name' with your actual application name
APP_NAME="TaskManager"
APP_USER="www-data"

# Create application directory
sudo mkdir -p /var/www/$APP_NAME/current
sudo mkdir -p /var/www/$APP_NAME/logs

# Set permissions
sudo chown -R $APP_USER:$APP_USER /var/www/$APP_NAME

# Create systemd service file
sudo tee /etc/systemd/system/$APP_NAME.service > /dev/null <<EOF
[Unit]
Description=$APP_NAME .NET Web API
After=network.target

[Service]
Type=notify
ExecStart=/var/www/$APP_NAME/current/TaskManager.API
Restart=on-failure
RestartSec=5
TimeoutStopSec=20
KillMode=mixed
User=$APP_USER
Group=$APP_USER
WorkingDirectory=/var/www/$APP_NAME/current
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://localhost:5216

# Logging
StandardOutput=journal
StandardError=journal
SyslogIdentifier=$APP_NAME

[Install]
WantedBy=multi-user.target
EOF

# Install Nginx (reverse proxy)
sudo apt update
sudo apt install -y nginx

# Create Nginx configuration
sudo tee /etc/nginx/sites-available/$APP_NAME > /dev/null <<EOF
server {
    listen 80;
    server_name thebitsphere.online www.thebitsphere.online; # Replace with your domain
    
    location / {
        proxy_pass http://localhost:5216;
        proxy_http_version 1.1;
        proxy_set_header Upgrade \$http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host \$host;
        proxy_set_header X-Real-IP \$remote_addr;
        proxy_set_header X-Forwarded-For \$proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto \$scheme;
        proxy_cache_bypass \$http_upgrade;
    }
}
EOF

# Enable the site
sudo ln -sf /etc/nginx/sites-available/$APP_NAME /etc/nginx/sites-enabled/
sudo rm -f /etc/nginx/sites-enabled/default


# Test Nginx configuration
sudo nginx -t

# Restart services
sudo systemctl daemon-reload
sudo systemctl restart nginx
sudo systemctl enable nginx