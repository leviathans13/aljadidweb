# API Documentation - Sistem Surat Menyurat

## Base URL
```
https://localhost:7000/api
```

## Authentication
Semua endpoint memerlukan JWT token (kecuali login). Token harus disertakan di header:
```
Authorization: Bearer <jwt-token>
```

---

## Authentication Endpoints

### POST /api/auth/login
Login user dan mendapatkan JWT token.

**Request Body:**
```json
{
  "username": "admin",
  "password": "admin123"
}
```

**Response (200):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

**Response (401):**
```json
{
  "message": "Username atau password salah"
}
```

### POST /api/auth/register
Mendaftarkan user baru. **Admin only.**

**Request Body:**
```json
{
  "username": "newuser",
  "email": "newuser@example.com",
  "password": "password123",
  "fullName": "New User",
  "role": "User"
}
```

**Response (201):**
```json
{
  "id": 3,
  "username": "newuser",
  "email": "newuser@example.com",
  "role": "User",
  "fullName": "New User",
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": null
}
```

### GET /api/auth/profile
Mendapatkan profile user yang sedang login.

**Response (200):**
```json
{
  "id": 1,
  "username": "admin",
  "email": "admin@surat.com",
  "role": "Admin",
  "fullName": "Administrator",
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": null
}
```

---

## Surat Endpoints

### GET /api/surat
Mendapatkan semua surat. **Admin only.**

**Response (200):**
```json
[
  {
    "id": 1,
    "nomorSurat": "001/SM/2024",
    "perihal": "Undangan Rapat",
    "isiSurat": "Dengan hormat, kami mengundang...",
    "pengirim": "HRD",
    "penerima": "Semua Karyawan",
    "tanggalSurat": "2024-01-01T00:00:00Z",
    "status": "Terkirim",
    "jenisSurat": "Biasa",
    "lampiran": null,
    "createdAt": "2024-01-01T00:00:00Z",
    "updatedAt": null,
    "createdById": 1,
    "createdByUsername": "admin",
    "assignedToId": 2,
    "assignedToUsername": "user1"
  }
]
```

### GET /api/surat/my-surats
Mendapatkan surat yang dibuat atau ditugaskan ke user yang login.

**Response (200):**
```json
[
  {
    "id": 1,
    "nomorSurat": "001/SM/2024",
    "perihal": "Undangan Rapat",
    "isiSurat": "Dengan hormat, kami mengundang...",
    "pengirim": "HRD",
    "penerima": "Semua Karyawan",
    "tanggalSurat": "2024-01-01T00:00:00Z",
    "status": "Terkirim",
    "jenisSurat": "Biasa",
    "lampiran": null,
    "createdAt": "2024-01-01T00:00:00Z",
    "updatedAt": null,
    "createdById": 1,
    "createdByUsername": "admin",
    "assignedToId": 2,
    "assignedToUsername": "user1"
  }
]
```

### GET /api/surat/{id}
Mendapatkan surat berdasarkan ID.

**Response (200):**
```json
{
  "id": 1,
  "nomorSurat": "001/SM/2024",
  "perihal": "Undangan Rapat",
  "isiSurat": "Dengan hormat, kami mengundang...",
  "pengirim": "HRD",
  "penerima": "Semua Karyawan",
  "tanggalSurat": "2024-01-01T00:00:00Z",
  "status": "Terkirim",
  "jenisSurat": "Biasa",
  "lampiran": null,
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": null,
  "createdById": 1,
  "createdByUsername": "admin",
  "assignedToId": 2,
  "assignedToUsername": "user1"
}
```

### POST /api/surat
Membuat surat baru.

**Request Body:**
```json
{
  "nomorSurat": "002/SM/2024",
  "perihal": "Surat Tugas",
  "isiSurat": "Dengan ini diberitahukan bahwa...",
  "pengirim": "Manager",
  "penerima": "Staff",
  "tanggalSurat": "2024-01-02T00:00:00Z",
  "status": "Draft",
  "jenisSurat": "Biasa",
  "lampiran": null,
  "assignedToId": 2
}
```

