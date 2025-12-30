# Spring Boot Demo Application

## How to Build Container Image

You can build the container image using either the **single-stage** or **multi-stage**
Dockerfile.


- Single stage build command : 

```bash
docker build -f Dockerfile.singlestage -t demo:single .
```

- Multiple stage command : 

```bash
docker build -f Dockerfile.multistage -t demo:multi .
```

## How to Run Container

- Single stage run command : 
```bash
docker run --rm -p 8081:8080 --name demo-single demo:single
```

- Multiple stage run command : 
```bash
docker run --rm -p 8081:8080 --name demo-multi demo:multi
```

## Verify
After starting the container, open your browser or run:
```bash
curl http://localhost:8081
```
