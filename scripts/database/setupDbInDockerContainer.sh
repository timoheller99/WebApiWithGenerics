#!/bin/bash
SlEEP_TIME=5
MAX_RETRIES=5

DB_CONTAINER_NAME=$1
DB_SCRIPT_PATH=$2
DATABASE_SCRIPTS_PATH=$3
FIX_DB_SCRIPT_PATH=$4

EXECUTE_SQL_SCRIPT_IN_CONTAINER_SCRIPT_PATH="${DATABASE_SCRIPTS_PATH}/executeSqlScriptInContainer.sh"


fix_sql_script() {
    local retries=0
    local script_path=$1

    echo "Fixing SQL script '${script_path}'."
    until bash $FIX_DB_SCRIPT_PATH $script_path ; do
        echo "Failed to execute '${FIX_DB_SCRIPT_PATH}'."
        if [ $retries == $MAX_RETRIES ];then
            echo "Max retries exceeded."
            exit 1
        fi

        echo "Waiting ${SlEEP_TIME} seconds for retry."
        sleep ${SlEEP_TIME}
    done
    echo "Successfully fixed SQL script '${script_path}'."
}

execute_script_in_container() {
    local retries=0
    local script_path=$1
    local script_name="${script_path##*/}"

    echo "Execute script '${script_name}' in container '${DB_CONTAINER_NAME}'."
    until bash $EXECUTE_SQL_SCRIPT_IN_CONTAINER_SCRIPT_PATH
 $DB_CONTAINER_NAME $script_path $script_name; do
        echo "Failed to execute '${EXECUTE_SQL_SCRIPT_IN_CONTAINER_SCRIPT_PATH
    }'."
        if [ $retries == $MAX_RETRIES ];then
            echo "Max retries exceeded."
            exit 1
        fi

        echo "Waiting ${SlEEP_TIME} seconds for retry."
        sleep ${SlEEP_TIME}
    done
    echo "Successfully executed script '$script_name' in container '${DB_CONTAINER_NAME}'."
}

fix_sql_script $DB_SCRIPT_PATH

execute_script_in_container $DB_SCRIPT_PATH $
