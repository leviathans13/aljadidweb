# Sistem Surat Menyurat API

Backend API untuk sistem surat menyurat dengan 2 jenis role (Admin dan User) menggunakan .NET 8 Web API.

## Fitur

- **Authentication & Authorization** dengan JWT Token
- **2 Role System**: Admin dan User
- **CRUD Operations** untuk surat menyurat
- **User Management** (Admin only)
- **Database** dengan Entity Framework Core
- **AutoMapper** untuk mapping DTOs
- **Swagger Documentation**

## Role dan Permission

### Admin
- Melihat semua surat
- Mengelola user (CRUD)
- Mengedit/menghapus semua surat
- Mendaftarkan user baru

### User
- Melihat surat yang dibuat atau ditugaskan kepadanya
- Membuat surat baru
- Mengedit/menghapus surat yang dibuatnya sendiri

## Database Schema

### User
- Id (Primary Key)
- Username (Unique)
- Email (Unique)
- PasswordHash
- Role (Admin/User)
- FullName
- CreatedAt, UpdatedAt

### Surat
- Id (Primary Key)
- NomorSurat (Unique)
- Perihal
- IsiSurat
- Pengirim
- Penerima
- TanggalSurat
- Status (Draft/Terkirim/Diterima/Ditolak)
- JenisSurat (Biasa/Penting/Rahasia)
- Lampiran
- CreatedById (Foreign Key ke User)
- AssignedToId (Foreign Key ke User)
- CreatedAt, UpdatedAt

## Setup dan Instalasi

### Prerequisites
- .NET 8 SDK
- SQL Server atau SQL Server LocalDB
- Visual Studio atau VS Code

### Langkah-langkah

1. **Clone atau download proyek**
   ```bash
   cd /path/to/project
   ```

2. **Install dependencies**
   ```bash
   dotnet restore
   ```

3. **Setup database**
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

4. **Jalankan aplikasi**
   ```bash
   dotnet run
   ```

5. **Akses Swagger UI**
   - Buka browser ke: `https://localhost:7000/swagger` (atau port yang ditampilkan di console)

## Default Users

Sistem sudah memiliki 2 user default:

### Admin
- Username: `admin`
- Password: `admin123`
- Role: Admin

### User
- Username: `user1`
- Password: `user123`
- Role: User

## API Endpoints

### Authentication
- `POST /api/auth/login` - Login user
- `POST /api/auth/register` - Register user baru (Admin only)
- `GET /api/auth/profile` - Get profile user yang login

### Surat Management
- `GET /api/surat` - Get semua surat (Admin only)
- `GET /api/surat/my-surats` - Get surat user yang login
- `GET /api/surat/{id}` - Get surat by ID
- `POST /api/surat` - Create surat baru
- `PUT /api/surat/{id}` - Update surat
- `DELETE /api/surat/{id}` - Delete surat

### User Management (Admin only)
- `GET /api/user` - Get semua user
- `GET /api/user/{id}` - Get user by ID
- `PUT /api/user/{id}` - Update user
- `DELETE /api/user/{id}` - Delete user

## Authentication

Semua endpoint (kecuali login) memerlukan JWT token. Token didapat dari endpoint login dan harus disertakan di header:

```
Authorization: Bearer <your-jwt-token>
```

## Testing dengan Swagger

1. Buka Swagger UI
2. Klik "Authorize" button
3. Masukkan token JWT yang didapat dari login
4. Format: `Bearer <token>`
5. Klik "Authorize"
6. Sekarang bisa test semua endpoint

## Configuration

Edit `appsettings.json` untuk mengubah:
- Connection string database
- JWT settings (secret key, expiry time, dll)

## Development

### Menambah Migration
```bash
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

### Menjalankan Tests
```bash
dotnet test
```

## Security Notes

- JWT Secret Key harus diganti di production
- Password di-hash menggunakan BCrypt
- Authorization checks di setiap endpoint
- CORS sudah dikonfigurasi untuk development

## Troubleshooting

### Database Connection Issues
- Pastikan SQL Server/LocalDB sudah running
- Check connection string di appsettings.json
- Pastikan database sudah dibuat dengan migration

### JWT Token Issues
- Check JWT settings di appsettings.json
- Pastikan token belum expired
- Format header: `Authorization: Bearer <token>`

### Permission Denied
- Pastikan user memiliki role yang tepat
- Check endpoint authorization requirements
- Admin bisa akses semua, User hanya surat miliknya