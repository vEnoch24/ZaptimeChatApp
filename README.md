# ZaptimeChatApp

Instant messaging web app — a full-stack Blazor WebAssembly + ASP.NET Core prototype with SignalR-style real-time messaging interfaces and a component-driven client UI.

---

<!-- BUTTON-STYLE TABLE OF CONTENTS -->
<div align="center" style="margin: 16px 0">
  <a href="#what-this-project-is" style="text-decoration:none">
    <span style="display:inline-block;background:#0d6efd;color:#fff;padding:10px 16px;border-radius:8px;margin:4px;font-weight:600">What this is</span>
  </a>
  <a href="#quick-links" style="text-decoration:none">
    <span style="display:inline-block;background:#6c757d;color:#fff;padding:10px 16px;border-radius:8px;margin:4px;font-weight:600">Quick Links</span>
  </a>
  <a href="#architecture-and-project-structure" style="text-decoration:none">
    <span style="display:inline-block;background:#198754;color:#fff;padding:10px 16px;border-radius:8px;margin:4px;font-weight:600">Architecture</span>
  </a>
  <a href="#prerequisites" style="text-decoration:none">
    <span style="display:inline-block;background:#0dcaf0;color:#fff;padding:10px 16px;border-radius:8px;margin:4px;font-weight:600">Prereqs</span>
  </a>
  <a href="#run-the-project-locally" style="text-decoration:none">
    <span style="display:inline-block;background:#ffc107;color:#000;padding:10px 16px;border-radius:8px;margin:4px;font-weight:600">Run</span>
  </a>
  <a href="#configuration" style="text-decoration:none">
    <span style="display:inline-block;background:#6610f2;color:#fff;padding:10px 16px;border-radius:8px;margin:4px;font-weight:600">Config</span>
  </a>
  <a href="#development-workflow" style="text-decoration:none">
    <span style="display:inline-block;background:#dc3545;color:#fff;padding:10px 16px;border-radius:8px;margin:4px;font-weight:600">Dev</span>
  </a>
  <a href="#deployment" style="text-decoration:none">
    <span style="display:inline-block;background:#0b5cff;color:#fff;padding:10px 16px;border-radius:8px;margin:4px;font-weight:600">Deploy</span>
  </a>
  <a href="#security-and-secrets" style="text-decoration:none">
    <span style="display:inline-block;background:#fd7e14;color:#fff;padding:10px 16px;border-radius:8px;margin:4px;font-weight:600">Security</span>
  </a>
  <a href="#contributing" style="text-decoration:none">
    <span style="display:inline-block;background:#20c997;color:#fff;padding:10px 16px;border-radius:8px;margin:4px;font-weight:600">Contribute</span>
  </a>
  <a href="#faq-and-troubleshooting" style="text-decoration:none">
    <span style="display:inline-block;background:#6f42c1;color:#fff;padding:10px 16px;border-radius:8px;margin:4px;font-weight:600">FAQ</span>
  </a>
  <a href="#license-and-credits" style="text-decoration:none">
    <span style="display:inline-block;background:#343a40;color:#fff;padding:10px 16px;border-radius:8px;margin:4px;font-weight:600">License</span>
  </a>
</div>

---

<a id="what-this-project-is"></a>
## What this project is

ZaptimeChatApp is an instant-messaging web application implemented with Blazor WebAssembly (client) and ASP.NET Core (server). It provides a component-driven client UI (razor components for chats, video, user lists, etc.), shared contracts for real-time communication (SignalR hub interfaces in the Shared project), and server-side token/service scaffolding for authentication and server logic.

This repository contains:
- A Blazor WebAssembly client (ZaptimeChatApp/Client) with pages and components: Chat, ChatsList, ChatDetails, UserList, Video, etc.
- An ASP.NET Core server (ZaptimeChatApp/Server) that hosts APIs, SignalR hubs, and token/auth utilities.
- Shared DTOs and hub interfaces (ZaptimeChatApp/Shared) used by both client and server for type-safe real-time messaging.

---

<a id="quick-links"></a>
## Quick links (files & entry points)

- Solution: `ZaptimeChatApp.sln`
- Client entry: `ZaptimeChatApp/Client/Program.cs` (Blazor WASM startup)
- Key client pages/components:
  - `ZaptimeChatApp/Client/Pages/Chat.razor`
  - `ZaptimeChatApp/Client/Pages/Index.razor`
  - `ZaptimeChatApp/Client/Components/ChatDetails.razor`
  - `ZaptimeChatApp/Client/Components/ChatsList.razor`
  - `ZaptimeChatApp/Client/Components/UserList.razor`
  - `ZaptimeChatApp/Client/Components/Video.razor`
- Server entry: `ZaptimeChatApp/Server/Program.cs` (configures services, hubs, middleware)
- Server helper: `ZaptimeChatApp/Server/TokenService.cs` (token creation/verification)
- Shared contracts: `ZaptimeChatApp/Shared/IZaptimeChatHubClient.cs`, `ZaptimeChatApp/Shared/IZaptimeChatHubServer.cs`, DTOs
- Package management: `package.json` (includes Bootstrap and Popper for client styling)
- Solution-level Visual Studio files and standard build folders are present.

---

<a id="architecture-and-project-structure"></a>
## Architecture and project structure

High-level architecture:
- Blazor WebAssembly (Client) talks to the ASP.NET Core Server via HTTP and a SignalR hub for real-time messaging. Shared C# projects provide DTOs and hub interfaces to keep client/server contracts in sync.
- Server exposes REST endpoints (authentication, user management) and a SignalR hub for exchanging messages and presence events; the client subscribes and drives UI updates via hub callbacks.
- Styling uses Bootstrap (package.json lists bootstrap dependency), and the client also uses third-party component libraries found in Program.cs (Blazored.Toast, Radzen).

Top-level structure (annotated):
