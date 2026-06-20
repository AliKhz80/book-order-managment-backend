# Book Store & Ordering Ecosystem

A modern, test-driven microservices project built to demonstrate advanced backend engineering capabilities, clean architectural patterns, and event-driven communication. 

This project simulates a **Book Store CRUD** service and a companion **Ordering Service** operating in a decoupled, fully containerized environment.

---

##  Purpose & Scope
The primary goal of this repository is to showcase technical proficiency in microservices architecture, asynchronous communication, and enterprise design patterns. 
* **Focus:** High-quality code organization, event-driven orchestration, and infrastructure automation.
* **Out of Scope:** To keep the focus strictly on core architecture, user authentication (JWT) and user membership tables are intentionally omitted.

---

##  Tech Stack & Architecture

* **Framework:** .NET 8
* **Architecture:** Clean Architecture (Separation of Concerns, Domain-Driven Design principles)
* **Databases:** SQL Server (Dedicated instances per service to ensure database-per-service pattern)
* **Message Broker:** RabbitMQ (Asynchronous event publishing and subscribing)
* **Containerization:** Docker & Docker Compose

---

##  System Architecture

The ecosystem consists of two core microservices that communicate asynchronously:

1. **Book Store Service (CRUD):** Manages the book catalog, inventory, and details.
2. **Ordering Service:** Handles book placement, order state, and processing.
