# ASP.NET Core Demo Application

The application is written as a Minimal API and runs on port `8080` inside the container.

## Endpoints

| Endpoint | Description |
|---|---|
| `/` | Returns a simple hello response. |
| `/info` | Returns basic information about the application. |
| `/healthz` | Used for the Kubernetes liveness probe. |
| `/readyz` | Used for the Kubernetes readiness probe. |

To test the application:

```bash
curl http://localhost:8080
curl http://localhost:8080/info
curl http://localhost:8080/healthz
curl http://localhost:8080/readyz
```

> Note: When running with Docker, port `8081` is used on the host side and port `8080` is used inside the container.

## Container Image Build

The project contains two different Dockerfiles:

- `Dockerfile.singlestage`: Build and runtime are handled in the same image. Suitable for simple tests.
- `Dockerfile.multistage`: Build and runtime are separated. Produces a smaller image that is more suitable for production.

### Single-stage Build

```bash
docker build -f Dockerfile.singlestage -t asp-net-core-demo:single .
```

### Multi-stage Build

```bash
docker build -f Dockerfile.multistage -t asp-net-core-demo:multi .
```

## Running the Container

### Single-stage Image

```bash
docker run --rm -p 8081:8080 --name asp-net-core-demo-single asp-net-core-demo:single
```

### Multi-stage Image

```bash
docker run --rm -p 8081:8080 --name asp-net-core-demo-multi asp-net-core-demo:multi
```

## Testing the Container

After the container is up and running:

```bash
curl http://localhost:8081
curl http://localhost:8081/info
curl http://localhost:8081/healthz
curl http://localhost:8081/readyz
```

## Kubernetes Deploy

The Kubernetes manifests are located under the `k8s/` directory:

```text
k8s/
├── deployment.yaml
└── service.yaml
```

### Deploy with Local Kubernetes / kind

First, build the image:

```bash
docker build -f Dockerfile.multistage -t asp-net-core-demo:multi .
```

Apply the manifests:

```bash
kubectl apply -f k8s/
```

Check the pods and service:

```bash
kubectl get pods
kubectl get svc
```

To access the service from your local machine:

```bash
kubectl port-forward svc/asp-net-core-demo 8081:80
```

Test:

```bash
curl http://localhost:8081
curl http://localhost:8081/healthz
curl http://localhost:8081/readyz
```

## Deploy via Registry

For production or remote Kubernetes environments, the image must be pushed to a registry.

## Project Structure

```text
asp-net-core/
├── AspNetCoreDemo.csproj
├── Program.cs
├── Dockerfile.singlestage
├── Dockerfile.multistage
├── .dockerignore
├── .gitignore
├── README.md
└── k8s/
    ├── deployment.yaml
    └── service.yaml
```
