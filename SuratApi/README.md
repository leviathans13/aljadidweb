SuratApi (.NET 8) - Sistem Surat Menyurat (CRUD) dengan 2 Role

API ini menyediakan autentikasi JWT dengan 2 role: Admin dan User. Admin dapat mengelola semua surat, sedangkan User hanya dapat mengelola surat miliknya.

Fitur
- Register/Login (JWT)
- Role: Admin, User
- CRUD `Surat` (Nomor, Judul, Isi, TanggalSurat)
- Pembatasan akses berdasarkan role/ownership
- SQLite (file `surat.db`)

Prasyarat
- .NET 8 SDK (sudah di-install oleh skrip)

Konfigurasi
File `appsettings.json`:
```
{
  "ConnectionStrings": {
    "Default": "Data Source=surat.db"
  },
  "Jwt": {
    "Issuer": "SuratApi",
    "Audience": "SuratApiClient",
    "Key": "CHANGE_THIS_DEVELOPMENT_SECRET_KEY_32CHARS_MINIMUM",
    "ExpiryMinutes": 120
  }
}
```

Ganti `Jwt:Key` untuk produksi dengan secret yang kuat.

Menjalankan
```
cd SuratApi
dotnet build
dotnet run --urls http://0.0.0.0:5005
```
Server berjalan di `http://localhost:5005`.

Saat pertama run, user admin default dibuat:
- username: `admin`
- password: `admin123`

Endpoint Utama
- POST `api/auth/register` { username, password }
- POST `api/auth/login` { username, password } -> { token }
- GET/POST/PUT/DELETE `api/surat`

Gunakan header:
```
Authorization: Bearer <token>
Content-Type: application/json
```

Contoh Alur
1) Login admin
```
curl -s http://localhost:5005/api/auth/login \
  -H 'Content-Type: application/json' \
  -d '{"username":"admin","password":"admin123"}'
```

2) Buat surat sebagai admin
```
TOKEN=... # ambil dari hasil login
curl -s http://localhost:5005/api/surat \
  -H "Authorization: Bearer $TOKEN" -H 'Content-Type: application/json' \
  -d '{"nomor":"001/IX","judul":"Undangan","isi":"Isi surat","tanggalSurat":"2025-09-09T00:00:00Z","ownerId":""}'
```

3) Akses surat sebagai user biasa hanya miliknya sendiri.

Catatan
- Untuk dev cepat, database dibuat otomatis (EnsureCreated). Untuk produksi, disarankan gunakan migrasi EF Core.
- Pastikan mengganti secret dan mengaktifkan HTTPS di lingkungan produksi.

