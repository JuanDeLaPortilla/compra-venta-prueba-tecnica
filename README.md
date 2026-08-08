# Sistema de Compra y Venta 🚀

Aplicación web para la gestión de **productos, compras, ventas y movimientos de inventario**, desarrollada como solución para una prueba técnica de Especialista de Desarrollo Core.

La solución implementa un backend desarrollado con **.NET 8**, arquitectura orientada a microservicios, **Clean Architecture**, API Gateway, autenticación mediante JWT y persistencia utilizando **SQL Server + Entity Framework Core**.

El frontend está desarrollado con **React + TypeScript** y consume las APIs mediante el API Gateway.

---

## Tabla de Contenidos

- [Acerca del Proyecto](#acerca-del-proyecto)
- [Funcionalidades](#funcionalidades)
- [Arquitectura](#arquitectura)
- [Justificación de Clean Architecture](#justificación-de-clean-architecture)
- [Arquitectura de Microservicios](#arquitectura-de-microservicios)
- [API Gateway](#api-gateway)
- [Frontend](#frontend)
- [Patrones de Diseño](#patrones-de-diseño)
- [Seguridad](#seguridad)
- [Tecnologías](#tecnologías)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Requisitos](#requisitos)
- [Configuración](#configuración)
- [Configuración de SQL Server](#configuración-de-sql-server)
- [Ejecución del Backend](#ejecución-del-backend)
- [Configuración del Frontend](#configuración-del-frontend)

---

## Acerca del Proyecto

El proyecto implementa un sistema de compra y venta con control de inventario.

Las principales operaciones son:

- Autenticación de usuarios.
- Listado de productos.
- Actualización de productos.
- Registro de productos.
- Registro de compras.
- Listado de compras.
- Registro de ventas.
- Listado de ventas.
- Consulta de Kardex. (stock y movimientos)
- Registro de movimientos de entrada y salida.
- Cálculo automático de subtotal, IGV y total.
- Validación de stock disponible.

La solución fue diseñada considerando los principios de **Clean Architecture**, **SOLID**, separación de responsabilidades y arquitectura orientada a microservicios.

---

# Funcionalidades

## Autenticación

- Inicio de sesión mediante usuario y contraseña.
- Generación de JWT.
- Tokens con duración de 30 minutos.
- Protección de endpoints mediante `[Authorize]`.
- Interceptor HTTP en el frontend para enviar automáticamente el token.
- API Gateway como punto de entrada principal.

## Productos

- Listado de productos.
- Registro de productos.
- Visualización de:
  - Nombre.
  - Número de lote.
  - Costo.
  - Precio de venta.
  - Stock disponible.

El registro de productos se realiza mediante un diálogo reutilizable que puede ser utilizado desde otros módulos del sistema.

## Compras

La vista de compras permite:

- Registrar una compra.
  
- Buscar productos mediante un selector.
  
- Registrar un producto directamente desde la compra.
  
- Definir cantidad.
  
- Definir precio de compra.
  
- Calcular automáticamente:
  
  - Subtotal.
  - IGV.
  - Total.
- Actualizar el costo del producto.
  
- Actualizar automáticamente el precio de venta.
  
- Generar un movimiento de tipo `Entrada`.
  

## Ventas

La vista de ventas permite:

- Registrar una venta.
- Agregar múltiples productos.
- Seleccionar productos existentes.
- Visualizar precio de venta.
- Visualizar stock disponible.
- Definir cantidad.
- Validar que la cantidad no supere el stock disponible.
- Calcular automáticamente:
  - Subtotal.
  - IGV.
  - Total.
- Generar un movimiento de tipo `Salida`.

## Kardex

El Kardex permite visualizar:

```
Id Producto
Nombre
Stock actual
Costo
Precio de venta
```

También permite consultar los movimientos de un producto:

```
Fecha
Tipo de movimiento
Cantidad
```

Los movimientos pueden ser:

```
Entrada
Salida
```

El stock actual se obtiene a partir de los movimientos:

```
Stock = Entradas - Salidas
```

---

# Arquitectura

La solución utiliza **Clean Architecture** combinada con una arquitectura orientada a microservicios.

La arquitectura general es:

```
                         ┌──────────────┐
                         │   React      │
                         │  Frontend    │
                         └──────┬───────┘
                                │
                                ▼
                       ┌─────────────────┐
                       │   API Gateway   │
                       │      YARP       │
                       └───────┬─────────┘
                               │
                ┌──────────────┴──────────────┐
                │                             │
                ▼                             ▼
       ┌─────────────────┐          ┌─────────────────┐
       │   Auth Service  │          │ Commerce Service│
       │                 │          │                 │
       │ Authentication  │          │ Products        │
       │ Users           │          │ Purchases       │
       │ JWT             │          │ Sales           │
       └────────┬────────┘          │ Movements       │
                │                   │ Kardex          │
                │                   └────────┬────────┘
                │                            │
                └────────────┬───────────────┘
                             ▼
                     ┌───────────────┐
                     │   SQL Server  │
                     └───────────────┘
```

## Justificación de Clean Architecture

Se eligió Clean Architecture principalmente por las siguientes razones:

### Separación de responsabilidades

Cada capa tiene una responsabilidad específica.

Esto evita que componentes de presentación, infraestructura y lógica de negocio terminen mezclados.

### Mantenibilidad

Las funcionalidades pueden evolucionar independientemente.

Por ejemplo, el componente utilizado para seleccionar productos puede reutilizarse desde compras y ventas sin depender directamente de ninguna de estas funcionalidades.

### Testabilidad

Al separar la lógica de negocio de infraestructura, es posible probar las reglas del sistema sin depender directamente de la base de datos o de una interfaz gráfica.

### Escalabilidad

La estructura permite incorporar nuevas funcionalidades y separar posteriormente determinadas responsabilidades en microservicios independientes.

---

# Arquitectura de Microservicios

La solución utiliza dos servicios principales:

```
Auth Service
Commerce Service
```

## Auth Service

Responsable de:

- Autenticación.
- Validación de credenciales.
- Generación de JWT.
- Gestión de usuarios relacionada con autenticación.

---

## Commerce Service

El servicio Commerce contiene las funcionalidades relacionadas directamente con el proceso comercial:

```
Products
Purchases
Sales
Movements
Kardex
```

### ¿Por qué no crear un microservicio independiente para cada funcionalidad?

Aunque conceptualmente podrían separarse:

```
Product Service
Purchase Service
Sale Service
Inventory Service
```

hacerlo para esta solución habría agregado complejidad innecesaria.

Las funcionalidades de productos, compras, ventas y movimientos tienen una fuerte relación transaccional.

Por ejemplo, registrar una compra requiere realizar conjuntamente:

```
CompraCab
     +
CompraDet
     +
Actualizar Producto
     +
Movimiento Entrada
     +
MovimientoDet
```

Separar estas responsabilidades en diferentes servicios implicaría introducir comunicación entre servicios, consistencia distribuida y mecanismos adicionales de coordinación para una solución cuyo alcance no lo requiere.

Por esta razón se decidió mantenerlas dentro de un único **Commerce Service**, manteniendo la separación interna mediante Clean Architecture y módulos de dominio.

Esto permite:

- Reducir complejidad.
- Mantener transacciones ACID locales.
- Evitar comunicación innecesaria entre servicios.
- Facilitar el desarrollo y despliegue.
- Mantener una clara separación de responsabilidades dentro del servicio.

La arquitectura mantiene la posibilidad de extraer posteriormente un dominio específico a otro microservicio si el crecimiento de la aplicación lo justificara.

---

# API Gateway

El proyecto utiliza **YARP (Yet Another Reverse Proxy)** como API Gateway.

El Gateway representa el punto de entrada del frontend hacia los servicios backend.

Responsabilidades:

- Enrutamiento.
- Autenticación.
- Autorización.
- CORS.
- Rate Limiting.
- Aplicación de políticas comunes.
- Protección de los servicios internos.
- Agregado de una clave interna para validar que las peticiones provienen del Gateway.

El flujo es:

```
React
  │
  │ JWT
  ▼
API Gateway
  │
  │ Internal Key + JWT
  ▼
Microservice
```

Los microservicios también validan el JWT como mecanismo de **defense in depth**, evitando depender exclusivamente del Gateway para la protección de los recursos.

---

# Frontend

El frontend utiliza React y está organizado también siguiendo principios de Clean Architecture.

Una funcionalidad se organiza aproximadamente de la siguiente manera:

```text
features/
└── purchases/
    ├── domain/
    ├── infrastructure/
    ├── hooks/
    └── presentation/
        ├── components/
        └── pages/
```

### Domain

Contiene los modelos y contratos relacionados con el dominio.

Ejemplo:

```text
domain/
├── purchase.ts
```

### Infrastructure

Contiene la comunicación con el backend.

```text
infrastructure/
└── purchase.service.ts
```

### Hooks

Contiene los hooks utilizados para manejar el estado y las operaciones asíncronas.

```text
hooks/
├── use-purchases.ts
├── use-create-purchase.ts
└── purchase-keys.ts
```

### Presentation

Contiene las páginas y componentes visuales.

```text
presentation/
├── components/
└── pages/
```

Esta estructura permite reutilizar componentes entre diferentes funcionalidades.

Por ejemplo:

```text
features/products/
└── presentation/
    └── components/
        ├── product-selector.tsx
        └── create-product-dialog.tsx
```

El `ProductSelector` es utilizado tanto por compras como por ventas.

---

# Patrones de Diseño

La solución utiliza diferentes patrones para reducir acoplamiento y organizar las responsabilidades.

## Unit of Work

El patrón **Unit of Work** se utiliza para coordinar las operaciones de persistencia realizadas durante una operación de negocio.

Por ejemplo, registrar una compra requiere modificar varias entidades:

```
Purchase
PurchaseDetails
Product
Movement
MovementDetails
```

Conceptualmente:

```
Begin Transaction
      │
      ├── Create Purchase
      ├── Create Purchase Details
      ├── Update Products
      ├── Create Movement
      └── Create Movement Details
      │
      ▼
Commit
```

Si alguna operación falla:

```
Rollback
```

### Beneficios

- Garantiza consistencia transaccional.
- Centraliza `SaveChanges`.
- Evita que diferentes componentes administren transacciones de manera independiente.
- Facilita la coordinación de múltiples repositories.

---

# Facade

El patrón **Facade** se utiliza en los casos de uso complejos, principalmente en compras y ventas.

Por ejemplo:

```
CreatePurchaseFacade
```

encapsula la complejidad necesaria para registrar una compra.

El controller solamente necesita ejecutar:

```
CreatePurchaseFacade.ExecuteAsync()
```

Internamente la Facade coordina:

```
Validación     ↓Productos     ↓Purchase     ↓Movement     ↓UnitOfWork     ↓Transaction
```

Esto evita colocar lógica de negocio dentro del controller.

### Beneficios

- Controllers más simples.
- Encapsulación de procesos complejos.
- Reduce el acoplamiento.
- Facilita la reutilización.
- Centraliza la coordinación de un caso de uso.

---

# CQRS

Se utiliza una aproximación **CQRS (Command Query Responsibility Segregation)** para separar las operaciones que modifican el estado de aquellas que únicamente consultan información.

### Commands

Representan operaciones que modifican información:

```
CreateProduct
UpdateProduct
CreatePurchase
CreateSale
```

### Queries

Representan operaciones de lectura:

```
ListProducts
ListPurchases
ListSales
GetKardex
GetProductMovementsConceptualmente:
```

```
             Application
                  │
          ┌───────┴────────┐
          │                │
       Commands          Queries
          │                │
          ▼                ▼
      Mutations         Read Models       
```

### Beneficios

- Separación clara entre lectura y escritura.
- Casos de uso pequeños y especializados.
- Facilita optimizar consultas independientemente.
- Mejora la organización del código.
- Facilita una futura evolución hacia arquitecturas CQRS más completas si el sistema lo requiere.

> En esta solución se utiliza CQRS a nivel de organización de casos de uso; no se implementa infraestructura separada de lectura/escritura ni Event Sourcing, ya que no es necesario para el alcance de la prueba técnica.

---

# Repository Pattern

Los repositories encapsulan el acceso a los datos.

Por ejemplo:

```
IProductRepository
```

La capa Core depende de la abstracción:

```
IProductRepository
```

y no directamente de:

```
DbContext
```

La implementación concreta pertenece a Infrastructure.

Esto permite:

- Reducir acoplamiento con Entity Framework.
- Facilitar pruebas.
- Centralizar consultas específicas.
- Mantener la persistencia fuera del dominio.

---

# Seguridad

La solución implementa múltiples mecanismos de seguridad.

## JWT

La autenticación utiliza JSON Web Tokens.

Flujo:

```
Usuario
   │
   ▼
Login
   │
   ▼
Auth Service
   │
   ▼
JWT
   │
   ▼
Frontend
   │
   │ Authorization: Bearer <token>
   ▼
API Gateway
   │
   ▼
Commerce API
```

---

## CORS

CORS se configura en el API Gateway para permitir únicamente los orígenes autorizados del frontend.

Los orígenes permitidos se configuran mediante `appsettings.json`.

Esto evita que aplicaciones web de orígenes no autorizados puedan consumir directamente el Gateway.

---

## Rate Limiting

El Gateway incorpora limitación de solicitudes para reducir el riesgo de:

- Fuerza bruta.
- Abuso de endpoints.
- Denegación de servicio a nivel de aplicación.

---

## Internal Gateway Key

Las solicitudes enviadas desde el Gateway hacia los servicios internos incluyen una clave interna.

Esto permite que los microservicios puedan verificar que una solicitud proviene del Gateway autorizado.

---

# Tecnologías

## Backend

- **.NET 8**
- ASP.NET Core
- Entity Framework Core
- SQL Server
- JWT
- YARP
- Swagger / OpenAPI
- Serilog
- REST API

## Frontend

- **React**
- TypeScript
- Vite
- React Router
- TanStack Query
- Axios
- Tailwind CSS
- shadcn/ui

## Arquitectura

- Clean Architecture
- Microservices
- API Gateway
- Repository Pattern
- Unit of Work
- Facade
- CQRS

---

# Estructura del Proyecto

Una representación simplificada del repositorio:

```text
compra-venta-prueba-tecnica/
│
├── backend/
│   │
│   ├── CompraVenta.Api/
│   ├── CompraVenta.Application/
│   ├── CompraVenta.Domain/
│   ├── CompraVenta.Persistence/
│   ├── CompraVenta.Security/
│   └── ...
│
├── frontend/
│   │
│   ├── src/
│   │   ├── core/
│   │   │   ├── http/
│   │   │   └── storage/
│   │   │
│   │   ├── features/
│   │   │   ├── auth/
│   │   │   ├── products/
│   │   │   ├── purchases/
│   │   │   └── sales/
│   │   │
│   │   ├── navigation/
│   │   └── shared/
│   │
│   ├── package.json
│   └── ...
│
└── README.md
```

---

# Requisitos

Para ejecutar el proyecto localmente se requiere instalar:

- [.NET 8 SDK](https://dotnet.microsoft.com/)
- [Node.js](https://nodejs.org/)
- [pnpm](https://pnpm.io/)
- SQL Server
- Git
- Visual Studio / Visual Code

---

# Configuración

## 1. Clonar el repositorio

```bash
git clone <URL_DEL_REPOSITORIO>
```

Ingresar al proyecto:

```bash
cd compra-venta-prueba-tecnica
```

---

# Configuración de SQL Server

La aplicación utiliza **SQL Server + Entity Framework Core Code First**.

Es necesario disponer de una instancia de SQL Server accesible desde las APIs.

La solución utiliza una única base de datos para los servicios que requieren persistencia.

> Para efectos de la prueba técnica se utiliza una base de datos compartida. En una arquitectura de microservicios completamente distribuida, cada servicio debería ser propietario de sus propios datos cuando los requisitos de escalabilidad, autonomía y aislamiento lo justifiquen.

---

# Cadena de conexión

Debido a que el proyecto contiene **dos APIs que utilizan la base de datos**, la cadena de conexión debe configurarse en el `appsettings.json` de **cada API**:

```
CompraVenta.Auth.API
CompraVenta.Commerce.API
```

Ejemplo:

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=compra_venta_db;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Si se utiliza autenticación mediante usuario y contraseña:

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=compra_venta_db;User Id=sa;Password=<PASSWORD>;TrustServerCertificate=True;"
  }
}
```

### ¿Por qué configurarla en ambas APIs?

Aunque ambos servicios utilizan la misma base de datos para esta prueba técnica, cada API posee su propio proceso de ejecución y su propio `DbContext`.

Por esta razón, cada aplicación necesita conocer la cadena de conexión que utilizará para inicializar Entity Framework Core.

---

# Migraciones

Las migraciones se implementan utilizando **Entity Framework Core Code First**.

No es necesario ejecutar manualmente:

```
dotnet ef database update
```

al iniciar el proyecto.

Cada API ejecuta automáticamente las migraciones pendientes durante el proceso de startup.

El flujo es:

```
Aplicación inicia
      │
      ▼
Verificar migraciones
      │
      ▼
Aplicar migraciones pendientes
      │
      ▼
Ejecutar Data Seeding
      │
      ▼
API disponible
```

Esto facilita la ejecución de la solución durante la evaluación técnica y evita pasos manuales adicionales de configuración de la base de datos.

---

# Ejecución del Backend

Se deben ejecutar los siguientes componentes:

```
1. Auth API
2. Commerce API
3. API Gateway
```

Cada proyecto puede ejecutarse mediante:

```
dotnet run
```

El API Gateway será el punto de entrada utilizado por el frontend.

Por ejemplo:

```
Frontend
   │
   ▼
API Gateway
   │
   ├── /auth/*
   │       ↓
   │    Auth API
   │
   └── /commerce/*
           ↓
       Commerce API
```

---

# Configuración del Frontend

Ingresar al directorio:

```
cd frontend
```

Instalar dependencias:

```
pnpm install
```

Configurar la URL del API Gateway mediante las variables de entorno.

Ejemplo:

```
VITE_API_URL=https://localhost:7000
```

Ejecutar:

```
pnpm dev
```

---

# 👨‍💻 Autor

**Juan De La Portilla Cárdenas**

Software Engineer | Full Stack .NET Developer

Lima, Perú