**Response (201):**
```json
{
  "id": 2,
  "nomorSurat": "002/SM/2024",
  "perihal": "Surat Tugas",
  "isiSurat": "Dengan ini diberitahukan bahwa...",
  "pengirim": "Manager",
  "penerima": "Staff",
  "tanggalSurat": "2024-01-02T00:00:00Z",
  "status": "Draft",
  "jenisSurat": "Biasa",
  "lampiran": null,
  "createdAt": "2024-01-02T00:00:00Z",
  "updatedAt": null,
  "createdById": 1,
  "createdByUsername": "admin",
  "assignedToId": 2,
  "assignedToUsername": "user1"
}
```

### PUT /api/surat/{id}
Mengupdate surat. User hanya bisa mengupdate surat yang dibuatnya sendiri, Admin bisa mengupdate semua surat.

**Request Body:**
```json
{
  "status": "Terkirim",
  "jenisSurat": "Penting"
}
```

**Response (200):**
```json
{
  "id": 2,
  "nomorSurat": "002/SM/2024",
  "perihal": "Surat Tugas",
  "isiSurat": "Dengan ini diberitahukan bahwa...",
  "pengirim": "Manager",
  "penerima": "Staff",
  "tanggalSurat": "2024-01-02T00:00:00Z",
  "status": "Terkirim",
  "jenisSurat": "Penting",
  "lampiran": null,
  "createdAt": "2024-01-02T00:00:00Z",
  "updatedAt": "2024-01-02T01:00:00Z",
  "createdById": 1,
  "createdByUsername": "admin",
  "assignedToId": 2,
  "assignedToUsername": "user1"
}
```

### DELETE /api/surat/{id}
Menghapus surat. User hanya bisa menghapus surat yang dibuatnya sendiri, Admin bisa menghapus semua surat.

**Response (204):** No Content

---

## User Management Endpoints (Admin Only)

### GET /api/user
Mendapatkan semua user.

**Response (200):**
```json
[
  {
    "id": 1,
    "username": "admin",
    "email": "admin@surat.com",
    "role": "Admin",
    "fullName": "Administrator",
    "createdAt": "2024-01-01T00:00:00Z",
    "updatedAt": null
  },
  {
    "id": 2,
    "username": "user1",
    "email": "user1@surat.com",
    "role": "User",
    "fullName": "User Pertama",
    "createdAt": "2024-01-01T00:00:00Z",
    "updatedAt": null
  }
]
```

### GET /api/user/{id}
Mendapatkan user berdasarkan ID.

**Response (200):**
```json
{
  "id": 2,
  "username": "user1",
  "email": "user1@surat.com",
  "role": "User",
  "fullName": "User Pertama",
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": null
}
```

### PUT /api/user/{id}
Mengupdate user.

**Request Body:**
```json
{
  "fullName": "User Pertama Updated",
  "role": "Admin"
}
```

**Response (200):**
```json
{
  "id": 2,
  "username": "user1",
  "email": "user1@surat.com",
  "role": "Admin",
  "fullName": "User Pertama Updated",
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": "2024-01-02T01:00:00Z"
}
```

### DELETE /api/user/{id}
Menghapus user. Admin tidak bisa menghapus dirinya sendiri.

**Response (204):** No Content

---

## Error Responses

### 400 Bad Request
```json
{
  "message": "Username atau email sudah digunakan"
}
```

### 401 Unauthorized
```json
{
  "message": "Username atau password salah"
}
```

### 403 Forbidden
```json
{
  "message": "Access denied"
}
```

### 404 Not Found
```json
{
  "message": "Resource not found"
}
```

### 500 Internal Server Error
```json
{
  "message": "An error occurred while processing your request"
}
```

---

## Status Codes

- **Draft**: Surat masih dalam tahap penyusunan
- **Terkirim**: Surat sudah dikirim
- **Diterima**: Surat sudah diterima oleh penerima
- **Ditolak**: Surat ditolak oleh penerima

## Jenis Surat

- **Biasa**: Surat biasa
- **Penting**: Surat penting
- **Rahasia**: Surat rahasia