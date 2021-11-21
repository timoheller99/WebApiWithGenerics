#!/bin/bash
SETUP_SlEEP_TIME=10
DB_CONTAINER_NAME="mysql"

CURRENT_PATH="`dirname \"$0\"`"
CURRENT_PATH="`( cd \"$CURRENT_PATH\" && pwd )`"
PROJECT_PATH="`( cd \"$CURRENT_PATH\" && cd .. && pwd )`"
PROJECT_SCRIPTS_PATH="${PROJECT_PATH}/scripts"

DB_SCRIPTS_PATH="${PROJECT_SCRIPTS_PATH}/database"
SETUP_DB_SCRIPT_PATH="${DB_SCRIPTS_PATH}/setupDb.sh"

clean_up_dangling_volumes () {
    echo "Remove dangling docker volumes"
    docker volume rm $(docker volume ls -qf dangling=true)
}

start_docker_compose () {
    echo "Start docker-compose"
    docker volume rm $(docker volume ls -qf dangling=true)
    cd $PROJECT_PATH && docker-compose build --no-cache && docker-compose up -d
}

setup_db() {
    bash $SETUP_DB_SCRIPT_PATH $DB_CONTAINER_NAME $PROJECT_PATH $DB_SCRIPTS_PATH
}

setup_dev_cert() {
  local certPath="${HOME}/.aspnet/https/aspnetapp.pfx"

  if test -f "$certPath"; then
      echo "$certPath exists."
      return
  fi

  echo "Setting up dev cert in '${certPath}'"
  dotnet dev-certs https -ep ${certPath} -p password
  dotnet dev-certs https --trust
  echo "Dev cert created in '${certPath}'"
}

setup_dev_cert

clean_up_dangling_volumes

start_docker_compose

echo "Waiting ${SETUP_SlEEP_TIME} seconds for mysql setup in docker container."
sleep ${SETUP_SlEEP_TIME}

setup_db $DB_CONTAINER_NAME $PROJECT_PATH $DB_SCRIPT_PATH
