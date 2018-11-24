#!/bin/bash
source ../deploy-envs.sh

#AWS_ACCOUNT_NUMBER={} set in private variable
export AWS_ECS_REPO_DOMAIN=$AWS_ACCOUNT_NUMBER.dkr.ecr.$AWS_DEFAULT_REGION.amazonaws.com

# Build process
docker build -t $IMAGE_NAME ../ \
    --build-arg ASPNETCORE_ENVIRONMENT="$ASPNETCORE_ENVIRONMENT" \
    --build-arg RDS_DB_NAME="$RDS_DB_NAME" \
    --build-arg RDS_USERNAME="$RDS_USERNAME" \
    --build-arg RDS_PASSWORD="$RDS_PASSWORD" \
    --build-arg RDS_HOSTNAME="$RDS_HOSTNAME" \
    --build-arg RDS_PORT="$RDS_PORT"
    --build-arg BING_KEY="$BING_KEY"
docker tag $IMAGE_NAME $AWS_ECS_REPO_DOMAIN/$IMAGE_NAME:$IMAGE_VERSION
