Test

# Implementación de un Pipeline CI/CD con ASP.NET Core, Angular y Docker

Este repositorio tiene como objetivo demostrar la implementación de un pipeline completo de **Integración Continua y Despliegue Continuo (CI/CD)** utilizando tecnologías ampliamente empleadas en entornos profesionales.

Para ello se desarrolló una **aplicación web full-stack básica de tipo CRUD**, compuesta por una interfaz desarrollada en **Angular**, una **API REST en ASP.NET Core (.NET 10)** y una base de datos **SQL Server**. Sobre esta aplicación se construyó un flujo automatizado de compilación, pruebas y despliegue.

El objetivo principal del proyecto es servir como referencia para comprender cómo integrar **GitHub Actions**, **Docker**, **Selenium Grid**, **SQL Server** y aplicaciones **ASP.NET Core + Angular** dentro de un pipeline CI/CD moderno.

---

# Índice

* **[1 - Objetivos](#link-objetivos-1)**

* **[2 - Arquitectura del sistema](#link-arquitectura-del-sistema-2)**

* **[3 - Pipeline CI/CD](#link-pipeline-cicd-3)**

* **[4 - Tecnologías utilizadas](#link-tecnologías-utilizadas-4)**

---

# :link: OBJETIVOS (1) <a id="link-objetivos-1"></a>

Este proyecto fue desarrollado con el objetivo de aprender y aplicar prácticas modernas de DevOps sobre una aplicación full-stack.

Durante el desarrollo de este proyecto se buscó:

- Implementar un pipeline completo utilizando **GitHub Actions**.
- Automatizar la compilación de una aplicación **ASP.NET Core** y **Angular**.
- Integrar pruebas **Unitarias**, **de Integración** y **de Interfaz** dentro del pipeline.
- Ejecutar pruebas UI utilizando **Selenium Grid** y **Docker**.
- Construir y publicar imágenes Docker en **GitHub Container Registry (GHCR)**.
- Simular un despliegue hacia un entorno de **Staging**.
- Simular la promoción de imágenes Docker hacia un entorno de **Producción**.
- Reproducir un flujo CI/CD similar al utilizado en proyectos empresariales.

---

# :link: ARQUITECTURA DEL SISTEMA (2) <a id="link-arquitectura-del-sistema-2"></a>

La aplicación sigue una arquitectura multicapa compuesta por una interfaz web desarrollada en **Angular**, una **API REST en ASP.NET Core (.NET 10)** y una base de datos **SQL Server**, utilizando **Entity Framework Core** como capa de acceso a datos.

El siguiente diagrama muestra la interacción entre los principales componentes del sistema.

<p align="center">
    <img src="assets/Diagrama de Arquitectura del sistema.png" alt="Arquitectura del sistema" width="450">
</p>

---

# :link: PIPELINE CI/CD (3) <a id="link-pipeline-cicd-3"></a>

El proyecto implementa un pipeline completo de **Integración Continua y Despliegue Continuo (CI/CD)** mediante **GitHub Actions**.

Aunque el repositorio tiene fines educativos y no dispone de servidores reales de **Staging** o **Producción**, ambos ambientes son simulados utilizando **Docker Compose**, permitiendo reproducir el flujo de trabajo que normalmente se encuentra en proyectos empresariales.

---

## Flujo del Pipeline

```text
Build
   │
   ▼
Unit Tests
   │
   ▼
Integration Tests
   │
   ▼
UI Tests (Selenium Grid)
   │
   ▼
Deploy to Staging (Docker)
   │
   ▼
Deploy to Production (Docker)
```

---

## Etapas del Pipeline

### 1. Build

Restaura las dependencias del proyecto y compila tanto la solución **ASP.NET Core** como la aplicación **Angular**, verificando que ambas puedan construirse correctamente antes de continuar con las siguientes etapas del pipeline.

---

### 2. Unit Tests

Ejecuta las pruebas unitarias para validar la lógica de negocio de forma aislada, sin depender de bases de datos ni servicios externos.

Al finalizar, los resultados de las pruebas son publicados como parte del pipeline.

---

### 3. Integration Tests

Levanta una instancia temporal de **SQL Server** y ejecuta las pruebas de integración para validar el correcto funcionamiento de la API, Entity Framework Core y la persistencia de datos utilizando una base de datos real.

Una vez finalizadas, los resultados de las pruebas son publicados.

---

### 4. UI Tests

Levanta un entorno completo de pruebas mediante **Docker Compose**, compuesto por:

- SQL Server
- ASP.NET Core API
- Angular UI
- Selenium Hub
- Chrome Node
- Edge Node

Una vez disponible la infraestructura, espera a que **Selenium Grid** se encuentre operativo y ejecuta las pruebas de interfaz utilizando Selenium WebDriver sobre ambos navegadores.

En caso de fallo, el pipeline recopila los logs de los contenedores para facilitar el diagnóstico antes de destruir completamente el entorno de pruebas.

---

### 5. Deploy to Staging

Una vez superadas todas las etapas de validación, el pipeline:

- Construye las imágenes Docker de la API y la aplicación Angular.
- Publica ambas imágenes en **GitHub Container Registry (GHCR)** utilizando la etiqueta `:staging`.
- Despliega un entorno completo de **Staging** mediante Docker Compose.
- Espera a que la API y la aplicación Angular estén disponibles.
- Ejecuta pruebas **Smoke** para verificar que los servicios principales respondan correctamente.
- En caso de error, recopila los logs de los contenedores antes de limpiar el entorno.

---

### 6. Deploy to Production

Una vez validado el entorno de **Staging**, el pipeline simula un despliegue a Producción siguiendo una estrategia de promoción de imágenes.

Durante esta etapa:

- Descarga las imágenes previamente validadas en Staging.
- Promueve las imágenes desde `:staging` hacia `:latest`.
- Publica las imágenes de Producción en **GitHub Container Registry (GHCR)**.
- Despliega un entorno independiente de **Producción** mediante Docker Compose.
- Espera a que la API y la aplicación Angular estén disponibles.
- Ejecuta pruebas **Smoke** para verificar el correcto funcionamiento de la aplicación.
- En caso de error, recopila los logs de los contenedores antes de finalizar el pipeline.

Para fines demostrativos, tanto **Staging** como **Production** se ejecutan mediante Docker Compose. En un entorno empresarial, esta última etapa normalmente desplegaría la aplicación sobre servidores físicos, máquinas virtuales, Kubernetes o plataformas cloud como Azure, AWS o Google Cloud.

---

## Arquitectura del Pipeline

```text
CI/CD Pipeline

├── JOB 1 — Build
│   ├── Checkout repository
│   ├── Setup .NET SDK
│   ├── Setup Node.js
│   ├── Restore .NET dependencies
│   ├── Install Angular dependencies (npm ci)
│   ├── Build ASP.NET Core solution
│   └── Build Angular application
│
├── JOB 2 — Unit Tests
│   ├── Checkout repository
│   ├── Setup .NET SDK
│   ├── Execute Unit Tests
│   └── Publish test results
│
├── JOB 3 — Integration Tests
│   ├── Checkout repository
│   ├── Setup .NET SDK
│   ├── Start SQL Server
│   ├── Execute Integration Tests
│   └── Publish test results
│
├── JOB 4 — UI Tests
│   ├── Checkout repository
│   ├── Setup .NET SDK
│   ├── Setup Node.js
│   ├── Start Test Environment
│   │   ├── SQL Server
│   │   ├── ASP.NET Core API
│   │   ├── Angular UI
│   │   ├── Selenium Hub
│   │   ├── Chrome Node
│   │   └── Edge Node
│   ├── Wait for Selenium Grid
│   ├── Execute UI Tests
│   ├── Publish test results
│   ├── Collect Docker logs on failure
│   └── Destroy test environment
│
├── JOB 5 — Deploy to Staging
│   ├── Checkout repository
│   ├── Normalize repository name
│   ├── Login to GitHub Container Registry
│   ├── Build and Push API image (:staging)
│   ├── Build and Push UI image (:staging)
│   ├── Start Staging Environment
│   │   ├── SQL Server (Staging)
│   │   ├── ASP.NET Core API (Staging)
│   │   └── Angular UI (Staging)
│   ├── Wait for API
│   ├── Wait for UI
│   ├── Execute Smoke Tests
│   ├── Collect Docker logs on failure
│   └── Cleanup
│
└── JOB 6 — Deploy to Production
    ├── Checkout repository
    ├── Normalize repository name
    ├── Login to GitHub Container Registry
    ├── Pull validated Staging images
    │   ├── api:staging
    │   └── ui:staging
    ├── Promote images
    │   ├── api:staging → api:latest
    │   └── ui:staging  → ui:latest
    ├── Push Production images (:latest)
    ├── Start Production Environment
    │   ├── SQL Server (Production)
    │   ├── ASP.NET Core API (Production)
    │   └── Angular UI (Production)
    ├── Wait for API
    ├── Wait for UI
    ├── Execute Smoke Tests
    ├── Collect Docker logs on failure
    └── Cleanup
```

---

# :link: TECNOLOGÍAS UTILIZADAS (4) <a id="link-tecnologías-utilizadas-4"></a>

## Backend

- ASP.NET Core (.NET 10)
- Entity Framework Core
- SQL Server 2022

## Frontend

- Angular 22

## Testing

- xUnit
- FluentAssertions
- Selenium WebDriver
- Selenium Grid
- Google Chrome
- Microsoft Edge

## DevOps

- GitHub Actions
- Docker
- Docker Compose
- GitHub Container Registry (GHCR)
