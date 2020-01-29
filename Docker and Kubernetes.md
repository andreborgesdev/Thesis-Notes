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

Sometimes environment variables can throw exceptions. For example the environment must be equal to production.

Create a .dockerignore

bin\
obj\

docker build -t primavera .

docker run -p 8080:80 primavera (to test if the image is working as expected and to create a container)

docker tag primavera-image andreborgesdev/primavera:v1

docker push andreborgesdev/primavera

To update the Image:

docker commit primavera-container andreborgesdev/primavera:v2

docker push andreborgesdev/primavera:v2

List commands:
docker images or docker immages ls

Delete images:
docker rmi IDorName

## Kubernetes

To update the kubectl in windows we have to download the binary and copy and past it to the Docker/resources/bin folder

Enable-WindowsOptionalFeature -Online -FeatureName Microsoft-Hyper-V -All

minikube start --vm-driver=hyperv

minikube config set vm-driver hyperv

minikube dashboard

minikube service hello-minikube --url

After the minikube initializes use the kubectl

Create a secret for private container registries and add the secret to the manifests in the containers field with:

kubectl create secret docker-registry <SECRET_NAME> --docker-server=<REGISTRY_ADDRESS> --docker-email=<YOUR_MAIL> --docker-username=<SERVICE_PRINCIPAL_ID> --docker-password=<YOUR_PASSWORD>

```yml
imagePullSecrets:
- name: <SECRET_NAME>
```

Get commands with basic output:
kubectl get services                          # List all services in the namespace
kubectl get pods --all-namespaces             # List all pods in all namespaces
kubectl get pods -o wide                      # List all pods in the namespace, with more details
kubectl get deployment my-dep                 # List a particular deployment
kubectl get pods                              # List all pods in the namespace
kubectl get pod my-pod -o yaml                # Get a pod's YAML
kubectl get pod my-pod -o yaml --export       # Get a pod's YAML without cluster specific information

Force replace, delete and then re-create the resource. Will cause a service outage:
kubectl replace --force -f ./pod.json

kubectl delete --all pods

Deployment:

kubectl create -f file.yml -para criar

kubectl apply -f file.yml -para criar ou dar update

Services:

minikube tunnel to expose the IP from the services with minikube because RouteIP is the only way to do it. If we are developing on a server we have other options like expose.

Use namespaces

KubeDNS add-on to resolve the backend service name to it's IP.

Use Ingress/IngressController