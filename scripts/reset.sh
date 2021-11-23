#!/bin/bash
CURRENT_PATH="`dirname \"$0\"`"
CURRENT_PATH="`( cd \"$CURRENT_PATH\" && pwd )`"
PROJECT_PATH="`( cd \"$CURRENT_PATH\" && cd .. && pwd )`"

PROJECT_SCRIPTS_PATH="${PROJECT_PATH}/scripts"

cd $PROJECT_SCRIPTS_PATH && bash ./remove.sh && bash ./init.sh