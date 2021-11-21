#!/bin/bash
DB_CONTAINER_NAME=$1
DB_SCRIPT_PATH=$2
DB_SCRIPT_NAME=$3

DOCKER_DB_SCRIPT_PATH="${DB_CONTAINER_NAME}:/${DB_SCRIPT_NAME}"

echo "Copy '${DB_SCRIPT_PATH}' to ${DOCKER_DB_SCRIPT_PATH}"
docker cp ${DB_SCRIPT_PATH} ${DOCKER_DB_SCRIPT_PATH}

echo "Execute SQL script in docker container '${DB_CONTAINER_NAME}'."

docker exec -e DB_SCRIPT_NAME=${DB_SCRIPT_NAME} ${DB_CONTAINER_NAME} /bin/sh -c 'mysql -u root -proot < /${DB_SCRIPT_NAME}'