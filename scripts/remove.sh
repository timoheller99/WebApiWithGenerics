#!/bin/bash
SETUP_SlEEP_TIME=10
SlEEP_TIME=5
RETRIES=0
MAX_RETRIES=5

DB_CONTAINER_NAME="mysql"

CURRENT_PATH="`dirname \"$0\"`"
CURRENT_PATH="`( cd \"$CURRENT_PATH\" && pwd )`"

PROJECT_PATH="`( cd \"$CURRENT_PATH\" && cd .. && pwd )`"

echo "Stopping and removing db container."
cd $PROJECT_PATH && docker-compose down && docker-compose rm -s
