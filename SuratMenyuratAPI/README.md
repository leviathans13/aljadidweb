# Surat Menyurat API

API backend untuk sistem surat menyurat dengan role-based access control menggunakan .NET 8.

## Fitur

### Autentikasi & Autorisasi
- JWT Token-based authentication
- Role-based access control (Admin & User)
- User registration dan login
- Password encryption dengan ASP.NET Identity

### Manajemen Surat
- CRUD operations untuk surat
- Status tracking (Draft, Sent, Received, Read, Archived)
- Priority levels (Low, Normal, High, Urgent)
- Tipe surat (Internal, External, Memo, Official)
- Automatic letter numbering system
- Search dan filtering
- Pagination

### Role System
- **Admin**: Full access ke semua fitur, user management, sistem statistics
- **User**: Akses terbatas untuk surat yang dikirim/diterima

## Tech Stack

- **.NET 8** - Web API Framework
- **Entity Framework Core** - ORM
- **SQL Server** - Database
- **ASP.NET Identity** - Authentication & Authorization
- **JWT Bearer** - Token Authentication
- **AutoMapper** - Object mapping
- **Swagger** - API Documentation

## Database Schema

### Tables
- **Users** - User information dengan ASP.NET Identity
- **Letters** - Surat dengan metadata
- **LetterAttachments** - File attachments
- **AspNetRoles** - Roles (Admin, User)
- **AspNetUserRoles** - User-Role relationships

## API Endpoints

### Authentication
```
POST /api/auth/register     - Register user baru
POST /api/auth/login        - Login user
GET  /api/auth/me          - Get current user info
POST /api/auth/logout      - Logout user
```

### Letters (Authenticated)
```
GET    /api/letters           - Get user's letters (paginated)
GET    /api/letters/{id}      - Get specific letter
POST   /api/letters           - Create new letter
PUT    /api/letters/{id}      - Update letter (draft only)
DELETE /api/letters/{id}      - Delete letter (draft only)
POST   /api/letters/{id}/send - Send letter
POST   /api/letters/{id}/read - Mark letter as read
POST   /api/letters/{id}/archive - Archive letter
GET    /api/letters/users     - Get all users for recipient selection
```

### Admin (Admin Role Only)
```
GET    /api/admin/users           - Get all users (paginated)
GET    /api/admin/users/{id}      - Get specific user
PUT    /api/admin/users/{id}/role - Update user role
DELETE /api/admin/users/{id}      - Delete user
GET    /api/admin/letters         - Get all letters (admin view)
GET    /api/admin/statistics      - Get system statistics
```

## Setup & Installation

### Prerequisites
- .NET 8 SDK
- SQL Server atau SQL Server Express
- Visual Studio atau VS Code

### Installation Steps

1. **Clone repository**
   ```bash
   git clone <repository-url>
   cd SuratMenyuratAPI
   ```

2. **Install dependencies**
   ```bash
   dotnet restore
   ```

3. **Update connection string**
   Edit `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=your-server;Database=SuratMenyuratDB;Trusted_Connection=true;"
     }
   }
   ```

4. **Run database migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run aplikasi**
   ```bash
   dotnet run
   ```

6. **Access Swagger UI**
   Buka browser ke `https://localhost:7xxx` atau `http://localhost:5xxx`

## Default Users

Setelah migration, sistem akan membuat default users:

### Admin User
- **Email**: admin@suratmenyurat.com
- **Password**: Admin123!
- **Role**: Admin

### Regular User
- **Email**: user@suratmenyurat.com
- **Password**: User123!
- **Role**: User

## Configuration

### JWT Settings (appsettings.json)
```json
{
  "JWT": {
    "Secret": "YourSuperSecretKeyThatShouldBeAtLeast32CharactersLong!",
    "Issuer": "SuratMenyuratAPI",
    "Audience": "SuratMenyuratClient",
    "ExpirationInHours": "24"
  }
}
```

## API Usage Examples

### 1. Register User
```http
POST /api/auth/register
Content-Type: application/json

{
  "email": "newuser@example.com",
  "password": "Password123!",
  "confirmPassword": "Password123!",
  "fullName": "John Doe",
  "department": "IT Department",
  "position": "Developer",
  "phoneNumber": "08123456789",
  "role": "User"
}
```

### 2. Login
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "user@suratmenyurat.com",
  "password": "User123!"
}
```

### 3. Create Letter
```http
POST /api/letters
Authorization: Bearer <token>
Content-Type: application/json

{
  "subject": "Meeting Request",
  "content": "I would like to schedule a meeting...",
  "type": 1,
  "recipientId": "recipient-user-id",
  "priority": 2
}
```

### 4. Get Letters with Filtering
```http
GET /api/letters?page=1&pageSize=10&search=meeting&status=2&type=1
Authorization: Bearer <token>
```

## Letter Status Flow

1. **Draft** - Surat baru dibuat, bisa diedit/dihapus
2. **Sent** - Surat dikirim, tidak bisa diedit
3. **Received** - Surat diterima oleh recipient
4. **Read** - Surat sudah dibaca
5. **Archived** - Surat diarsipkan

## Letter Types

1. **Internal** (1) - Surat internal organisasi
2. **External** (2) - Surat eksternal
3. **Memo** (3) - Memorandum
4. **Official** (4) - Surat resmi

## Priority Levels

1. **Low** (1) - Prioritas rendah
2. **Normal** (2) - Prioritas normal
3. **High** (3) - Prioritas tinggi
4. **Urgent** (4) - Urgent

## Error Handling

API menggunakan standard HTTP status codes:
- `200 OK` - Request berhasil
- `201 Created` - Resource berhasil dibuat
- `400 Bad Request` - Invalid request data
- `401 Unauthorized` - Authentication required
- `403 Forbidden` - Insufficient permissions
- `404 Not Found` - Resource tidak ditemukan
- `500 Internal Server Error` - Server error

## Security Features

- Password hashing dengan ASP.NET Identity
- JWT token dengan expiration
- Role-based authorization
- CORS configuration
- Input validation
- SQL injection protection (Entity Framework)

## Development

### Database Migrations
```bash
# Add new migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Remove last migration
dotnet ef migrations remove
```

### Testing
```bash
# Run tests
dotnet test

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"
```

## Deployment

### Production Checklist
1. Update connection string untuk production database
2. Change JWT secret key
3. Configure CORS untuk specific domains
4. Enable HTTPS only
5. Configure logging
6. Set up database backup
7. Configure environment variables

### Docker Support (Optional)
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SuratMenyuratAPI.csproj", "."]
RUN dotnet restore "SuratMenyuratAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "SuratMenyuratAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SuratMenyuratAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SuratMenyuratAPI.dll"]
```

## Support

Untuk pertanyaan dan support, silakan buat issue di repository atau hubungi tim development.

## License

This project is licensed under the MIT License.