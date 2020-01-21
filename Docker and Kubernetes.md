# Docker and Kubernetes

## Docker

1 - Create an Image of the app

2 - Put it on the Repository - Docker Hub

dotnet publish -c release -o app/ .

Create a Dockerfile (with no extension) - Example:

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY /app /app
ENTRYPOINT ["dotnet", "primavera.dll"]

Create a .dockerignore

bin\
obj\

docker build -t primavera .

docker run -p 8080:80 primavera (to test if the image is working as expected and to create a container)

docker tag primavera-container andreborgesdev/primavera:v1

docker push andreborgesdev/primavera

To update the Image:

docker commit primavera-container andreborgesdev/primavera:v2

docker push andreborgesdev/primavera:v2

## Kubernetes

Enable-WindowsOptionalFeature -Online -FeatureName Microsoft-Hyper-V -All

minikube start --vm-driver=hyperv

minikube config set vm-driver hyperv

minikube dashboard

After the minikube initializes use the kubectl

Create a secret for private container registries and add the secret to the manifests in the containers field with:

kubectl create secret docker-registry <SECRET_NAME> --docker-server=<REGISTRY_ADDRESS> --docker-email=<YOUR_MAIL> --docker-username=<SERVICE_PRINCIPAL_ID> --docker-password=<YOUR_PASSWORD>

```yml
imagePullSecrets:
- name: <SECRET_NAME>
```

Deployment:

kubectl create -f file.yml -para criar

kubectl apply -f file.yml -para criar ou dar update

Services:
