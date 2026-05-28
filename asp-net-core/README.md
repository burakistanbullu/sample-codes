# ASP.NET Core Demo Application

Uygulama minimal API olarak yazılmıştır ve container içinde `8080` portundan yayınlanır.

## Endpoints

| Endpoint | Açıklama |
|---|---|
| `/` | Basit hello response döner. |
| `/info` | Uygulama hakkında temel bilgi döner. |
| `/healthz` | Kubernetes liveness probe için kullanılır. |
| `/readyz` | Kubernetes readiness probe için kullanılır. |

Uygulamayı test etmek için:

```bash
curl http://localhost:8080
curl http://localhost:8080/info
curl http://localhost:8080/healthz
curl http://localhost:8080/readyz
```

> Not: Docker ile çalıştırırken host tarafında `8081`, container tarafında `8080` kullanılmaktadır.

## Container Image Build

Projede iki farklı Dockerfile bulunur:

- `Dockerfile.singlestage`: Build ve runtime aynı image içinde yapılır. Basit testler için uygundur.
- `Dockerfile.multistage`: Build ve runtime ayrıdır. Daha küçük ve production'a daha uygun image üretir.

### Single-stage Build

```bash
docker build -f Dockerfile.singlestage -t asp-net-core-demo:single .
```

### Multi-stage Build

```bash
docker build -f Dockerfile.multistage -t asp-net-core-demo:multi .
```

## Container Çalıştırma

### Single-stage Image

```bash
docker run --rm -p 8081:8080 --name asp-net-core-demo-single asp-net-core-demo:single
```

### Multi-stage Image

```bash
docker run --rm -p 8081:8080 --name asp-net-core-demo-multi asp-net-core-demo:multi
```

## Container Test

Container ayağa kalktıktan sonra:

```bash
curl http://localhost:8081
curl http://localhost:8081/info
curl http://localhost:8081/healthz
curl http://localhost:8081/readyz
```

## Kubernetes Deploy

Kubernetes manifestleri `k8s/` dizini altındadır:

```text
k8s/
├── deployment.yaml
└── service.yaml
```

### Local Kubernetes / kind ile Deploy

Önce image build edilir:

```bash
docker build -f Dockerfile.multistage -t asp-net-core-demo:multi .
```

Manifestler uygulanır:

```bash
kubectl apply -f k8s/
```

Pod ve servis kontrolü:

```bash
kubectl get pods
kubectl get svc
```

Servise local makineden erişmek için:

```bash
kubectl port-forward svc/asp-net-core-demo 8081:80
```

Test:

```bash
curl http://localhost:8081
curl http://localhost:8081/healthz
curl http://localhost:8081/readyz
```

## Registry Üzerinden Deploy

Production veya remote Kubernetes ortamı için image bir registry'ye push edilmelidir.


## Proje Yapısı

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

