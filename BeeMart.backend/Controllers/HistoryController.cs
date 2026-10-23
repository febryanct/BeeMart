using Microsoft.AspNetCore.Mvc;
using BeeMart.backend.Data;
using BeeMart.backend.DTO;

namespace BeeMart.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HistoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetHistory(
            [FromQuery] DateTime date,
            [FromQuery] string role,
            [FromQuery] string idUser,
            [FromQuery] int page = 1)
        {
            int pageSize = 20;

            // 1. Filter awal berdasarkan Tanggal
            var query = _context.TrTransactions
                .Where(t => t.TransactionDate.Date == date.Date);

            // 2. Filter Role (Kasir hanya miliknya, Admin lewatkan filter ini)
            if (role == "Cashier")
            {
                query = query.Where(t => t.IdUser == idUser);
            }

            // Hitung total halaman untuk pagination
            int totalRecords = query.Count();
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            // 3. Sorting Ascending dan Pagination
            var pagedTransactions = query
                .OrderBy(t => t.TransactionDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // 4. Membentuk hasil akhir dengan join manual ke tabel User, Payment, dan Detail Transaksi
            var historyData = pagedTransactions.Select(t => new HistoryResponseDto
            {
                NoTransaksi = t.InvoiceNo,
                TanggalJam = t.TransactionDate.ToString("yyyy-MM-dd HH:mm"),
                Kasir = _context.MsUsers.Where(u => u.IdUser == t.IdUser).Select(u => u.FullName).FirstOrDefault() ?? "Unknown",
                JmlItem = _context.TrTransactionDetails.Where(d => d.IdTransaction == t.IdTransaction).Sum(d => d.Qty),
                Pembayaran = _context.LtPaymentMethods.Where(p => p.IdPaymentMethod == t.IdPaymentMethod).Select(p => p.MethodName).FirstOrDefault() ?? "-",
                TotalHarga = t.TotalPrice
            }).ToList();

            var response = new PagedHistoryResponse
            {
                CurrentPage = page,
                TotalPages = totalPages == 0 ? 1 : totalPages,
                Data = historyData
            };

            return Ok(response);
        }
    }
}