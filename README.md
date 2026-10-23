# BeeMart
mini project: bootcamp it division binus

# BeeMart - Point of Sale (POS) Web Application

BeeMart adalah aplikasi sistem kasir (POS) berbasis .NET 8 yang dirancang dengan menerapkan arsitektur *Separation of Concerns* secara ketat, memisahkan secara independen antara Backend API dan Frontend WebClient.

## 🚀 Tech Stack
- **Backend:** C#, ASP.NET Core RESTful API, Entity Framework Core, SQL Server
- **Frontend:** ASP.NET Core Razor Pages, JavaScript (Fetch API), HTML5/CSS3 (Pure Custom CSS)
- **Database:** Microsoft SQL Server

## ✨ Fitur Utama
- **Autentikasi & Otorisasi:** Sistem Login & Register aman dengan *password hashing* dan *Role-based Authorization* (Admin & Kasir).
- **Transaksi Penjualan (New Transaction):** Pencarian produk real-time (maksimal 10 data), manajemen keranjang belanja (*cart*), kalkulasi diskon/subtotal, dan *generate* nomor *invoice* otomatis sesuai format baku.
- **Riwayat Transaksi (History Transaction):** Filter pencarian berdasarkan tanggal dengan fitur *pagination* (maksimal 20 data per halaman) serta pembagian hak akses data antara Kasir dan Admin.

## 📂 Cara Menjalankan Proyek
1. **Database:** Jalankan skrip yang ada di `database/script.sql` pada SQL Server Management Studio (SSMS).
2. **Backend:** Buka folder `backend`, sesuaikan *connection string* pada `appsettings.json`, lalu jalankan menggunakan Visual Studio atau perintah `dotnet run`.
3. **Frontend:** Buka folder `frontend`, sesuaikan endpoint API pada konfigurasi, lalu jalankan aplikasi.
